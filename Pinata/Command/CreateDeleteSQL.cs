using System;
using System.Collections.Generic;
using System.Linq;
using Jil;
using Pinata.Common;

namespace Pinata.Command
{
    public class CreateDeleteSQL : IGenerateTSQL
    {
        public void CreateTSQL(SampleSQLData sample, IList<object> sqlList)
        {
            string baseSQL = @"DELETE FROM {0} WHERE {1};";
            string dataSQL = string.Empty;

            foreach (var row in sample.Rows)
            {
                string fields = string.Empty;

                foreach (var key in sample.Keys)
                {
                    Common.Schema schema = sample.Schema.SingleOrDefault(s => s.Column == key);

                    string value = JSON.DeserializeDynamic(row.ToString())[schema.Column];

                    string parsedValue = "{0}".FormatWith(ParserDataType.ParseSQL((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value));

                    fields += "{0}={1} AND ".FormatWith(schema.Column, parsedValue);
                }

                dataSQL += baseSQL.FormatWith(sample.Table, fields.Substring(0, fields.LastIndexOf("AND")));
            }

            if (!string.IsNullOrEmpty(dataSQL))
            {
                sqlList.Add(dataSQL);
            }
        }
    }
}