using System.Collections.Generic;
using System.IO;
using Jil;
using Pinata.Core;

namespace Pinata
{
    public class Pinata : BasePinata
    {
        public Pinata(string connectionString, string provider, params string[] samplePath) :
            base(connectionString, provider, samplePath)
        {
        }

        public override IList<SampleData> Load()
        {
            List<SampleData> sampleDataList = new List<SampleData>();

            foreach (var sample in SamplePath)
            {
                string sampleRaw = File.ReadAllText(Path.GetFullPath(sample));

                sampleDataList.AddRange(JSON.Deserialize<IList<SampleData>>(sampleRaw));
            }

            return sampleDataList;
        }
    }
}
