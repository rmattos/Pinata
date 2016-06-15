using System.Collections.Generic;
using Pinata.Common;

namespace Pinata.Command
{
    public class TSQLProcessor
    {
        public static void Execute(IGenerateTSQL sqlCommand, SampleSQLData sample, IList<object> sqlList)
        {
            sqlCommand.CreateTSQL(sample, sqlList);
        }
    }
}