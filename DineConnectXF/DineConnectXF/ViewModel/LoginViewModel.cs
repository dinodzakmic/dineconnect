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

        private List<Location> _locationList;
        private List<Supplier> _supplierList;
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


        public List<Location> LocationList
        {
            get { return _locationList; }
            set
            {
                _locationList = value; 
                RaisePropertyChanged(() => _locationList);
            }
        }

        public List<Supplier> SupplierList
        {
            get { return _supplierList; }
            set
            {
                _supplierList = value;
                RaisePropertyChanged(() => _supplierList);
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
                    //var outlet = await WebHelper.getInstance().PurchLoc();
                    //LocationList = outlet.Result.Items;

                    //var suppliers = await WebHelper.getInstance().Suppliers();
                    //SupplierList = suppliers.Result.Items;

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
