using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsStor : Page
    {
        private HttpClient httpClient;
        private HttpResponseMessage response;

        // This is the feed address that will be parsed and displayed
        private String feedAddress = "http://sante-az.aufeminin.com/world/edito/index/rss.xml.asp";

        public ObservableCollection<ItemsRSS> li = new ObservableCollection<ItemsRSS>();
        MediaElement m = new MediaElement();
        SpeechSynthesizer synth = new SpeechSynthesizer();
        int i = 0;

        public NewsStor()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            httpClient = new HttpClient();

            // Add a user-agent header
            var headers = httpClient.DefaultRequestHeaders;

            // HttpProductInfoHeaderValueCollection is a collection of 
            // HttpProductInfoHeaderValue items used for the user-agent header

            headers.UserAgent.ParseAdd("ie");
            headers.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            if (SharedInformattion.item != null)
            {
                ///categorie
                categorie.Text = SharedInformattion.item.Category;
                //image source 
                Uri myUri = new Uri(SharedInformattion.item.Image, UriKind.Absolute);
                BitmapImage bmi = new BitmapImage();
                bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bmi.UriSource = myUri;
                image.Source = bmi;
                //titre 
                titre.Text = SharedInformattion.item.Title;
                //description
                descritption.Text = SharedInformattion.item.Description;
                SpeechSynthesizer.AllVoices.First();
            }
           
         
            rafrich();
           
        }

       
        private async void rafrich()
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                //Do something

                response = new HttpResponseMessage();

                // if 'feedAddress' value changed the new URI must be tested --------------------------------
                // if the new 'feedAddress' doesn't work, 'feedStatus' informs the user about the incorrect input.

                //feedStatus.Text = "Test if URI is valid";

                Uri resourceUri;
                if (!Uri.TryCreate(feedAddress.Trim(), UriKind.Absolute, out resourceUri))
                {
                    //  feedStatus.Text = "Invalid URI, please re-enter a valid URI";
                    return;
                }
                if (resourceUri.Scheme != "http" && resourceUri.Scheme != "https")
                {
                    //feedStatus.Text = "Only 'http' and 'https' schemes supported. Please re-enter URI";
                    return;
                }
                // ---------- end of test---------------------------------------------------------------------

                string responseText;
                //feedStatus.Text = "Waiting for response ...";

                try
                {
                    response = await httpClient.GetAsync(resourceUri);

                    response.EnsureSuccessStatusCode();

                    responseText = await response.Content.ReadAsStringAsync();
                    //statusPanel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                }
                catch (Exception ex)
                {
                    // Need to convert int HResult to hex string
                    //  feedStatus.Text = "Error = " + ex.HResult.ToString("X") +  "  Message: " + ex.Message;
                    responseText = "";
                }
                //  feedStatus.Text = response.StatusCode + " " + response.ReasonPhrase;

                // now 'responseText' contains the feed as a verified text.
                // next 'responseText' is converted as the rssItems class model definition to be displayed as a list

                List<ItemsRSS> lstData = new List<ItemsRSS>();

                XElement _xml = XElement.Parse(responseText);
                foreach (XElement value in _xml.Elements("channel").Elements("item"))
                {
                    ItemsRSS _item = new ItemsRSS();

                    _item.Title = value.Element("title").Value;

                    _item.Description = value.Element("description").Value;

                    _item.Link = value.Element("link").Value;

                    _item.Category = value.Element("category").Value;

                    _item.Image = value.Element("enclosure").Attribute("url").Value;

                    lstData.Add(_item);
                }

                // lstRSS is bound to the lstData: the final result of the RSS parsing
                lstRSS.DataContext = lstData;

            }
            else
            {
                MessageDialog msg = new MessageDialog("No Network !!", "Network Erreur !");
                await msg.ShowAsync();
            }
        }


        private async void lstRSS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if item clicked navigate to the webpage

            var selected = lstRSS.SelectedItem as ItemsRSS;
            categorie.Visibility = Visibility.Visible;
            SharedInformattion.item = selected;
            categorie.Text = SharedInformattion.item.Category;
            //image source 
            Uri myUri = new Uri(SharedInformattion.item.Image, UriKind.Absolute);
            BitmapImage bmi = new BitmapImage();
            bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmi.UriSource = myUri;
            image.Source = bmi;
            //titre 
            titre.Text = SharedInformattion.item.Title;
            //description
            descritption.Text = SharedInformattion.item.Description;
            SpeechSynthesizer.AllVoices.First();


        }

        private void Saveclick(object sender, TappedRoutedEventArgs e)
        {
            rafrich();
        }



        private void adduser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUserStor));
        }

        private void btnprecaution(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PrecautionStor));
        }

        private void news_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddDrugStor));
        }

        private void listuser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListsStor));
        }

        private void btnAbout(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DescriptionStor));
        }
    }
}
