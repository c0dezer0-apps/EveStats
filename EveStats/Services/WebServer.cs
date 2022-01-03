using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EveStats.Services
{
    interface ISSOWebServer : IDisposable
    {
        void WaitForCode(string state, Action<Task<string>> callback);
        void Start();
        void Stop();
        Task<string> WaitForCodeAsync(string state);
    }
}
