using System;
using System.Collections.Generic;
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Pinata
{
    public class ParserDataType
    {
        public static string ParseSQL(DataType type, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "null";
            }

            string parsedValue = string.Empty;

            switch (type)
            {
                case DataType.Int:
                    {
                        parsedValue = "{0}".FormatWith(int.Parse(value));
                        break;
                    }
                case DataType.Long:
                    {
                        parsedValue = "{0}".FormatWith(long.Parse(value));
                        break;
                    }
                case DataType.Short:
                    {
                        parsedValue = "{0}".FormatWith(short.Parse(value));
                        break;
                    }
                case DataType.Byte:
                    {
                        parsedValue = "{0}".FormatWith(byte.Parse(value));
                        break;
                    }
                case DataType.Bool:
                    {
                        parsedValue = "{0}".FormatWith(bool.Parse(value));
                        break;
                    }
                case DataType.String:
                    {
                        parsedValue = "'{0}'".FormatWith(Convert.ToString(value));
                        break;
                    }
                case DataType.Char:
                    {
                        parsedValue = "'{0}'".FormatWith(Convert.ToString(value));
                        break;
                    }
                case DataType.Guid:
                    {
                        parsedValue = "'{0}'".FormatWith(Guid.Parse(value));
                        break;
                    }
                case DataType.Double:
                    {
                        parsedValue = "'{0}'".FormatWith(CultureInfo.InvariantCulture, double.Parse(value));
                        break;
                    }
                case DataType.Decimal:
                    {
                        parsedValue = "'{0}'".FormatWith(CultureInfo.InvariantCulture, decimal.Parse(value));
                        break;
                    }
                case DataType.Float:
                    {
                        parsedValue = "'{0}'".FormatWith(CultureInfo.InvariantCulture, float.Parse(value));
                        break;
                    }
                case DataType.DateTime:
                    {
                        parsedValue = "'{0}'".FormatWith(DateTime.Parse(value).ToString("yyyy-MM-dd hh:mm:ss"));
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid Data Type");
            }

            return parsedValue;
        }

        public static BsonValue ParseMongo(DataType type, string value)
        {
            BsonValue parsedValue = null;

            switch (type)
            {
                case DataType.Int:
                    {
                        parsedValue = BsonValue.Create(int.Parse(value)).AsInt32;
                        break;
                    }
                case DataType.Long:
                    {
                        parsedValue = BsonValue.Create(long.Parse(value)).AsInt64;
                        break;
                    }
                case DataType.Short:
                    {
                        parsedValue = BsonValue.Create(short.Parse(value));
                        break;
                    }
                case DataType.Byte:
                    {
                        parsedValue = BsonValue.Create(byte.Parse(value));
                        break;
                    }
                case DataType.Bool:
                    {
                        parsedValue = BsonValue.Create(bool.Parse(value)).AsBoolean;
                        break;
                    }
                case DataType.String:
                    {
                        parsedValue = BsonValue.Create(Convert.ToString(value)).AsString;
                        break;
                    }
                case DataType.Char:
                    {
                        parsedValue = BsonValue.Create(Convert.ToChar(value)).AsString;
                        break;
                    }
                case DataType.Guid:
                    {
                        parsedValue = BsonValue.Create(Guid.Parse(value.Replace(new List<string>() { "{", "}" }, ""))).AsGuid;
                        break;
                    }
                case DataType.Double:
                    {
                        parsedValue = BsonValue.Create(double.Parse(value)).AsDouble;
                        break;
                    }
                case DataType.Decimal:
                    {
                        parsedValue = BsonValue.Create(decimal.Parse(value));
                        break;
                    }
                case DataType.Float:
                    {
                        parsedValue = BsonValue.Create(float.Parse(value));
                        break;
                    }
                case DataType.DateTime:
                    {
                        parsedValue = BsonValue.Create(DateTime.Parse(value, CultureInfo.InvariantCulture));
                        break;
                    }
                case DataType.Array:
                    {
                        parsedValue = BsonValue.Create(BsonSerializer.Deserialize<BsonArray>(value)).AsBsonArray;
                        break;
                    }
                case DataType.Document:
                    {
                        parsedValue = BsonValue.Create(BsonDocument.Parse(value)).AsBsonDocument;
                        break;
                    }
                case DataType.ObjectId:
                    {
                        parsedValue = BsonValue.Create(ObjectId.Parse(value.Replace(new List<string>() { "\\", "/", "'" }, ""))).AsObjectId;
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid Data Type");
            }

            return parsedValue;
        }

        public enum DataType
        {
            Int = 1,
            Long,
            Short,
            Byte,
            Bool,
            String,
            Char,
            Guid,
            Double,
            Decimal,
            Float,
            DateTime,
            Array,
            Document,
            ObjectId
        }
    }
}