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

namespace duckDisk.Windows
{
    /// <summary>
    /// Interaction logic for wdMainInterface.xaml
    /// </summary>
    public partial class wdMainInterface : Window
    {
        public class dsa
        {

            string Name;

            public string? imageTypeIcon;

            public dsa(string name)
            {
                Name = name;
               
            }
        }


        public wdMainInterface()
        {
            InitializeComponent();

            List<dsa> listFile = new List<dsa>
            {
                new dsa("ds")
                {

                    imageTypeIcon = "\\Resources\\draft.jpg"
                },
                new dsa("dss")
                {

                    imageTypeIcon = "\\Resources\\folderIcon.png"
                },
                new dsa("dsw")
                {

                    imageTypeIcon = "\\Resources\\folderIcon.png"
                }
            };
            lvMain.ItemsSource = listFile;
        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
