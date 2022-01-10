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
        protected static HttpClient Client { get; set; }
        protected static Uri SSOBase => new(APIConstants.SSOBase);
        protected static Uri ESI_BASE => new(APIConstants.ESIBase);
        protected static TimeSpan RTimeout => TimeSpan.FromSeconds(30);

        /// <summary>
        /// Creates a new instance of the HttpClient set to be a service for either SSO or ESI.
        /// </summary>
        /// <param name="receiver"></param>
        public HttpClientService(string receiver)
        {
            Client = new();
            Client.BaseAddress = receiver == "SSO" ? SSOBase : ESI_BASE;
            // Making sure there are no unnecessary Headers
            Client.DefaultRequestHeaders.Accept.Clear();
            // Set header depending on receipient.
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(receiver == "SSO" ? "application/x-www-form-urlencoded" : "application/json"));
            Client.Timeout = RTimeout;
        }

        /// <summary>
        /// Cancel all requests.
        /// </summary>
        public static void Cancel()
        {
            Client.CancelPendingRequests();
        }

        /// <summary>
        /// Release and dispose of resources.
        /// </summary>
        public static void Dispose()
        {
            Client.Dispose();
        }

        /// <summary>
        /// Verifies endpoint.
        /// </summary>
        /// <returns>string</returns>
        public static string Mode()
        {
            return Client.BaseAddress == SSOBase ? "SSO" : "ESI";
        }
    }
}