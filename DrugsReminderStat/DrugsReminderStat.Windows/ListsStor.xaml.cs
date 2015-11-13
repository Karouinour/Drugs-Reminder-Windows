using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
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
    public sealed partial class ListsStor : Page
    {
        public List<User> li = new List<User>();
        public ObservableCollection<User> li_user = new ObservableCollection<User>();



        public List<Drug> listdtotal = new List<Drug>();
        public ObservableCollection<Drug> listdforuser = new ObservableCollection<Drug>();

        //list alarm param 
        public static AlarmData sharedAlarm;
        ObservableCollection<AlarmData> alarms = new ObservableCollection<AlarmData>();
        ObservableCollection<AlarmData> bindingList = new ObservableCollection<AlarmData>();
        public ListsStor()
        {
            this.InitializeComponent();
            User d = new User();
            d.name = "Me";
            d.Id = 0;
            d.icone = "Assets/homme.png";
            d.phone = 22222222;
            d.email = "default@def.tn";
            li.Add(d);
            if (IsolatedStorageHelper.GetObject<List<User>>("User_list") == null)
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

            //drugs

            d = IsolatedStorageHelper.GetObject<User>("User_curr");
            listdtotal = IsolatedStorageHelper.GetObject<List<Drug>>("Drug_list");
            if (listdtotal != null)
            {
                foreach (var item in listdtotal)
                {
                    if (item.iduser == d.Id)
                    {
                        listdforuser.Add(item);
                    }
                }
            }

            listDrugs.DataContext = listdforuser;
        }


        //clic user !! 
        private void sectionchange(object sender, SelectionChangedEventArgs e)
        {
            User u = new User();
            u = listuser.SelectedItem as User;
            ItemGridView.ItemsSource = null;
            IsolatedStorageHelper.SaveObject("User_curr", u);




        }

        //clic change 
        private void listDrugs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drug u = new Drug();
            u = listDrugs.SelectedItem as Drug;
            try
            {
                IsolatedStorageHelper.SaveObject("Drug_curr", u);
            }
            catch (Exception)
            {
            }
           
            btn_listuser.Visibility = Visibility.Collapsed;
            btn_addalarm.Visibility = Visibility.Visible;
            btn_adddrug.Visibility = Visibility.Collapsed;
            StoryboardAddAlarm.Begin();
           
            //---------------     
            RefreshListView();

        }
        //reflerch view for list alarm 
        public void RefreshListView()
        {
            Drug drugcurr = new Drug();
            drugcurr = IsolatedStorageHelper.GetObject<Drug>("Drug_curr");
            IReadOnlyList<ScheduledToastNotification> scheduledToasts = ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications();
            IReadOnlyList<ScheduledTileNotification> scheduledTiles = TileUpdateManager.CreateTileUpdaterForApplication().GetScheduledTileNotifications();
            alarms = IsolatedStorageHelper.GetObject<ObservableCollection<AlarmData>>("alarms");
            // List<string> notifications = alarms[0].shs;
            int toastLength = scheduledToasts.Count;
            int tileLength = scheduledTiles.Count;

            
            bindingList = new ObservableCollection<AlarmData>();
            if (alarms != null)
            {
                for (int i = 0; i < alarms.Count; i++)
                {
                    AlarmData aa = alarms[i];
                    if (aa.drugid == drugcurr.Id)
                    {
                        bindingList.Add(aa);
                    }
                }
            }


            ItemGridView.ItemsSource = bindingList;
        }
        //---------------------------------------------------------------


        private  void listuser_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
            //Frame.Navigate(typeof(ListsStor));
        }

        private void adduser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUserStor));
        }

    

        private void slectuser(object sender, SelectionChangedEventArgs e)
        {
            
            listDrugs.DataContext = null;
             User d = new User();
            

            d = listuser.SelectedItem as User;
             IsolatedStorageHelper.SaveObject<User>("User_curr",d);
            listdtotal = IsolatedStorageHelper.GetObject<List<Drug>>("Drug_list");
            listdforuser.Clear();
            if (listdtotal != null)
            {
                foreach (var item in listdtotal)
                {
                    if (item.iduser == d.Id)
                    {
                        listdforuser.Add(item);
                    }
                }
            }
            
            listDrugs.DataContext = listdforuser;
            btn_listuser.Visibility = Visibility.Collapsed;
            btn_addalarm.Visibility = Visibility.Collapsed;
            btn_adddrug.Visibility = Visibility.Visible;
            StoryboardAdddrug.Begin();
            ItemGridView.ItemsSource = null;
           

        }

        private void btnAdddrugtapped(object sender, TappedRoutedEventArgs e)
        {
           
            Frame.Navigate(typeof(AddDrugStor));
        }

        private void selectchangelistAlarm(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddalarmtapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddAlarmStor));

        }

        private void btnnews(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsStor));
        }

        private void btnprecautio(object sender, TappedRoutedEventArgs e)
        {

        }

        private void btnAbout(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DescriptionStor));
        }


        private void precaution(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PrecautionStor));
        }

       

      
    }
}