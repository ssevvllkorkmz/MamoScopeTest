using MamoScopeTest.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq; 
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MamoScopeTest.ViewModels
{
    public class GecmisKayıtViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<MamoScopeTest.Models.MotorDriver> _gecmisTestler;

        public ObservableCollection<MamoScopeTest.Models.MotorDriver> GecmisTestler
        {
            get => _gecmisTestler;
            set { _gecmisTestler = value; OnPropertyChanged(nameof(GecmisTestler)); }
        }


        public ICommand YeniTestCommand { get; set; }

        public GecmisKayıtViewModel()
        {
         
            YeniTestCommand = new RelayCommand(KayıtSayfasınıAc);

            VerileriVeritabanindanYukle();
        }


        private void VerileriVeritabanindanYukle()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var sqlVerileri = db.MotorDriver.ToList();

                    
                    GecmisTestler = new ObservableCollection<MamoScopeTest.Models.MotorDriver>(sqlVerileri);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanından veriler yüklenirken bir hata oluştu: {ex.Message}");
            }

        }
        public void KayıtSayfasınıAc()
        {
            var mainWindow = Application.Current.MainWindow;

            if (mainWindow != null && mainWindow.DataContext is MainWindowViewModel mainVM)
            {
                mainVM.CurrentView = new TestMotorDriver();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}