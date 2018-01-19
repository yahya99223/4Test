﻿using System;
using System.ComponentModel;
using System.Windows.Input;

namespace OrderManagement.ViewModel
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67
    }

    public class TextConverter
    {
        private readonly Func<string, string> convertion;

        public TextConverter(Func<string, string> convertion)
        {
            this.convertion = convertion;
        }

        public string ConvertText(string inputText)
        {
            return convertion(inputText);
        }
    }
}