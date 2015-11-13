using DrugsReminderStat.Common;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class AddUser2 : Page
    {
        
        //private DatabaseHelperClass dbh = new DatabaseHelperClass();
        //private NavigationHelper navigationHelper;
       // private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public List<User> li = new List<User>();
        int maxiduser = -1;

        public AddUser2()
        {
            this.InitializeComponent();

      
             
            if (IsolatedStorageHelper.GetObject<List<User>>("User_list") == null )
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

        private async void Save(object sender, RoutedEventArgs e)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if ((phone.Text == "") && (email.Text == ""))
            {
                MessageDialog msg = new MessageDialog("Please enter a phone and a Email", "Invalid field");
                await msg.ShowAsync();
            }

                    Regex re = new Regex(strRegex);
                   

               
                   
             

            if ((phone.Text != "") && (email.Text != ""))
            {
                if (re.IsMatch(email.Text))
                {


                    SharedInformattion.user.email = email.Text;
                    SharedInformattion.user.phone = Int32.Parse(phone.Text);
                    SharedInformattion.user.Id = maxiduser+1;


                    li.Add(SharedInformattion.user);
                    
                    IsolatedStorageHelper.SaveObject("User_list", li);
                    SharedInformattion.user = null;
                    Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Please enter Email valid", "Invalid mail");
                    await msg.ShowAsync();
                }
               
            }
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            SharedInformattion.user = null;
            Frame.Navigate(typeof(ListUser2));
        }

    }
}
