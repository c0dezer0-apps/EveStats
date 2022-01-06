using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using EveStats.Data.Resources;

namespace EveStats.Services.Web
{
    /// <summary>
    /// Implementing a base web client for making API calls.
    /// </summary>

    public class HttpClientService
    {
        static HttpClient client;

        private static Uri SSOBase => new(APIConstants.SSOBase);
        private static Uri ESI_BASE => new(APIConstants.ESIBase);
        private static TimeSpan RTimeout => TimeSpan.FromSeconds(30);

        /// <summary>
        /// Creates a new instance of the HttpClient set to be a service for either SSO or ESI.
        /// </summary>
        /// <param name="receiver"></param>
        public HttpClientService(string receiver)
        {
            client = new();
            client.BaseAddress = receiver == "SSO" ? SSOBase : ESI_BASE;
            // Making sure there are no unnecessary Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = RTimeout;
        }

        public void ShowResponse(HttpResponseMessage response)
        {

        }
    }
}