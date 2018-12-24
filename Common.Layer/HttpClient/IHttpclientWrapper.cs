namespace Common.Layer.HttpClient
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IHttpClientWrapper
    {
        Task<T> GetAsync<T>(string path);

        Task<T> GetAsync<T>(string basepath, string path);

        Task<HttpResponseMessage> PostAsync<T>(string path, T model);

        Task<HttpResponseMessage> PostAsync<T>(T model, string completeUrl);
    }
}
