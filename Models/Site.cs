using MVVMKitchenDemo1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMKitchenDemo1.Models
{
    public class Site : ViewModelBase
	{
        private int siteId;
    
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; OnPropertyChanged("SiteId"); }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged("UserId"); }
        }


        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged("IsActive"); }
        }

    
    }

}
