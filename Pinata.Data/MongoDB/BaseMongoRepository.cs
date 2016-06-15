using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Pinata.Data.MongoDB
{
    public abstract class BaseMongoRepository<T> : IBaseMongoRepository<T> where T : class
    {
        #region [ PRIVATE ]

        private readonly MongoDatabase _database = null;

        private AggregateArgs ProcessPipeline(string aggregationPipeline, dynamic parameters)
        {
            string pipeline = ReplaceParameters(parameters, aggregationPipeline);

            AggregateArgs args = new AggregateArgs();

            args.AllowDiskUse = true;
            args.OutputMode = AggregateOutputMode.Cursor;
            args.Pipeline = BsonSerializer.Deserialize<BsonDocument[]>(pipeline);

            return args;
        }

        private IMongoQuery ProcessQuery(string mongoQuery, dynamic parameters)
        {
            string query = ReplaceParameters(parameters, mongoQuery);

            var document = BsonSerializer.Deserialize<BsonDocument>(query);

            return new QueryDocument(document);
        }

        private IMongoQuery ProcessQuery(string mongoQuery, string projectQuery, dynamic parameters)
        {
            var document = BsonSerializer.Deserialize<BsonDocument>(ReplaceParameters(parameters, mongoQuery));

            var project = BsonSerializer.Deserialize<BsonDocument>(ReplaceParameters(parameters, projectQuery));

            return new QueryDocument(document, project);
        }

        #endregion

        #region [ PROTECTED ]

        protected virtual string CollectionName { get; set; }

        protected virtual MongoCollection GetCollection(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        protected virtual MongoCollection GetCollection()
        {
            return _database.GetCollection<T>(CollectionName);
        }

        protected static string ReplaceParameters(dynamic parameters, string pipeline)
        {
            if (parameters != null)
            {
                Type parametersType = parameters.GetType();

                var properties = parametersType.GetProperties();

                if (properties != null && properties.Length > 0)
                {
                    foreach (PropertyInfo pi in parametersType.GetProperties())
                    {
                        pipeline = pipeline.Replace("@" + pi.Name, ((BsonValue)BsonValue.Create(pi.GetValue(parameters))).ToJson());
                    }
                }
                else if (typeof(IDictionary<string, object>).IsAssignableFrom(parametersType))
                {
                    foreach (var kv in ((IDictionary<string, object>)parameters))
                    {
                        pipeline = pipeline.Replace("@" + kv.Key, ((BsonValue)BsonValue.Create(kv.Value)).ToJson());
                    }
                }
            }

            return pipeline.Replace('\'', '"');
        }

        protected string CreateRegex(IList<string> queryStrings)
        {
            string regex = "";

            foreach (var visitedQuery in queryStrings)
            {
                if (!string.IsNullOrEmpty(regex))
                {
                    regex = regex + ", ";
                }

                regex = regex + "/.*" + Regex.Escape(visitedQuery.Replace("&", "|")).Replace("/", @"\/") + ".*/";
            }

            return regex;
        }

        #endregion

        #region [ CONSTRUCTORS ]

        public BaseMongoRepository(MongoUrl mongoUrl)
        {
            Check.Argument.IsNotNull(mongoUrl, "mongoUrl");

            MongoClient client = new MongoClient(mongoUrl);
            MongoServer server = client.GetServer();

            _database = server.GetDatabase(mongoUrl.DatabaseName);
        }

        #endregion

        public virtual T GetById(Guid id)
        {
            return GetCollection(CollectionName).FindOneAs<T>(Query.EQ("_id", new BsonBinaryData(id)));
        }

        public virtual T1 GetById<T1>(Guid id)
        {
            return GetCollection(CollectionName).FindOneAs<T1>(Query.EQ("_id", new BsonBinaryData(id)));
        }

        public virtual bool Create(T document)
        {
            return GetCollection(CollectionName).Save(document, new MongoInsertOptions { WriteConcern = WriteConcern.Acknowledged }).Ok;
        }

        public virtual void CreateBatch(IList<T> listDocument)
        {
            GetCollection(CollectionName).InsertBatch(listDocument, new MongoInsertOptions { WriteConcern = WriteConcern.Acknowledged });
        }

        public virtual bool Delete(Guid id)
        {
            return GetCollection(CollectionName).Remove(Query.EQ("_id", new BsonBinaryData(id)), RemoveFlags.None, WriteConcern.Acknowledged).Ok;
        }

        public virtual bool Delete(IMongoQuery query)
        {
            return GetCollection(CollectionName).Remove(query, RemoveFlags.None, WriteConcern.Acknowledged).Ok;
        }

        public virtual bool Update(IMongoQuery query, IMongoUpdate update)
        {
            return GetCollection(CollectionName).Update(query, update).Ok;
        }

        public virtual bool Update(Guid id, IMongoUpdate update, UpdateFlags updateFlag = UpdateFlags.None)
        {
            if (updateFlag == UpdateFlags.None)
            {
                return GetCollection(CollectionName).Update(Query.EQ("_id", new BsonBinaryData(id)), update).Ok;
            }

            return GetCollection(CollectionName).Update(Query.EQ("_id", new BsonBinaryData(id)), update, updateFlag).Ok;
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return GetCollection(CollectionName).AsQueryable<T>();
        }

        public virtual IQueryable<T1> AsQueryable<T1>()
        {
            return GetCollection(CollectionName).AsQueryable<T1>();
        }

        public virtual bool DropCollection(string collectionName)
        {
            return _database.DropCollection(collectionName).Ok;
        }

        public virtual IQueryable<T> AggregateToOutput(string aggregationPipeline, string outputCollection, dynamic parameters = null)
        {
            AggregateArgs args = ProcessPipeline(aggregationPipeline, parameters);

            GetCollection(CollectionName).Aggregate(args);

            return GetCollection(outputCollection).FindAllAs<T>().AsQueryable();
        }

        public virtual IQueryable<T1> AggregateToOutput<T1>(string aggregationPipeline, string outputCollection, dynamic parameters = null)
        {
            AggregateArgs args = ProcessPipeline(aggregationPipeline, parameters);

            GetCollection(CollectionName).Aggregate(args);

            return GetCollection(outputCollection).FindAllAs<T1>().AsQueryable();
        }

        public virtual IQueryable<T> Aggregate(string aggregationPipeline, dynamic parameters = null)
        {
            AggregateArgs args = ProcessPipeline(aggregationPipeline, parameters);

            List<T> result = new List<T>();

            foreach (BsonDocument doc in GetCollection(CollectionName).Aggregate(args))
            {
                result.Add(BsonSerializer.Deserialize<T>(doc));
            }

            return result.AsQueryable();
        }

        public virtual IQueryable<T1> Aggregate<T1>(string aggregationPipeline, dynamic parameters = null)
        {
            AggregateArgs args = ProcessPipeline(aggregationPipeline, parameters);

            List<T1> result = new List<T1>();

            foreach (BsonDocument doc in GetCollection(CollectionName).Aggregate(args))
            {
                result.Add(BsonSerializer.Deserialize<T1>(doc));
            }

            return result.AsQueryable<T1>();
        }

        public virtual IQueryable<T1> Aggregate<T1>(string collectionName, string aggregationPipeline, dynamic parameters = null)
        {
            AggregateArgs args = ProcessPipeline(aggregationPipeline, parameters);

            List<T1> result = new List<T1>();

            foreach (BsonDocument doc in GetCollection(collectionName).Aggregate(args))
            {
                result.Add(BsonSerializer.Deserialize<T1>(doc));
            }

            return result.AsQueryable<T1>();
        }

        public virtual IQueryable<T> Find(string mongoQuery, dynamic parameters = null, string collectionName = "")
        {
            IMongoQuery query = ProcessQuery(mongoQuery, parameters);

            if (string.IsNullOrEmpty(collectionName))
            {
                return GetCollection(CollectionName).FindAs<T>(query).SetFlags(QueryFlags.NoCursorTimeout).AsQueryable();
            }

            return GetCollection(collectionName).FindAs<T>(query).SetFlags(QueryFlags.NoCursorTimeout).AsQueryable();
        }

        public virtual IQueryable<T1> Find<T1>(string mongoQuery, dynamic parameters = null, string collectionName = "")
        {
            IMongoQuery query = ProcessQuery(mongoQuery, parameters);

            if (string.IsNullOrEmpty(collectionName))
            {
                return GetCollection(CollectionName).FindAs<T1>(query).SetFlags(QueryFlags.NoCursorTimeout).AsQueryable();
            }

            return GetCollection(collectionName).FindAs<T1>(query).SetFlags(QueryFlags.NoCursorTimeout).AsQueryable();
        }

        public virtual IQueryable<T1> Find<T1>(string mongoQuery, IMongoFields fields, dynamic parameters = null, string collectionName = "")
        {
            IMongoQuery query = ProcessQuery(mongoQuery, parameters);

            if (string.IsNullOrEmpty(collectionName))
            {
                return GetCollection(CollectionName).FindAs<T1>(query).SetFields(fields).SetFlags(QueryFlags.NoCursorTimeout).AsQueryable();
            }

            return GetCollection(collectionName).FindAs<T1>(query).SetFields(fields).SetFlags(QueryFlags.NoCursorTimeout).AsQueryable();
        }

        public T1 FindOneAs<T1>(string mongoQuery, dynamic parameters = null, string collectionName = "")
        {
            IMongoQuery query = ProcessQuery(mongoQuery, parameters);

            if (string.IsNullOrEmpty(collectionName))
            {
                return GetCollection(CollectionName).FindOneAs<T1>(query);
            }

            return GetCollection(collectionName).FindOneAs<T1>(query);
        }

        public virtual long Count(string mongoQuery, dynamic parameters = null)
        {
            IMongoQuery query = ProcessQuery(mongoQuery, parameters);

            return GetCollection(CollectionName).Count(query);
        }

        public IMongoQuery CreateQuery(string mongoQuery, dynamic parameters = null)
        {
            return ProcessQuery(mongoQuery, parameters);
        }
    }
}
