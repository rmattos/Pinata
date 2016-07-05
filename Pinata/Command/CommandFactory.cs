using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using Pinata.Data;

namespace Pinata.Command
{
    public class CommandFactory
    {
        #region [ PRIVATE ]

        private static object _locker = new object();

        private static void MongoCustomSerializer()
        {
            lock (_locker)
            {
                try
                {
                    BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeSerializationOptions.LocalInstance));
                }
                catch (Exception ex)
                {
                    if (ex.HResult != -2146233088)
                    {
                        throw ex;
                    }
                }
            }
        }

        #endregion

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
                        MongoCustomSerializer();
                        command = new CommandMongo();
                        break;
                    }
            }

            return command;
        }
    }
}
