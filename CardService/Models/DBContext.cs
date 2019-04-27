using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace CardService.Models {
    public class DBContext {
        //public readonly IMongoCollection<CardModel> Cards;

        private Dictionary<string, Repository<BaseCollection>> collections = new Dictionary<string, Repository<BaseCollection>>();

        public readonly IMongoDatabase Database;

        public static DBContext Instance {
            get;
            private set;
        }

        public static DBContext GetInstance(IConfiguration config) {
            if (Instance == null) {
                Instance = new DBContext(config);
            }
            return Instance;
        }

        private DBContext(IConfiguration config) {
            // mongo use string for id
            BsonSerializer.RegisterIdGenerator(typeof(string), new StringObjectIdGenerator());

            string connectionString = config.GetConnectionString("CardStoreDb");
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase("CardStoreDb");
        }
    }
}