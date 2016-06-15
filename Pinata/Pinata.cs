using System.Collections.Generic;
using System.IO;
using Jil;
using Pinata.Command;
using Pinata.Common;
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
                        commandResposne = Repository.ExecuteCommand(_command.CreateInsert(base.sampleData));
                        break;
                    }
                case CommandType.Update:
                    break;
                case CommandType.Delete:
                    {
                        commandResposne = Repository.ExecuteCommand(_command.CreateDelete(base.sampleData));
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

            List<SampleData> sampleDataList = new List<SampleData>();

            if (samplePath != null)
            {
                SetDataFiles(samplePath);
            }

            foreach (var sample in SamplePath)
            {
                string sampleRaw = File.ReadAllText(Path.GetFullPath(sample));

                sampleDataList.AddRange(JSON.Deserialize<IList<SampleData>>(sampleRaw));
            }

            base.sampleData = sampleDataList;
        }
    }
}