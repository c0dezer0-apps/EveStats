using Microsoft.Win32.SafeHandles;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EveStats.Services
{
    public class SSOServerHttpListener : ISSOWebServer
    {
        public const int PORT = 4916;
        private string[] prefixes;
        private HttpListener listener;
        private bool disposed;
        private SafeHandle handle;

        protected string[] Prefixes 
        {
            get { return prefixes; }
            set { prefixes = value; }
        }
        protected HttpListener Listener 
        { 
            get { return listener; }
            set { listener = value; }
        }
        protected bool Disposed 
        { 
            get { return disposed; }
            set { disposed = value; }
        }
        protected SafeHandle Handle
        {
            get { return handle; }
            set { handle = value; }
        }

        public SSOServerHttpListener(string[] prefixes)
        {
            Prefixes = prefixes;
            Listener = new();
            Disposed = false;
            Handle = new SafeFileHandle(IntPtr.Zero, true);
        }

        public void ChangePrefixes(string[] prefixes)
        {
            this.Listener.Stop();
            this.Prefixes = prefixes;
        }

        public void Start()
        {
            this.Listener.Start();
        }

        public void Stop()
        {
            this.Listener.Stop();
        }

        // Get rid of the Listener when we're done with it.
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            this.Disposed = true;
        }

        public void WaitForCode(string state, Action<Task<string>> action)
        { }

        public Task<string> WaitForCodeAsync(string state)
        {
            Task<string> test = null;
            return test;
        }

    }
}
