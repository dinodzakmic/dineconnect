using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace DineConnectXF.View
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
        }

        private async void AddImage_OnClicked(object sender, EventArgs e)
        {
            try
            {
                MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions());

                if (file != null)
                {
                    AddImage.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });

                }
            }
            catch (Exception ex)
            {
                //ignored
            }
           
        }


        private async void TakeImage_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                if (file != null)
                {
                    TakeImage.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });

                }
            }
            catch (Exception ex)
            {
                //ignore
            }
            
        }
    }
}
