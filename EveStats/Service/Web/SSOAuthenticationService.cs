using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EveStats.Service.Web
{
    ///
    /// <summary>
    /// Derived from the HttpClientService and authorizes with SSO.
    /// </summary>
    public class SSOAuthenticationService : HttpClientService
    {
        private HttpRequestHeader _header;
        private HttpResponseMessage _httpResponseMessage;
        private HttpRequestMessage _httpRequestMessage;
        private HttpStatusCode _statusCode;
        private HttpWebRequest _httpRequest;
        private HttpWebResponse _httpResponse;

        protected HttpRequestHeader Header { 
            get { return _header; }
            set { _header = value; }
        }
        protected HttpResponseMessage ResponseMessage
        {
            get { return _httpResponseMessage; }
            set { _httpResponseMessage = value; }
        }
        protected HttpRequestMessage RequestMessage
        {
            get { return _httpRequestMessage; }
            set { _httpRequestMessage = value; }
        }
        protected HttpStatusCode StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }
        protected HttpWebRequest HttpRequest
        {
            get { return _httpRequest; }
            set { _httpRequest = value; }
        }
        protected HttpWebResponse HttpResponse
        {
            get { return _httpResponse; }
            set { _httpResponse = value; }
        }

        public SSOAuthenticationService() : base("SSO")
        {
            Header = new();
            ResponseMessage = new();
            RequestMessage = new();
            StatusCode = new();
        }

        public static string GenerateSSOLink()
        {

            return string.Empty;
        }

        async Task<HttpResponseMessage> Authenticate(string path)
        {
            ResponseMessage = await Client.GetAsync(path);

            return ResponseMessage;
        }
    }
}
