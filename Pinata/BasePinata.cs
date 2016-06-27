using System.Collections.Generic;
using Pinata.Command;
using Pinata.Data;

namespace Pinata
{
    public abstract class BasePinata
    {
        protected List<object> SampleData { get; set; }
        protected string[] SamplePath { get; set; }

        protected ICommand Command { get; set; }
        protected IPinataRepository Repository { get; set; }

        protected Options OptionType { get; set; }
        protected Provider.Type Provider { get; set; }

        public BasePinata(string connectionString, Provider.Type provider)
        {
            Provider = provider;
            Command = CommandFactory.Create(provider);
            Repository = RepositoryFactory.Create(connectionString, provider);
        }

        public BasePinata(string connectionString, Provider.Type provider, params string[] samplePath)
        {
            Provider = provider;
            SamplePath = samplePath;
            Command = CommandFactory.Create(provider);
            Repository = RepositoryFactory.Create(connectionString, provider);
        }

        protected void SetDataFiles(params string[] samplePath)
        {
            SamplePath = samplePath;
        }

        public abstract void Feed();

        public abstract void Feed(params string[] samplePath);

        public abstract bool Execute(CommandType commandType);
    }
}