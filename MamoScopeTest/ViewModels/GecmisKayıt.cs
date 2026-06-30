using MamoScopeTest.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq; // SQL'den veri çekebilmek için eklendi
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MamoScopeTest.ViewModels
{
    public class GecmisKayıtViewModel : INotifyPropertyChanged
    {
        // 1. ADIM: Tek bir kayıt yerine, veritabanından gelecek tüm satırları tutacak KOLEKSİYON oluşturuyoruz
        private ObservableCollection<TestMotorDriver> _gecmisTestler;
        public ObservableCollection<TestMotorDriver> GecmisTestler
        {
            get => _gecmisTestler;
            set { _gecmisTestler = value; OnPropertyChanged(nameof(GecmisTestler)); }
        }

        public ICommand YeniTestCommand { get; set; }

        public GecmisKayıtViewModel()
        {
            // Yeni test sayfasına geri dönme buton komutu
            YeniTestCommand = new RelayCommand(KayıtSayfasınıAc);

            // 3. ADIM: Sayfa açılır açılmaz veritabanına gidip verileri çekmesi için metodu tetikliyoruz
            VerileriVeritabanindanYukle();
        }

        // 2. ADIM: Veritabanından verileri çeken asıl metot
        private void VerileriVeritabanindanYukle()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // SQL Server'daki MotorDrivers tablosundaki tüm geçmiş verileri çekiyoruz
                    var sqlVerileri = db.MotorDriver.ToList();

                    // Çektiğimiz verileri arayüze bağlı olan koleksiyonumuza aktarıyoruz
                    GecmisTestler = new ObservableCollection<TestMotorDriver>(sqlVerileri);
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