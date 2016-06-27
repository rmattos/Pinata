﻿using Pinata.Data;

namespace Pinata.Command
{
    public class CommandFactory
    {
        public static ICommand Create(Provider.Type provider)
        {
            ICommand command = null;

            switch (provider)
            {
                case Provider.Type.MySQL:
                    {
                        command = new CommandSQL();
                        break;
                    }
                case Provider.Type.MongoDB:
                    {
                        command = new CommandMongo();
                        break;
                    }
            }

            return command;
        }
    }
}
