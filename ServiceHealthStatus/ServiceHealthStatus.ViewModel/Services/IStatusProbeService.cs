using System.Net;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public  interface IStatusProbeService
    {
        Task<(string body, HttpStatusCode status)> Probe(string url);
        Task<string> GetJsonFromUri(string uri);
    }
}
