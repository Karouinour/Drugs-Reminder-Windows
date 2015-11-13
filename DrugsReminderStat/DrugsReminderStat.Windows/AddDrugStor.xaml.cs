using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class AddDrugStor : Page
    {
        List<Drug> drugs = new List<Drug>();
        int maxid = -1;
        public AddDrugStor()
        {
            this.InitializeComponent();
            if (SharedInformattion.drug != null)
                drugname.Text = SharedInformattion.drug.name;
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

     

       

        private void adduser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUserStor));
        }

        private async void save_Click(object sender, TappedRoutedEventArgs e)
        {
            Drug drug = new Drug();
            if (drugname.Text != null)
            {
                drug.name = drugname.Text;
                if (ampoule.IsChecked == true)
                {
                    drug.type_medoc = "Assets/vial.png";
                }
                else if (pommade.IsChecked == true)
                {
                    drug.type_medoc = "Assets/pommade.png";
                }
                else if (sirop.IsChecked == true)
                {
                    drug.type_medoc = "Assets/syrup.png";
                }
                else if (injection.IsChecked == true)
                {
                    drug.type_medoc = "Assets/injection.png";
                }
                else if (pill.IsChecked == true)
                {
                    drug.type_medoc = "Assets/pill.png";
                }

           

           
                //instruction
                if (before.IsChecked == true)
                {
                    drug.instruction = "Before  meal";
                }
                else if (aftter.IsChecked == true)
                {
                    drug.instruction = "Aftter  meal";
                }
                else if (noinstruction.IsChecked == true)
                {
                    drug.instruction = "No Instrucction";
                }
                // instancce pour user current 
                User u = new User();
                // recuperation user current
                u = IsolatedStorageHelper.GetObject<User>("User_curr");
                //ajouter l'id du user current 
                drug.user = u;
                drug.iduser = u.Id;
                //ajouter id au drug
                drug.Id = maxid + 1;
                // ajout medicameent a la list 
                drugs.Add(drug);
                // sauvgardé la nouvel list 
                IsolatedStorageHelper.SaveObject("Drug_list", drugs);
                
                Frame.Navigate(typeof(ListsStor));
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please enter a drug name", "Invalid field ");
                await msg.ShowAsync();
            }
           

        }

        private void NewsTapped(object sender, TappedRoutedEventArgs e)
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
            Frame.Navigate(typeof(AboutStor));
        }
    }
}