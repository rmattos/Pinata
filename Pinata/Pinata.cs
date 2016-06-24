using System.Collections.Generic;
using Pinata.Command;
using Pinata.Data;

namespace Pinata
{
    public class Pinata : BasePinata
    {
        private ICommand _command = null;

        public Pinata(string connectionString, Provider.Type provider) :
            base(connectionString, provider)
        {
            _command = CommandFactory.Create(provider);
        }

        public Pinata(string connectionString, Provider.Type provider, params string[] samplePath) :
            base(connectionString, provider, samplePath)
        {
            _command = CommandFactory.Create(provider);
        }

        public override bool Execute(CommandType commandType)
        {
            bool commandResposne = false;

            switch (commandType)
            {
                case CommandType.Insert:
                    {
                        commandResposne = Repository.Insert(_command.CreateInsert(base.sampleData));
                        break;
                    }
                case CommandType.Update:
                    break;
                case CommandType.Delete:
                    {
                        commandResposne = Repository.Delete(_command.CreateDelete(base.sampleData));
                        break;
                    }
            }

            return commandResposne;
        }

        public override void Feed(Options option = Options.None)
        {
            Feed(option, null);
        }

        public override void Feed(Options option = Options.None, params string[] samplePath)
        {
            OptionType = option;
            base.sampleData = new List<object>();

            if (samplePath != null)
            {
                SetDataFiles(samplePath);
            }

            foreach (var sample in SamplePath)
            {
                switch (Provider)
                {
                    case Data.Provider.Type.MySQL:
                        {
                            base.sampleData.AddRange(DeserializerProcessor.Execute(new DeserializerSQL(), sample));
                            break;
                        }
                    case Data.Provider.Type.SQLServer:
                        break;
                    case Data.Provider.Type.MongoDB:
                        {
                            base.sampleData.AddRange(DeserializerProcessor.Execute(new DeserializerMongo(), sample));
                            break;
                        }
                }
            }
        }
    }
}