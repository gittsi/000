using MongoDB.Bson;
using MongoDB.Driver;
using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Repository.MongoDBRepository
{
    public class MongoDBConnectionHelper
    {
        MongoDBSettings _settings;
        public MongoDBConnectionHelper(MongoDBSettings settings)
        {
            _settings = settings;
        }

        public IMongoDatabase GetMongoDbDatabase()
        {

            var client = new MongoClient(_settings.ConnectionString);
            return client.GetDatabase(_settings.DB);
        }
    }
}
