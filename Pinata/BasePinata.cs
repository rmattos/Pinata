using System.Collections.Generic;
using Pinata.Command;
using Pinata.Common;
using Pinata.Data;

namespace Pinata
{
    public abstract class BasePinata
    {
        protected List<SampleData> sampleData = new List<SampleData>();

        protected IPinataRepository Repository { get; set; }
        protected string[] SamplePath { get; set; }
        protected Options OptionType { get; set; }

        public BasePinata(string connectionString, Provider.Type provider)
        {
            Repository = RepositoryFactory.Create(connectionString, provider);
        }

        public BasePinata(string connectionString, Provider.Type provider, params string[] samplePath)
        {
            SamplePath = samplePath;
            Repository = RepositoryFactory.Create(connectionString, provider);
        }

        protected void SetDataFiles(params string[] samplePath)
        {
            SamplePath = samplePath;
        }

        public abstract void Feed(Options option = Options.None);

        public abstract void Feed(Options option = Options.None, params string[] samplePath);

        public abstract bool Execute(CommandType commandType);
    }
}