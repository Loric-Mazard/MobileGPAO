using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GPAOnGo.WEBSERVICE
{
    public static class RestServiceHelper
    {
        // Basic auth
        public static HttpClient GetClient(string username, string password)
        {
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; //bypass a changer

            var client = new HttpClient(clientHandler)
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            return client;
        }

        // Auth with bearer token
        public static HttpClient GetClient(string token)
        {
            var authValue = new AuthenticationHeaderValue("Bearer", token);

            var client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            return client;
        }
    }
}