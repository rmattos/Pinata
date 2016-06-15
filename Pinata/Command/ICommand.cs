using System.Collections.Generic;

namespace Pinata.Command
{
    public interface ICommand
    {
        IList<object> CreateInsert(IList<object> list);

        IList<object> CreateUpdate(IList<object> list);

        IList<object> CreateDelete(IList<object> list);
    }
}