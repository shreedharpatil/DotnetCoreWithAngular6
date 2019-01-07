using Newtonsoft.Json;

namespace Data.Repository.Models
{
    public class SendMessageResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
