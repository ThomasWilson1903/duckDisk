using duckDisk.data.api.file;
using duckDisk.data.api.file.model;
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

            public ClassFile(Folder? folder, FileModel? fileModels)
            {
                if (folder != null)
                {
                    Name = folder.Name;
                    imageTypeIcon = "\\Resources\\folderIcon.png";

                }
                else
                {

                    Name = fileModels.Name;
                    imageTypeIcon = "\\Resources\\FileIcon.png";


                }




                /*if (Path.HasExtension(puth))
                {
                    imageTypeIcon = "\\Resources\\FileIcon.png";

                }
                else
                {
                    imageTypeIcon = "\\Resources\\folderIcon.png";
                    dsas = true;
                }*/


            }
        }

        List<ClassFile> listFile;
        public wdMainInterface()
        {
            InitializeComponent();

            //selectGetDirectories();
            FolderNetworkApi api = new FolderNetworkApi();
            FileNetworkApi FilsApi = new FileNetworkApi();
            api.Add(DateTime.Now.ToString());
            var dsa = api.GetAll();

            List<ClassFile> classFiles = new List<ClassFile>();
            /*foreach (Folder fils in dsa.Content)
            {
                var itemAdd = new ClassFile(fils,null);
                classFiles.Add(itemAdd);
            }*/

            var listFils = FilsApi.GetAll();
            foreach (FileModel fileModel in listFils.Content)
            {
                var itemAdd = new ClassFile(null, fileModel);
                classFiles.Add(itemAdd);
            }

            dgTest.ItemsSource = classFiles;
        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void clBackEnd(object sender, RoutedEventArgs e)
        {
            
        }

        private void clOpenImage(object sender, RoutedEventArgs e)
        {

        }
    }
}
