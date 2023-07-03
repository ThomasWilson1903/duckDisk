using duckDisk.data.api.folder;
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

namespace duckDisk.Windows.wdCreateItem
{
    /// <summary>
    /// Interaction logic for wdCreateFolder.xaml
    /// </summary>
    public partial class wdCreateFolder : Window
    {
        int? Folder;
        public wdCreateFolder(int? folder)
        {
            Folder = folder;
            InitializeComponent();
            tbNameFolder.Focus();
        }

        private void clSaveEdit(object sender, RoutedEventArgs e)
        {
            creatFolder();
        }

        void creatFolder()
        {
            var api = new FolderNetworkApi();
            if (tbNameFolder.Text != "")
            {
                api.Add(tbNameFolder.Text, Folder);
            }
            else
                tbNameFolder.BorderBrush = Brushes.Red;
            Close();
        }

        private void kdWdClose(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void kdCreatFolder(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                creatFolder();
            }
        }
    }
}
