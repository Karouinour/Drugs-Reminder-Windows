using Models;
using NotificationsExtensions.ToastContent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddAlarmStor : Page
    {

        Drug drug = new Drug();
        AlarmData a;
        int test;
        int maxid = -1;
        ObservableCollection<AlarmData> alarms = new ObservableCollection<AlarmData>();
        public AddAlarmStor()
        {
            this.InitializeComponent();

            drug = IsolatedStorageHelper.GetObject<Drug>("Drug_curr");

            if (IsolatedStorageHelper.GetObject<ObservableCollection<AlarmData>>("alarms") != null)
            {
                alarms = IsolatedStorageHelper.GetObject<ObservableCollection<AlarmData>>("alarms");
            }
            if (alarms != null)
            {
                foreach (var item in alarms)
                {
                    if (item.id > maxid)
                        maxid = item.id;
                }
            }
        }

        private void Save_alarm(object sender, TappedRoutedEventArgs e)
        {
            if (dimanche.IsChecked == true)
            {
                SharedInformattion.tab[0] = true;
            }
            if (lundi.IsChecked == true)
            {
                SharedInformattion.tab[1] = true;
            }
            if (mardi.IsChecked == true)
            {
                SharedInformattion.tab[2] = true;
            }
            if (mercredi.IsChecked == true)
            {
                SharedInformattion.tab[3] = true;
            }
            if (jeudi.IsChecked == true)
            {
                SharedInformattion.tab[4] = true;
            }
            if (vendredi.IsChecked == true)
            {
                SharedInformattion.tab[5] = true;
            }
            if (samedi.IsChecked == true)
            {
                SharedInformattion.tab[6] = true;
            }
            a = new AlarmData();
            test = 0;
            DateTime dueTime = new DateTime();
            foreach (var item in SharedInformattion.tab)
            {
                if (item)
                {
                    test++;
                }
                if (test == 0)
                {
                    for (int i = 0; i < SharedInformattion.tab.Length; i++)
                    {

                        SharedInformattion.tab[i] = true;
                    }
                }

            }

            try
            {
                for (int i = 0; i < SharedInformattion.tab.Length; i++)
                {

                    if (SharedInformattion.tab[i])
                    {

                        DateTime today = DateTime.Today;

                        int daysUntilTuesday = ((int)SharedInformattion.days[i] - (int)today.DayOfWeek + 7) % 7;
                        DateTime nextTuesday = today.AddDays(daysUntilTuesday);

                        double dateNow = DateTime.Now.TimeOfDay.TotalSeconds;
                        double timePickerTime = time.Time.TotalSeconds;
                        double difference = timePickerTime - dateNow;
                        DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, nextTuesday.Day, time.Time.Hours, time.Time.Minutes, time.Time.Seconds, 0);

                        String updateString = "It is time to take your medicine";
                        dueTime = d;

                        Random rand = new Random();
                        int idNumber = rand.Next(0, 10000000);

                        ScheduleToastWithStringManipulation(updateString, dueTime, idNumber);

                        //     ScheduleToast(updateString, dueTime, idNumber);
                    }
                }
            }
            catch (Exception)
            {
                // NotifyUser("You must input a valid time in seconds.", NotifyType.ErrorMessage);
            }
            for (int i = 0; i < SharedInformattion.tab.Length; i++)
            {
                SharedInformattion.tab[i] = false;
            }
            //  ObservableCollection<AlarmData> alarmDatas = IsolatedStorageHelper.GetObject<ObservableCollection<AlarmData>>("alarms");
            a.id = maxid + 1;

            a.IsEnable = true;

            a.drug = drug;
            a.drugid = drug.Id;
            a.ItemType = "Drug name";
            a.ItemDays = drug.name;
            if (time.Time.Minutes < 10)
            {
                a.DueTime = time.Time.Hours + " : 0" + time.Time.Minutes;
            }
            else
            {
                a.DueTime = time.Time.Hours + " : " + time.Time.Minutes;
            }
            alarms.Add(a);
            IsolatedStorageHelper.SaveObject<ObservableCollection<AlarmData>>("alarms", alarms);


            // IsolatedStorageHelper.SaveObject<int>("ids", IsolatedStorageHelper.idAlarm);

            Frame.Navigate(typeof(ListsStor));
        }

        //----

        void ScheduleToastWithStringManipulation(string updateString, DateTime dueTime, int idNumber)
        {
            string drugnameandinformation;
            if (drug.instruction == "No Instrucction")
            {
                drugnameandinformation = drug.name;
            }
            else
            {
                drugnameandinformation = drug.name + " : " + drug.instruction;
            }

            // Scheduled toasts use the same toast templates as all other kinds of toasts.
            string toastXmlString = "<toast>"
            + "<visual version='2'>"
            + "<binding template='ToastText02'>"
            + "<text id='1'>" + updateString + "</text>"
            + "<text id='2'>" + drugnameandinformation + "</text>"
            + "</binding>"
            + "</visual>"
            + "</toast>";

            Windows.Data.Xml.Dom.XmlDocument toastDOM = new Windows.Data.Xml.Dom.XmlDocument();
            try
            {
                toastDOM.LoadXml(toastXmlString);

                ScheduledToastNotification toast;

                //   toast = new ScheduledToastNotification(toastDOM, dueTime, TimeSpan.FromSeconds(60), 5);

                // You can specify an ID so that you can manage toasts later.
                // Make sure the ID is 15 characters or less.
                //    toast.Id = "Repeat" + idNumber;


                toast = new ScheduledToastNotification(toastDOM, dueTime);
                toast.Id = "Toast" + idNumber;

                ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
                //   NotifyUser("Scheduled a toast with ID: " + toast.Id, NotifyType.StatusMessage);
                myNotification m = new myNotification();
                m.myShedule = toast;
                m.alarmId = maxid + 1;
                a.shs.Add(toast.Id);
                ObservableCollection<myNotification> notifs = IsolatedStorageHelper.GetObject<ObservableCollection<myNotification>>("notifications");
                notifs.Add(m);
                IsolatedStorageHelper.SaveObject<ObservableCollection<myNotification>>("notifications", notifs);
                //  a.notifications.Add(toast);
            }
            catch (Exception)
            {
                // NotifyUser("Error loading the xml, check for invalid characters in the input", NotifyType.ErrorMessage);
            }
        }

        //-----------------------------------------------------------------------------------------------

        void ScheduleToast(String updateString, DateTime dueTime, int idNumber)
        {
            // Scheduled toasts use the same toast templates as all other kinds of toasts.
            IToastText02 toastContent = ToastContentFactory.CreateToastText02();
            toastContent.TextHeading.Text = updateString;
            if (drug.instruction == "No Instrucction")
            {
                toastContent.TextBodyWrap.Text = drug.name;
            }
            else
            {
                toastContent.TextBodyWrap.Text = drug.name + " : " + drug.instruction;
            }



            ScheduledToastNotification toast;

            toast = new ScheduledToastNotification(toastContent.GetXml(), dueTime);
            toast.Id = "Toast" + idNumber;


            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            // NotifyUser("Scheduled a toast with ID: " + toast.Id, NotifyType.StatusMessage);
            myNotification m = new myNotification();
            m.myShedule = toast;

            m.alarmId = maxid + 1;
            a.shs.Add(toast.Id);
            ObservableCollection<myNotification> notifs = IsolatedStorageHelper.GetObject<ObservableCollection<myNotification>>("notifications");
            notifs.Add(m);
            IsolatedStorageHelper.SaveObject<ObservableCollection<myNotification>>("notifications", notifs);


        }

        private void gotolist(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListsStor));
        }

        private void AddUser(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUserStor));
        }

        private void BtnNews(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsStor));
        }

        private void btnprecaution(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PrecautionStor));
        }

        private void btnAbout(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutStor));
        }
    }
}
