using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineConnectXF.Helpers;
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

        protected override void OnAppearing()
        {
            App.Locator.Start.LocationList = WebHelper.Locations.Result.Items;
            App.Locator.Start.SupplierList = WebHelper.Suppliers.Result.Items;

            foreach (var item in App.Locator.Start.LocationList)
            {
                LocationPicker.Items.Add(item.ToString());
            }
            foreach (var item in App.Locator.Start.SupplierList)
            {
                SupplierPicker.Items.Add(item.ToString());
            }
        }


        private async void AddImage_OnClicked(object sender, EventArgs e)
        {
            try
            {
                MediaFile file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Large
                });

                if (file != null)
                {
                    FrameUpload.IsVisible = true;
                    ImageUpload.Source = ImageSource.FromStream(() =>
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
                
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                    PhotoSize =  PhotoSize.Large
                });

                if (file != null)
                {
                    FrameUpload.IsVisible = true;
                    ImageUpload.Source = ImageSource.FromStream(() =>
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

        private void SpecificPeriod_OnTapped(object sender, EventArgs e)
        {
            if (CalendarGrid.IsVisible) return;

            SpecificRadio.Source = "radioselected";
            AllRadio.Source = "radio";
            CalendarGrid.IsVisible = true;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (!CalendarGrid.IsVisible) return;

            AllRadio.Source = "radioselected";
            SpecificRadio.Source = "radio";
            CalendarGrid.IsVisible = false;
        }
    }
}
