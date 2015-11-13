using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<User> users = new ObservableCollection<User>();

       // private DatabaseHelperClass dbh = new DatabaseHelperClass();
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

          

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
     
     
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //This should be written here rather than the contructor
            //  HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //remove the handler before you leave!
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }
        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                var msg = new MessageDialog("Confirm Close");
                var okBtn = new UICommand("OK");
                var cancelBtn = new UICommand("Cancel");
                msg.Commands.Add(okBtn);
                msg.Commands.Add(cancelBtn);
                IUICommand result = await msg.ShowAsync();
                if (result != null && result.Label == "OK")
                {
                    Application.Current.Exit();
                }
            }


        }

        private void listuser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListUser2));
        }

        private void adduser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUser));
        }

        private void precaution_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddDrugAlarm));
        }


        private void about_Tapped(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private void Precautapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Precaution));
        }

        private void Newstapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(News));
        }
    }
}
