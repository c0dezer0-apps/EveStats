using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.IO;
using EveStats.Data.Resources;

namespace EveStats.Service
{
    /// <summary>
    /// Base listener for SSO calls.
    /// </summary>
    public class SSOServerHttpListener : ISSOWebServer
    {
        private const int PORT = 4916;
        private const string CLIENTID = "acf04602dbf44fa28754758410ba246f";

        private static readonly object RESPONSE_LOCK = new();
        private static readonly TimeSpan TIMEOUT_IDLE = TimeSpan.FromSeconds(10.0);
        private static readonly TimeSpan TIMEOUT_READ = TimeSpan.FromSeconds(3.0);
        private static readonly TimeSpan TIMEOUT_WRITE = TimeSpan.FromSeconds(2.0);

        private static byte[] responseOK = null;
        private static byte[] responseErr = null;

        private readonly HttpListener listener;
        private readonly HttpListenerTimeoutManager manager;
        private string prefix;

        private static void InitResponses()
        {
            lock(RESPONSE_LOCK)
            { 
                if (responseErr == null || responseOK == null)
                {
                    responseErr = Encoding.UTF8.GetBytes(OtherResources.fail);
                    responseOK = Encoding.UTF8.GetBytes(OtherResources.pass);
                }
            }
        }

        protected string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }
        protected static string ClientId => CLIENTID;
        protected static int Port => PORT;

        public SSOServerHttpListener()
        {
            if (!HttpListener.IsSupported)
                throw new InvalidOperationException("HTTP Listener is not supported.");

            listener = new();
            Prefix = String.Format(APIConstants.SSORedirect, PORT);

            if (!Prefix.EndsWith("/"))
            {
                Prefix += "/";
            }
            listener.Prefixes.Add(Prefix);
            listener.IgnoreWriteExceptions = true;
            manager = listener.TimeoutManager;
            manager.IdleConnection = TIMEOUT_IDLE;
            manager.DrainEntityBody = TIMEOUT_WRITE;

            // These are only available on the windows platform.
            if (OperatingSystem.IsWindows())
            {
                manager.HeaderWait = TIMEOUT_READ;
                manager.RequestQueue = TIMEOUT_WRITE;
                manager.EntityBody = TIMEOUT_READ;
            }
            // Seek alternate methods for mobile devices and Mac.
            InitResponses();
        }

        public void Start()
        {
            try
            {
                listener.Start();
            }
            catch (HttpListenerException e)
            {
                Console.WriteLine(e);
            }
        }

        public void Stop()
        {
            try
            {
                listener.Stop();
            }
            catch (HttpListenerException e)
            {
                Console.WriteLine(e);
            }
        }

        // Get rid of the Listener when we're done with it.
        public void Dispose()
        {
            listener.Stop();
            listener.Close();
            GC.SuppressFinalize(this);
        }

        public void WaitForCode(string state, Action<Task<string>> callback)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException(nameof(state));
            WaitForCodeAsync(state).ContinueWith(result => );
        }

        private static async Task<string> SendResponseAsync(string state, HttpListenerResponse output, NameValueCollection queryParams)
        {
            string code = "";
            byte[] response;
            HttpStatusCode responseCode;
            var stateParams = queryParams.GetValues("state");

            if (stateParams != null && stateParams.Length == 1 && stateParams[0] == state)
            {
                var codeParams = queryParams.GetValues("code");

                if (codeParams != null && codeParams.Length > 0)
                    code = codeParams[0];
            }
           
            if (string.IsNullOrEmpty(code))
            {
                response = responseErr;
                responseCode = HttpStatusCode.NotFound;
            }
            else
            {
                response = responseOK;
                responseCode = HttpStatusCode.OK;
            }

            using var stream = output.OutputStream;
            int len = response.Length;
            output.StatusCode = (int)responseCode;
            output.ContentLength64 = len;
            output.ContentType = "text/html";
            output.ContentEncoding = Encoding.UTF8;

            await stream.WriteAsync(response.AsMemory(0, len));
            await stream.FlushAsync();
            
            return code;
        }

        public async Task<string> WaitForCodeAsync(string state)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException(nameof(state));

            string code = string.Empty;
            try
            {
                do
                {
                    var context = await listener.GetContextAsync().ConfigureAwait(false);
                    using var output = context.Response;
                    string query = context.Request.Url.Query;

                    if (query == null)
                        query = "";
                    var queryParams = HttpUtility.ParseQueryString(query);
                    code = await SendResponseAsync(state, output, queryParams);
                } while (string.IsNullOrEmpty(code));
            }
            catch (ObjectDisposedException)
            {

            }
            catch (HttpListenerException e)
            {
                throw new IOException("Error when waiting for auth code", e);
            }

            return code;
        }

        public void AbortCall()
        {
            listener.Abort();
        }

        public bool StatusCheck()
        {
            return listener.IsListening;
        }

    }
}
