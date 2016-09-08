using Pinata.Common;
using System.Collections.Generic;

namespace Pinata.Command
{
    public interface IGenerateTSQL
    {
        void CreateTSQL(SampleSQLData sample, IList<object> sqlList);

        void CreateTSQL(SampleSQLData sample, IList<object> sqlList, IDictionary<string, string> dynamicParameters);
    }
}
