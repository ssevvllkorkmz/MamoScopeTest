using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using MamoScopeTest.Views; 
namespace MamoScopeTest.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        
        private object _currentView;

        
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainWindowViewModel()
        {
           
            CurrentView = new TestMotorDriver();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
