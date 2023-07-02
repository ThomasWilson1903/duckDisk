using duckDisk.data.api.user;
using duckDisk.Pages.wdAuthAndRegisterMain;
using duckDisk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace duckDisk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            openAuth();
        }

        private void clOpenPageAuth(object sender, RoutedEventArgs e)
        {
            openAuth();
        }

        public void openAuth()
        {
            frMainAuthAndRegister.Navigate(new pgAuth());
            btOpenAyth.BorderThickness = new Thickness(0, 0, 0, 2);
            btOpenRegister.BorderThickness = new Thickness(0, 0, 0, 0);
        }
        private void clOpenPageRegister(object sender, RoutedEventArgs e)
        {
            frMainAuthAndRegister.Navigate(new pgRegister());
            btOpenRegister.BorderThickness = new Thickness(0, 0, 0, 2);
            btOpenAyth.BorderThickness = new Thickness(0, 0, 0, 0);

        }
    }
}
