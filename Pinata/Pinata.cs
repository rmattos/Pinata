using Pinata.Command;
using Pinata.Data;
using System.Collections.Generic;

namespace Pinata
{
    public class Pinata : BasePinata
    {
        public Pinata(string connectionString, Provider.Type provider) :
            base(connectionString, provider)
        {
        }

        public Pinata(string connectionString, Provider.Type provider, params string[] samplePath) :
            base(connectionString, provider, samplePath)
        {
        }

        public override bool Execute(CommandType commandType)
        {
            bool commandResponse = false;

            switch (commandType)
            {
                case CommandType.Insert:
                    {
                        commandResponse = Repository.Insert(Command.CreateInsert(base.SampleData));
                        break;
                    }
                case CommandType.Delete:
                    {
                        commandResponse = Repository.Delete(Command.CreateDelete(base.SampleData));
                        break;
                    }
            }

            return commandResponse;
        }

        public override void Feed()
        {
            Feed(null);
        }

        public override void Feed(params string[] samplePath)
        {
            base.SampleData = new List<object>();

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
                            base.SampleData.AddRange(DeserializerProcessor.Execute(new DeserializerSQL(), sample));
                            break;
                        }
                    case Data.Provider.Type.MongoDB:
                        {
                            base.SampleData.AddRange(DeserializerProcessor.Execute(new DeserializerMongo(), sample));
                            break;
                        }
                }
            }
        }

        public override bool Execute(CommandType commandType, IDictionary<string, string> parameters)
        {
            SetDynamicParameters(parameters);

            bool commandResponse = false;

            switch (commandType)
            {
                case CommandType.Insert:
                    {
                        commandResponse = Repository.Insert(Command.CreateInsert(base.SampleData, DynamicParameters));

                        break;
                    }
                case CommandType.Delete:
                    {
                        commandResponse = Repository.Delete(Command.CreateDelete(base.SampleData, DynamicParameters));
                        break;
                    }
            }

            return commandResponse;
        }
    }
}