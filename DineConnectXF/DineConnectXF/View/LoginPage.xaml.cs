using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DineConnectXF.Helpers;
using DineConnectXF.Model;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace DineConnectXF.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            App.UpdateLoading(true);
            User user = new User("DP", "admin", "123qwe");
            WebHelper.getInstance("http://dineplan.dynns.com".ToString(), user);

            var success = await WebHelper.getInstance().Auth();
            if (success)
            {
                App.Current.MainPage = new MasterDetailPage()
                {
                    Master = new MenuPage(),
                    Detail = new NavigationPage(new StartPage()),
                    Title = "Dine Connect"
                };

                App.UpdateLoading(false);
                UserDialogs.Instance.ShowSuccess("Successfully login!");
            }
            else
            {
                App.UpdateLoading(false);
                UserDialogs.Instance.ShowError("Unable to login!");
            }
        }
    }
}
