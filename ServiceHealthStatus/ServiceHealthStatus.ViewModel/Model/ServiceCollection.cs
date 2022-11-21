using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel.Model
{
    public class ServiceCollection : ServiceItemBase
    {
        public IEnumerable<Service> Services { get; set; }
    }
}
