using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using ServiceHealthStatus.ViewModel.Model;
using System.Text.RegularExpressions;

namespace ServiceHealthStatus.ViewModel
{
    public class ServiceInstanceViewModel : BaseViewModel<ServiceInstance, object, DummyViewModel>
    {
        private const int MaxBodyDisplayChars = 30;
        private string _watch;
        private readonly IStatusProbeService _probeService;
        private string _response;

        public ServiceInstanceViewModel(IServiceProvider services, IStatusProbeService probeService)
                    : base(services)
        {
            _probeService = probeService;
        }

        public string Response
        {
            get => _response;
            set
            {
                _response = value;
                OnPropertyChanged();
            }
        }

        public string Watch
        {
            get => _watch;
            set
            {
                _watch = value;
                OnPropertyChanged();
            }
        }

        protected override async Task DoExecuteProbe()
        {
            var watch = new Stopwatch();
            watch.Start();
            var result = await _probeService.Probe(Model.Url);
            watch.Stop();
            Watch = watch.Elapsed.ToString(@"mm\:ss\.fff") + " ms";
            if (result.body == string.Empty)
            {
                Response = $"Failed - Status {(int)result.status}";
            }
            else
            {
                Response = BuildResultPatern(result.body);
            }

            if ((int)result.status >= 200 && (int)result.status <= 299)
            {
                Status = Status.Success;
            }
            else
            {
                Status = Status.Failure;
            }
        }

        private string BuildResultPatern(string input)
        {
            string resultPatern = ResultPattern;
            if (string.IsNullOrEmpty(resultPatern))
            {
                return input.Substring(0, MaxBodyDisplayChars) + "...";
            }

            Regex regex1 = new Regex(resultPatern);
            Match match = regex1.Match(input);

            return match.Groups[1].Value;
        }

        protected override Task<IEnumerable<object>> GetChildrenModels()
        {
            return Task.FromResult(Enumerable.Empty<object>());
        }
    }
}
