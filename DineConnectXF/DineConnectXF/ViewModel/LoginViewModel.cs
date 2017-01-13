using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using DineConnectXF.Helpers;
using DineConnectXF.Model;
using DineConnectXF.View;
using GalaSoft.MvvmLight;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace DineConnectXF.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        #region Private properties
        private string _url;
        private string _tenant;
        private string _user;
        private string _password;
        private bool _loginButtonEnabled;

       
        #endregion
        #region Public properties
        public string Url
        {
            get { return _url;}
            set
            {
                _url = value;
                RaisePropertyChanged(() => Url);
            }
        }

        public string Tenant
        {
            get { return _tenant;}
            set
            {
                _tenant = value;
                RaisePropertyChanged(() => Tenant);
            }
        }

        public string User
        {
            get { return _user;}
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }

        public string Password
        {
            get { return _password;}
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public bool LoginButtonEnabled
        {
            get { return _loginButtonEnabled; }
            set
            {
                _loginButtonEnabled = value; 
                RaisePropertyChanged(() => LoginButtonEnabled);
            }
        }


        
        #endregion

        #region Commands
        public ICommand LoginCommand { private set; get; }
        #endregion

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }

        #region Methods
        private async Task Login()
        {
            LoginButtonEnabled = false;

            App.UpdateLoading(true);
            User user = new User(Tenant, User, Password);

#warning URL hardcoded
            WebHelper.getInstance("http://dineplan.dynns.com", user);

            bool successLogin = false;
            try
            {
                successLogin = await WebHelper.getInstance().Auth();
                if (successLogin)
                {
                    App.Current.MainPage = new MasterDetailPage()
                    {
                        Master = new MenuPage(),
                        Detail = new NavigationPage(new StartPage()),
                        Title = "Dine Connect"
                    };
                }
            }
            catch (Exception e)
            {
                // ignored
            }
            finally
            {
                App.UpdateLoading(false);
                if (successLogin)
                {
                    UserDialogs.Instance.ShowSuccess("Successfully login!");
                }
                else
                {
                    UserDialogs.Instance.ShowError("Unable to login!");
                }

                LoginButtonEnabled = true;
            }

        }
        
        #endregion

    }
}
