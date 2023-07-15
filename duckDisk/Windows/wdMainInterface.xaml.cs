using duckDisk.data.api.file;
using duckDisk.data.api.file.model;
using duckDisk.data.api.folder;
using duckDisk.data.api.folder.model;
using duckDisk.data.api.user;
using duckDisk.Pages.pgBasket;
using duckDisk.Windows.wdCreateItem;
using duckDisk.Windows.wdPublic;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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

            public bool FileInFolder { get; set; }

            public string UriString { get; set; }

            public string? ExpansionString { get; set; }

            public string? isPublic { get; set; }

            public bool isPublicBool { get; set; }


            public ClassFile(FolderModel? folder, FileModel? fileModels)
            {

                if (folder != null)
                {
                    Id = folder.Id;
                    Name = folder.Name;
                    imageTypeIcon = folder.getIcon();
                    FileInFolder = true;
                    isPublicBool = folder.IsPublic;
                    if (folder.IsPublic)
                    {
                        isPublic = "\\Resources\\LinkBorder.png";
                    }
                    
                }
                else
                {
                    Id = fileModels.Id;
                    Name = fileModels.Name;
                    imageTypeIcon = "\\Resources\\FileIcon.png";
                    FileInFolder = false;
                    UriString = fileModels.Url;
                    ExpansionString = fileModels.Expansion;
                    isPublicBool = fileModels.IsPublic;
                    if (fileModels.IsPublic)
                    {
                        isPublic = "\\Resources\\LinkBorder.png";
                    }
                }
            }
        }

        


        public bool checkLev = false;

        List<ClassFile> classFiles = new List<ClassFile>();
        List<ClassFile> listSelectFolder;
        int? selectFolder = null;

        string? selectFolderName = null;
        public wdMainInterface()
        {
            /*listSelectFolder = new List<ClassFile>
            {
                new ClassFile(){},

            };*/
            InitializeComponent();
            ShowSelectFolder();
            ShowSelectPgBasket();
            lvSelectFolder.ItemsSource = listSelectFolder;
        }

        void ShowSelectPgBasket()
        {
            frBasket.Navigate(new pgBasketMain());
        }

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
            selectFolder = null;
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
            fileDialog.DefaultExt = "*.*"; // Required file extension 
            fileDialog.Filter = "All files (*.*)|*.*"; // Optional file extensions
            fileDialog.ShowDialog();
            

            if (fileDialog.FileName != "")
            {
            var api = new FileNetworkApi();
            {
                if (fileDialog != null)
                {
                    byte[] buffer = File.ReadAllBytes(fileDialog.FileName);
                    api.Add(fileDialog.SafeFileName, buffer, selectFolder);
                }
            }

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
            

            SaveFileDialog folderBrowserDialog1 = new SaveFileDialog();

            folderBrowserDialog1.FileName = classFiles[lvMain.SelectedIndex].Name;
            folderBrowserDialog1.Filter = "All Files (*.*)|*.*";

            if (folderBrowserDialog1.ShowDialog() == true)
            {

                MessageBox.Show(folderBrowserDialog1.FileName);
            }

            string UriString = classFiles[lvMain.SelectedIndex].UriString;
            string puthFileSave = folderBrowserDialog1.FileName;
            MessageBox.Show(UriString);

            var userApi = new UserNetworkApi();


            Thread thread = new Thread(() =>
            {
                DownloadFile(UriString, puthFileSave);
            });
            thread.Start();

        }

        public void DownloadFile(string url, string destinationPath)
        {
            using (WebClient client = new WebClient())
            {
                var userApi = new UserNetworkApi();

                client.Headers.Add("Authorization", $"Bearer {userApi.GetLocalJwtResponse().AccessToken}");

                if (FileExists(url))
                {
                    client.DownloadFile(url, destinationPath);
                }
                else
                {
                    MessageBox.Show("Файл отсутствует на сервере или возникают другие ошибки.", "Error");
                }
            }
        }
        public static bool FileExists(string url)
        {
            try
            {
                var userApi = new UserNetworkApi();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Authorization", $"Bearer {userApi.GetLocalJwtResponse().AccessToken}");
                request.Method = "HEAD";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException)
            {
                // Обработка исключения, если нет связи с сервером или возникают другие ошибки
                return false;
            }
        }

        private void lvFolderHandleDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuItemEditPublick_Click(object sender, RoutedEventArgs e)
        {
            new wdEditPublic(classFiles[lvMain.SelectedIndex].UriString, classFiles[lvMain.SelectedIndex].Id,
                classFiles[lvMain.SelectedIndex].isPublicBool).ShowDialog();

        }
    }
}
