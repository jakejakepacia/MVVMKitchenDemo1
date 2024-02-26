using MVVMKitchenDemo1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMKitchenDemo1.Models
{
    public class Equipment : ViewModelBase
    {
        private int equipmentId;

        public int EquipmentId
        {
            get { return equipmentId; }
            set { equipmentId = value; OnPropertyChanged("EquipmentId"); }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged("UserId"); }
        }

        private int serialNumber;

        public int SerialNumber
        {
            get { return serialNumber; }
            set { serialNumber = value; OnPropertyChanged("SerialNumber"); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        private string condition;

        public string Condition
        {
            get { return condition; }
            set { condition = value; OnPropertyChanged("Condition"); }
        }

        private int siteId;

        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; OnPropertyChanged("SiteId"); }
        }

        private string _siteDescription;
        public string SiteDescription
        {
            get { return _siteDescription; }
            set { _siteDescription = value; OnPropertyChanged("SiteId"); }
        }
    }
}
