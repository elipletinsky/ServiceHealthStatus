using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class MainViewModel: BaseViewModel<ServiceCollection, Service, ServiceViewModel>
    {
        public string ModelFilePath { get; set; }
        public MainViewModel(IServiceProvider services) 
            : base(services)
        {
           
        }

        private ServiceViewModel _selectedService;
        public ServiceViewModel SelectedService
        {
            get => _selectedService;
            set { _selectedService = value; OnPropertyChanged(nameof(SelectedService)); }
        }

        protected override Task<IEnumerable<Service>> GetChildrenModels()
        {
            if (string.IsNullOrEmpty(ModelFilePath)) throw new InvalidDataException($"Not initialized {nameof(ModelFilePath)}");
            return Service.Load(ModelFilePath);
        }

       
    }
}
