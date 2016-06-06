using System.Collections.Generic;
using Pinata.Core;

namespace Pinata
{
    public abstract class BasePinata
    {
        protected string[] SamplePath { get; set; }

        public BasePinata(string connectionString, string provider, params string[] samplePath)
        {
            SamplePath = samplePath;
        }

        public abstract IList<SampleData> Load();
    }
}
