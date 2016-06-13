using System;

namespace Pinata.Data
{
    public class RepositoryFactory
    {
        public static IPinataRepository Create(string connectionString, Provider.Type type)
        {
            IPinataRepository repository = null;

            switch (type)
            {
                case Provider.Type.MySQL:
                    {
                        repository = new MySQL.PinataRepository(connectionString, Provider.MySQL);
                        break;
                    }
                case Provider.Type.SQLServer:
                    {
                        break;
                    }
                case Provider.Type.MongoDB:
                    {
                        break;
                    }
                default:
                    throw new ArgumentException("Invalid Provider");
            }

            return repository;
        }
    }
}