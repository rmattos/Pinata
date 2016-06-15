using System.Collections.Generic;

namespace Pinata.Data
{
    public interface IPinataRepository
    {
        bool ExecuteCommand(IList<string> sqlList);
    }
}
