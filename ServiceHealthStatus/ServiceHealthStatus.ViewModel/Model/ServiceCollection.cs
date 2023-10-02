using System.Collections.Generic;

namespace ServiceHealthStatus.ViewModel.Model
{
    public class ServiceCollection : ServiceItemBase
    {
        public IEnumerable<Service> Services { get; set; }
    }
}
