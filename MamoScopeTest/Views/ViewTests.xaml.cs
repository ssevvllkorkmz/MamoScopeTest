using MamoScopeTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MamoScopeTest.Views
{
    /// <summary>
    /// ViewTests.xaml etkileşim mantığı
    /// </summary>
    public partial class ViewTests : UserControl
    {
        public ViewTests()
        {
            InitializeComponent();
            
            this.DataContext = new GecmisKayıtViewModel();
        }
    }
}
