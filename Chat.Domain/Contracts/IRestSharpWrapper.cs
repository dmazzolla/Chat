using RestSharp;
using System.Net;

namespace Chat.Domain.Contracts
{
    public  interface IRestSharpWrapper
    {
        T ExecutRequest<T>(string nameMethod, string body, out HttpStatusCode httpStatusCode, Method typeMethod = Method.GET, string parameters = "") where T : new();

        T ExecutRequest<T>(string nameMethod, out HttpStatusCode httpStatusCode, Method typeMethod = Method.GET, string parameters = "") where T : new();

        HttpStatusCode ExecutRequest(string nameMethod, Method typeMethod = Method.GET, string body = "", string parameters = "");

        HttpStatusCode ExecutRequest(string nameMethod, Method typeMethod = Method.GET, string parameters = "");
    }
}
