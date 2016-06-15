using System.Collections.Generic;

namespace Pinata
{
    public interface IDeserializer
    {
        IList<object> DeserializeData(string samplePath);
    }
}