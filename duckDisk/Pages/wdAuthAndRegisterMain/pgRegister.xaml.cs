using duckDisk.data.api.user;
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
            tbLogin.BorderBrush = Brushes.Black;
            if (tbLogin.Text != "")
            {

                pbPassword.BorderBrush = Brushes.Black;
                pbPasswordRepeat.BorderBrush = Brushes.Black;

                string dsa1 = pbPassword.Password;
                string dsa = pbPasswordRepeat.Password;

                if ((pbPassword.Password != "") && (dsa1 == dsa))
                {
                    cbUserAgreement.Foreground = Brushes.Black;

                    if (cbUserAgreement.IsChecked == true)
                    {
                        var api = new UserNetworkApi();
                        var response = api.Register(new data.api.user.dto.JwtRequestDto
                        {
                            Email = tbLogin.Text,
                            Password = pbPassword.Password,
                        });
                        if (response.AccessToken != "")
                        {
                            var OpenWd = new wdMainInterface();
                            Application.Current.MainWindow.Hide();
                            OpenWd.ShowDialog();
                            if (OpenWd.checkLev)
                            {
                                tbLogin.Text = "";
                                pbPassword.Password = "";
                                pbPasswordRepeat.Password = "";
                                Application.Current.MainWindow.Show();
                                cbUserAgreement.IsChecked = false;
                            }
                            else
                            {
                                api.SignOut();
                                Application.Current.MainWindow.Close();
                            }
                        }

                    }
                    else
                    {
                        cbUserAgreement.Focus();
                        cbUserAgreement.Foreground = Brushes.Red;
                    }
                }
                else
                {
                    pbPassword.BorderBrush = Brushes.Red;
                    pbPasswordRepeat.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                tbLogin.BorderBrush = Brushes.Red;
            }


        }

        private void chUserAgreement(object sender, RoutedEventArgs e)
        {
            var openWd = new wdUserAgreement();
            openWd.ShowDialog();
            if (openWd.checkContinue)
            {
                cbUserAgreement.IsChecked = true;
            }
            else
                cbUserAgreement.IsChecked = false;
        }
    }
}
