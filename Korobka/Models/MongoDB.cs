using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using System.IO;
using Renci.SshNet;
using System.Text.Json;
using Korobka;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Korobka.Models
{
    public class MongoDBConnect
    {
        IMongoDatabase database;

        public MongoDBConnect()
        {

            string SSHServerHost = (AsyncHelper.RunSync(() => GetJsonValues("host"))).ToString();
            string SSHServerUserName = (AsyncHelper.RunSync(() => GetJsonValues("userName"))).ToString();
            string SSHServerPassword = (AsyncHelper.RunSync(() => GetJsonValues("password"))).ToString();

            ConnectionInfo conn = new ConnectionInfo(
                SSHServerHost,
                SSHServerUserName, 
                new PasswordAuthenticationMethod(
                    SSHServerUserName,
                    SSHServerPassword));

            SshClient sshClient = new SshClient(conn);

            sshClient.Connect();

            ForwardedPortLocal forwardedPortLocal = new ForwardedPortLocal("127.0.0.1", 27017, "127.0.0.1", 27017);
            sshClient.AddForwardedPort(forwardedPortLocal);
            forwardedPortLocal.Start();


            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            database = client.GetDatabase("test");
        }

        public IMongoCollection<BsonDocument> Collection
        {
            get { return database.GetCollection<BsonDocument>("users"); }
        }

        // Добавить документ
        public async Task AddDoc(BsonDocument doc)
        {
            await Collection.InsertOneAsync(doc);
        }

        // Получить общее число документов в коллекции
        public long AllDocCount()
        {
            var filter = new BsonDocument();
            long count = Collection.CountDocuments(filter);
            return count;
        }

        public static async Task<string> GetJsonValues(string value)
        {
            var httpClient = new HttpClient();
            var resultJson = await httpClient.GetStringAsync("http://188.225.85.229:5002/");
            var resultValue = JsonConvert.DeserializeObject<JsonFile>(resultJson);
            if(value == "host") return resultValue.Host.ToString();
            if(value == "userName") return resultValue.UserName.ToString();
            if (value == "password") return resultValue.Password.ToString();
            else return null;
        }
    }

    public class JsonFile
    {
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public static class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                        TaskCreationOptions.None,
                        TaskContinuationOptions.None,
                        TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
    }
}
