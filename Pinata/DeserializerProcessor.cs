using System.Collections.Generic;

namespace Pinata
{
    public class DeserializerProcessor
    {
        public static IList<object> Execute(IDeserializer deserializer, string samplePath)
        {
            return deserializer.DeserializeData(samplePath);
        }
    }
}
