using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Pinata.Data.MongoDB
{
    public class PinataRepository : BaseMongoRepository<BsonDocument>, IPinataRepository
    {
        #region [ CONSTRUCTORS ]

        public PinataRepository(MongoUrl mongoUrl)
            : base(mongoUrl)
        {
        }

        #endregion

        public bool Insert(IList<object> list)
        {
            try
            {
                foreach (var item in list)
                {
                    var data = ((IDictionary<string, IList<BsonDocument>>)item).First();

                    CollectionName = data.Key;
                    CreateBatch(data.Value);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IList<object> list)
        {
            try
            {
                foreach (var element in list)
                {
                    var data = ((IDictionary<string, IList<BsonElement>>)element).First();

                    CollectionName = data.Key;

                    Parallel.ForEach(data.Value, item =>
                    {
                        IMongoQuery query = CreateQuery("{'" + item.Name + "': @value }", new { value = item.Value });

                        Delete(query);
                    });
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}