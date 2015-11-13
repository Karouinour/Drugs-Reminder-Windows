using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class PrecautionStor : Page
    {
        public PrecautionStor()
        {
            this.InitializeComponent();
        }

        private void listuser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListsStor));
        }

        private void adduser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUserStor));
        }

        private void Precautionstor(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PrecautionStor));
        }

        private void BtnAbouttapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DescriptionStor));
        }

        private void btnNews(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DescriptionStor));
        }
    }
}
