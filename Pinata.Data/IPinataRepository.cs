using System.Collections.Generic;

namespace Pinata.Data
{
    public interface IPinataRepository
    {
        bool InsertData(IList<string> sqlList);
    }
}
