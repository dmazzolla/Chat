using Chat.Common;
using Chat.Domain.Contracts;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.Net;

namespace Chat.Infraestructure.Helpers.RestSharp
{
    public class RestSharpWrapper : IRestSharpWrapper
    {
        private readonly string _authorization;
        private readonly string _baseUrl;
        public RestSharpWrapper()
        {
            _authorization = ConfigurationManager.AppSettings["Authorization"];
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"].EnsureEndsWith("/");
        }

        public RestSharpWrapper(string authorization, string baseUrl)
        {
            _authorization = authorization;
            _baseUrl = baseUrl.EnsureEndsWith("/"); 
        }

        public T ExecutRequest<T>(string nameMethod, string body, out HttpStatusCode httpStatusCode, Method typeMethod = Method.GET, string parameters = "") where T : new()
        {
            if (parameters.IsNotNullOrEmptyOrWhiteSpace())
                parameters = parameters.EnsureStartsWith("/");

            IRestResponse response = Request(_baseUrl + nameMethod + parameters, typeMethod, body);
            httpStatusCode = response.StatusCode;
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<T>(response.Content);
            else
                return new T();
        }

        public T ExecutRequest<T>(string nameMethod, out HttpStatusCode httpStatusCode, Method typeMethod = Method.GET, string parameters = "") where T : new()
        {
            if (parameters.IsNotNullOrEmptyOrWhiteSpace())
                parameters = parameters.EnsureStartsWith("/");

            IRestResponse response = Request(_baseUrl + nameMethod + parameters, typeMethod);
            httpStatusCode = response.StatusCode;
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<T>(response.Content);
            else
                return new T();
        }

        public HttpStatusCode ExecutRequest(string nameMethod, Method typeMethod = Method.GET, string body = "", string parameters = "")
        {
            if (parameters.IsNotNullOrEmptyOrWhiteSpace())
                parameters = parameters.EnsureStartsWith("/");

            IRestResponse response = Request(_baseUrl + nameMethod + parameters, typeMethod, body);
            return response.StatusCode;
        }

        public HttpStatusCode ExecutRequest(string nameMethod, Method typeMethod = Method.GET, string parameters = "")
        {
            if (parameters.IsNotNullOrEmptyOrWhiteSpace())
                parameters = parameters.EnsureStartsWith("/");

            IRestResponse response = Request(_baseUrl + nameMethod + parameters, typeMethod);
            return response.StatusCode;
        }

        IRestResponse Request(string url, Method typeMethod, string body = "")
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(typeMethod);
            request.AddHeader("Content-Type", "application/json");

            if (_authorization.IsNotNullOrEmptyOrWhiteSpace())
                request.AddHeader("Authorization", _authorization);

            if (body.IsNotNullOrEmptyOrWhiteSpace())
                request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return response;
        }

    }
}
