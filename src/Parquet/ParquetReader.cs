﻿using Parquet.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Parquet.Thrift;

namespace Parquet
{
   /// <summary>
   /// Implements Apache Parquet format reader
   /// </summary>
   public class ParquetReader : IDisposable
   {
      private readonly Stream _input;
      private readonly BinaryReader _reader;
      private readonly ThriftStream _thrift;
      private readonly FileMetaData _meta;
      private readonly Schema _schema;

      public ParquetReader(Stream input)
      {
         _input = input;
         _reader = new BinaryReader(input);
         _thrift = new ThriftStream(input);

         _meta = ReadMetadata();
         _schema = new Schema(_meta);
      }

      /// <summary>
      /// Test read, to be defined
      /// </summary>
      public ParquetDataSet Read()
      {
         var result = new List<ParquetColumn>();

         foreach(RowGroup rg in _meta.Row_groups)
         {
            foreach(ColumnChunk cc in rg.Columns)
            {
               var p = new PColumn(cc, _schema, _input, _thrift);
               ParquetColumn column = p.Read();
               result.Add(column);
            }
         }

         return new ParquetDataSet(result);
      }

      private FileMetaData ReadMetadata()
      {
         //todo: validation that it's a parquet format indeed

         //go to -4 bytes (PAR1) -4 bytes (footer length number)
         _input.Seek(-8, SeekOrigin.End);
         int footerLength = _reader.ReadInt32();
         char[] magic = _reader.ReadChars(4);

         //go to footer data and deserialize it
         _input.Seek(-8 - footerLength, SeekOrigin.End);
         return _thrift.Read<FileMetaData>();
      }

      public void Dispose()
      {
      }
   }
}