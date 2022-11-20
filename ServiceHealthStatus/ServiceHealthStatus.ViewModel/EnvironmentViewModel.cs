using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class EnvironmentViewModel : BaseViewModel<Model.Environment, Model.ServiceInstance, ServiceInstanceViewModel>
    {
        public EnvironmentViewModel(IServiceProvider services)
            : base(services)
        {
        }

        private ServiceInstanceViewModel _selectedServiceInstance;
        public ServiceInstanceViewModel SelectedServiceInstance
        {
            get => _selectedServiceInstance;
            set { _selectedServiceInstance = value; OnPropertyChanged(nameof(SelectedServiceInstance)); }
        }


        protected override Task<IEnumerable<ServiceInstance>> GetChildrenModels()
        {
            return Task.FromResult((IEnumerable<ServiceInstance>)Model.Instances);
        }
    }
}
