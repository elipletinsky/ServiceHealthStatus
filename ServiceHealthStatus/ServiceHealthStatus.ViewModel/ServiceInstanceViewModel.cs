using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class ServiceInstanceViewModel: BaseViewModel<ServiceInstance, object, DummyViewModel>
    {
        public string Response { get; set; }

        public ServiceInstanceViewModel(IServiceProvider services)
            : base(services)
        {

        }

        public Task Populate()
        {
            return Task.CompletedTask;
        }

        public override bool CallChild()
        {
            bool noProblem = true;//temp
            return noProblem;
        }

        protected override Task<IEnumerable<object>> GetChildrenModels()
        {
            return Task.FromResult(Enumerable.Empty<object>());
        }
    }
}
