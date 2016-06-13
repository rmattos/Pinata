using System;
using Pinata.Data;

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
                case Provider.Type.SQLServer:
                    {
                        command = new CommandSQL();
                        break;
                    }
                case Provider.Type.MongoDB:
                    {
                        break;
                    }
                default:
                    throw new NullReferenceException("Invalid Provider");
            }

            return command;
        }
    }
}
