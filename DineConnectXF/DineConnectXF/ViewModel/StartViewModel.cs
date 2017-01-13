using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineConnectXF.Helpers;
using DineConnectXF.Model;
using GalaSoft.MvvmLight;

namespace DineConnectXF.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        private List<Location> _locationList = new List<Location>();
        private List<Supplier> _supplierList = new List<Supplier>();
        private bool _specificSelected;

        public List<Location> LocationList
        {
            get { return _locationList; }
            set
            {
                _locationList = value;
                RaisePropertyChanged(() => LocationList);
            }
        }

        public List<Supplier> SupplierList
        {
            get { return _supplierList; }
            set
            {
                _supplierList = value;
                RaisePropertyChanged(() => SupplierList);
            }
        }

        public StartViewModel()
        {          
        }

       
    }
}
