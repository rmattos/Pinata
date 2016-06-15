using System.Collections.Generic;
using Pinata.Common;

namespace Pinata.Command
{
    public interface IGenerateTSQL
    {
        void CreateTSQL(SampleSQLData sample, IList<object> sqlList);
    }
}
