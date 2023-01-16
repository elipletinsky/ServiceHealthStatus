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

    public abstract class BaseViewModel<TModel, TChildModel, TChildViewModel>  :  IViewModel<TModel>, INotifyPropertyChanged
        where TChildViewModel : IViewModel<TChildModel>
        where TModel : IResultPatternHolderModel
    {
        
        private bool _inProgress;
        public TModel Model { get; set; }

        private Status _status;

        public Status Status 
        { 
            get => _status; 
            set
            {
                _status = value;
                OnPropertyChanged();
                Parent?.OnChildStatusChanged();
            }
        }

        public bool InProgress {
            get => _inProgress;
            set
            {
                _inProgress = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<TChildViewModel> Children { get; } = new ObservableCollection<TChildViewModel>();
        private readonly IServiceProvider _services;
        public IResultPatternHolder Parent { get; set;}
        public virtual string ResultPattern
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Model?.ResultPattern))
                {
                    return Parent?.ResultPattern;
                }
                else
                {
                    return Model.ResultPattern;
                }
            }
        }

        public void OnChildStatusChanged() 
        {
            Status tempStatus = Status.Unprobed;
            int successfulChildrenCounter = 0;
            foreach(var child in Children)
            {
                if (child.Status == Status.InProgress)
                {
                    tempStatus = Status.InProgress;
                    break;
                }

                if (child.Status == Status.Failure)
                {
                    tempStatus = Status.Failure;
                    break;
                }
                if (child.Status == Status.Success)
                {
                    successfulChildrenCounter++;
                }

            }
            if(successfulChildrenCounter == Children.Count)
            {
                tempStatus = Status.Success;
            }
            Status = tempStatus;
        }

        async Task IViewModel<TModel>.PerfromExecuteProbe()
        {
            try
            {
                Status= Status.InProgress;
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
                await child.PerfromExecuteProbe();
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
            viewModel.Parent = this;
            return viewModel;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Populate()
        {
            Children.Clear();
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
