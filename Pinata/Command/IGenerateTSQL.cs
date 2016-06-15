using System.Collections.Generic;
using Pinata.Common;

namespace Pinata.Command
{
    public interface IGenerateTSQL
    {
        void CreateTSQL(SampleData sample, IList<string> sqlList);
    }
}
