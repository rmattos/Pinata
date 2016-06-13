using System.Collections.Generic;
using Pinata.Common;

namespace Pinata.Command
{
    public interface ICommand
    {
        IList<string> CreateInsert(IList<SampleData> list);

        IList<string> CreateUpdate(IList<SampleData> list);

        IList<string> CreateDelete(IList<SampleData> list);
    }
}