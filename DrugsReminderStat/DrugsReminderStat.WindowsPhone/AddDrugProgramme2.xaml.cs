using Models;
using System;
using System.Collections.Generic;
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
    public sealed partial class AddDrugProgramme2 : Page
    {

        List<alarm> alarms = new List<alarm>();
        int maxid = -1;
        public AddDrugProgramme2()
        {
            this.InitializeComponent();

            if (IsolatedStorageHelper.GetObject<List<alarm>>("Alarm_list") != null)
            {
                alarms = IsolatedStorageHelper.GetObject<List<alarm>>("Alarm_list");
            }
          /*  if (drugs != null)
            {
                foreach (var item in alarms)
                {
                    if (item.Id > maxid)
                        maxid = item.Id;
                }
            }*/
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
       

        private void Save(object sender, RoutedEventArgs e)
        {
            Drug d = new Drug();
            alarm a = new alarm();
            // recuperation user current
            d = IsolatedStorageHelper.GetObject<Drug>("Drug_curr");
            a.Id = maxid + 1;
            a.drug = d;
            alarms.Add(a);
            // sauvgardé la nouvel list 
           //s IsolatedStorageHelper.SaveObject("Alarm_list", drugs);
            //retour list drug
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
