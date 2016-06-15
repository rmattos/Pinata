using System.Collections.Generic;

namespace Pinata.Data
{
    public interface IPinataRepository
    {
        bool Insert(IList<object> list);

        bool Update(IList<object> list);

        bool Delete(IList<object> list);
    }
}
