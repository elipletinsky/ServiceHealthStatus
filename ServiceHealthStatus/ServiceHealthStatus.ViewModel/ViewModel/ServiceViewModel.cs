using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class ServiceViewModel : BaseViewModel<Service, Model.Environment, EnvironmentViewModel>
    {

        public ServiceViewModel(IServiceProvider services)
            : base(services)
        {
        }

        private EnvironmentViewModel _selectedEnvironment;
        public EnvironmentViewModel SelectedEnvironment
        {
            get => _selectedEnvironment;
            set { _selectedEnvironment = value; OnPropertyChanged(nameof(SelectedEnvironment)); }
        }

        protected override Task<IEnumerable<Model.Environment>> GetChildrenModels()
            => Task.FromResult((IEnumerable<Model.Environment>)Model.Environments);
    }
}
