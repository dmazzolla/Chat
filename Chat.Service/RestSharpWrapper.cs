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
        readonly string _autorization;
        readonly string _baseUrl;
        public RestSharpWrapper()
        {
            _autorization = ConfigurationManager.AppSettings["Autorization"];
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"].EnsureEndsWith("/");
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

        public HttpStatusCode ExecutRequest(string nameMethod, Method typeMethod = Method.GET, string parameters = "")
        {
            if (parameters.IsNotNullOrEmptyOrWhiteSpace())
                parameters = parameters.EnsureStartsWith("/");

            IRestResponse response = Request(_baseUrl + nameMethod + parameters, typeMethod);
            return response.StatusCode;
        }


        IRestResponse Request(string url, Method typeMethod)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(typeMethod);
            request.AddHeader("Content-Type", "application/json");
            if (!string.IsNullOrEmpty(_autorization))
            {
                request.AddHeader("Authorization", _autorization);
            }
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
