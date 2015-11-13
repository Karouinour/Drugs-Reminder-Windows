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
using Models;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDrugDays : Page
    {
        //instance recuperation list medicament 
        List<Drug> drugs = new List<Drug>();
        int maxid = -1;
        public AddDrugDays()
        {
            this.InitializeComponent();

            //recuperation list medicament 
            if (IsolatedStorageHelper.GetObject<List<Drug>>("Drug_list") != null)
            {
                drugs = IsolatedStorageHelper.GetObject<List<Drug>>("Drug_list");
            }
            if (drugs != null)
            {
                foreach (var item in drugs)
                {
                    if (item.Id > maxid)
                        maxid = item.Id;
                }
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //}


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
        //}
   /*     private void next_Click(object sender, RoutedEventArgs e)
        {
            if (verif())
            {
                if (un.IsChecked == true)
                {
                    SharedInformattion.drug.nombre_durer = 1;
                }
                else if (deux.IsChecked == true)
                {
                    SharedInformattion.drug.nombre_durer = 2;
                }
                else if (trois.IsChecked == true)
                {
                    SharedInformattion.drug.nombre_durer = 3;
                }
                else if (qutre.IsChecked == true)
                {
                    SharedInformattion.drug.nombre_durer = 4;
                }
                else if (cinque.IsChecked == true)
                {
                    SharedInformattion.drug.nombre_durer = 5;
                }
                else if (six.IsChecked == true)
                {
                    SharedInformattion.drug.nombre_durer = 4;
                }

                // instancce pour user current 
                User u = new User();
                // recuperation user current
                u = IsolatedStorageHelper.GetObject<User>("User_curr");
                //ajouter l'id du user current 
                SharedInformattion.drug.user = u;
                //ajouter id au drug
                SharedInformattion.drug.Id = maxid + 1;
                // ajout medicameent a la list 
                drugs.Add(SharedInformattion.drug);
                // sauvgardé la nouvel list 
                IsolatedStorageHelper.SaveObject("Drug_list", drugs);
                //retour list drug
                Frame.Navigate(typeof(ListDrug));
            }
        }*/
        private bool verif()
        {
            if (un.IsChecked == true || deux.IsChecked == true || trois.IsChecked == true || qutre.IsChecked == true || cinque.IsChecked == true || six.IsChecked == true)
            {
                return true;
            }
            return false;
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
        private async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
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
