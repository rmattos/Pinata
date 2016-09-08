using Jil;
using MongoDB.Bson;
using Pinata.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pinata.Command
{
    public class CommandMongo : ICommand
    {
        private IDictionary<string, bool> _collectionsLoaded = new Dictionary<string, bool>();

        public IList<object> CreateDelete(IList<object> list)
        {
            IList<SampleMongoData> convertedList = list.Cast<SampleMongoData>().ToList();
            IList<object> deleteList = new List<object>();

            foreach (var sample in convertedList)
            {
                IDictionary<string, IList<BsonElement>> data = new Dictionary<string, IList<BsonElement>>();
                IList<BsonElement> elementList = new List<BsonElement>();

                foreach (var row in sample.Rows)
                {
                    foreach (var key in sample.Keys)
                    {
                        Common.Schema schema = sample.Schema.SingleOrDefault(s => s.Column == key);

                        string value = JSON.DeserializeDynamic(row.ToString())[schema.Column];

                        elementList.Add(new BsonElement(schema.Column, ParserDataType.ParseMongo((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value)));
                    }
                }

                data.Add(sample.Collection, elementList);

                deleteList.Add(data);
            }

            return deleteList;
        }

        public IList<object> CreateInsert(IList<object> list)
        {
            IList<SampleMongoData> convertedList = list.Cast<SampleMongoData>().ToList();
            IList<object> insertList = new List<object>();

            foreach (var sample in convertedList)
            {
                if (!_collectionsLoaded.ContainsKey(sample.Collection))
                {
                    IDictionary<string, IList<BsonDocument>> data = new Dictionary<string, IList<BsonDocument>>();
                    IList<BsonDocument> docList = new List<BsonDocument>();

                    foreach (var row in sample.Rows)
                    {
                        BsonDocument document = new BsonDocument();

                        foreach (var schema in sample.Schema)
                        {
                            string value = string.Empty;

                            if (schema.Type.ToLower() == ParserDataType.DataType.Array.ToString().ToLower() || schema.Type.ToLower() == ParserDataType.DataType.Document.ToString().ToLower())
                            {
                                value = JSON.DeserializeDynamic(row.ToString())[schema.Column].ToString();
                            }
                            else
                            {
                                value = JSON.DeserializeDynamic(row.ToString())[schema.Column];
                            }

                            document.Add(new BsonElement(schema.Column, ParserDataType.ParseMongo((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value)));
                        }

                        docList.Add(document);
                    }

                    data.Add(sample.Collection, docList);

                    insertList.Add(data);

                    _collectionsLoaded.Add(sample.Collection, true);
                }
            }

            return insertList;
        }

        public IList<object> CreateUpdate(IList<object> list)
        {
            throw new NotImplementedException();
        }


        public IList<object> CreateDelete(IList<object> list, IDictionary<string, string> dynamicParameters)
        {
            IList<SampleMongoData> convertedList = list.Cast<SampleMongoData>().ToList();
            IList<object> deleteList = new List<object>();

            foreach (var sample in convertedList)
            {
                IDictionary<string, IList<BsonElement>> data = new Dictionary<string, IList<BsonElement>>();
                IList<BsonElement> elementList = new List<BsonElement>();

                foreach (var row in sample.Rows)
                {
                    foreach (var key in sample.Keys)
                    {
                        Common.Schema schema = sample.Schema.SingleOrDefault(s => s.Column == key);

                        string value = JSON.DeserializeDynamic(row.ToString())[schema.Column];

                        if (dynamicParameters.ContainsKey(value))
                        {
                            value = dynamicParameters[value];
                        }

                        elementList.Add(new BsonElement(schema.Column, ParserDataType.ParseMongo((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value)));
                    }
                }

                data.Add(sample.Collection, elementList);

                deleteList.Add(data);
            }

            return deleteList;
        }

        public IList<object> CreateInsert(IList<object> list, IDictionary<string, string> dynamicParameters)
        {
            IList<SampleMongoData> convertedList = list.Cast<SampleMongoData>().ToList();
            IList<object> insertList = new List<object>();

            foreach (var sample in convertedList)
            {
                if (!_collectionsLoaded.ContainsKey(sample.Collection))
                {
                    IDictionary<string, IList<BsonDocument>> data = new Dictionary<string, IList<BsonDocument>>();
                    IList<BsonDocument> docList = new List<BsonDocument>();

                    foreach (var row in sample.Rows)
                    {
                        BsonDocument document = new BsonDocument();

                        foreach (var schema in sample.Schema)
                        {
                            string value = string.Empty;

                            if (schema.Type.ToLower() == ParserDataType.DataType.Array.ToString().ToLower() || schema.Type.ToLower() == ParserDataType.DataType.Document.ToString().ToLower())
                            {
                                value = JSON.DeserializeDynamic(row.ToString())[schema.Column].ToString();

                                string key = value.Replace("\"", "");

                                if (dynamicParameters.ContainsKey(key))
                                {
                                    value = dynamicParameters[key];
                                }
                            }
                            else
                            {
                                value = JSON.DeserializeDynamic(row.ToString())[schema.Column];

                                if (dynamicParameters.ContainsKey(value))
                                {
                                    value = dynamicParameters[value];
                                }
                            }

                            document.Add(new BsonElement(schema.Column, ParserDataType.ParseMongo((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value)));
                        }

                        docList.Add(document);
                    }

                    data.Add(sample.Collection, docList);

                    insertList.Add(data);

                    _collectionsLoaded.Add(sample.Collection, true);
                }
            }

            return insertList;
        }

        public IList<object> CreateUpdate(IList<object> list, IDictionary<string, string> dynamicParameters)
        {
            throw new NotImplementedException();
        }
    }
}