using System;
using System.Collections.Generic;
using Jil;
using Pinata.Common;

namespace Pinata.Command
{
    public class CommandSQL : ICommand
    {
        public IList<string> CreateDelete(IList<SampleData> list)
        {
            throw new NotImplementedException();
        }

        public IList<string> CreateInsert(IList<SampleData> list)
        {
            IList<string> insertList = new List<string>();

            foreach (SampleData sample in list)
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

                        values += "'{0}',".FormatWith(value);
                    }

                    dataSQL += baseSQL.FormatWith(sample.Table, fields.Substring(0, fields.LastIndexOf(',')), values.Substring(0, values.LastIndexOf(',')));
                }

                insertList.Add(dataSQL);
            }

            return insertList;
        }

        public IList<string> CreateUpdate(IList<SampleData> list)
        {
            throw new NotImplementedException();
        }
    }
}
