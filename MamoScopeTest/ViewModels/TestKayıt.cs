using MamoScopeTest.Models;
using MamoScopeTest.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Input;
using System.Windows;

namespace MamoScopeTest.ViewModels
{
    public class TestKayıtViewModel : INotifyPropertyChanged
    {


        private string _seriNumarası;
        public string SeriNumarası
        {
            get => _seriNumarası;
            set { _seriNumarası = value; OnPropertyChanged(nameof(SeriNumarası)); }
        }

        private double _voltajDegeri;
        public double VoltajDegeri
        {
            get => _voltajDegeri;
            set { _voltajDegeri = value; OnPropertyChanged(nameof(VoltajDegeri)); }
        }

        private string _tarih;
        public string Tarih
        {
            get => _tarih;
            set { _tarih = value; OnPropertyChanged(nameof(Tarih)); }
        }

        private string _testSonucu;
        public string TestSonucu
        {
            get => _testSonucu;
            set { _testSonucu = value; OnPropertyChanged(nameof(TestSonucu)); }
        }

        public ICommand TestEtCommand { get; set; }
        public ICommand GecmisTestlerCommand { get; set; }


        public TestKayıtViewModel()
        {

            Tarih = DateTime.Now.ToString("dd.mm.yyyy HH:mm");

            TestEtCommand = new RelayCommand(VoltajTest);
            GecmisTestlerCommand = new RelayCommand(GecmisKayıtlarıAc);
        }


        private void VoltajTest()
        {

            bool BasariliMi = false;

            if (VoltajDegeri >= 23.5 && VoltajDegeri <= 24.5)
            {
                TestSonucu = $"BAŞARILI";
                BasariliMi = true;
            }
            else
            {
                TestSonucu = $"BAŞARISIZ";
                BasariliMi = false;
            }

            var yeniKayit = new MotorDriver
            {
                SerialNumber = this.SeriNumarası ?? "",
                Voltage = this.VoltajDegeri,
                TestDate = DateTime.Now,
                IsPassed = BasariliMi
            };

            using (var db = new AppDbContext())
            {
                db.MotorDriver.Add(yeniKayit);
                db.SaveChanges();
            }

        }

        private void GecmisKayıtlarıAc()
        {
            var mainWindow = Application.Current.MainWindow;

            if (mainWindow != null && mainWindow.DataContext is MainWindowViewModel mainVM)
            {
                
                mainVM.CurrentView = new ViewTests();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}