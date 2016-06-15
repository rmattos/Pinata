using System.Collections.Generic;
using Pinata.Common;

namespace Pinata.Command
{
    public class TSQLProcessor
    {
        public static void Execute(IGenerateTSQL sqlCommand, SampleData sample, IList<string> sqlList)
        {
            sqlCommand.CreateTSQL(sample, sqlList);
        }
    }
}