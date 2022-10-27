using Microsoft.VisualBasic;
using ServiceHealthStatus.ViewModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public class ServiceViewModel : BaseViewModel<Service, Model.Environment, EnvironmentViewModel>
    {

        public ServiceViewModel(IServiceProvider services) : base(services)
        {
        }

        public override bool CallChild()
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<Model.Environment>> GetChildrenModels()
            => Task.FromResult((IEnumerable<Model.Environment>)Model.Environments);
    }
}
