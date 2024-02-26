using MVVMKitchenDemo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MVVMKitchenDemo1.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        UserService _userService;

        private User _currentUser;

        private string _message;



        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged("Message"); }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged("CurrentUser"); }
        }
        public ICommand SignupCommand { get; }

        public RegisterViewModel()
        {
            _userService = new UserService();
             _currentUser = new User();
            SignupCommand = new ViewModelCommand(ExecuteSingupCommand, CanExecuteSignupCommand);
 
        }

        private bool CanExecuteSignupCommand(object obj)
        {
            if (_currentUser != null
                && !string.IsNullOrEmpty(_currentUser.FirstName)
                && !string.IsNullOrEmpty(_currentUser.LastName)
                && !string.IsNullOrEmpty(_currentUser.Username)
                && !string.IsNullOrEmpty(_currentUser.Password)
                && !string.IsNullOrEmpty(_currentUser.EmailAddress)
                && !string.IsNullOrEmpty(_currentUser.UserType))
                return true;

            return false;
        }


        private void ExecuteSingupCommand(object obj)
        {
            Save();
        }

        public void Save()
        {
            try
            {
                var IsSave = _userService.Add(CurrentUser);
                //LoadData();
                if (IsSave)
                {
                    ResetCurrentUser();
                    Message = "User saved";
                }
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                //Message = ex.Message;
            }
        }

        private void ResetCurrentUser()
        {
            string empty = string.Empty;
            _currentUser.Username = empty; 
            _currentUser.Password = empty;
            _currentUser.FirstName = empty;
            _currentUser.LastName = empty;
            _currentUser.EmailAddress = empty;
            _currentUser.UserType = empty;
        }
    }

}
