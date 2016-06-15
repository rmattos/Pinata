using MongoDB.Driver;

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
                    break;
                case Provider.Type.MongoDB:
                    {
                        repository = new MongoDB.PinataRepository(new MongoUrl(connectionString));
                        break;
                    }
            }

            return repository;
        }
    }
}