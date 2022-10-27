using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public interface IViewModel<TChildModel>
    {
        TChildModel Model { get; set; }
        Task Populate();
    }
}
