using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Shapes;

namespace duckDisk.Windows
{
    /// <summary>
    /// Interaction logic for wdUserAgreement.xaml
    /// </summary>
    public partial class wdUserAgreement : Window
    {
        public bool checkContinue = false;

        public wdUserAgreement()
        {
            InitializeComponent();

            var MyList = new List<string>();
            using (var streamReader = new StreamReader("UserAgreement.txt", Encoding.UTF8))
            {
                var s = string.Empty;
                while ((s = streamReader.ReadLine()) != null)
                    MyList.Add(s);
            }
            CultureInfo culture = new CultureInfo("ru-RU");
            MyList.Add($"Подпись пользователя: _______________\r\n" +
                       $"Дата: {DateTime.Today.ToString("d",culture)}");
            MyList.Add($"");
            lbTextString.ItemsSource = MyList;

        }

        private void clContinue(object sender, RoutedEventArgs e)
        {
            checkContinue = true;
            Close();
        }

        private void clRefusal(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
