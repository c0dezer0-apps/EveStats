using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EveStats.Service.Web
{
    /// <summary>
    /// The parent class for making requests using the HttpClientService.
    /// </summary>
    public class HttpRequestService : HttpClientService
    {
        public HttpRequestService(string type) : base(type)
        {

        }
    }
}
