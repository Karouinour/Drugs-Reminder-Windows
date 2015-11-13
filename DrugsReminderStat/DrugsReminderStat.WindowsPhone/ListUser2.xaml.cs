using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ListUser2 : Page
    {
        //private DatabaseHelperClass dbh = new DatabaseHelperClass();
        public List<User> li = new List<User>();
        public ObservableCollection<User> li_user = new ObservableCollection<User>();
        public ListUser2()
        {
            this.InitializeComponent();
            //isolate
            User d = new User();
            d.name = "Me";
            d.Id = 0;
            d.icone = "Assets/homme.png";
            d.phone = 22222222;
            d.email = "default@def.tn";
            li.Add(d);
            if (IsolatedStorageHelper.GetObject<List<User>>("User_list") == null )
            {
                IsolatedStorageHelper.SaveObject("User_list", li);
            }
            else
            {
                li = IsolatedStorageHelper.GetObject<List<User>>("User_list");
                
            }
            foreach (var u in li)
            {
                li_user.Add(u);
            }
           
            listuser.DataContext = li_user;

           
            
           // HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}
    

      

        private void Image_Tapped_Drugs(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListDrug));
        }

        

        //private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        //{
        //    Frame frame = Window.Current.Content as Frame;
        //    if (frame == null)
        //    {
        //        return;
        //    }

        //    if (frame.CanGoBack)
        //    {
        //        frame.GoBack();
        //        e.Handled = true;
        //    }
        //   // Frame.Navigate(typeof(MainPage));
        //}

        private void Add_user(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUser));
        

        }

      

        private void sectionchange(object sender, SelectionChangedEventArgs e)
        {
            User u = new User();
            u = listuser.SelectedItem as User;
           
                IsolatedStorageHelper.SaveObject("User_curr", u);
            
            
           // Debug.WriteLine(u.email +" "+u.name+" "+ u.phone);
            Frame.Navigate(typeof(ListDrug));
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
