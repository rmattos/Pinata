using System.Collections.Generic;
using System.IO;
using Jil;
using Pinata.Common;

namespace Pinata
{
    public class DeserializerMongo : IDeserializer
    {
        public IList<object> DeserializeData(string samplePath)
        {
            List<object> sampleDataList = new List<object>();

            string sampleRaw = File.ReadAllText(Path.GetFullPath(samplePath));

            sampleDataList.AddRange(JSON.Deserialize<IList<SampleMongoData>>(sampleRaw));

            return sampleDataList;
        }
    }
}
