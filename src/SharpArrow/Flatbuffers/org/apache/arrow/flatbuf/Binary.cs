// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace org.apache.arrow.flatbuf
{

using global::System;
using global::FlatBuffers;

public struct Binary : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static Binary GetRootAsBinary(ByteBuffer _bb) { return GetRootAsBinary(_bb, new Binary()); }
  public static Binary GetRootAsBinary(ByteBuffer _bb, Binary obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Binary __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }


  public static void StartBinary(FlatBufferBuilder builder) { builder.StartObject(0); }
  public static Offset<Binary> EndBinary(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<Binary>(o);
  }
};


}
