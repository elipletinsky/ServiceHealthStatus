using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public enum Status
    {
        Unprobed,
        InProgress,
        Success,
        Failure
    }
}
