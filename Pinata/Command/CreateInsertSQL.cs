using System;
using System.Collections.Generic;
using Jil;
using Pinata.Common;

namespace Pinata.Command
{
    public class CreateInsertSQL : IGenerateTSQL
    {
        public void CreateTSQL(SampleData sample, IList<string> sqlList)
        {
            string baseSQL = @"INSERT INTO {0} ({1}) VALUES ({2});";
            string dataSQL = string.Empty;

            foreach (var row in sample.Rows)
            {
                string fields = string.Empty;
                string values = string.Empty;

                foreach (var schema in sample.Schema)
                {
                    fields += "{0},".FormatWith(schema.Column);

                    string value = JSON.DeserializeDynamic(row.ToString())[schema.Column];

                    values += "{0},".FormatWith(ParserDataType.Parse((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value));
                }

                dataSQL += baseSQL.FormatWith(sample.Table, fields.Substring(0, fields.LastIndexOf(',')), values.Substring(0, values.LastIndexOf(',')));
            }

            sqlList.Add(dataSQL);
        }
    }
}
