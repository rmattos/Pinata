using System.Collections.Generic;

namespace Pinata.Command
{
    public interface ICommand
    {
        IList<object> CreateInsert(IList<object> list);

        IList<object> CreateUpdate(IList<object> list);

        IList<object> CreateDelete(IList<object> list);

        IList<object> CreateInsert(IList<object> list, IDictionary<string, string> dynamicParameters);

        IList<object> CreateUpdate(IList<object> list, IDictionary<string, string> dynamicParameters);

        IList<object> CreateDelete(IList<object> list, IDictionary<string, string> dynamicParameters);
    }
}