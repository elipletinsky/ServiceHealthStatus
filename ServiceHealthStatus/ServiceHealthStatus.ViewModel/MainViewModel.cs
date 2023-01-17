using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class MainViewModel: BaseViewModel<ServiceCollection, Service, ServiceViewModel>
    {
        public string ModelFilePath { get; set; }
        public string ModelFileContent { get; set; }
        private IStatusProbeService _probeService;
        public MainViewModel(IServiceProvider services, IStatusProbeService probeService) 
            : base(services)
        {
            _probeService = probeService;
        }

        private ServiceViewModel _selectedService;
        public ServiceViewModel SelectedService
        {
            get => _selectedService;
            set { _selectedService = value; OnPropertyChanged(nameof(SelectedService)); }
        }

        protected override async Task<IEnumerable<Service>> GetChildrenModels()
        {
            if (string.IsNullOrEmpty(ModelFilePath) && string.IsNullOrEmpty(ModelFileContent)) 
                throw new InvalidDataException($"Not initialized {nameof(ModelFilePath)}" +
                                               $"Not initialized {nameof(ModelFileContent)}");
            
            if (!string.IsNullOrEmpty(ModelFilePath) && string.IsNullOrEmpty(ModelFileContent))
            {
                if(Uri.IsWellFormedUriString(ModelFilePath, UriKind.Absolute))
                {
                    var json = await _probeService.GetJsonFromUri(ModelFilePath);
                    return Service.LoadFrom(json);
                }
                return await Service.Load(ModelFilePath);
            }
            else
            {
                return Service.LoadFrom(ModelFileContent);
            }
        }
    }
}
