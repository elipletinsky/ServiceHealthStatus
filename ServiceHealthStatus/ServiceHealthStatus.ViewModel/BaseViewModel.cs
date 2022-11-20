using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceHealthStatus.ViewModel.Model;

namespace ServiceHealthStatus.ViewModel
{
    public class DummyViewModel : IViewModel<object>
    {
        public object Model { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task Populate()
        {
            throw new NotImplementedException();
        }
        public Task PerfromExecuteProbe() => throw new NotImplementedException();
        
    }

    public abstract class BaseViewModel<TModel, TChildModel, TChildViewModel> : IViewModel<TModel>, INotifyPropertyChanged
        where TChildViewModel : IViewModel<TChildModel>
    {
        public TModel Model { get; set; }
        public bool Status { get; set; }
        public bool InProgress { get; set; }
        public ObservableCollection<TChildViewModel> Children { get; } = new ObservableCollection<TChildViewModel>();
        private readonly IServiceProvider _services;

        async Task IViewModel<TModel>.PerfromExecuteProbe()
        {
            try
            {
                InProgress = true;
                await DoExecuteProbe();
            }
            finally
            {
                InProgress= false;
            }
        }

        protected virtual async Task DoExecuteProbe()
        {
            foreach (var child in Children)
            {
                await child.PerfromExecuteProbe(); //ExecuteProbe.Execute(this);
            }
        }

        protected BaseViewModel(IServiceProvider services)
        {
            _services = services;
            ExecuteProbe = new RelayCommand(_ => true, async _ => await ((IViewModel<TModel>)this).PerfromExecuteProbe());
        }

        public RelayCommand ExecuteProbe { get; set; }

        protected virtual void CreateChild(string propertyName) { }

        protected TChildViewModel CreateChildViewModel(TChildModel model)
        {
            var viewModel = _services.GetRequiredService<TChildViewModel>();
            viewModel.Model = model;
            return viewModel;
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
