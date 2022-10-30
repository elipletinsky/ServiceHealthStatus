using ServiceHealthStatus.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public class EnvironmentViewModel : BaseViewModel<Model.Environment, Model.ServiceInstance, ServiceInstanceViewModel>
    {
        public EnvironmentViewModel(IServiceProvider services) : base(services)
        {
        }


        public override bool CallChild()
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<ServiceInstance>> GetChildrenModels()
        {
            return Task.FromResult((IEnumerable<ServiceInstance>)Model.Instances);
        }
    }
}
