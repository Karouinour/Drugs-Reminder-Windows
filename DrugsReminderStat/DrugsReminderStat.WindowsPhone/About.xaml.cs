using DrugsReminderStat.Common;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class About : Page
    {
        RelayCommand _checkedGoBackCommand;
        public About()
        {
         this.InitializeComponent();
            Storyboard1.Begin();
            
          //  _checkedGoBackCommand = new RelayCommand(
          //                  () => this.CheckGoBack(),
           //                 () => this.CanCheckGoBack()
             //                   );

            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}

        //private void AppBarButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (this.Frame.CanGoBack)
        //    {
        //        this.Frame.GoBack();
        //    }
        //}
        // private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        // {
        //    Frame frame = Window.Current.Content as Frame;
        //     if (frame == null)
        //     {
        //         return;
        //     }
        //     if (frame.CanGoBack)
        //     {
        //         frame.GoBack();
        //         e.Handled = true;
        //     }
        //     Frame.Navigate(typeof(MainPage));

        // }

        private async void goFacebookNour(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = "https://www.facebook.com/karouinour";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void goFacebookAmani(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"https://www.facebook.com/amani.ghali";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void back_main(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Description));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //This should be written here rather than the contructor
             HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //remove the handler before you leave!
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            e.Handled = true;

            if (frame == null)
            {
                return;
            }
            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }
    }
}


