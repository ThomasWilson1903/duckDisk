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
    /// Interaction logic for pgAuth.xaml
    /// </summary>
    public partial class pgAuth : Page
    {
        public pgAuth()
        {
            InitializeComponent();
            tbLogin.Focus();
            tbLogin.Text = "string";
            pbPassword.Password = "string";
        }

        private void clAuthEnterSystem(object sender, RoutedEventArgs e)
        {
            EnterSystem();
        }

        void EnterSystem()
        {
            var UserApi = new UserNetworkApi();
            var respons = UserApi.Login(new data.api.user.dto.JwtRequestDto { Email = tbLogin.Text, Password = pbPassword.Password });

            if (respons.AccessToken != "")
            {
                Application.Current.MainWindow.Hide();
                var openWd = new wdMainInterface();
                openWd.ShowDialog();
                if (openWd.checkLev)
                {
                    Application.Current.MainWindow.Show();
                }
                else
                {
                    UserApi.SignOut();
                    Application.Current.MainWindow.Close();

                }
            }
            else
            {
                MessageBox.Show("проверьте логин или пароль", "Ошибка ввода данный");
            }
        }

        private void pbKeyEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterSystem();
            }
        }
    }
}
