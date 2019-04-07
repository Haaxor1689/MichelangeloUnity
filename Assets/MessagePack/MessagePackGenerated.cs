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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(23)
            {
                {typeof(global::Michelangelo.Model.MsgSerialized.GeometricModel[]), 0 },
                {typeof(global::System.Collections.Generic.Dictionary<string, string>), 1 },
                {typeof(global::System.Collections.Generic.IList<string>), 2 },
                {typeof(global::Michelangelo.Model.MsgSerialized.SemanticModel[]), 3 },
                {typeof(global::System.Collections.Generic.Dictionary<int, string>), 4 },
                {typeof(global::System.Collections.Generic.HashSet<string>), 5 },
                {typeof(global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>), 6 },
                {typeof(global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>), 7 },
                {typeof(global::System.Collections.Generic.Dictionary<string, long>), 8 },
                {typeof(global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>), 9 },
                {typeof(global::MessagePack.MessagePackType), 10 },
                {typeof(global::Michelangelo.Model.GrammarSource), 11 },
                {typeof(global::SimpleJSON.JSONNodeType), 12 },
                {typeof(global::SimpleJSON.JSONTextMode), 13 },
                {typeof(global::Michelangelo.Model.MsgSerialized.AxiomPostModel), 14 },
                {typeof(global::Michelangelo.Model.MsgSerialized.TriangularMesh), 15 },
                {typeof(global::Michelangelo.Model.MsgSerialized.GeometricModel), 16 },
                {typeof(global::Michelangelo.Model.MsgSerialized.SemanticModel), 17 },
                {typeof(global::Michelangelo.Model.MsgSerialized.ParseTreeModel), 18 },
                {typeof(global::Michelangelo.Model.MsgSerialized.RuleExtraInfo), 19 },
                {typeof(global::Michelangelo.Model.MsgSerialized.SkyMaterial), 20 },
                {typeof(global::Michelangelo.Model.MsgSerialized.SceneEnvironment), 21 },
                {typeof(global::Michelangelo.Model.MsgSerialized.PostResponseModel), 22 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ArrayFormatter<global::Michelangelo.Model.MsgSerialized.GeometricModel>();
                case 1: return new global::MessagePack.Formatters.DictionaryFormatter<string, string>();
                case 2: return new global::MessagePack.Formatters.InterfaceListFormatter<string>();
                case 3: return new global::MessagePack.Formatters.ArrayFormatter<global::Michelangelo.Model.MsgSerialized.SemanticModel>();
                case 4: return new global::MessagePack.Formatters.DictionaryFormatter<int, string>();
                case 5: return new global::MessagePack.Formatters.HashSetFormatter<string>();
                case 6: return new global::MessagePack.Formatters.DictionaryFormatter<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>();
                case 7: return new global::MessagePack.Formatters.DictionaryFormatter<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>();
                case 8: return new global::MessagePack.Formatters.DictionaryFormatter<string, long>();
                case 9: return new global::MessagePack.Formatters.ListFormatter<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>();
                case 10: return new MessagePack.Formatters.MessagePack.MessagePackTypeFormatter();
                case 11: return new MessagePack.Formatters.Michelangelo.Model.GrammarSourceFormatter();
                case 12: return new MessagePack.Formatters.SimpleJSON.JSONNodeTypeFormatter();
                case 13: return new MessagePack.Formatters.SimpleJSON.JSONTextModeFormatter();
                case 14: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.AxiomPostModelFormatter();
                case 15: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.TriangularMeshFormatter();
                case 16: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.GeometricModelFormatter();
                case 17: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.SemanticModelFormatter();
                case 18: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.ParseTreeModelFormatter();
                case 19: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.RuleExtraInfoFormatter();
                case 20: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.SkyMaterialFormatter();
                case 21: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.SceneEnvironmentFormatter();
                case 22: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.PostResponseModelFormatter();
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

namespace MessagePack.Formatters.Michelangelo.Model.MsgSerialized
{
    using System;
    using MessagePack;


    public sealed class AxiomPostModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public AxiomPostModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "P", 0},
                { "A", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("P"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("A"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.AxiomPostModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.P);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.A);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.AxiomPostModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __P__ = default(uint);
            var __A__ = default(uint);

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
                        __P__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __A__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.AxiomPostModel();
            ____result.P = __P__;
            ____result.A = __A__;
            return ____result;
        }
    }


    public sealed class TriangularMeshFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.TriangularMesh>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TriangularMeshFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Indices", 0},
                { "Points", 1},
                { "Indexed", 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Indices"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Points"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Indexed"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.TriangularMesh value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.Indices, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<double[]>().Serialize(ref bytes, offset, value.Points, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Indexed);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.TriangularMesh Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Indices__ = default(int[]);
            var __Points__ = default(double[]);
            var __Indexed__ = default(bool);

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
                        __Indices__ = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Points__ = formatterResolver.GetFormatterWithVerify<double[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Indexed__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.TriangularMesh();
            ____result.Indices = __Indices__;
            ____result.Points = __Points__;
            ____result.Indexed = __Indexed__;
            return ____result;
        }
    }


    public sealed class GeometricModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.GeometricModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public GeometricModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "G", 0},
                { "M", 1},
                { "N", 2},
                { "T", 3},
                { "V", 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("G"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("M"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("N"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("T"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("V"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.GeometricModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.G, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.M);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.N);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.T, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.TriangularMesh>().Serialize(ref bytes, offset, value.V, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.GeometricModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __G__ = default(string);
            var __M__ = default(int);
            var __N__ = default(uint);
            var __T__ = default(float[]);
            var __V__ = default(global::Michelangelo.Model.MsgSerialized.TriangularMesh);

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
                        __G__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __M__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __N__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __T__ = formatterResolver.GetFormatterWithVerify<float[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __V__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.TriangularMesh>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.GeometricModel();
            ____result.G = __G__;
            ____result.M = __M__;
            ____result.N = __N__;
            ____result.T = __T__;
            ____result.V = __V__;
            return ____result;
        }
    }


    public sealed class SemanticModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.SemanticModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SemanticModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Ta", 0},
                { "Go", 1},
                { "At", 2},
                { "On", 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Ta"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Go"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("At"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("On"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.SemanticModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.Ta, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Serialize(ref bytes, offset, value.Go, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Serialize(ref bytes, offset, value.At, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IList<string>>().Serialize(ref bytes, offset, value.On, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.SemanticModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Ta__ = default(string[]);
            var __Go__ = default(global::System.Collections.Generic.Dictionary<string, string>);
            var __At__ = default(global::System.Collections.Generic.Dictionary<string, string>);
            var __On__ = default(global::System.Collections.Generic.IList<string>);

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
                        __Ta__ = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Go__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __At__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __On__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IList<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.SemanticModel();
            ____result.Ta = __Ta__;
            ____result.Go = __Go__;
            ____result.At = __At__;
            ____result.On = __On__;
            return ____result;
        }
    }


    public sealed class ParseTreeModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.ParseTreeModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public ParseTreeModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "ID", 0},
                { "R", 1},
                { "G", 2},
                { "S", 3},
                { "P", 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("R"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("G"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("S"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("P"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.ParseTreeModel value, global::MessagePack.IFormatterResolver formatterResolver)
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
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.R, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Serialize(ref bytes, offset, value.G, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SemanticModel[]>().Serialize(ref bytes, offset, value.S, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.P, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.ParseTreeModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
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
            var __R__ = default(string);
            var __G__ = default(global::Michelangelo.Model.MsgSerialized.GeometricModel[]);
            var __S__ = default(global::Michelangelo.Model.MsgSerialized.SemanticModel[]);
            var __P__ = default(uint[]);

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
                        __R__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __G__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __S__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SemanticModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __P__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.ParseTreeModel();
            ____result.ID = __ID__;
            ____result.R = __R__;
            ____result.G = __G__;
            ____result.S = __S__;
            ____result.P = __P__;
            return ____result;
        }
    }


    public sealed class RuleExtraInfoFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public RuleExtraInfoFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "RUID", 0},
                { "TypeStr", 1},
                { "Local", 2},
                { "FulfillsGoals", 3},
                { "FulfillsAttributes", 4},
                { "CallsCount", 5},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RUID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("TypeStr"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Local"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("FulfillsGoals"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("FulfillsAttributes"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("CallsCount"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.RUID, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.TypeStr, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Local);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.FulfillsGoals, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.FulfillsAttributes, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.CallsCount);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.RuleExtraInfo Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __RUID__ = default(string);
            var __TypeStr__ = default(string);
            var __Local__ = default(bool);
            var __FulfillsGoals__ = default(string[]);
            var __FulfillsAttributes__ = default(string[]);
            var __CallsCount__ = default(uint);

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
                        __RUID__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __TypeStr__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Local__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 3:
                        __FulfillsGoals__ = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __FulfillsAttributes__ = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 5:
                        __CallsCount__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.RuleExtraInfo();
            ____result.RUID = __RUID__;
            ____result.TypeStr = __TypeStr__;
            ____result.Local = __Local__;
            ____result.FulfillsGoals = __FulfillsGoals__;
            ____result.FulfillsAttributes = __FulfillsAttributes__;
            ____result.CallsCount = __CallsCount__;
            return ____result;
        }
    }


    public sealed class SkyMaterialFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.SkyMaterial>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SkyMaterialFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Turbidity", 0},
                { "Albedo", 1},
                { "Year", 2},
                { "Month", 3},
                { "Day", 4},
                { "Hour", 5},
                { "Minute", 6},
                { "Second", 7},
                { "Timezone", 8},
                { "Latitude", 9},
                { "Longitude", 10},
                { "SunDirection", 11},
                { "Scale", 12},
                { "SunScale", 13},
                { "SkyScale", 14},
                { "ActiveSun", 15},
                { "ActiveSky", 16},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Turbidity"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Albedo"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Year"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Month"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Day"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Hour"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Minute"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Second"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Timezone"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Latitude"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Longitude"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SunDirection"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Scale"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SunScale"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SkyScale"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ActiveSun"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ActiveSky"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.SkyMaterial value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteMapHeader(ref bytes, offset, 17);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Turbidity);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Albedo);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Year);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Month);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Day);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Hour);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Minute);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Second);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Timezone);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Latitude);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.Longitude);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
            offset += formatterResolver.GetFormatterWithVerify<double[]>().Serialize(ref bytes, offset, value.SunDirection, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
            offset += formatterResolver.GetFormatterWithVerify<double?>().Serialize(ref bytes, offset, value.Scale, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
            offset += formatterResolver.GetFormatterWithVerify<double?>().Serialize(ref bytes, offset, value.SunScale, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
            offset += formatterResolver.GetFormatterWithVerify<double?>().Serialize(ref bytes, offset, value.SkyScale, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.ActiveSun);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.ActiveSky);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.SkyMaterial Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Turbidity__ = default(double);
            var __Albedo__ = default(double);
            var __Year__ = default(int);
            var __Month__ = default(int);
            var __Day__ = default(int);
            var __Hour__ = default(int);
            var __Minute__ = default(int);
            var __Second__ = default(int);
            var __Timezone__ = default(int);
            var __Latitude__ = default(double);
            var __Longitude__ = default(double);
            var __SunDirection__ = default(double[]);
            var __Scale__ = default(double?);
            var __SunScale__ = default(double?);
            var __SkyScale__ = default(double?);
            var __ActiveSun__ = default(bool);
            var __ActiveSky__ = default(bool);

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
                        __Turbidity__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Albedo__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 2:
                        __Year__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __Month__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 4:
                        __Day__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 5:
                        __Hour__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 6:
                        __Minute__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 7:
                        __Second__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 8:
                        __Timezone__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 9:
                        __Latitude__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 10:
                        __Longitude__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 11:
                        __SunDirection__ = formatterResolver.GetFormatterWithVerify<double[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 12:
                        __Scale__ = formatterResolver.GetFormatterWithVerify<double?>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 13:
                        __SunScale__ = formatterResolver.GetFormatterWithVerify<double?>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 14:
                        __SkyScale__ = formatterResolver.GetFormatterWithVerify<double?>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 15:
                        __ActiveSun__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 16:
                        __ActiveSky__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.SkyMaterial();
            ____result.Turbidity = __Turbidity__;
            ____result.Albedo = __Albedo__;
            ____result.Year = __Year__;
            ____result.Month = __Month__;
            ____result.Day = __Day__;
            ____result.Hour = __Hour__;
            ____result.Minute = __Minute__;
            ____result.Second = __Second__;
            ____result.Timezone = __Timezone__;
            ____result.Latitude = __Latitude__;
            ____result.Longitude = __Longitude__;
            ____result.SunDirection = __SunDirection__;
            ____result.Scale = __Scale__;
            ____result.SunScale = __SunScale__;
            ____result.SkyScale = __SkyScale__;
            ____result.ActiveSun = __ActiveSun__;
            ____result.ActiveSky = __ActiveSky__;
            return ____result;
        }
    }


    public sealed class SceneEnvironmentFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.SceneEnvironment>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SceneEnvironmentFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Ambient", 0},
                { "Bounces", 1},
                { "RR", 2},
                { "SamplesPower", 3},
                { "Bidirectional", 4},
                { "Sky", 5},
                { "LowRes", 6},
                { "HideEmitters", 7},
                { "PathTrace", 8},
                { "StepsCount", 9},
                { "RulesCount", 10},
                { "GrammarsCount", 11},
                { "Statistics", 12},
                { "AssemblyLength", 13},
                { "AssemblyAnimationMinVolume", 14},
                { "IgnoreCams", 15},
                { "MaxOtherCams", 16},
                { "AnimStart", 17},
                { "AnimStop", 18},
                { "AnimFps", 19},
                { "UseBlender", 20},
                { "UseRenderman", 21},
                { "IgnoreDeforms", 22},
                { "TimeLimit", 23},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Ambient"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Bounces"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RR"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("SamplesPower"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Bidirectional"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Sky"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("LowRes"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("HideEmitters"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("PathTrace"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("StepsCount"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RulesCount"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("GrammarsCount"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Statistics"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AssemblyLength"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AssemblyAnimationMinVolume"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IgnoreCams"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("MaxOtherCams"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AnimStart"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AnimStop"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("AnimFps"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("UseBlender"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("UseRenderman"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IgnoreDeforms"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("TimeLimit"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.SceneEnvironment value, global::MessagePack.IFormatterResolver formatterResolver)
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
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Bounces);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RR);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SamplesPower);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Bidirectional);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SkyMaterial>().Serialize(ref bytes, offset, value.Sky, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.LowRes);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.HideEmitters);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.PathTrace);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
            offset += MessagePackBinary.WriteUInt64(ref bytes, offset, value.StepsCount);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RulesCount);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.GrammarsCount);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, long>>().Serialize(ref bytes, offset, value.Statistics, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AssemblyLength);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
            offset += MessagePackBinary.WriteDouble(ref bytes, offset, value.AssemblyAnimationMinVolume);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IgnoreCams);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.MaxOtherCams);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.AnimStart);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.AnimStop);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.AnimFps);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.UseBlender);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.UseRenderman);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IgnoreDeforms);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TimeLimit);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.SceneEnvironment Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
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
            var __Bounces__ = default(int);
            var __RR__ = default(int);
            var __SamplesPower__ = default(int);
            var __Bidirectional__ = default(bool);
            var __Sky__ = default(global::Michelangelo.Model.MsgSerialized.SkyMaterial);
            var __LowRes__ = default(bool);
            var __HideEmitters__ = default(bool);
            var __PathTrace__ = default(bool);
            var __StepsCount__ = default(ulong);
            var __RulesCount__ = default(int);
            var __GrammarsCount__ = default(int);
            var __Statistics__ = default(global::System.Collections.Generic.Dictionary<string, long>);
            var __AssemblyLength__ = default(int);
            var __AssemblyAnimationMinVolume__ = default(double);
            var __IgnoreCams__ = default(bool);
            var __MaxOtherCams__ = default(uint);
            var __AnimStart__ = default(uint);
            var __AnimStop__ = default(uint);
            var __AnimFps__ = default(uint);
            var __UseBlender__ = default(bool);
            var __UseRenderman__ = default(bool);
            var __IgnoreDeforms__ = default(bool);
            var __TimeLimit__ = default(int);

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
                        __Bounces__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __RR__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __SamplesPower__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 4:
                        __Bidirectional__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 5:
                        __Sky__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SkyMaterial>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 6:
                        __LowRes__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 7:
                        __HideEmitters__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 8:
                        __PathTrace__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 9:
                        __StepsCount__ = MessagePackBinary.ReadUInt64(bytes, offset, out readSize);
                        break;
                    case 10:
                        __RulesCount__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 11:
                        __GrammarsCount__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 12:
                        __Statistics__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, long>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 13:
                        __AssemblyLength__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 14:
                        __AssemblyAnimationMinVolume__ = MessagePackBinary.ReadDouble(bytes, offset, out readSize);
                        break;
                    case 15:
                        __IgnoreCams__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 16:
                        __MaxOtherCams__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 17:
                        __AnimStart__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 18:
                        __AnimStop__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 19:
                        __AnimFps__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 20:
                        __UseBlender__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 21:
                        __UseRenderman__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 22:
                        __IgnoreDeforms__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 23:
                        __TimeLimit__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Model.MsgSerialized.SceneEnvironment();
            ____result.Ambient = __Ambient__;
            ____result.Bounces = __Bounces__;
            ____result.RR = __RR__;
            ____result.SamplesPower = __SamplesPower__;
            ____result.Bidirectional = __Bidirectional__;
            ____result.Sky = __Sky__;
            ____result.LowRes = __LowRes__;
            ____result.HideEmitters = __HideEmitters__;
            ____result.PathTrace = __PathTrace__;
            ____result.StepsCount = __StepsCount__;
            ____result.RulesCount = __RulesCount__;
            ____result.GrammarsCount = __GrammarsCount__;
            ____result.Statistics = __Statistics__;
            ____result.AssemblyLength = __AssemblyLength__;
            ____result.AssemblyAnimationMinVolume = __AssemblyAnimationMinVolume__;
            ____result.IgnoreCams = __IgnoreCams__;
            ____result.MaxOtherCams = __MaxOtherCams__;
            ____result.AnimStart = __AnimStart__;
            ____result.AnimStop = __AnimStop__;
            ____result.AnimFps = __AnimFps__;
            ____result.UseBlender = __UseBlender__;
            ____result.UseRenderman = __UseRenderman__;
            ____result.IgnoreDeforms = __IgnoreDeforms__;
            ____result.TimeLimit = __TimeLimit__;
            return ____result;
        }
    }


    public sealed class PostResponseModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.PostResponseModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public PostResponseModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "ML", 0},
                { "O", 1},
                { "E", 2},
                { "A", 3},
                { "IMG", 4},
                { "ParsedJSON", 5},
                { "PT", 6},
                { "RS", 7},
                { "LR", 8},
                { "LL", 9},
                { "ENV", 10},
                { "PS", 11},
                { "RC", 12},
                { "DL", 13},
                { "HasAnim", 14},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ML"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("O"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("E"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("A"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IMG"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ParsedJSON"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("PT"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RS"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("LR"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("LL"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ENV"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("PS"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RC"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("DL"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("HasAnim"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.PostResponseModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 15);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<int, string>>().Serialize(ref bytes, offset, value.ML, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Serialize(ref bytes, offset, value.O, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.E, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.HashSet<string>>().Serialize(ref bytes, offset, value.A, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.IMG, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ParsedJSON, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>>().Serialize(ref bytes, offset, value.PT, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>>().Serialize(ref bytes, offset, value.RS, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.LR, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.LL, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SceneEnvironment>().Serialize(ref bytes, offset, value.ENV, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>>().Serialize(ref bytes, offset, value.PS, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RC);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DL, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.HasAnim);
            return offset - startOffset;
        }

        public global::Michelangelo.Model.MsgSerialized.PostResponseModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __ML__ = default(global::System.Collections.Generic.Dictionary<int, string>);
            var __O__ = default(global::Michelangelo.Model.MsgSerialized.GeometricModel[]);
            var __E__ = default(string);
            var __A__ = default(global::System.Collections.Generic.HashSet<string>);
            var __IMG__ = default(string);
            var __ParsedJSON__ = default(string);
            var __PT__ = default(global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>);
            var __RS__ = default(global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>);
            var __LR__ = default(uint[]);
            var __LL__ = default(uint[]);
            var __ENV__ = default(global::Michelangelo.Model.MsgSerialized.SceneEnvironment);
            var __PS__ = default(global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>);
            var __RC__ = default(int);
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
                        __ML__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<int, string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __O__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __E__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __A__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.HashSet<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __IMG__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 5:
                        __ParsedJSON__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 6:
                        __PT__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 7:
                        __RS__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 8:
                        __LR__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 9:
                        __LL__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 10:
                        __ENV__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SceneEnvironment>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 11:
                        __PS__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 12:
                        __RC__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 13:
                        __DL__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 14:
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

            var ____result = new global::Michelangelo.Model.MsgSerialized.PostResponseModel();
            ____result.ML = __ML__;
            ____result.O = __O__;
            ____result.E = __E__;
            ____result.A = __A__;
            ____result.IMG = __IMG__;
            ____result.ParsedJSON = __ParsedJSON__;
            ____result.PT = __PT__;
            ____result.RS = __RS__;
            ____result.LR = __LR__;
            ____result.LL = __LL__;
            ____result.ENV = __ENV__;
            ____result.PS = __PS__;
            ____result.RC = __RC__;
            ____result.DL = __DL__;
            ____result.HasAnim = __HasAnim__;
            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
