using duckDisk.data.api.folder;
using duckDisk.data.api.folder.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml.Linq;
using Path = System.IO.Path;

namespace duckDisk.Windows
{
    /// <summary>
    /// Interaction logic for wdMainInterface.xaml
    /// </summary>
    public partial class wdMainInterface : Window
    {
        /// <summary>
        /// Back button
        /// create, upload button
        /// 
        /// filtering by folder, photo, trash
        /// 
        /// </summary>


        public class ClassFile
        {

            public string Name { get; set; }

            public string? imageTypeIcon { get; set; }

            public bool dsas = false;

            public string puthFile { get; set; }

            public List<ClassFile>? dssa { get; set; }

            public ClassFile(string puth)
            {

                string extension = Path.GetExtension(puth);

                if (!string.IsNullOrEmpty(extension))
                {
                    string[] words = puth.Split(new char[] { '\\' });

                    Name = words.Last();

                }

                else
                {
                    string[] words = puth.Split(new char[] { '\\' });
                    Name = puth;
                }


                puthFile = puth;


                if (Path.HasExtension(puth))
                {
                    imageTypeIcon = "\\Resources\\FileIcon.png";

                }
                else
                {
                    imageTypeIcon = "\\Resources\\folderIcon.png";
                    dsas = true;
                }


            }
        }

        List<ClassFile> listFile;
        public wdMainInterface()
        {
            InitializeComponent();

            //selectGetDirectories();
            FolderNetworkApi api = new FolderNetworkApi();
            api.Add(DateTime.Now.ToString());
            var dsa = api.GetAll();
            
            dgTest.ItemsSource = dsa.Content;

        }

        void selectGetDirectories(string puth = "C:\\TestFile")
        {
            /*if (!Path.HasExtension(puth))
            {
                string[] Files = Directory.GetFiles(puth);
                string[] dirss = Directory.GetDirectories(puth);
                List<ClassFile> ds = new List<ClassFile>();
                foreach (string file in dirss)
                {
                    var adas = new ClassFile(file);
                    ds.Add(adas);
                }
                foreach (string file in Files)
                {
                    var adas = new ClassFile(file);
                    ds.Add(adas);
                }
                listFile = ds;
                lvMain.ItemsSource = listFile;
            }
            else
                return;
*/

        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*if (listFile[lvMain.SelectedIndex].dsas)
            {
                selectGetDirectories(listFile[lvMain.SelectedIndex].puthFile);
            }
            else
                MessageBox.Show("Скачивания");*/

        }

        private void clBackEnd(object sender, RoutedEventArgs e)
        {
            /*listFile = new List<ClassFile>
            {
                new ClassFile(1, "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\")
                {
                    dssa = new List<ClassFile>
                    {
                        new ClassFile(2, "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.png")
                            {

                            },
                        new ClassFile(2, "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.png")
                            {

                            },
                        new ClassFile(2, "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.png")
                            {

                            },
                    }
                },

                new ClassFile(1, "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.jpg")
                {

                },

            };*/
            //lvMain.ItemsSource = listFile;
        }

        private void clOpenImage(object sender, RoutedEventArgs e)
        {

        }
    }
}
