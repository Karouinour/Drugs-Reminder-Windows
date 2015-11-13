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
    public sealed partial class ListDrug : Page
    {
         public List<Drug> listdtotal = new List<Drug>();
         public List<Drug> listdforuser = new List<Drug>();
        public ListDrug()
        {
            this.InitializeComponent();
         
            User u = new User();

            u = IsolatedStorageHelper.GetObject<User>("User_curr");
            listdtotal = IsolatedStorageHelper.GetObject<List<Drug>>("Drug_list");
            if (listdtotal != null)
            {
                foreach (var item in listdtotal)
                {
                    if (item.iduser == u.Id)
                    {
                        listdforuser.Add(item);
                    }
                }
            }

            listDrugs.DataContext = listdforuser;
           // HardwareButtons.BackPressed += HardwareButtons_BackPressed;
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

        private void adddrug_Tapped(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddDrug));
        }

        private  void listDrugs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drug u = new Drug();
            u = listDrugs.SelectedItem as Drug;

            IsolatedStorageHelper.SaveObject("Drug_curr", u);
            
            Frame.Navigate(typeof(AddDrugAlarm));
        }

       
    }
}
