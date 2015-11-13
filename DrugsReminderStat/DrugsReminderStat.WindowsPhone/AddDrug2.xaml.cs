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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddDrug2 : Page
    {

        List<Drug> drugs = new List<Drug>();
        int maxid = -1;
        public AddDrug2()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
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
        private  void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
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
        private bool verif()
        {
            if (
                (before.IsChecked == true || aftter.IsChecked == true || noinstruction.IsChecked == true)
               
               
                )
            {
                return true;
            }
            return false;
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (verif())
            {
                //instruction
                if (before.IsChecked == true)
                {
                    SharedInformattion.drug.instruction = "Before  meal";
                }
                else if (aftter.IsChecked == true)
                {
                    SharedInformattion.drug.instruction = "Aftter  meal";
                }
                else if (noinstruction.IsChecked == true)
                {
                    SharedInformattion.drug.instruction = "No Instrucction";
                }
                // instancce pour user current 
                User u = new User();
                // recuperation user current
                u = IsolatedStorageHelper.GetObject<User>("User_curr");
                //ajouter l'id du user current 
                SharedInformattion.drug.user = u;
                SharedInformattion.drug.iduser = u.Id;
                //ajouter id au drug
                SharedInformattion.drug.Id = maxid + 1;
                // ajout medicameent a la list 
                drugs.Add(SharedInformattion.drug);
                // sauvgardé la nouvel list 
                IsolatedStorageHelper.SaveObject("Drug_list", drugs);
                //retour list drug
                SharedInformattion.drug = null;
                Frame.Navigate(typeof(ListDrug));


                //days week month
              /*  if (days.IsChecked == true)
                {
                    SharedInformattion.drug.durer = "Days";
                    Frame.Navigate(typeof(AddDrugDays));
                }
                else if (week.IsChecked == true)
                {
                    SharedInformattion.drug.durer = "Weeks";
                    Frame.Navigate(typeof(AddDrugWeek));
                }
                else if (months.IsChecked == true)
                {
                    SharedInformattion.drug.durer = "Months";
                    Frame.Navigate(typeof(AddDrugMouth));
                }*/
                
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            SharedInformattion.drug = null;
            Frame.Navigate(typeof(ListDrug));
        }
    }
}
