# MamoScopeTest - Motor Sürücü Test Otomasyon Yazılımı

MamoScopeTest, motor sürücü kartlarının voltaj ve kalite kontrol testlerini gerçekleştirmek, test sonuçlarını yerel bir veri tabanında saklamak ve geriye dönük test geçmişini listelemek amacıyla geliştirilmiş "WPF (MVVM)" tabanlı bir masaüstü yazılımıdır.

Kaynak Kod Yapısı:
MamascopeTest
 -Models
   --Product.cs // tanımlama sınıfı
 -Viewmodel
   --GecmisKayıt.cs // geçmiş sayfaları görüntüleyebilmek için yazdığım sınıf
   --TestKayıt.cs // kayıt,veri test simülasyonunun gerçekleşmesi için yazdığım sınıf
 -Views
   --MainWindowViewModel.cs // sayfalar arası geçiş sağlayan sınıf
   --TestMotorDriver.xmal // kayıt sayfasını görüntüleyen arayüz dosyası
   --ViewTests.xmal // geçmiş kayıtların görüntülendiği arayüz dosyası
 -AppDbContext.cs // Entity Framework bağlantı sınıfı
 -RelayCommand.cs // command özelliklerini kullanabilmek için gereken yardıcı sınıf

Veri Tabanı Yapısı:
Entity Framework Core kullanarak modelledim.
Sistem, `AppDbContext` sınıfı aracılığıyla MS SQL Server'a bağlandı ve C# nesnelerini ilişkisel veri tabanı tablolarına dönüştü.
Data/AppDbContext.cs` dosyasındaki `ConnectionString` alanına kendi yerel SQL Server adresimi yazdım.
Visual Studio altındaki "Package Manager Console" ekranını açtım.
Sırasıyla şu EF Core komutlarını çalıştırarak tablomu ve sütunlarımı oluşturdum
   --Add-Migration InitialCreate
   --Update-Database

Uygulama Çalıştırılması:
Kullanıcı isterse manuel olarak girdiği seri numarası ve voltaj değerini test edebilir ya da "test verisi simüle et" butonuna basarak random değerlerle test yapabilir.
Geçmiş kayıtları görüntüle butonuyla yaptığı kayıtların listesine göz atabilir.
