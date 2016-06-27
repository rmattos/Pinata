﻿using System.Collections.Generic;
using Pinata.Command;
using Pinata.Data;

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
            bool commandResposne = false;

            switch (commandType)
            {
                case CommandType.Insert:
                    {
                        commandResposne = Repository.Insert(Command.CreateInsert(base.SampleData));
                        break;
                    }
                case CommandType.Delete:
                    {
                        commandResposne = Repository.Delete(Command.CreateDelete(base.SampleData));
                        break;
                    }
            }

            return commandResposne;
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
    }
}