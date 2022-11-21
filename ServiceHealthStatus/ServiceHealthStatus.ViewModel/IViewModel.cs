using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public interface IViewModel<TChildModel> : IResultPatternHolder
    {
        TChildModel Model { get; set; }
        Task Populate();
        Task PerfromExecuteProbe();
        IResultPatternHolder Parent { get; set; }
    }
    public interface IResultPatternHolder
    {
        string ResultPattern { get;}
    }
}
