using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using Path = System.IO.Path;

namespace duckDisk.Windows
{
    /// <summary>
    /// Interaction logic for wdMainInterface.xaml
    /// </summary>
    public partial class wdMainInterface : Window
    {

        public class ClassFile
        {

            public string Name { get; set; }

            public string? imageTypeIcon { get; set; }

            public bool dsas = false;

            public string puthFile { get; set; }

            public List<ClassFile>? dssa { get; set; }

            public ClassFile(string name, string puth)
            {
                Name = name;
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



            listFile = new List<ClassFile>
            {
                new ClassFile("Папка", "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\")
                {
                    dssa = new List<ClassFile>
                    {
                        new ClassFile("Файл", "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.png")
                            {

                            },
                        new ClassFile("Файл", "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.png")
                            {

                            },
                        new ClassFile("Файл", "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.png")
                            {

                            },
                    }
                },
                
                new ClassFile("Файл", "C:\\Users\\hdnhd\\source\\repos\\duckDisk\\duckDisk\\Resources\\FileIcon.png")
                {

                },

            };

            lvMain.ItemsSource = listFile;
        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listFile[lvMain.SelectedIndex].dsas)
            {
                
                    listFile = listFile[lvMain.SelectedIndex].dssa;
                    lvMain.ItemsSource = listFile;
            }
            else
                MessageBox.Show("Скачивания");

        }
    }
}
