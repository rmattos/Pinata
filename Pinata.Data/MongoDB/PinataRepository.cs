using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool Insert(IList<object> docList)
        {
            try
            {
                foreach (var item in docList)
                {
                    var data = ((IDictionary<string, IList<BsonDocument>>)item).First();

                    base.CollectionName = data.Key;
                    base.CreateBatch(data.Value);
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

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
