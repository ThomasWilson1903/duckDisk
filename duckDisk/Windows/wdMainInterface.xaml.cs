using duckDisk.data.api.file;
using duckDisk.data.api.file.model;
using duckDisk.data.api.folder;
using duckDisk.data.api.folder.model;
using duckDisk.Windows.wdCreateItem;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;

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

            public string UriString { get; set; } 


            public ClassFile(FolderModel? folder, FileModel? fileModels)
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
                    UriString = fileModels.Url;
                }
            }
        }

        public bool checkLev = false;

        List<ClassFile> classFiles = new List<ClassFile>();
        public wdMainInterface()
        {
            InitializeComponent();
            ShowSelectFolder();
        }

        int? selectFolder = null;

        string? selectFolderName = null;

        void ShowSelectFolder(int? folder = null)
        {
            if (folder != null)
            {
                selectFolder = folder;
            }

            if (lvMain.SelectedIndex != -1)
            {
                selectFolderName = classFiles[lvMain.SelectedIndex].Name;
            }

            if (folder != null)
            {
                tbNavigation.Text = $"home/.../{selectFolderName}";
            }
            else
                tbNavigation.Text = $"home/.../";

            lvMain.ItemsSource = null;
            classFiles.Clear();
            FolderNetworkApi api = new FolderNetworkApi();
            FileNetworkApi FilsApi = new FileNetworkApi();

            var dsa = api.GetAll(folder);


            foreach (FolderModel fils in dsa.Content)
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
        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvMain.SelectedIndex != -1)
            {
                if (classFiles[lvMain.SelectedIndex].FileInFolder)
                {
                    ShowSelectFolder(classFiles[lvMain.SelectedIndex].Id);
                }
                else
                    MessageBox.Show("download");
            }
        }

        private void clBackEnd(object sender, RoutedEventArgs e)
        {
            ShowSelectFolder();
        }

        private void clOpenImage(object sender, RoutedEventArgs e)
        {

        }


        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvMain.SelectedIndex != -1)
            {
                var del = classFiles[lvMain.SelectedIndex];

                if (del != null)
                {
                    if (MessageBox.Show($"{del.Name} удалить?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (del.FileInFolder)
                        {

                            var api = new FolderNetworkApi();
                            api.Delete(del.Id);
                            ShowSelectFolder(selectFolder);
                        }
                        else
                        {
                            var api = new FileNetworkApi();
                            api.Delete(del.Id);
                            ShowSelectFolder(selectFolder);
                        }
                    }
                }
            }
        }

        private void dsa(object sender, RoutedEventArgs e)
        {


        }

        private void clAddFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt"; // Required file extension 
            fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Optional file extensions
            fileDialog.ShowDialog();

            var api = new FileNetworkApi();
            if (fileDialog != null)
            {
                byte[] buffer = File.ReadAllBytes(fileDialog.FileName);
                api.Add(fileDialog.SafeFileName, buffer, selectFolder);
            }
            ShowSelectFolder(selectFolder);

        }

        private void clExitSystem(object sender, RoutedEventArgs e)
        {
            checkLev = true;
            Close();
        }

        /// <summary>
        /// Apply Blur Effect on the window
        /// </summary>
        /// <param name=”win”></param>
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 4;
            win.Effect = objBlur;
        }

        /// <summary>
        /// Remove Blur Effects
        /// </summary>
        /// <param name=”win”></param>
        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }

        private void MenuItemCreate_Click(object sender, RoutedEventArgs e)
        {
            new FolderNetworkApi().Add(showWdCreatfolder(), selectFolder);

            ShowSelectFolder(selectFolder);
        }

        string showWdCreatfolder(string? classFile = "", bool FileInFolder = true)
        {
            wdCreateFolder newWindow = new wdCreateFolder(classFile, FileInFolder);  // Создание нового экземпляра окна

            // Установка родительского окна для нового окна
            newWindow.Owner = this;

            // Расчет координат центра родительского окна
            double parentWindowLeft = this.Left;
            double parentWindowTop = this.Top;
            double parentWindowWidth = this.Width;
            double parentWindowHeight = this.Height;
            double newWindowLeft = parentWindowLeft + (parentWindowWidth - newWindow.Width) / 2;
            double newWindowTop = parentWindowTop + (parentWindowHeight - newWindow.Height) / 2;

            // Установка координат центра нового окна
            newWindow.Left = newWindowLeft;
            newWindow.Top = newWindowTop;
            ApplyEffect(this);
            // Открытие нового окна


            newWindow.ShowDialog();


            ClearEffect(this);
            return newWindow.returnString;
        }

        private void MenuItemRename_Click(object sender, RoutedEventArgs e)
        {
            if (lvMain.SelectedIndex != -1)
            {
                var rename = classFiles[lvMain.SelectedIndex];
                if (rename.FileInFolder)
                {
                    var stringName = showWdCreatfolder(rename.Name);
                    if (stringName != null)
                    {
                        new FolderNetworkApi().Rename(classFiles[lvMain.SelectedIndex].Id, stringName);
                        ShowSelectFolder(selectFolder);
                    }
                }
                else
                {
                    var stringName = showWdCreatfolder(rename.Name, false);
                    if (stringName != null)
                    {
                        new FileNetworkApi().Rename(classFiles[lvMain.SelectedIndex].Id, stringName);
                        ShowSelectFolder(selectFolder);
                    }
                }


            }
        }

        private void MenuItemDowload_Click(object sender, RoutedEventArgs e)
        {
            string parameter = "Hello, World!";

            SaveFileDialog folderBrowserDialog1 = new SaveFileDialog();

            if (folderBrowserDialog1.ShowDialog() == true)
            {

                MessageBox.Show(folderBrowserDialog1.FileName);
            }

            Thread thread = new Thread(() =>
            {
                DownloadFile(classFiles[lvMain.SelectedIndex].UriString, );
            });
            thread.Start();
        }

        public void DownloadFile(string url, string destinationPath)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, destinationPath);
            }
        }
    }
}
