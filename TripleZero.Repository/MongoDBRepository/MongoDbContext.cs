using MongoDB.Bson;
using MongoDB.Driver;
using SWGoH;
using SWGoH.Model;
using SWGoH.Model.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripleZero.Repository.Dto;

namespace TripleZeroApi.Repository.MongoDBRepository
{
    public class MongoDBContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; }

        MongoDBSettings _settings;
        //public MongoDBConnectionHelper(MongoDBSettings settings)
        //{
          
        //}

        //public IMongoDatabase GetMongoDbDatabase()
        //{

        //    var client = new MongoClient(_settings.ConnectionString);
        //    return client.GetDatabase(_settings.DB);
        //}


        public MongoDBContext(MongoDBSettings settings)
        {
            _settings = settings;
            try
            {
                MongoClientSettings mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(_settings.ConnectionString));
                if (IsSSL)
                {
                    mongoSettings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                ////var mongoClient = new MongoClient(settings);

                //var mongoClient = new MongoClient(_settings.ConnectionString);

                //_database = mongoClient.GetDatabase(_settings.DB);

                //_database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();

                ////var dsgfd = mongoClient.ListDatabases();
                ////var asfgasgf = _database.ListCollections();

                //var a = _database.GetCollection<GuildConfigDto>("Config.Guild");
                //var b = _database.GetCollection<BsonDocument>("Config.Guild");


                //var results = a.AsQueryable<GuildConfigDto>().ToList();


                //var asgvaf = a.Find("Config.Guild");
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        //public IMongoCollection<PlayerDto> Players
        //{
        //    get
        //    {
        //        return _database.GetCollection<PlayerDto>("PlayerS");
        //    }
        //}

        //public IMongoCollection<TestCollection> TestCollection
        //{
        //    get
        //    {
        //        return _database.GetCollection<TestCollection>("testcollection");
        //    }
        //}
    }
}
