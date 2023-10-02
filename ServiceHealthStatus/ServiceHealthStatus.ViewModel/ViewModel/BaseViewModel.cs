using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace ServiceHealthStatus.ViewModel
{

    public abstract class BaseViewModel<TModel, TChildModel, TChildViewModel> : ObservableObject, IViewModel<TModel>
        where TChildViewModel : IViewModel<TChildModel>
        where TModel : IResultPatternHolderModel
    {

        private bool _inProgress;
        private readonly IServiceProvider _services;
        private Status _status;

        protected BaseViewModel(IServiceProvider services)
        {
            _services = services;
            ExecuteProbe = new RelayCommand(_ => true, async _ => await ((IViewModel<TModel>)this).PerfromExecuteProbe());
        }

        public TModel Model { get; set; }

        public RelayCommand ExecuteProbe { get; set; }

        public IResultPatternHolder Parent { get; set; }

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

        public bool InProgress
        {
            get => _inProgress;
            set
            {
                _inProgress = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TChildViewModel> Children { get; } = new ObservableCollection<TChildViewModel>();
        
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
            foreach (var child in Children)
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
            if (successfulChildrenCounter == Children.Count)
            {
                tempStatus = Status.Success;
            }
            Status = tempStatus;
        }

        async Task IViewModel<TModel>.PerfromExecuteProbe()
        {
            try
            {
                Status = Status.InProgress;
                InProgress = true;
                await DoExecuteProbe();
            }
            finally
            {
                InProgress = false;
            }
        }

        protected virtual async Task DoExecuteProbe()
        {
            foreach (var child in Children)
            {
                await child.PerfromExecuteProbe();
            }
        }
        
        protected virtual void CreateChild(string propertyName) { }

        protected TChildViewModel CreateChildViewModel(TChildModel model)
        {
            var viewModel = _services.GetRequiredService<TChildViewModel>();
            viewModel.Model = model;
            viewModel.Parent = this;
            return viewModel;
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
