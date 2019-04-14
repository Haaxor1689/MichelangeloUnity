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
                {typeof(global::System.Collections.Generic.HashSet<string>), 4 },
                {typeof(global::System.Collections.Generic.Dictionary<string, long>), 5 },
                {typeof(global::System.Collections.Generic.Dictionary<int, string>), 6 },
                {typeof(global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>), 7 },
                {typeof(global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>), 8 },
                {typeof(global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>), 9 },
                {typeof(global::MessagePack.MessagePackType), 10 },
                {typeof(global::Michelangelo.Model.GrammarSource), 11 },
                {typeof(global::SimpleJSON.JSONNodeType), 12 },
                {typeof(global::SimpleJSON.JSONTextMode), 13 },
                {typeof(global::Michelangelo.Model.MsgSerialized.AxiomPostModel), 14 },
                {typeof(global::Michelangelo.Model.MsgSerialized.TriangularMesh), 15 },
                {typeof(global::Michelangelo.Model.MsgSerialized.GeometricModel), 16 },
                {typeof(global::Michelangelo.Model.MsgSerialized.SemanticModel), 17 },
                {typeof(global::Michelangelo.Model.MsgSerialized.ParseTreeModel), 18 },
                {typeof(global::Michelangelo.Model.MsgSerialized.SkyMaterial), 19 },
                {typeof(global::Michelangelo.Model.MsgSerialized.SceneEnvironment), 20 },
                {typeof(global::Michelangelo.Model.MsgSerialized.RuleExtraInfo), 21 },
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
                case 4: return new global::MessagePack.Formatters.HashSetFormatter<string>();
                case 5: return new global::MessagePack.Formatters.DictionaryFormatter<string, long>();
                case 6: return new global::MessagePack.Formatters.DictionaryFormatter<int, string>();
                case 7: return new global::MessagePack.Formatters.ListFormatter<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>();
                case 8: return new global::MessagePack.Formatters.DictionaryFormatter<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>();
                case 9: return new global::MessagePack.Formatters.DictionaryFormatter<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>();
                case 10: return new MessagePack.Formatters.MessagePack.MessagePackTypeFormatter();
                case 11: return new MessagePack.Formatters.Michelangelo.Model.GrammarSourceFormatter();
                case 12: return new MessagePack.Formatters.SimpleJSON.JSONNodeTypeFormatter();
                case 13: return new MessagePack.Formatters.SimpleJSON.JSONTextModeFormatter();
                case 14: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.AxiomPostModelFormatter();
                case 15: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.TriangularMeshFormatter();
                case 16: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.GeometricModelFormatter();
                case 17: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.SemanticModelFormatter();
                case 18: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.ParseTreeModelFormatter();
                case 19: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.SkyMaterialFormatter();
                case 20: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.SceneEnvironmentFormatter();
                case 21: return new MessagePack.Formatters.Michelangelo.Model.MsgSerialized.RuleExtraInfoFormatter();
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
                { "A", 0},
                { "P", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("A"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("P"),
                
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
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.A);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.P);
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

            var __A__ = default(uint);
            var __P__ = default(uint);

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
                        __A__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __P__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
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
            ____result.A = __A__;
            ____result.P = __P__;
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


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.TriangularMesh value, global::MessagePack.IFormatterResolver formatterResolver)
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

            var ____result = new global::Michelangelo.Model.MsgSerialized.TriangularMesh();
            ____result.Indexed = __Indexed__;
            ____result.Indices = __Indices__;
            ____result.Points = __Points__;
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


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.SemanticModel value, global::MessagePack.IFormatterResolver formatterResolver)
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

            var ____result = new global::Michelangelo.Model.MsgSerialized.SemanticModel();
            ____result.At = __At__;
            ____result.Go = __Go__;
            ____result.On = __On__;
            ____result.Ta = __Ta__;
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
                { "G", 0},
                { "ID", 1},
                { "P", 2},
                { "R", 3},
                { "S", 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("G"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("P"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("R"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("S"),
                
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
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Serialize(ref bytes, offset, value.G, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.ID);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.P, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.R, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SemanticModel[]>().Serialize(ref bytes, offset, value.S, formatterResolver);
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

            var __G__ = default(global::Michelangelo.Model.MsgSerialized.GeometricModel[]);
            var __ID__ = default(uint);
            var __P__ = default(uint[]);
            var __R__ = default(string);
            var __S__ = default(global::Michelangelo.Model.MsgSerialized.SemanticModel[]);

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
                        __G__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __ID__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __P__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __R__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __S__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SemanticModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
            ____result.G = __G__;
            ____result.ID = __ID__;
            ____result.P = __P__;
            ____result.R = __R__;
            ____result.S = __S__;
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


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.SkyMaterial value, global::MessagePack.IFormatterResolver formatterResolver)
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

            var ____result = new global::Michelangelo.Model.MsgSerialized.SkyMaterial();
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


    public sealed class SceneEnvironmentFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.SceneEnvironment>
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
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SkyMaterial>().Serialize(ref bytes, offset, value.Sky, formatterResolver);
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
            var __Sky__ = default(global::Michelangelo.Model.MsgSerialized.SkyMaterial);
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
                        __Sky__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SkyMaterial>().Deserialize(bytes, offset, formatterResolver, out readSize);
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

            var ____result = new global::Michelangelo.Model.MsgSerialized.SceneEnvironment();
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


    public sealed class RuleExtraInfoFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>
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


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo value, global::MessagePack.IFormatterResolver formatterResolver)
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

            var ____result = new global::Michelangelo.Model.MsgSerialized.RuleExtraInfo();
            ____result.CallsCount = __CallsCount__;
            ____result.FulfillsAttributes = __FulfillsAttributes__;
            ____result.FulfillsGoals = __FulfillsGoals__;
            ____result.Local = __Local__;
            ____result.RUID = __RUID__;
            ____result.TypeStr = __TypeStr__;
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
                { "A", 0},
                { "DL", 1},
                { "E", 2},
                { "ENV", 3},
                { "HasAnim", 4},
                { "IMG", 5},
                { "LL", 6},
                { "LR", 7},
                { "ML", 8},
                { "O", 9},
                { "Parsed", 10},
                { "Info", 11},
                { "PS", 12},
                { "PT", 13},
                { "RC", 14},
                { "RS", 15},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("A"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("DL"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("E"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ENV"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("HasAnim"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IMG"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("LL"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("LR"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ML"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("O"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Parsed"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Info"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("PS"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("PT"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RC"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("RS"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Model.MsgSerialized.PostResponseModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteMapHeader(ref bytes, offset, 16);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.HashSet<string>>().Serialize(ref bytes, offset, value.A, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DL, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.E, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SceneEnvironment>().Serialize(ref bytes, offset, value.ENV, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.HasAnim);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.IMG, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.LL, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.LR, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<int, string>>().Serialize(ref bytes, offset, value.ML, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Serialize(ref bytes, offset, value.O, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Parsed, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Info, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>>().Serialize(ref bytes, offset, value.PS, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>>().Serialize(ref bytes, offset, value.PT, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RC);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>>().Serialize(ref bytes, offset, value.RS, formatterResolver);
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

            var __A__ = default(global::System.Collections.Generic.HashSet<string>);
            var __DL__ = default(string);
            var __E__ = default(string);
            var __ENV__ = default(global::Michelangelo.Model.MsgSerialized.SceneEnvironment);
            var __HasAnim__ = default(bool);
            var __IMG__ = default(string);
            var __LL__ = default(uint[]);
            var __LR__ = default(uint[]);
            var __ML__ = default(global::System.Collections.Generic.Dictionary<int, string>);
            var __O__ = default(global::Michelangelo.Model.MsgSerialized.GeometricModel[]);
            var __Parsed__ = default(string);
            var __Info__ = default(string);
            var __PS__ = default(global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>);
            var __PT__ = default(global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>);
            var __RC__ = default(int);
            var __RS__ = default(global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>);

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
                        __A__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.HashSet<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __DL__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __E__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __ENV__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.SceneEnvironment>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __HasAnim__ = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
                        break;
                    case 5:
                        __IMG__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 6:
                        __LL__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 7:
                        __LR__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 8:
                        __ML__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<int, string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 9:
                        __O__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Model.MsgSerialized.GeometricModel[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 10:
                        __Parsed__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 11:
                        __Info__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 12:
                        __PS__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::Michelangelo.Model.MsgSerialized.AxiomPostModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 13:
                        __PT__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<uint, global::Michelangelo.Model.MsgSerialized.ParseTreeModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 14:
                        __RC__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 15:
                        __RS__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.Dictionary<string, global::Michelangelo.Model.MsgSerialized.RuleExtraInfo>>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
            ____result.A = __A__;
            ____result.DL = __DL__;
            ____result.E = __E__;
            ____result.ENV = __ENV__;
            ____result.HasAnim = __HasAnim__;
            ____result.IMG = __IMG__;
            ____result.LL = __LL__;
            ____result.LR = __LR__;
            ____result.ML = __ML__;
            ____result.O = __O__;
            ____result.Parsed = __Parsed__;
            ____result.Info = __Info__;
            ____result.PS = __PS__;
            ____result.PT = __PT__;
            ____result.RC = __RC__;
            ____result.RS = __RS__;
            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
