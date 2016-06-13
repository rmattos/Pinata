using System.Collections.Generic;
using Pinata.Command;
using Pinata.Common;
using Pinata.Data;

namespace Pinata
{
    public abstract class BasePinata
    {
        protected IPinataRepository Repository { get; set; }
        protected string[] SamplePath { get; set; }
        protected List<SampleData> sampleData = new List<SampleData>();
        protected Options OptionType { get; set; }

        public BasePinata(string connectionString, Provider.Type provider, params string[] samplePath)
        {
            SamplePath = samplePath;
            Repository = RepositoryFactory.Create(connectionString, provider);
        }

        public abstract void Feed(Options option = Options.None);

        public abstract bool Execute(CommandType commandType);
    }
}