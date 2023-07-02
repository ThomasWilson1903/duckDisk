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

namespace duckDisk.Pages.wdAuthAndRegisterMain
{
    /// <summary>
    /// Interaction logic for pgRegister.xaml
    /// </summary>
    public partial class pgRegister : Page
    {
        public pgRegister()
        {
            InitializeComponent();
            tbLogin.Focus();
        }

        private void pbKeyEnter(object sender, KeyEventArgs e)
        {

        }

        private void clAuthEnterSystem(object sender, RoutedEventArgs e)
        {

        }

        private void chUserAgreement(object sender, RoutedEventArgs e)
        {
            new wdUserAgreement().ShowDialog();
        }
    }
}
