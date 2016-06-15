using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pinata.Common;

namespace Pinata.Command
{
    public class CommandSQL : ICommand
    {
        private IDictionary<string, bool> _tablesLoaded = new Dictionary<string, bool>();

        public IList<string> CreateDelete(IList<SampleData> list)
        {
            IList<string> deleteList = new List<string>();

            IList<SampleData> childData = list.Where(l => l.FK_References.Count > 0).ToList();
            IList<SampleData> parentData = list.Where(l => l.FK_References.Count == 0).ToList();

            Parallel.ForEach(childData, sample =>
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), sample, deleteList);
            });

            Parallel.ForEach(parentData, sample =>
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), sample, deleteList);
            });

            return deleteList;
        }

        public IList<string> CreateInsert(IList<SampleData> list)
        {
            IList<string> insertList = new List<string>();

            foreach (SampleData sample in list)
            {
                foreach (var fk in sample.FK_References)
                {
                    SampleData sampleDataFiltered = list.Where(l => !_tablesLoaded.ContainsKey(fk.Table) && l.Table == fk.Table).SingleOrDefault();

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

        public IList<string> CreateUpdate(IList<SampleData> list)
        {
            throw new NotImplementedException();
        }
    }
}
