using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Models;
using Windows.Phone.UI.Input;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   
    public sealed partial class AddDrugAlarm : Page
    {
        public static AlarmData sharedAlarm;
        ObservableCollection<AlarmData> alarms = new ObservableCollection<AlarmData>();
        ObservableCollection<AlarmData> bindingList = new ObservableCollection<AlarmData>();
        Drug drugcurr = new Drug();

        public AddDrugAlarm()
        {
            this.InitializeComponent();

            drugcurr = IsolatedStorageHelper.GetObject<Drug>("Drug_curr");
            //---------------     
            RefreshListView();
           // HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }


        //private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        //{
        //     Frame frame = Window.Current.Content as Frame;
        //     if (frame == null)
        //     {
        //         return;
        //     }

        //     if (frame.CanGoBack)
        //     {
        //         frame.GoBack();
        //         e.Handled = true;
        //     }
        //    Frame.Navigate(typeof(ListDrug));
        //}
        public void RefreshListView()
        {
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


        private void Add_alarm(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(addAlarm));
        }

        private void delete_alarm(object sender, RoutedEventArgs e)
        {
            if (sharedAlarm != null)
            {
                // ObservableCollection<AlarmData> alarmDatas = IsolatedStorageHelper.GetObject<ObservableCollection<AlarmData>>("alarms");
                string itemId = sharedAlarm.ItemId;
                int id = sharedAlarm.id;
                ObservableCollection<myNotification> notifications = IsolatedStorageHelper.GetObject<ObservableCollection<myNotification>>("notifications");

                ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
                IReadOnlyList<ScheduledToastNotification> scheduled = notifier.GetScheduledToastNotifications();
                for (int j = 0; j < scheduled.Count; j++)
                {
                    foreach (var item in bindingList)
                    {
                        foreach (var item2 in item.shs)
                        {



                            if (scheduled[j].Id == item2)
                            {
                                notifier.RemoveFromSchedule(scheduled[j]);
                            }

                        }
                    }
                }


                bindingList.Remove(sharedAlarm);

                IsolatedStorageHelper.SaveObject<ObservableCollection<AlarmData>>("alarms", bindingList);



                RefreshListView();
            }
            

        }

        private void select(object sender, SelectionChangedEventArgs e)
        {
            sharedAlarm = ItemGridView.SelectedItem as AlarmData;
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
