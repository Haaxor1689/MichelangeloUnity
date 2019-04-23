#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Resolvers
{
    using System;
    using MessagePack;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(26)
            {
                {typeof(global::System.Collections.Generic.Dictionary<string, double>), 0 },
                {typeof(global::System.Collections.Generic.Dictionary<string, double[]>), 1 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.GeometricModel[]), 2 },
                {typeof(string[][]), 3 },
                {typeof(global::System.Collections.Generic.Dictionary<int, global::Michelangelo.Model.MichelangeloApi.MaterialModel>), 4 },
                {typeof(global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MichelangeloApi.ParseTreeModel>), 5 },
                {typeof(global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo>), 6 },
                {typeof(global::System.Collections.Generic.Dictionary<string, long>), 7 },
                {typeof(global::System.Collections.Generic.List<global::Michelangelo.Model.MichelangeloApi.AxiomPostModel>), 8 },
                {typeof(global::System.Collections.Generic.Dictionary<string, string>), 9 },
                {typeof(global::System.Collections.Generic.IList<string>), 10 },
                {typeof(global::MessagePack.MessagePackType), 11 },
                {typeof(global::Michelangelo.Model.GrammarSource), 12 },
                {typeof(global::Michelangelo.Model.Handlers.SourceType), 13 },
                {typeof(global::SimpleJSON.JSONNodeType), 14 },
                {typeof(global::SimpleJSON.JSONTextMode), 15 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.AxiomPostModel), 16 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.TriangularMesh), 17 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.GeometricModel), 18 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.MaterialModel), 19 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.ParseTreeModel), 20 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo), 21 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.SkyMaterial), 22 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.SceneEnvironment), 23 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.PostResponseModel), 24 },
                {typeof(global::Michelangelo.Model.MichelangeloApi.SemanticModel), 25 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.DictionaryFormatter<string, double>();
                case 1: return new global::MessagePack.Formatters.DictionaryFormatter<string, double[]>();
                case 2: return new global::MessagePack.Formatters.ArrayFormatter<global::Michelangelo.Model.MichelangeloApi.GeometricModel>();
                case 3: return new global::MessagePack.Formatters.ArrayFormatter<string[]>();
                case 4: return new global::MessagePack.Formatters.DictionaryFormatter<int, global::Michelangelo.Model.MichelangeloApi.MaterialModel>();
                case 5: return new global::MessagePack.Formatters.DictionaryFormatter<uint, global::Michelangelo.Model.MichelangeloApi.ParseTreeModel>();
                case 6: return new global::MessagePack.Formatters.DictionaryFormatter<string, global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo>();
                case 7: return new global::MessagePack.Formatters.DictionaryFormatter<string, long>();
                case 8: return new global::MessagePack.Formatters.ListFormatter<global::Michelangelo.Model.MichelangeloApi.AxiomPostModel>();
                case 9: return new global::MessagePack.Formatters.DictionaryFormatter<string, string>();
                case 10: return new global::MessagePack.Formatters.InterfaceListFormatter<string>();
                case 11: return new MessagePack.Formatters.MessagePack.MessagePackTypeFormatter();
                case 12: return new MessagePack.Formatters.Michelangelo.Model.GrammarSourceFormatter();
                case 13: return new MessagePack.Formatters.Michelangelo.Model.Handlers.SourceTypeFormatter();
                case 14: return new MessagePack.Formatters.SimpleJSON.JSONNodeTypeFormatter();
                case 15: return new MessagePack.Formatters.SimpleJSON.JSONTextModeFormatter();
                case 16: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.AxiomPostModelFormatter();
                case 17: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.TriangularMeshFormatter();
                case 18: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.GeometricModelFormatter();
                case 19: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.MaterialModelFormatter();
                case 20: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.ParseTreeModelFormatter();
                case 21: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.RuleExtraInfoFormatter();
                case 22: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.SkyMaterialFormatter();
                case 23: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.SceneEnvironmentFormatter();
                case 24: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.PostResponseModelFormatter();
                case 25: return new MessagePack.Formatters.Michelangelo.Model.MichelangeloApi.SemanticModelFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.MessagePack
{
    using System;
    using MessagePack;

    public sealed class MessagePackTypeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MessagePack.MessagePackType>
    {
        public int Serialize(ref byte[] bytes, int offset, global::MessagePack.MessagePackType value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteByte(ref bytes, offset, (Byte)value);
        }
        
        public global::MessagePack.MessagePackType Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::MessagePack.MessagePackType)MessagePackBinary.ReadByte(bytes, offset, out readSize);
        }
    }


}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.Michelangelo.Model
{
    using System;
    using MessagePack;

    public sealed class GrammarSourceFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.GrammarSource>
    {
        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.GrammarSource value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, (Int32)value);
        }
        
        public global::Michelangelo.Model.GrammarSource Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::Michelangelo.Model.GrammarSource)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        }
    }


}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.Michelangelo.Model.Handlers
{
    using System;
    using MessagePack;

    public sealed class SourceTypeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.Handlers.SourceType>
    {
        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.Handlers.SourceType value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, (Int32)value);
        }
        
        public global::Michelangelo.Model.Handlers.SourceType Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::Michelangelo.Model.Handlers.SourceType)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        }
    }


}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.SimpleJSON
{
    using System;
    using MessagePack;

    public sealed class JSONNodeTypeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::SimpleJSON.JSONNodeType>
    {
        public int Serialize(ref byte[] bytes, int offset, global::SimpleJSON.JSONNodeType value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, (Int32)value);
        }
        
        public global::SimpleJSON.JSONNodeType Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::SimpleJSON.JSONNodeType)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        }
    }

    public sealed class JSONTextModeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::SimpleJSON.JSONTextMode>
    {
        public int Serialize(ref byte[] bytes, int offset, global::SimpleJSON.JSONTextMode value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, (Int32)value);
        }
        
        public global::SimpleJSON.JSONTextMode Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::SimpleJSON.JSONTextMode)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        }
    }


}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612


#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.Michelangelo.Model.MichelangeloApi
{
    using System;
    using MessagePack;


    public sealed class AxiomPostModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.AxiomPostModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public AxiomPostModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "AxiomID", 0},
                { "ParseTreeIndex", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AxiomID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ParseTreeIndex"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.AxiomPostModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.AxiomID);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.ParseTreeIndex);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.AxiomPostModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __AxiomID__ = default(uint);
            var __ParseTreeIndex__ = default(uint);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __AxiomID__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __ParseTreeIndex__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.AxiomPostModel();
            ____result.AxiomID = __AxiomID__;
            ____result.ParseTreeIndex = __ParseTreeIndex__;
            return ____result;
        }
    }


    public sealed class TriangularMeshFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.TriangularMesh>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TriangularMeshFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Indexed", 0},
                { "Indices", 1},
                { "Points", 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Indexed"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Indices"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Points"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.TriangularMesh value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Indexed);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.Indices, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<double[]>().Serialize(ref bytes, offset, value.Points, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.TriangularMesh Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Indexed__ = default(bool);
            var __Indices__ = default(int[]);
            var __Points__ = default(double[]);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __Indexed__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Indices__ = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Points__ = formatterResolver.GetFormatterWithVerify<double[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.TriangularMesh();
            ____result.Indexed = __Indexed__;
            ____result.Indices = __Indices__;
            ____result.Points = __Points__;
            return ____result;
        }
    }


    public sealed class GeometricModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.GeometricModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public GeometricModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Primitive", 0},
                { "MaterialID", 1},
                { "NodeID", 2},
                { "Transform", 3},
                { "Mesh", 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Primitive"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("MaterialID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("NodeID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Transform"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Mesh"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.GeometricModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Primitive, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaterialID);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.NodeID);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.Transform, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.TriangularMesh>().Serialize(ref bytes, offset, value.Mesh, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.GeometricModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Primitive__ = default(string);
            var __MaterialID__ = default(int);
            var __NodeID__ = default(uint);
            var __Transform__ = default(float[]);
            var __Mesh__ = default(global::Michelangelo.Model.MichelangeloApi.TriangularMesh);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __Primitive__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __MaterialID__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __NodeID__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __Transform__ = formatterResolver.GetFormatterWithVerify<float[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __Mesh__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.TriangularMesh>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.GeometricModel();
            ____result.Primitive = __Primitive__;
            ____result.MaterialID = __MaterialID__;
            ____result.NodeID = __NodeID__;
            ____result.Transform = __Transform__;
            ____result.Mesh = __Mesh__;
            return ____result;
        }
    }


    public sealed class MaterialModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.MaterialModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public MaterialModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Albedo", 0},
                { "Scalars", 1},
                { "Vectors", 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Albedo"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Scalars"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Vectors"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.MaterialModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<double[]>().Serialize(ref bytes, offset, value.Albedo, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, double>>().Serialize(ref bytes, offset, value.Scalars, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, double[]>>().Serialize(ref bytes, offset, value.Vectors, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.MaterialModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Albedo__ = default(double[]);
            var __Scalars__ = default(global::System.Collections.Generic.Dictionary<string, double>);
            var __Vectors__ = default(global::System.Collections.Generic.Dictionary<string, double[]>);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __Albedo__ = formatterResolver.GetFormatterWithVerify<double[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Scalars__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, double>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Vectors__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, double[]>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.MaterialModel();
            ____result.Albedo = __Albedo__;
            ____result.Scalars = __Scalars__;
            ____result.Vectors = __Vectors__;
            return ____result;
        }
    }


    public sealed class ParseTreeModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.ParseTreeModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public ParseTreeModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "ID", 0},
                { "Rule", 1},
                { "Shape", 2},
                { "Ontology", 3},
                { "ChildIndices", 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Rule"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Shape"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Ontology"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ChildIndices"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.ParseTreeModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.ID);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Rule, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.GeometricModel[]>().Serialize(ref bytes, offset, value.Shape, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<string[][]>().Serialize(ref bytes, offset, value.Ontology, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.ChildIndices, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.ParseTreeModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __ID__ = default(uint);
            var __Rule__ = default(string);
            var __Shape__ = default(global::Michelangelo.Model.MichelangeloApi.GeometricModel[]);
            var __Ontology__ = default(string[][]);
            var __ChildIndices__ = default(uint[]);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __ID__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Rule__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Shape__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.GeometricModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Ontology__ = formatterResolver.GetFormatterWithVerify<string[][]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __ChildIndices__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.ParseTreeModel();
            ____result.ID = __ID__;
            ____result.Rule = __Rule__;
            ____result.Shape = __Shape__;
            ____result.Ontology = __Ontology__;
            ____result.ChildIndices = __ChildIndices__;
            return ____result;
        }
    }


    public sealed class RuleExtraInfoFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public RuleExtraInfoFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "CallsCount", 0},
                { "FulfillsAttributes", 1},
                { "FulfillsGoals", 2},
                { "Local", 3},
                { "RUID", 4},
                { "TypeStr", 5},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("CallsCount"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("FulfillsAttributes"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("FulfillsGoals"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Local"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RUID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("TypeStr"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.CallsCount);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.FulfillsAttributes, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.FulfillsGoals, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Local);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.RUID, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.TypeStr, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __CallsCount__ = default(uint);
            var __FulfillsAttributes__ = default(string[]);
            var __FulfillsGoals__ = default(string[]);
            var __Local__ = default(bool);
            var __RUID__ = default(string);
            var __TypeStr__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __CallsCount__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __FulfillsAttributes__ = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __FulfillsGoals__ = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Local__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 4:
                        __RUID__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 5:
                        __TypeStr__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo();
            ____result.CallsCount = __CallsCount__;
            ____result.FulfillsAttributes = __FulfillsAttributes__;
            ____result.FulfillsGoals = __FulfillsGoals__;
            ____result.Local = __Local__;
            ____result.RUID = __RUID__;
            ____result.TypeStr = __TypeStr__;
            return ____result;
        }
    }


    public sealed class SkyMaterialFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.SkyMaterial>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SkyMaterialFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "ActiveSky", 0},
                { "ActiveSun", 1},
                { "Albedo", 2},
                { "Day", 3},
                { "Hour", 4},
                { "Latitude", 5},
                { "Longitude", 6},
                { "Minute", 7},
                { "Month", 8},
                { "Scale", 9},
                { "Second", 10},
                { "SkyScale", 11},
                { "SunDirection", 12},
                { "SunScale", 13},
                { "Timezone", 14},
                { "Turbidity", 15},
                { "Year", 16},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ActiveSky"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ActiveSun"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Albedo"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Day"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Hour"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Latitude"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Longitude"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Minute"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Month"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Scale"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Second"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SkyScale"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SunDirection"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SunScale"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Timezone"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Turbidity"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Year"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.SkyMaterial value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteMapHeader(ref bytes, offset, 17);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.ActiveSky);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.ActiveSun);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Albedo);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Day);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Hour);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Latitude);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Longitude);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Minute);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Month);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
            offset += formatterResolver.GetFormatterWithVerify<double?>().Serialize(ref bytes, offset, value.Scale, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Second);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
            offset += formatterResolver.GetFormatterWithVerify<double?>().Serialize(ref bytes, offset, value.SkyScale, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
            offset += formatterResolver.GetFormatterWithVerify<double[]>().Serialize(ref bytes, offset, value.SunDirection, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
            offset += formatterResolver.GetFormatterWithVerify<double?>().Serialize(ref bytes, offset, value.SunScale, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Timezone);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Turbidity);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Year);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.SkyMaterial Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __ActiveSky__ = default(bool);
            var __ActiveSun__ = default(bool);
            var __Albedo__ = default(double);
            var __Day__ = default(int);
            var __Hour__ = default(int);
            var __Latitude__ = default(double);
            var __Longitude__ = default(double);
            var __Minute__ = default(int);
            var __Month__ = default(int);
            var __Scale__ = default(double?);
            var __Second__ = default(int);
            var __SkyScale__ = default(double?);
            var __SunDirection__ = default(double[]);
            var __SunScale__ = default(double?);
            var __Timezone__ = default(int);
            var __Turbidity__ = default(double);
            var __Year__ = default(int);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __ActiveSky__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 1:
                        __ActiveSun__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 2:
                        __Albedo__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 3:
                        __Day__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 4:
                        __Hour__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 5:
                        __Latitude__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 6:
                        __Longitude__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 7:
                        __Minute__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 8:
                        __Month__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 9:
                        __Scale__ = formatterResolver.GetFormatterWithVerify<double?>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 10:
                        __Second__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 11:
                        __SkyScale__ = formatterResolver.GetFormatterWithVerify<double?>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 12:
                        __SunDirection__ = formatterResolver.GetFormatterWithVerify<double[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 13:
                        __SunScale__ = formatterResolver.GetFormatterWithVerify<double?>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 14:
                        __Timezone__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 15:
                        __Turbidity__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 16:
                        __Year__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.SkyMaterial();
            ____result.ActiveSky = __ActiveSky__;
            ____result.ActiveSun = __ActiveSun__;
            ____result.Albedo = __Albedo__;
            ____result.Day = __Day__;
            ____result.Hour = __Hour__;
            ____result.Latitude = __Latitude__;
            ____result.Longitude = __Longitude__;
            ____result.Minute = __Minute__;
            ____result.Month = __Month__;
            ____result.Scale = __Scale__;
            ____result.Second = __Second__;
            ____result.SkyScale = __SkyScale__;
            ____result.SunDirection = __SunDirection__;
            ____result.SunScale = __SunScale__;
            ____result.Timezone = __Timezone__;
            ____result.Turbidity = __Turbidity__;
            ____result.Year = __Year__;
            return ____result;
        }
    }


    public sealed class SceneEnvironmentFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.SceneEnvironment>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SceneEnvironmentFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Ambient", 0},
                { "AnimFps", 1},
                { "AnimStart", 2},
                { "AnimStop", 3},
                { "AssemblyAnimationMinVolume", 4},
                { "AssemblyLength", 5},
                { "Bidirectional", 6},
                { "Bounces", 7},
                { "GrammarsCount", 8},
                { "HideEmitters", 9},
                { "IgnoreCams", 10},
                { "IgnoreDeforms", 11},
                { "LowRes", 12},
                { "MaxOtherCams", 13},
                { "PathTrace", 14},
                { "RR", 15},
                { "RulesCount", 16},
                { "SamplesPower", 17},
                { "Sky", 18},
                { "Statistics", 19},
                { "StepsCount", 20},
                { "TimeLimit", 21},
                { "UseBlender", 22},
                { "UseRenderman", 23},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Ambient"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AnimFps"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AnimStart"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AnimStop"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AssemblyAnimationMinVolume"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AssemblyLength"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Bidirectional"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Bounces"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("GrammarsCount"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("HideEmitters"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IgnoreCams"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IgnoreDeforms"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("LowRes"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("MaxOtherCams"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("PathTrace"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RR"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RulesCount"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SamplesPower"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Sky"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Statistics"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("StepsCount"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("TimeLimit"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("UseBlender"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("UseRenderman"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.SceneEnvironment value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteMapHeader(ref bytes, offset, 24);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Ambient);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.AnimFps);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.AnimStart);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.AnimStop);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.AssemblyAnimationMinVolume);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AssemblyLength);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Bidirectional);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Bounces);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.GrammarsCount);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.HideEmitters);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IgnoreCams);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IgnoreDeforms);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.LowRes);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.MaxOtherCams);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.PathTrace);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RR);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RulesCount);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SamplesPower);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.SkyMaterial>().Serialize(ref bytes, offset, value.Sky, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, long>>().Serialize(ref bytes, offset, value.Statistics, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
            offset += MessagePackBinary.WriteUInt64(ref bytes, offset, value.StepsCount);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TimeLimit);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.UseBlender);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.UseRenderman);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.SceneEnvironment Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Ambient__ = default(double);
            var __AnimFps__ = default(uint);
            var __AnimStart__ = default(uint);
            var __AnimStop__ = default(uint);
            var __AssemblyAnimationMinVolume__ = default(double);
            var __AssemblyLength__ = default(int);
            var __Bidirectional__ = default(bool);
            var __Bounces__ = default(int);
            var __GrammarsCount__ = default(int);
            var __HideEmitters__ = default(bool);
            var __IgnoreCams__ = default(bool);
            var __IgnoreDeforms__ = default(bool);
            var __LowRes__ = default(bool);
            var __MaxOtherCams__ = default(uint);
            var __PathTrace__ = default(bool);
            var __RR__ = default(int);
            var __RulesCount__ = default(int);
            var __SamplesPower__ = default(int);
            var __Sky__ = default(global::Michelangelo.Model.MichelangeloApi.SkyMaterial);
            var __Statistics__ = default(global::System.Collections.Generic.Dictionary<string, long>);
            var __StepsCount__ = default(ulong);
            var __TimeLimit__ = default(int);
            var __UseBlender__ = default(bool);
            var __UseRenderman__ = default(bool);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __Ambient__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 1:
                        __AnimFps__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __AnimStart__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __AnimStop__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 4:
                        __AssemblyAnimationMinVolume__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 5:
                        __AssemblyLength__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 6:
                        __Bidirectional__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 7:
                        __Bounces__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 8:
                        __GrammarsCount__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 9:
                        __HideEmitters__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 10:
                        __IgnoreCams__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 11:
                        __IgnoreDeforms__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 12:
                        __LowRes__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 13:
                        __MaxOtherCams__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 14:
                        __PathTrace__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 15:
                        __RR__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 16:
                        __RulesCount__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 17:
                        __SamplesPower__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 18:
                        __Sky__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.SkyMaterial>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 19:
                        __Statistics__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, long>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 20:
                        __StepsCount__ = MessagePackBinary.ReadUInt64(bytes, offset, out readSize);
                        break;
                    case 21:
                        __TimeLimit__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 22:
                        __UseBlender__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 23:
                        __UseRenderman__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.SceneEnvironment();
            ____result.Ambient = __Ambient__;
            ____result.AnimFps = __AnimFps__;
            ____result.AnimStart = __AnimStart__;
            ____result.AnimStop = __AnimStop__;
            ____result.AssemblyAnimationMinVolume = __AssemblyAnimationMinVolume__;
            ____result.AssemblyLength = __AssemblyLength__;
            ____result.Bidirectional = __Bidirectional__;
            ____result.Bounces = __Bounces__;
            ____result.GrammarsCount = __GrammarsCount__;
            ____result.HideEmitters = __HideEmitters__;
            ____result.IgnoreCams = __IgnoreCams__;
            ____result.IgnoreDeforms = __IgnoreDeforms__;
            ____result.LowRes = __LowRes__;
            ____result.MaxOtherCams = __MaxOtherCams__;
            ____result.PathTrace = __PathTrace__;
            ____result.RR = __RR__;
            ____result.RulesCount = __RulesCount__;
            ____result.SamplesPower = __SamplesPower__;
            ____result.Sky = __Sky__;
            ____result.Statistics = __Statistics__;
            ____result.StepsCount = __StepsCount__;
            ____result.TimeLimit = __TimeLimit__;
            ____result.UseBlender = __UseBlender__;
            ____result.UseRenderman = __UseRenderman__;
            return ____result;
        }
    }


    public sealed class PostResponseModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.PostResponseModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public PostResponseModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Materials", 0},
                { "Objects", 1},
                { "Errors", 2},
                { "IMG", 3},
                { "Parsed", 4},
                { "Info", 5},
                { "ParseTree", 6},
                { "Rules", 7},
                { "RootLocks", 8},
                { "LeafLocks", 9},
                { "ENV", 10},
                { "Models", 11},
                { "DL", 12},
                { "HasAnim", 13},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Materials"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Objects"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Errors"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IMG"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Parsed"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Info"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ParseTree"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Rules"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RootLocks"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("LeafLocks"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ENV"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Models"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("DL"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("HasAnim"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.PostResponseModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 14);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<int, global::Michelangelo.Model.MichelangeloApi.MaterialModel>>().Serialize(ref bytes, offset, value.Materials, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.GeometricModel[]>().Serialize(ref bytes, offset, value.Objects, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Errors, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.IMG, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Parsed, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Info, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MichelangeloApi.ParseTreeModel>>().Serialize(ref bytes, offset, value.ParseTree, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo>>().Serialize(ref bytes, offset, value.Rules, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.RootLocks, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.LeafLocks, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.SceneEnvironment>().Serialize(ref bytes, offset, value.ENV, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Michelangelo.Model.MichelangeloApi.AxiomPostModel>>().Serialize(ref bytes, offset, value.Models, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DL, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.HasAnim);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.PostResponseModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Materials__ = default(global::System.Collections.Generic.Dictionary<int, global::Michelangelo.Model.MichelangeloApi.MaterialModel>);
            var __Objects__ = default(global::Michelangelo.Model.MichelangeloApi.GeometricModel[]);
            var __Errors__ = default(string);
            var __IMG__ = default(string);
            var __Parsed__ = default(string);
            var __Info__ = default(string);
            var __ParseTree__ = default(global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MichelangeloApi.ParseTreeModel>);
            var __Rules__ = default(global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo>);
            var __RootLocks__ = default(uint[]);
            var __LeafLocks__ = default(uint[]);
            var __ENV__ = default(global::Michelangelo.Model.MichelangeloApi.SceneEnvironment);
            var __Models__ = default(global::System.Collections.Generic.List<global::Michelangelo.Model.MichelangeloApi.AxiomPostModel>);
            var __DL__ = default(string);
            var __HasAnim__ = default(bool);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __Materials__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<int, global::Michelangelo.Model.MichelangeloApi.MaterialModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Objects__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.GeometricModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Errors__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __IMG__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __Parsed__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 5:
                        __Info__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 6:
                        __ParseTree__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MichelangeloApi.ParseTreeModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 7:
                        __Rules__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MichelangeloApi.RuleExtraInfo>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 8:
                        __RootLocks__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 9:
                        __LeafLocks__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 10:
                        __ENV__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MichelangeloApi.SceneEnvironment>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 11:
                        __Models__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Michelangelo.Model.MichelangeloApi.AxiomPostModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 12:
                        __DL__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 13:
                        __HasAnim__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.PostResponseModel();
            ____result.Materials = __Materials__;
            ____result.Objects = __Objects__;
            ____result.Errors = __Errors__;
            ____result.IMG = __IMG__;
            ____result.Parsed = __Parsed__;
            ____result.Info = __Info__;
            ____result.ParseTree = __ParseTree__;
            ____result.Rules = __Rules__;
            ____result.RootLocks = __RootLocks__;
            ____result.LeafLocks = __LeafLocks__;
            ____result.ENV = __ENV__;
            ____result.Models = __Models__;
            ____result.DL = __DL__;
            ____result.HasAnim = __HasAnim__;
            return ____result;
        }
    }


    public sealed class SemanticModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MichelangeloApi.SemanticModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SemanticModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "At", 0},
                { "Go", 1},
                { "On", 2},
                { "Ta", 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("At"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Go"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("On"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Ta"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MichelangeloApi.SemanticModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Serialize(ref bytes, offset, value.At, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Serialize(ref bytes, offset, value.Go, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IList<string>>().Serialize(ref bytes, offset, value.On, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.Ta, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MichelangeloApi.SemanticModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __At__ = default(global::System.Collections.Generic.Dictionary<string, string>);
            var __Go__ = default(global::System.Collections.Generic.Dictionary<string, string>);
            var __On__ = default(global::System.Collections.Generic.IList<string>);
            var __Ta__ = default(string[]);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __At__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Go__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __On__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IList<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Ta__ = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MichelangeloApi.SemanticModel();
            ____result.At = __At__;
            ____result.Go = __Go__;
            ____result.On = __On__;
            ____result.Ta = __Ta__;
            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
