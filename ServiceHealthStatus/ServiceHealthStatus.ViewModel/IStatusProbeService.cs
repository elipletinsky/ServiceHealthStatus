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
        public Task<(string body, HttpStatusCode status)> Probe(string url);
    }
}
