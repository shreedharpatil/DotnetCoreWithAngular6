using Common.Layer.DTO;
using Common.Layer.HttpClient;
using Common.Layer.Models;
using File.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Repository.Implementations
{
    public class SendMessage : ISendMessage
    {
        private readonly IHttpClientWrapper httpClientWrapper;

        private readonly Configuration configuration;

        public SendMessage(IHttpClientWrapper httpClientWrapper, Configuration configuration)
        {
            this.httpClientWrapper = httpClientWrapper;
            this.configuration = configuration;
        }

        public async Task<Tuple<IEnumerable<SendMessageResponse>, IEnumerable<string>>> Send(SendMessageDTO sendMessageDTO)
        {
            var data = System.IO.File.ReadAllText(string.Format(configuration.AppSettings.DbTablesFilePath, "User.json"));
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            var mobileNumbers = users.Where(p => p.Feeder == sendMessageDTO.FeederId).Select(p => p.Mobile);
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
