using MVVMKitchenDemo1.Commands;
using MVVMKitchenDemo1.Models;
using MVVMKitchenDemo1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMKitchenDemo1.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private UserService _userService;
        private bool _isViewVisible = true;
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged("Message"); }
        }

        public string Username { get { return _username; } set { _username = value; OnPropertyChanged("Username"); } }
        public string Password { get { return _password;} set { _password = value; OnPropertyChanged("Password"); } }

        public bool IsViewVisible { get { return _isViewVisible; } set { _isViewVisible = value; OnPropertyChanged("IsViewVisible"); } }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand);
            _userService = new UserService();

        }

        private void ExecuteRegisterCommand(object obj)
        {
            var registerPage = new RegisterView();
            registerPage.Show();
        }

        private bool CanExecuteLoginCommand(object obj)
        {

            if(!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                return true;

            return false;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var test = _userService.GetUserByCredentials(Username, Password);
            if (test.Count > 0) 
            {

                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
              
                var adminPage = new AdminView();
                adminPage.Show();

                IsViewVisible = false;
            }
            else
            {
                Message = "Invalid username or password";
            }
        }

    }
}
