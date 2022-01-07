using System;
using System.Net.Http;
using System.Net.Http.Headers;
using EveStats.Data.Resources;

namespace EveStats.Service.Web
{
    /// <summary>
    /// Implementing a base web client for making API calls.
    /// </summary>

    public class HttpClientService
    {
        static HttpClient client;

        protected static Uri SSOBase => new(APIConstants.SSOBase);
        protected static Uri ESI_BASE => new(APIConstants.ESIBase);
        protected static TimeSpan RTimeout => TimeSpan.FromSeconds(30);

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

        /// <summary>
        /// Cancel all requests.
        /// </summary>
        public void Cancel()
        {
            client.CancelPendingRequests();
        }

        /// <summary>
        /// Release and dispose of resources.
        /// </summary>
        public static void Dispose()
        {
            client.Dispose();
        }

        /// <summary>
        /// Verifies endpoint.
        /// </summary>
        /// <returns>string</returns>
        public static string Mode()
        {
            return client.BaseAddress == SSOBase ? "SSO" : "ESI";
        }
    }
}