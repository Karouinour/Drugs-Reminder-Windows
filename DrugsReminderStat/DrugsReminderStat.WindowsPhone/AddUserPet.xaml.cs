using DrugsReminderStat.Common;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
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
    public sealed partial class AddUserPet : Page
    {
        
        public List<User> li = new List<User>();
        int maxiduser = -1;
      

        public AddUserPet()
        {
            this.InitializeComponent();
            if (IsolatedStorageHelper.GetObject<List<User>>("User_list") == null)
            {
                IsolatedStorageHelper.SaveObject("User_list", li);
            }
            else
            {
                li = IsolatedStorageHelper.GetObject<List<User>>("User_list");
            }
            if (li != null)
            {
                foreach (var item in li)
                {
                    if (item.Id > maxiduser)
                        maxiduser = item.Id;
                }
            }
        }

       


      

       

        private void Save(object sender, RoutedEventArgs e)
        {
            if (dog.IsChecked == true)
            {
                SharedInformattion.user.icone = "Assets/dog_icon.png";
            }
            else
                if (cat.IsChecked == true)
                {
                    SharedInformattion.user.icone = "Assets/cat_icon.png";
                }
                else
                if (bird.IsChecked == true)
                    {
                       SharedInformattion.user.icone = "Assets/bird_icon.png";
                    }
                else
                if (horse.IsChecked == true)
                {
                     SharedInformattion.user.icone = "Assets/horse_icon.png";
                }
                else
                {
                     SharedInformattion.user.icone = "Assets/rabbit_icon.png";
                }
            SharedInformattion.user.Id = maxiduser+1;
           
            li.Add(SharedInformattion.user);
            IsolatedStorageHelper.SaveObject("User_list", li);
            SharedInformattion.user = null;
            Frame.Navigate(typeof(ListUser2));
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            SharedInformattion.user = null;
            Frame.Navigate(typeof(MainPage));
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
