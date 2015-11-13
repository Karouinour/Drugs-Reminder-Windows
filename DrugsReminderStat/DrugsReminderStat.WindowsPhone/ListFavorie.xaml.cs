using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Models;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListFavorie : Page
    {
        ObservableCollection<ItemsRSS> listrss = new ObservableCollection<ItemsRSS>();
        public ListFavorie()
        {
            this.InitializeComponent();

            if (IsolatedStorageHelper.GetObject<ObservableCollection<ItemsRSS>>("favori_list") == null)
            {
                IsolatedStorageHelper.SaveObject("favori_list", listrss);
            }
            listrss = IsolatedStorageHelper.GetObject<ObservableCollection<ItemsRSS>>("favori_list");
            lstRSSFavorie.DataContext = listrss;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var selected = lstRSSFavorie.SelectedItem as ItemsRSS;

            listrss.Remove(selected);

            IsolatedStorageHelper.SaveObject("favori_list", listrss);
            lstRSSFavorie.DataContext = listrss;

        }

        
    }
}
