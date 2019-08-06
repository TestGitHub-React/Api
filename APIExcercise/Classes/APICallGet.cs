using System.Net.Http;

namespace APIExcercise.Classes
{
    public class APICallGet : APICallBuilder
    {
        protected override HttpResponseMessage GetResponseMessage()
        {
            return apiClient.GetAsync(urlParameters).Result;
        }
    }
}