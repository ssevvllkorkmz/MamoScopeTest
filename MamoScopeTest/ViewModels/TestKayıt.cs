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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
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
            
            if (string.IsNullOrEmpty(VoltajDegeri) || string.IsNullOrEmpty(SeriNumarası))
            {
                System.Windows.MessageBox.Show("Lütfen önce voltaj ve seri numarası girin veya simüle edin!");
                return;
            }

            
            string temizVoltajMetni = VoltajDegeri?.Replace('.', ',') ?? "0";
            double gercekVoltaj = 0;
            double.TryParse(temizVoltajMetni, out gercekVoltaj);

            bool BasariliMi = false;

            if (gercekVoltaj >= 23.5 && gercekVoltaj <= 24.5)
            {
                TestSonucu = "BAŞARILI";
                BasariliMi = true;
            }
            else
            {
                TestSonucu = "BAŞARISIZ";
                BasariliMi = false;
            }

            var yeniKayit = new MotorDriver
            {
                SerialNumber = this.SeriNumarası ?? "",
                Voltage = gercekVoltaj,
                TestDate = DateTime.Now,
                IsPassed = BasariliMi
            };

            
            try
            {
                using (var db = new AppDbContext())
                {
                    db.MotorDriver.Add(yeniKayit);
                    db.SaveChanges();
                }

                System.Windows.MessageBox.Show($"Test Sonuçlandırıldı!\nSeri No: {SeriNumarası}\nVoltaj: {gercekVoltaj}V\nSonuç: {TestSonucu}");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Veri tabanı hatası: {ex.Message}\n\nLütfen SQL Server bağlantınızı veya AppDbContext dosyasındaki ConnectionString'i kontrol edin.");
            }
        }

        private void TestVerisiSimuleEt()
        {
            Random rnd = new Random();

            int rastgeleSayi = rnd.Next(1000, 9999);
            SeriNumarası = $"OPT-DRV-{rastgeleSayi}";

            double uretilenVoltaj = 20.0 + (rnd.NextDouble() * 6.0);
            uretilenVoltaj = Math.Round(uretilenVoltaj, 1);
            VoltajDegeri = uretilenVoltaj.ToString();

            TestSonucu = "";
        }

        private async void GecmisKayıtlarıAc()
        {
            
            IsLoading = true;

            await Task.Delay(50);

            var yeniSayfa = new ViewTests();
           
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null && mainWindow.DataContext is MainWindowViewModel mainVM)
            {
                mainVM.CurrentView = yeniSayfa;
            }

            
            IsLoading = false;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}