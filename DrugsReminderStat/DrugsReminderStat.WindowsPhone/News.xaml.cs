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
using Models;

// include the rssItems class to your work

using Windows.Web.Http;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using Windows.UI.Popups;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace DrugsReminderStat
{
        
    public sealed partial class News : Page
    {
        //The Windows.Web.Http.HttpClient class provides the main class for 
        // sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
        private HttpClient httpClient;
        private HttpResponseMessage response;

        // This is the feed address that will be parsed and displayed
        private String feedAddress = "http://sante-az.aufeminin.com/world/edito/index/rss.xml.asp";
        public News()
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
           // Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            rafrich();
           

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
      
       
        //private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        //{
        //   /* Frame frame = Window.Current.Content as Frame;
        //    if (frame == null)
        //    {
        //        return;
        //    }
        //    if (frame.CanGoBack)
        //    {
        //        frame.GoBack();
        //        e.Handled = true;
        //    }*/
        //    Frame.Navigate(typeof(MainPage));
        //}

        private  void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
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

            SharedInformattion.item = selected;
           Frame.Navigate(typeof(Itemdetail));
           // WebView webBrowserTask = new WebView();
            //Uri targetUri = new Uri(selected.Link);
            
            //webbrowser task launcher for Windows 8.1
            //http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh701480.aspx
           // var success = await Windows.System.Launcher.LaunchUriAsync(targetUri);

         
            
        }

        private void listfavorie(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListFavorie));
        }
    }
}
