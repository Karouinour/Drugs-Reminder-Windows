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
    public sealed partial class AddDrug : Page
    {
        public AddDrug()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (SharedInformattion.drug != null)
            drugname.Text = SharedInformattion.drug.name;
            

            
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

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            SharedInformattion.drug = new Drug();
            if (drugname.Text != null)
            {
                SharedInformattion.drug.name = drugname.Text;
                if (ampoule.IsChecked == true)
                {
                    SharedInformattion.drug.type_medoc = "Assets/vial.png";
                }
                else if (pommade.IsChecked == true)
                {
                    SharedInformattion.drug.type_medoc = "Assets/pommade.png";
                }
                else if (sirop.IsChecked == true)
                {
                    SharedInformattion.drug.type_medoc = "Assets/syrup.png";
                }
                else if (injection.IsChecked == true)
                {
                    SharedInformattion.drug.type_medoc = "Assets/injection.png";
                }
                else if (pill.IsChecked == true)
                {
                    SharedInformattion.drug.type_medoc = "Assets/pill.png";
                }
                Frame.Navigate(typeof(AddDrug2));
            }else
            {
                MessageDialog msg = new MessageDialog("Please enter a drug name", "Invalid field ");
                await msg.ShowAsync();
            }
           
        }
       
    }
}
