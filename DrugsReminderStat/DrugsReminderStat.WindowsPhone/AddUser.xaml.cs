using Models;
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
    public sealed partial class AddUser : Page
    {
        public AddUser()
        {
            this.InitializeComponent();
          //  HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
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

       

        private async void Nextadduser(object sender, RoutedEventArgs e)
        {
            SharedInformattion.user = new User();
            if (name.Text != "")
            {
                

                if (pet.IsChecked == true)
                {
                    SharedInformattion.user.name = name.Text;
                    SharedInformattion.user.phone = 0;
                    SharedInformattion.user.email = "";
                    Frame.Navigate(typeof(AddUserPet));
                }
                else
                {
                    SharedInformattion.user.name = name.Text;
                    if (male.IsChecked == true)
                    {
                        SharedInformattion.user.icone = "Assets/homme.png";
                    }
                    else 
                        if (female.IsChecked == true)
                    {
                        SharedInformattion.user.icone = "Assets/femme.png";
                    }

                    Frame.Navigate(typeof(AddUser2));
                }
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please enter a name", "Invalid name");
                await msg.ShowAsync();
            }

        }

        private void Backtolist(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListUser2));
        }
    }
}
