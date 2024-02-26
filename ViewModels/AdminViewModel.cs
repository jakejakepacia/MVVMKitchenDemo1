using MVVMKitchenDemo1.Models;
using MVVMKitchenDemo1.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MVVMKitchenDemo1.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {

		private string _welcomeText;
        private UserService _userService;
        private bool _isViewVisible = true;

        private bool _userButtonEnabled = true;

        public bool UserButtonEnable
        {
            get { return _userButtonEnabled; }
            set { _userButtonEnabled = value; }
        }
        public string WelcomeText
		{
			get { return _welcomeText; }
			set { _welcomeText = value; }
		}

        public bool IsViewVisible { get { return _isViewVisible; } set { _isViewVisible = value; OnPropertyChanged("IsViewVisible"); } }


        public ICommand UserButtonCommand { get; }
        public ICommand SiteButtonCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand EquipmentCommand { get; }

        public AdminViewModel()
        {
			UserButtonCommand = new ViewModelCommand(ExecuteUserButtonCommand, CanExecuteUserButtonCommand);
            LogoutCommand = new ViewModelCommand(ExecuteLogoutcommand);
            SiteButtonCommand = new ViewModelCommand(ExecuteSiteButtonCommand);
            EquipmentCommand = new ViewModelCommand(ExecuteEquipmentButtonCommand);
            _userService = new UserService();
            LoadCurrentUser();
        }

        private void ExecuteEquipmentButtonCommand(object obj)
        {
            var equipmentView = new EquipmentView();
            equipmentView.Show();
        }

        private void ExecuteSiteButtonCommand(object obj)
        {
            var siteView = new SiteView();
            siteView.Show();
        }

        private void ExecuteLogoutcommand(object obj)
        {
            var loginPage = new LoginView();
            loginPage.Show();
        }

        private void LoadCurrentUser()
        {
            User currentUser = _userService.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name);
            UserButtonEnable = currentUser.UserType.ToLower() == "superadmin" ? true : false;
            WelcomeText = $"WELCOME {currentUser.FirstName} {currentUser.LastName}";
        }
        private bool CanExecuteUserButtonCommand(object obj)
        {
            return true; //Add validation before executing command, for now its always true
        }

        private void ExecuteUserButtonCommand(object obj)
        {
            NavigateToUserPage(); // TO DO: Change to follow MVVM design pattern
        }

        private void NavigateToUserPage()
        {
            var adminView = new AdminView();
            adminView.Close();

            var userPage = new UserView();
            userPage.Show();
        }
    }
}
