using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class MainViewModel : BaseViewModel<ServiceCollection, Service, ServiceViewModel>
    {
        private ServiceViewModel _selectedService;
        private IStatusProbeService _probeService;

        public MainViewModel(IServiceProvider services, IStatusProbeService probeService)
            : base(services)
        {
            _probeService = probeService;
        }

        public string ModelFilePath { get; set; }
        public string ModelFileContent { get; set; }

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
                if (Uri.IsWellFormedUriString(ModelFilePath, UriKind.Absolute))
                {
                    var json = await _probeService.GetJsonFromUri(ModelFilePath);
                    return ServiceHealthStatus.ViewModel.Model.Model.LoadFrom(json);
                }
                return await ServiceHealthStatus.ViewModel.Model.Model.Load(ModelFilePath);
            }
            else
            {
                return ServiceHealthStatus.ViewModel.Model.Model.LoadFrom(ModelFileContent);
            }
        }
    }
}
