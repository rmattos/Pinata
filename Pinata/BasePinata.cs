using System.Collections.Generic;
using Pinata.Command;
using Pinata.Data;

namespace Pinata
{
    public abstract class BasePinata
    {
        protected List<object> sampleData = new List<object>();

        protected IPinataRepository Repository { get; set; }
        protected string[] SamplePath { get; set; }
        protected Options OptionType { get; set; }
        protected Provider.Type Provider { get; set; }

        public BasePinata(string connectionString, Provider.Type provider)
        {
            Provider = provider;
            Repository = RepositoryFactory.Create(connectionString, provider);
        }

        public BasePinata(string connectionString, Provider.Type provider, params string[] samplePath)
        {
            Provider = provider;
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