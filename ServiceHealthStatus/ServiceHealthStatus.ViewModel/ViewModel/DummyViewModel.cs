using System;
using System.Threading.Tasks;


namespace ServiceHealthStatus.ViewModel
{
    public class DummyViewModel : IViewModel<object>
    {
        public object Model { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ResultPattern => throw new NotImplementedException();

        IResultPatternHolder IViewModel<object>.Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Status Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task Populate()
        {
            throw new NotImplementedException();
        }
        public Task PerfromExecuteProbe() => throw new NotImplementedException();

        public void OnChildStatusChanged()
        {
            throw new NotImplementedException();
        }
    }
}
