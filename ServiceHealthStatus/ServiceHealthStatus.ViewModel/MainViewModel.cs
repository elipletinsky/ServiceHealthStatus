using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class MainViewModel: BaseViewModel<IEnumerable<Service>, Service, ServiceViewModel>
    {
        public string ModelFilePath { get; set; } //@"j:\temp\CatalogServiceModel.json"
        public MainViewModel(IServiceProvider services) : base(services)
        {
           
        }

        protected override Task<IEnumerable<Service>> GetChildrenModels()
        {
            if (string.IsNullOrEmpty(ModelFilePath)) throw new InvalidDataException($"Not initialized {nameof(ModelFilePath)}");
            return Service.Load(ModelFilePath);
        }

        public override bool CallChild()
        {
            return true;
        }
    }
}
