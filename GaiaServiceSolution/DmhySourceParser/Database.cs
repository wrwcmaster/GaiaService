using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DmhySourceParser
{
    class Database
    {
        #region Singleton
        private class SingletonHelper
        {
            static SingletonHelper() { }
            internal static readonly Database _instance = new Database();
        }

        public static Database Instance
        {
            get
            {
                return SingletonHelper._instance;
            }
        }
        private Database()
        {
            //Pre init codes come here
            _client = new MongoClient(_connectionString);
            _server = _client.GetServer();
        }
        #endregion

        private string _dbName = "tracy";
        private string _connectionString = "mongodb://localhost:27001";
        private MongoClient _client;
        private MongoServer _server;

        public MongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = _server.GetDatabase(_dbName);
            return db.GetCollection<T>(collectionName);
        }
    }
}
