using MVVMKitchenDemo1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMKitchenDemo1.Models
{
    public class UserType : ViewModelBase
    {

		private string _utype;

		public string UType
		{
			get { return _utype; }
			set { _utype = value; OnPropertyChanged("UType"); }
		}

	}
}
