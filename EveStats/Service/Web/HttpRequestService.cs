using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EveStats.Service.Web
{
    ///
    /// <summary>
    /// Derived from the HttpClientService and gives the ability to make requests to the API.
    /// </summary>
    public class HttpRequestService : HttpClientService
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
        protected HttpResponseMessage HttpResponseMessage
        {
            get { return _httpResponseMessage; }
            set { _httpResponseMessage = value; }
        }
        protected HttpRequestMessage HttpRequestMessage
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

        public HttpRequestService(string type) : base(type)
        {

        }
    }
}
