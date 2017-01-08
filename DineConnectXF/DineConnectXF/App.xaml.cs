using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using DineConnectXF.View;
using Xamarin.Forms;

namespace DineConnectXF
{
    public partial class App : Application
    {
        public static NavigationPage NavPage;
        public static MasterDetailPage MenuDetailPage;
        public App()
        {
            InitializeComponent();
            NavPage = new NavigationPage(new LoginPage() {Title = "Login"});
            MenuDetailPage = new MasterDetailPage() {Detail = NavPage,
                Master = new ContentPage() {Title = "Login"}, IsGestureEnabled = false, Title = "Login"};
            MainPage = new LoginPage();
        }

        public static void UpdateLoading(bool isLoading, string text = "Loading ...")
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (isLoading)
                    UserDialogs.Instance.ShowLoading(text, MaskType.Black);
                else
                    UserDialogs.Instance.HideLoading();
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
