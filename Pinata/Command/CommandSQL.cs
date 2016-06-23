using System;
using System.Collections.Generic;
using System.Linq;
using Pinata.Common;

namespace Pinata.Command
{
    public class CommandSQL : ICommand
    {
        private IDictionary<string, bool> _tablesLoaded = new Dictionary<string, bool>();

        public IList<object> CreateDelete(IList<object> list)
        {
            IList<SampleSQLData> convertedList = list.Cast<SampleSQLData>().ToList();
            IList<object> deleteList = new List<object>();

            IList<SampleSQLData> childData = convertedList.Where(l => l.FK_References.Count > 0).ToList();
            IList<SampleSQLData> parentData = convertedList.Where(l => l.FK_References.Count == 0).ToList();

            foreach (var child in childData)
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), child, deleteList);
            }

            foreach (var parent in parentData)
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), parent, deleteList);
            }

            return deleteList;
        }

        public IList<object> CreateInsert(IList<object> list)
        {
            IList<SampleSQLData> convertedList = list.Cast<SampleSQLData>().ToList();
            IList<object> insertList = new List<object>();

            foreach (SampleSQLData sample in convertedList)
            {
                foreach (var fk in sample.FK_References)
                {
                    SampleSQLData sampleDataFiltered = convertedList.Where(l => !_tablesLoaded.ContainsKey(fk.Table) && l.Table == fk.Table).SingleOrDefault();

                    if (sampleDataFiltered != null)
                    {
                        TSQLProcessor.Execute(new CreateInsertSQL(), sampleDataFiltered, insertList);

                        _tablesLoaded.Add(fk.Table, true);
                    }
                }

                if (!_tablesLoaded.ContainsKey(sample.Table))
                {
                    TSQLProcessor.Execute(new CreateInsertSQL(), sample, insertList);

                    _tablesLoaded.Add(sample.Table, true);
                }
            }

            return insertList;
        }

        public IList<object> CreateUpdate(IList<object> list)
        {
            throw new NotImplementedException();
        }
    }
}
