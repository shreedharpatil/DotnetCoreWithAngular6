using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Layer.HttpClient;
using Data.Repository.Interfaces;
using Data.Repository.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Data.Repository.Implementations
{
    public class SendMessage : ISendMessage
    {
        private readonly IConfiguration configuration;

        private readonly IMongoCollection<Models.User> users;

        private readonly IHttpClientWrapper httpClientWrapper;

        public SendMessage(IConfiguration configuration, IHttpClientWrapper httpClientWrapper)
        {
            this.configuration = configuration;
            this.httpClientWrapper = httpClientWrapper;
            var client = new MongoClient(this.configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("aem");
            this.users = database.GetCollection<User>("User");
        }

        public async Task<Tuple<IEnumerable<SendMessageResponse>, IEnumerable<string>>> Send(SendMessageDTO sendMessageDTO)
        {
            var mobileNumbers = this.users.Find(p => p.Feeder == sendMessageDTO.FeederId).ToList().Select(p => p.Mobile);
            var result = new List<SendMessageResponse>();
            foreach(var mobile in mobileNumbers)
            {
                var r = await this.httpClientWrapper.GetAsync<SendMessageResponse>("https://smsapi.engineeringtgr.com/", $"send/?Mobile=8880299973&Password=1989&Message={sendMessageDTO.Message}&To={mobile}&Key=shreePcyLq6mCtfBs7TG1FKjUAek");
                result.Add(r);
            }

            return  new Tuple<IEnumerable<SendMessageResponse>, IEnumerable<string>>(result, mobileNumbers);
        }
    }
}
