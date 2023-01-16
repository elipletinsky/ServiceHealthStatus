using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ServiceHealthStatus.ViewModel
{
    internal class StatusProbeService : IStatusProbeService
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public StatusProbeService(HttpClient httpClient, ILoggerFactory loggerFactory)
        {
            _httpClient = httpClient;
            _logger = loggerFactory.CreateLogger<StatusProbeService>();
        }

        public async Task<(string, HttpStatusCode)> Probe(string url)
        {
            try
            {
                var result = await _httpClient.GetAsync(url);
                
                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Failed to call {url} status:{result.StatusCode}");
                }

                return (await result.Content.ReadAsStringAsync(), result.StatusCode);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Service request failed");
            }

            return (string.Empty, HttpStatusCode.InternalServerError);
        }

        public async Task<string> GetJsonFromUri(string uri)
        {
            try
            {
                
                var x = await _httpClient.GetStringAsync(uri);
                return x;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
