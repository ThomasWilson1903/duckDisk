﻿using duckDisk.data.api.basket;
using duckDisk.data.api.file;
using duckDisk.data.api.file.model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace duckDisk.Pages.pgBasket
{
    /// <summary>
    /// Interaction logic for pgBasketMain.xaml
    /// </summary>
    public partial class pgBasketMain : Page
    {
        public pgBasketMain()
        {
            InitializeComponent();
            ShowSelectBasket();
        }

        void ShowSelectBasket()
        {
            var fileApi = new FileNetworkApi();
            List<FileModel> listFile = fileApi.GetAll(null, true).Content;
            dgTest.ItemsSource = listFile;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelAll_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Очистить корзину?", "Очистить корзину?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var apiBasket = new BasketNetworkApi();
                apiBasket.DeleteAll();
                NavigationService.Navigate(new pgBasketMain());
            }
        }
    }
}
