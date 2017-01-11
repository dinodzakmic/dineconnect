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
           
    }
}
