using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Layer.DTO
{
    public class SendMessageResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
