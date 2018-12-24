namespace Common.Layer.HttpClient
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class HttpclientWrapper : IHttpClientWrapper
    {
        private readonly string basePath;

        public HttpclientWrapper(string basePath)
        {
            this.basePath = basePath;
        }

        public HttpclientWrapper()
        {

        }

        public async Task<T> GetAsync<T>(string path)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(this.basePath);
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var r = await httpclient.GetStringAsync(path);
                return JsonConvert.DeserializeObject<T>(r);
            }
        }

        public async Task<T> GetAsync<T>(string basepath, string path)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(basepath);
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var r = await httpclient.GetStringAsync(path);
                return JsonConvert.DeserializeObject<T>(r);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string path, T model)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(this.basePath);
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var body = new StringContent(JsonConvert.SerializeObject(model));
                body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return await httpclient.PostAsync(path, body);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(T model, string completeUrl)
        {
            using (var httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var body = new StringContent(JsonConvert.SerializeObject(model));
                body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return await httpclient.PostAsync(completeUrl, body);
            }
        }
    }
}
