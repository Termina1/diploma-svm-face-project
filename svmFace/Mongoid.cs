using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using SVM;

namespace svmFace
{
    public class Mongoid
    {
        private static Mongoid instance;

        private MongoServer _server;

        private MongoDatabase _db;

        private Mongoid() {
            _server = MongoServer.Create("mongodb://localhost");
            _db = _server.GetDatabase("planes");
            BsonClassMap.RegisterClassMap<Person>(cm =>
            {
                cm.AutoMap();
            });
        }

        public static Mongoid getInstance() {
            if(Mongoid.instance == null) {
                Mongoid.instance = new Mongoid();
            }
            return Mongoid.instance;
        }

        public void registerPerson(Person man) {
            var people = _db.GetCollection<Person>("people");
            people.Insert(man);
        }

        public MongoCollection<BsonDocument> planes() {
            return _db.GetCollection<BsonDocument>("people");
        }
 
        public void wipe()
        {
            _db.Drop();
        }
    }
}
