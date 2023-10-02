﻿using System;
using System.Windows.Input;

namespace ServiceHealthStatus.ViewModel
{
    public class RelayCommand : ICommand
    {

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }



        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
