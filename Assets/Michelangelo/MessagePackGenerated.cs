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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(16)
            {
                {typeof(global::System.Collections.Generic.IReadOnlyList<float>), 0 },
                {typeof(global::System.Collections.Generic.IReadOnlyList<double>), 1 },
                {typeof(global::System.Collections.Generic.IReadOnlyDictionary<string, double>), 2 },
                {typeof(global::System.Collections.Generic.IReadOnlyDictionary<string, double[]>), 3 },
                {typeof(global::System.Collections.Generic.IReadOnlyList<string[]>), 4 },
                {typeof(global::System.Collections.Generic.IReadOnlyList<global::Michelangelo.Models.MichelangeloApi.GeometricModel>), 5 },
                {typeof(global::System.Collections.Generic.IReadOnlyDictionary<int, global::Michelangelo.Models.MichelangeloApi.MaterialModel>), 6 },
                {typeof(global::System.Collections.Generic.IReadOnlyDictionary<uint, global::Michelangelo.Models.MichelangeloApi.ParseTreeModel>), 7 },
                {typeof(global::System.Collections.Generic.IReadOnlyDictionary<string, global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo>), 8 },
                {typeof(global::Michelangelo.Models.Handlers.SourceType), 9 },
                {typeof(global::Michelangelo.Models.MichelangeloApi.TriangularMesh), 10 },
                {typeof(global::Michelangelo.Models.MichelangeloApi.GeometricModel), 11 },
                {typeof(global::Michelangelo.Models.MichelangeloApi.MaterialModel), 12 },
                {typeof(global::Michelangelo.Models.MichelangeloApi.ParseTreeModel), 13 },
                {typeof(global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo), 14 },
                {typeof(global::Michelangelo.Models.MichelangeloApi.PostResponseModel), 15 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.InterfaceReadOnlyListFormatter<float>();
                case 1: return new global::MessagePack.Formatters.InterfaceReadOnlyListFormatter<double>();
                case 2: return new global::MessagePack.Formatters.InterfaceReadOnlyDictionaryFormatter<string, double>();
                case 3: return new global::MessagePack.Formatters.InterfaceReadOnlyDictionaryFormatter<string, double[]>();
                case 4: return new global::MessagePack.Formatters.InterfaceReadOnlyListFormatter<string[]>();
                case 5: return new global::MessagePack.Formatters.InterfaceReadOnlyListFormatter<global::Michelangelo.Models.MichelangeloApi.GeometricModel>();
                case 6: return new global::MessagePack.Formatters.InterfaceReadOnlyDictionaryFormatter<int, global::Michelangelo.Models.MichelangeloApi.MaterialModel>();
                case 7: return new global::MessagePack.Formatters.InterfaceReadOnlyDictionaryFormatter<uint, global::Michelangelo.Models.MichelangeloApi.ParseTreeModel>();
                case 8: return new global::MessagePack.Formatters.InterfaceReadOnlyDictionaryFormatter<string, global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo>();
                case 9: return new MessagePack.Formatters.Michelangelo.Models.Handlers.SourceTypeFormatter();
                case 10: return new MessagePack.Formatters.Michelangelo.Models.MichelangeloApi.TriangularMeshFormatter();
                case 11: return new MessagePack.Formatters.Michelangelo.Models.MichelangeloApi.GeometricModelFormatter();
                case 12: return new MessagePack.Formatters.Michelangelo.Models.MichelangeloApi.MaterialModelFormatter();
                case 13: return new MessagePack.Formatters.Michelangelo.Models.MichelangeloApi.ParseTreeModelFormatter();
                case 14: return new MessagePack.Formatters.Michelangelo.Models.MichelangeloApi.RuleExtraInfoFormatter();
                case 15: return new MessagePack.Formatters.Michelangelo.Models.MichelangeloApi.PostResponseModelFormatter();
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

namespace MessagePack.Formatters.Michelangelo.Models.Handlers
{
    using System;
    using MessagePack;

    public sealed class SourceTypeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Models.Handlers.SourceType>
    {
        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Models.Handlers.SourceType value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, (Int32)value);
        }
        
        public global::Michelangelo.Models.Handlers.SourceType Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::Michelangelo.Models.Handlers.SourceType)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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

namespace MessagePack.Formatters.Michelangelo.Models.MichelangeloApi
{
    using System;
    using MessagePack;


    public sealed class TriangularMeshFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Models.MichelangeloApi.TriangularMesh>
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


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Models.MichelangeloApi.TriangularMesh value, global::MessagePack.IFormatterResolver formatterResolver)
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

        public global::Michelangelo.Models.MichelangeloApi.TriangularMesh Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
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

            var ____result = new global::Michelangelo.Models.MichelangeloApi.TriangularMesh();
            ____result.Indexed = __Indexed__;
            ____result.Indices = __Indices__;
            ____result.Points = __Points__;
            return ____result;
        }
    }


    public sealed class GeometricModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Models.MichelangeloApi.GeometricModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public GeometricModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "MaterialID", 0},
                { "Mesh", 1},
                { "Primitive", 2},
                { "Transform", 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("MaterialID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Mesh"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Primitive"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Transform"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Models.MichelangeloApi.GeometricModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaterialID);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::Michelangelo.Models.MichelangeloApi.TriangularMesh>().Serialize(ref bytes, offset, value.Mesh, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Primitive, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<float>>().Serialize(ref bytes, offset, value.Transform, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Models.MichelangeloApi.GeometricModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __MaterialID__ = default(int);
            var __Mesh__ = default(global::Michelangelo.Models.MichelangeloApi.TriangularMesh);
            var __Primitive__ = default(string);
            var __Transform__ = default(global::System.Collections.Generic.IReadOnlyList<float>);

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
                        __MaterialID__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Mesh__ = formatterResolver.GetFormatterWithVerify<global::Michelangelo.Models.MichelangeloApi.TriangularMesh>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Primitive__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Transform__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<float>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Models.MichelangeloApi.GeometricModel(__Primitive__, __MaterialID__, __Transform__, __Mesh__);
            return ____result;
        }
    }


    public sealed class MaterialModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Models.MichelangeloApi.MaterialModel>
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


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Models.MichelangeloApi.MaterialModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<double>>().Serialize(ref bytes, offset, value.Albedo, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<string, double>>().Serialize(ref bytes, offset, value.Scalars, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<string, double[]>>().Serialize(ref bytes, offset, value.Vectors, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Models.MichelangeloApi.MaterialModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Albedo__ = default(global::System.Collections.Generic.IReadOnlyList<double>);
            var __Scalars__ = default(global::System.Collections.Generic.IReadOnlyDictionary<string, double>);
            var __Vectors__ = default(global::System.Collections.Generic.IReadOnlyDictionary<string, double[]>);

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
                        __Albedo__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<double>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Scalars__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<string, double>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Vectors__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<string, double[]>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Models.MichelangeloApi.MaterialModel(__Albedo__, __Scalars__, __Vectors__);
            return ____result;
        }
    }


    public sealed class ParseTreeModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Models.MichelangeloApi.ParseTreeModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public ParseTreeModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "ChildIndices", 0},
                { "ID", 1},
                { "Ontology", 2},
                { "Rule", 3},
                { "Shape", 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ChildIndices"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ID"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Ontology"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Rule"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Shape"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Models.MichelangeloApi.ParseTreeModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.ChildIndices, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.ID);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<string[]>>().Serialize(ref bytes, offset, value.Ontology, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Rule, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<global::Michelangelo.Models.MichelangeloApi.GeometricModel>>().Serialize(ref bytes, offset, value.Shape, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Models.MichelangeloApi.ParseTreeModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __ChildIndices__ = default(uint[]);
            var __ID__ = default(uint);
            var __Ontology__ = default(global::System.Collections.Generic.IReadOnlyList<string[]>);
            var __Rule__ = default(string);
            var __Shape__ = default(global::System.Collections.Generic.IReadOnlyList<global::Michelangelo.Models.MichelangeloApi.GeometricModel>);

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
                        __ChildIndices__ = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __ID__ = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __Ontology__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<string[]>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Rule__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __Shape__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyList<global::Michelangelo.Models.MichelangeloApi.GeometricModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Models.MichelangeloApi.ParseTreeModel(__ID__, __Rule__, __ChildIndices__, __Ontology__, __Shape__);
            return ____result;
        }
    }


    public sealed class RuleExtraInfoFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo>
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


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo value, global::MessagePack.IFormatterResolver formatterResolver)
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

        public global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
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

            var ____result = new global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo();
            ____result.CallsCount = __CallsCount__;
            ____result.FulfillsAttributes = __FulfillsAttributes__;
            ____result.FulfillsGoals = __FulfillsGoals__;
            ____result.Local = __Local__;
            ____result.RUID = __RUID__;
            ____result.TypeStr = __TypeStr__;
            return ____result;
        }
    }


    public sealed class PostResponseModelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Michelangelo.Models.MichelangeloApi.PostResponseModel>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public PostResponseModelFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "Errors", 0},
                { "IMG", 1},
                { "Info", 2},
                { "Materials", 3},
                { "ParseTree", 4},
                { "Rules", 5},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Errors"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("IMG"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Info"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Materials"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("ParseTree"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("Rules"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Michelangelo.Models.MichelangeloApi.PostResponseModel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Errors, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.IMG, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Info, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<int, global::Michelangelo.Models.MichelangeloApi.MaterialModel>>().Serialize(ref bytes, offset, value.Materials, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<uint, global::Michelangelo.Models.MichelangeloApi.ParseTreeModel>>().Serialize(ref bytes, offset, value.ParseTree, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<string, global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo>>().Serialize(ref bytes, offset, value.Rules, formatterResolver);
            return offset - startOffset;
        }

        public global::Michelangelo.Models.MichelangeloApi.PostResponseModel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Errors__ = default(string);
            var __IMG__ = default(string);
            var __Info__ = default(string);
            var __Materials__ = default(global::System.Collections.Generic.IReadOnlyDictionary<int, global::Michelangelo.Models.MichelangeloApi.MaterialModel>);
            var __ParseTree__ = default(global::System.Collections.Generic.IReadOnlyDictionary<uint, global::Michelangelo.Models.MichelangeloApi.ParseTreeModel>);
            var __Rules__ = default(global::System.Collections.Generic.IReadOnlyDictionary<string, global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo>);

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
                        __Errors__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __IMG__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Info__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Materials__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<int, global::Michelangelo.Models.MichelangeloApi.MaterialModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __ParseTree__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<uint, global::Michelangelo.Models.MichelangeloApi.ParseTreeModel>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 5:
                        __Rules__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.IReadOnlyDictionary<string, global::Michelangelo.Models.MichelangeloApi.RuleExtraInfo>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Michelangelo.Models.MichelangeloApi.PostResponseModel(__Rules__, __ParseTree__, __Materials__, __Info__, __IMG__, __Errors__);
            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
