using System;
using System.Globalization;

namespace Pinata
{
    public class ParserDataType
    {
        public static string Parse(DataType type, string value)
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
            DateTime
        }
    }
}
