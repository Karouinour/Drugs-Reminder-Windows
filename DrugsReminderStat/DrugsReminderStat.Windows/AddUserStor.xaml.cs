using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AddUserStor : Page
    {

        public List<User> li = new List<User>();
        int maxiduser = -1;
        public AddUserStor()
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

        private void listuser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListsStor));
        }

        private void adduser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUserStor));
        }

        private void femmecheked(object sender, RoutedEventArgs e)
        {
            userpet.Visibility = Visibility.Collapsed;
            Userphonemail.Visibility = Visibility.Visible;
        }

        private void malchek(object sender, RoutedEventArgs e)
        {
            try
            {
                userpet.Visibility = Visibility.Collapsed;
                Userphonemail.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                
               
            }
           
        }

        private void petcheked(object sender, RoutedEventArgs e)
        {
            userpet.Visibility = Visibility.Visible;
            Userphonemail.Visibility = Visibility.Collapsed;
        }

        private async void Saveclick(object sender, TappedRoutedEventArgs e)
        {
            User u = new User();

            if (name.Text != "")
            {


                if (pet.IsChecked == true)
                {
                    u.name = name.Text;
                    u.phone = 0;
                    u.email = "";

                    //suit
                    if (dog.IsChecked == true)
                    {
                        u.icone = "Assets/dog_icon.png";
                    }
                    else
                        if (cat.IsChecked == true)
                        {
                            u.icone = "Assets/cat_icon.png";
                        }
                        else
                            if (bird.IsChecked == true)
                            {
                                u.icone = "Assets/bird_icon.png";
                            }
                            else
                                if (horse.IsChecked == true)
                                {
                                    u.icone = "Assets/horse_icon.png";
                                }
                                else
                                {
                                    u.icone = "Assets/rabbit_icon.png";
                                }
                    u.Id = maxiduser + 1;

                    li.Add(u);
                    IsolatedStorageHelper.SaveObject("User_list", li);
                    
                    Frame.Navigate(typeof(MainPage));

                }
                else
                {
                    u.name = name.Text;
                    if (male.IsChecked == true)
                    {
                        u.icone = "Assets/homme.png";
                    }
                    else
                        if (female.IsChecked == true)
                        {
                            u.icone = "Assets/femme.png";
                        }
                    string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
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


                            u.email = email.Text;
                            u.phone = Int32.Parse(phone.Text);
                            u.Id = maxiduser + 1;


                            li.Add(u);

                            IsolatedStorageHelper.SaveObject("User_list", li);

                            Frame.Navigate(typeof(MainPage));
                        }
                        else
                        {
                            MessageDialog msg = new MessageDialog("Please enter Email valid", "Invalid mail");
                            await msg.ShowAsync();
                        }

                    }
                   
                }
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please enter a name", "Invalid name");
                await msg.ShowAsync();
            }
            Frame.Navigate(typeof(ListsStor));
        }

        private void News(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsStor));
        }

        private void Liststor(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListsStor));
        }

        private void btnprecaution(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PrecautionStor));
        }

        private void btnAbout(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DescriptionStor));
        }

       }
}
