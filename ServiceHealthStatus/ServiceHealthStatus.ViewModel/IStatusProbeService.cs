using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public  interface IStatusProbeService
    {
        Task<(string body, HttpStatusCode status)> Probe(string url);
        Task<string> GetJsonFromUri(string uri);
    }
}
