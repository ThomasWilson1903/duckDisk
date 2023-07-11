using duckDisk.data.api.file;
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
using System.Windows.Shapes;

namespace duckDisk.Windows.wdPublic
{
    /// <summary>
    /// Interaction logic for wdEditPublic.xaml
    /// </summary>
    public partial class wdEditPublic : Window
    {

        string Url;
        string downloadLink;
        public wdEditPublic(string url, int fileId, bool ispublic)
        {

            Url = url;
            InitializeComponent();
            if (!ispublic)
            {
                var api = new FileNetworkApi();
                api.IsPublickChange(fileId);

            }

            string fileUrl = $"{Url}";
            Uri downloadUri = new Uri(fileUrl);
            downloadLink = downloadUri.ToString();

            tbUrl.Text = downloadLink;


        }

        private void clCopy(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText($"{downloadLink}");
        }

        private void kdWdClose(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
