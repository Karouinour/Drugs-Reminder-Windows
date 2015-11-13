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
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.SpeechSynthesis;
using System.Diagnostics;
using Windows.UI.Popups;
using System.Collections.ObjectModel;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace DrugsReminderStat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Itemdetail : Page
    {
        public ObservableCollection<ItemsRSS> li = new ObservableCollection<ItemsRSS>();
        MediaElement m = new MediaElement();
        SpeechSynthesizer synth = new SpeechSynthesizer();
        int i = 0;
        //SpeechSynthesisStream s;
        
        public  Itemdetail()
        {
            this.InitializeComponent();
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
           
         //   Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            
        }

        async void lecture()
        {
            SpeechSynthesizer.AllVoices.First();
            Debug.WriteLine(synth.Voice);
            SpeechSynthesisStream s = await synth.SynthesizeTextToStreamAsync(SharedInformattion.item.Description);
            m.SetSource(s, s.ContentType);
            m.Stop();
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

       

       /* private async void ecouter(object sender, TappedRoutedEventArgs e)
        {
            MediaElement m = new MediaElement();
            var synth = new SpeechSynthesizer();
            SpeechSynthesizer.AllVoices.First();
            Debug.WriteLine(synth.Voice);
               //  await VoiceCommandService.InstallCommandSetsFromFileAsync(new Uri("ms-appx:///ToothBrushingTimerVoiceCommands.xml"));
            SpeechSynthesisStream s = await synth.SynthesizeTextToStreamAsync(SharedInformattion.item.Description);
            m.SetSource(s, s.ContentType);
            m.Play();
        }*/

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //MediaElement m = new MediaElement();
            //var synth = new SpeechSynthesizer();
          // SpeechSynthesizer.AllVoices.First();
            //Debug.WriteLine(synth.Voice);
            //  await VoiceCommandService.InstallCommandSetsFromFileAsync(new Uri("ms-appx:///ToothBrushingTimerVoiceCommands.xml"));
           
            if(i == 0){
            SpeechSynthesisStream s = await synth.SynthesizeTextToStreamAsync(SharedInformattion.item.Description);
            m.SetSource(s, s.ContentType);
            m.Play();
            i = i + 1;
            ecouticone.Icon = new SymbolIcon(Symbol.Pause);
            }
            else
            {
                i = 0;
                m.Pause();
                ecouticone.Icon = new SymbolIcon(Symbol.Play);
            }
           

        }

        private async void Addfavofire(object sender, RoutedEventArgs e)
        {
            if (IsolatedStorageHelper.GetObject<ObservableCollection<ItemsRSS>>("favori_list") == null)
            {
                IsolatedStorageHelper.SaveObject("favori_list", li);
            }
            li = IsolatedStorageHelper.GetObject<ObservableCollection<ItemsRSS>>("favori_list");

            li.Add(SharedInformattion.item);
           /* foreach (var fav in li)
            {
                if (fav.Title != SharedInformattion.item.Title && fav.Image != SharedInformattion.item.Image || li.Count ==0)
                {
                    li.Add(SharedInformattion.item);
                }
                else
                {
                    MessageDialog msg = new MessageDialog("This New existe to the favorie ! ", "Favorie !");
                    await msg.ShowAsync();
                }
            }*/


            IsolatedStorageHelper.SaveObject("favori_list", li);

        }
    }
}
