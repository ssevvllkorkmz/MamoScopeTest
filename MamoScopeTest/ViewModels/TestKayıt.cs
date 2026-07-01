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

        private string _voltajDegeri;
        public string VoltajDegeri
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
        public ICommand SimuleEtCommand { get; set; }
        public ICommand TestEtCommand { get; set; }
        public ICommand GecmisTestlerCommand { get; set; }


        public TestKayıtViewModel()
        {

            Tarih = DateTime.Now.ToString("dd.mm.yyyy HH:mm");

            SimuleEtCommand = new RelayCommand(TestVerisiSimuleEt);
            TestEtCommand = new RelayCommand(VoltajTest);
            GecmisTestlerCommand = new RelayCommand(GecmisKayıtlarıAc);
        }


        private void VoltajTest()
        {

            string temizVoltajMetni = VoltajDegeri?.Replace('.', ',') ?? "0";

          
            double gercekVoltaj = 0;

            
            double.TryParse(temizVoltajMetni, out gercekVoltaj);

            bool BasariliMi = false;

            if (gercekVoltaj >= 23.5 && gercekVoltaj <= 24.5)
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
                Voltage = gercekVoltaj,
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

        private void TestVerisiSimuleEt()
        {
            Random rnd = new Random();

            
            int rastgeleSayi = rnd.Next(1000, 9999); 
            string uretilenSeriNo = $"OPT-DRV-{rastgeleSayi}";

          
            double uretilenVoltaj = 20.0 + (rnd.NextDouble() * 6.0);
            uretilenVoltaj = Math.Round(uretilenVoltaj, 1);

            
            bool basariliMi = false;
            if (uretilenVoltaj >= 23.5 && uretilenVoltaj <= 24.5)
            {
                TestSonucu = "BAŞARILI";
                basariliMi = true;
            }
            else
            {
                TestSonucu = "BAŞARISIZ";
                basariliMi = false;
            }

           
            SeriNumarası = uretilenSeriNo;
            VoltajDegeri = uretilenVoltaj.ToString(); 

           
            var yeniKayit = new MotorDriver
            {
                SerialNumber = uretilenSeriNo,
                Voltage = uretilenVoltaj,
                TestDate = DateTime.Now,
                IsPassed = basariliMi
            };

            using (var db = new AppDbContext())
            {
                db.MotorDriver.Add(yeniKayit);
                db.SaveChanges();
            }

            System.Windows.MessageBox.Show($"Simülasyon Tamamlandı!\nSeri No: {uretilenSeriNo}\nVoltaj: {uretilenVoltaj}V\nSonuç: {TestSonucu}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}