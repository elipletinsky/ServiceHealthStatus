using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public interface IViewModel<TChildModel>
    {
        TChildModel Model { get; set; }
        Task Populate();
    }

    public class DummyViewModel : IViewModel<object>
    {
        public object Model { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task Populate()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseViewModel<TModel, TChildModel, TChildViewModel> : IViewModel<TModel>, INotifyPropertyChanged
        where TChildViewModel : IViewModel<TChildModel>
    {
        public TModel Model { get; set; }
        public bool Status { get; set; }
        public bool InProgress { get; set; }
        public List<TChildViewModel> Children { get; } = new List<TChildViewModel>();
        private readonly IServiceProvider _services;
        //public abstract async Task ExecuteProbe();
        

        protected BaseViewModel(IServiceProvider services)
        {
            _services = services;
            ExecuteProb = new RelayCommand(_ => true, _ => Console.WriteLine(this.GetType().Name));
        }

        public RelayCommand ExecuteProb { get; set; }

        protected virtual void CreateChild(string propertyName) { }

        protected TChildViewModel CreateChildViewModel(TChildModel model)
        {
            var viewModel = _services.GetRequiredService<TChildViewModel>();
            viewModel.Model = model;
            return viewModel;
        }
        public abstract bool CallChild();

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task Populate()
        {
            foreach (var childModel in await GetChildrenModels())
            {
                var childVm = CreateChildViewModel(childModel);
                Children.Add(childVm); 
                await childVm.Populate();
            }
        }

        protected abstract Task<IEnumerable<TChildModel>> GetChildrenModels();
    }
}
