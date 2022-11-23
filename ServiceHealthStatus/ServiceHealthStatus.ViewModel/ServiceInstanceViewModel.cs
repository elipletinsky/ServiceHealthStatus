using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceHealthStatus.ViewModel.Model;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceHealthStatus.ViewModel
{
    public class ServiceInstanceViewModel : BaseViewModel<ServiceInstance, object, DummyViewModel>
    {
        public string Response 
        { 
            get => _response; 
            set 
            {
                _response = value;
                OnPropertyChanged();
            } 
        }

        private readonly IStatusProbeService _probeService;
        private string _response;

        public ServiceInstanceViewModel(IServiceProvider services, IStatusProbeService probeService)
                    : base(services)
        {
            _probeService = probeService;
        }

        protected override async Task DoExecuteProbe()
        {
            var result = await _probeService.Probe(Model.Url);
            if(result.body == string.Empty)
            {
                Response = "Failed";
            }
            else
            {
                Response = BuildResultPatern("", result.body);
            }
            
            Status = ((int)result.status >= 200) && ((int)result.status <= 299);
        }
        
        private string BuildResultPatern(string pattern, string input)
        {
            Regex regex1 = new Regex(ResultPattern);
            Match match = regex1.Match(input);
            
            return match.Groups[1].Value;
        }

        public Task Populate()
        {
            return Task.CompletedTask;
        }

      

        protected override Task<IEnumerable<object>> GetChildrenModels()
        {
            return Task.FromResult(Enumerable.Empty<object>());
        }
    }
}
