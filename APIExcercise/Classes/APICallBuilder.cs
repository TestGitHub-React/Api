using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace APIExcercise.Classes
{
    public abstract class APICallBuilder
    {
        protected HttpClient apiClient = new HttpClient();
        protected string urlParameters;
        protected MediaType mediaType;

        public APICallBuilder SetBaseAddress(string baseAddress)
        {
            apiClient.BaseAddress = new Uri(baseAddress);
            return this;
        }

        public APICallBuilder SetURLParameters(string urlParameters)
        {
            this.urlParameters = urlParameters;
            return this;
        }

        public APICallBuilder SetAcceptMediaType(MediaType mediaType)
        {
            this.mediaType = mediaType;
            apiClient.DefaultRequestHeaders.Accept.Clear();
            switch (mediaType)
            {
                case MediaType.JSON:
                    apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    break;
                case MediaType.XML:
                    throw new NotImplementedException();
            }

            return this;
        }

        private JObject RetriveResponse()
        {
            JObject result = null;
            string response;
            var httpResponseMessage = GetResponseMessage();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("API Call does not have success status code.");
            }

            switch (mediaType)
            {
                case MediaType.JSON:
                    result = JObject.Parse(response);
                    break;

                case MediaType.XML:
                    throw new NotImplementedException();
            }

            return result;
        }

        protected abstract HttpResponseMessage GetResponseMessage();

        public JObject PlaceAPICall()
        {
            var result = RetriveResponse();
            apiClient.Dispose();
            return result;
        }

    }

}



