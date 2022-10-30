using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel.Model
{
    public class Environment : ServiceItemBase
    {
        public ServiceInstance[] Instances { get; set; }
    }
}
