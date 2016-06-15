using System;
using System.Globalization;
using MongoDB.Bson;

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
                    parsedValue = "{0}".FormatWith(int.Parse(value));
                    break;
                case DataType.Long:
                    parsedValue = "{0}".FormatWith(long.Parse(value));
                    break;
                case DataType.Short:
                    parsedValue = "{0}".FormatWith(short.Parse(value));
                    break;
                case DataType.Byte:
                    parsedValue = "{0}".FormatWith(byte.Parse(value));
                    break;
                case DataType.Bool:
                    parsedValue = "{0}".FormatWith(bool.Parse(value));
                    break;
                case DataType.String:
                    parsedValue = "'{0}'".FormatWith(Convert.ToString(value));
                    break;
                case DataType.Char:
                    parsedValue = "'{0}'".FormatWith(Convert.ToString(value));
                    break;
                case DataType.Guid:
                    parsedValue = "'{0}'".FormatWith(Guid.Parse(value));
                    break;
                case DataType.Double:
                    parsedValue = "'{0}'".FormatWith(CultureInfo.InvariantCulture, double.Parse(value));
                    break;
                case DataType.Decimal:
                    parsedValue = "'{0}'".FormatWith(CultureInfo.InvariantCulture, decimal.Parse(value));
                    break;
                case DataType.Float:
                    parsedValue = "'{0}'".FormatWith(CultureInfo.InvariantCulture, float.Parse(value));
                    break;
                case DataType.DateTime:
                    parsedValue = "'{0}'".FormatWith(DateTime.Parse(value).ToString("yyyy-MM-dd hh:mm:ss"));
                    break;
                default:
                    throw new ArgumentException("Invalid Data Type");
            }

            return parsedValue;
        }

        public static BsonValue ParseMongo(DataType type, string value)
        {
            BsonValue parsedValue = string.Empty;

            switch (type)
            {
                case DataType.Int:
                    {
                        parsedValue = BsonValue.Create(value).AsInt32;
                        break;
                    }
                case DataType.Long:
                    {
                        parsedValue = BsonValue.Create(value).AsInt64;
                        break;
                    }
                case DataType.Short:
                case DataType.Byte:
                    {
                        parsedValue = BsonValue.Create(value);
                        break;
                    }
                case DataType.Bool:
                    {
                        parsedValue = BsonValue.Create(value).AsBoolean;
                        break;
                    }
                case DataType.String:
                case DataType.Char:
                    {
                        parsedValue = BsonValue.Create(value).AsString;
                        break;
                    }
                case DataType.Guid:
                    {
                        parsedValue = BsonValue.Create(value).AsGuid;
                        break;
                    }
                case DataType.Double:
                    {
                        parsedValue = BsonValue.Create(value).AsDouble;
                        break;
                    }
                case DataType.Decimal:
                case DataType.Float:
                    {
                        parsedValue = BsonValue.Create(value);
                        break;
                    }
                case DataType.DateTime:
                    {
                        parsedValue = BsonValue.Create(value).ToUniversalTime();
                        break;
                    }
                case DataType.Array:
                    {
                        parsedValue = BsonValue.Create(value).AsBsonArray;
                        break;
                    }
                case DataType.Document:
                    {
                        parsedValue = BsonValue.Create(value).AsBsonDocument;
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
            Document
        }
    }
}