using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Xamarin.Forms;

namespace DineConnectXF.Droid
{
    [Activity(Label = "DineConnectXF", Icon = "@drawable/icondine", Theme = "@style/splashscreen", 
     MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);

            SetStatusBarColor(new Android.Graphics.Color(56, 140, 59));
            Window.SetNavigationBarColor(new Android.Graphics.Color(56, 140, 59));
            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            LoadApplication(new App());
        }
    }
}

