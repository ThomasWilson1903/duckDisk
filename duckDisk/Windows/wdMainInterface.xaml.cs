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
            public int Id { get; set; }

            public string Name { get; set; }

            public string? imageTypeIcon { get; set; }

            public Boolean FileInFolder { get; set; }


            public ClassFile(Folder? folder, FileModel? fileModels)
            {
                if (folder != null)
                {
                    Id = folder.Id;
                    Name = folder.Name;
                    imageTypeIcon = folder.getIcon();
                    FileInFolder = true;

                }
                else
                {
                    Id = fileModels.Id;
                    Name = fileModels.Name;
                    imageTypeIcon = "\\Resources\\FileIcon.png";
                    FileInFolder = false;

                }
            }
        }

        List<ClassFile> classFiles = new List<ClassFile>();
        public wdMainInterface()
        {
            InitializeComponent();
            selectFolder();
        }
        void selectFolder(int? folder = null)
        {
            if (folder != null)
            {
                tbNavigation.Text = $"home/.../{classFiles[lvMain.SelectedIndex].Name}";
            }
            else
                tbNavigation.Text = $"home/.../";

            lvMain.ItemsSource = null;
            classFiles.Clear();
            FolderNetworkApi api = new FolderNetworkApi();
            FileNetworkApi FilsApi = new FileNetworkApi();

            var dsa = api.GetAll(folder);


            foreach (Folder fils in dsa.Content)
            {
                var itemAdd = new ClassFile(fils, null);
                classFiles.Add(itemAdd);
            }

            var listFils = FilsApi.GetAll(folder);
            foreach (FileModel fileModel in listFils.Content)
            {
                var itemAdd = new ClassFile(null, fileModel);
                classFiles.Add(itemAdd);
            }

            lvMain.ItemsSource = classFiles;
            if (folder != null)
            {
                BackIndex = folder.Value;
            }
        }
        int BackIndex;

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvMain.SelectedIndex != -1)
            {
                if (classFiles[lvMain.SelectedIndex].FileInFolder)
                {
                    selectFolder(classFiles[lvMain.SelectedIndex].Id);
                }
                else
                    MessageBox.Show("download");
            }
        }

        private void clBackEnd(object sender, RoutedEventArgs e)
        {
            selectFolder();
        }

        private void clOpenImage(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            var del = classFiles[lvMain.SelectedIndex];
            if (del != null)
            {
                if (MessageBox.Show($"{del.Name} удалить?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                }
            }
        }

        private void dsa(object sender, RoutedEventArgs e)
        {


        }
    }
}
