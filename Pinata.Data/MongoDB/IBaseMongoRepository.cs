using System;
using System.Linq;
using MongoDB.Driver;

namespace Pinata.Data.MongoDB
{
    public interface IBaseMongoRepository<T> where T : class
    {
        T GetById(Guid id);

        bool Create(T document);

        bool Delete(Guid id);

        bool Delete(IMongoQuery query);

        bool Update(IMongoQuery query, IMongoUpdate update);

        bool Update(Guid id, IMongoUpdate update, UpdateFlags updateFlag = UpdateFlags.None);

        bool DropCollection(string collectionName);

        IQueryable<T> AsQueryable();

        IQueryable<T1> AsQueryable<T1>();

        IQueryable<T> AggregateToOutput(string aggregationPipeline, string outputCollection, dynamic parameters = null);

        IQueryable<T1> AggregateToOutput<T1>(string aggregationPipeline, string outputCollection, dynamic parameters = null);

        IQueryable<T> Aggregate(string aggregationPipeline, dynamic parameters = null);

        IQueryable<T1> Aggregate<T1>(string aggregationPipeline, dynamic parameters = null);

        IQueryable<T> Find(string mongoQuery, dynamic parameters = null, string collectionName = "");

        IQueryable<T1> Find<T1>(string mongoQuery, dynamic parameters = null, string collectionName = "");

        IQueryable<T1> Find<T1>(string mongoQuery, IMongoFields fields, dynamic parameters = null, string collectionName = "");

        IMongoQuery CreateQuery(string mongoQuery, dynamic parameters = null);
    }
}
