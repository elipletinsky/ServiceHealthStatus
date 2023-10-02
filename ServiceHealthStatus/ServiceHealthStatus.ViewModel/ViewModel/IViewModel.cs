using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public interface IViewModel<TChildModel> : IResultPatternHolder
    {
        TChildModel Model { get; set; }
        Task Populate();
        Task PerfromExecuteProbe();
        IResultPatternHolder Parent { get; set; }
        Status Status { get; }
    }

    public interface IResultPatternHolder
    {
        string ResultPattern { get; }
        void OnChildStatusChanged();
    }
}
