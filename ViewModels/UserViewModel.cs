using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MVVMKitchenDemo1.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using MVVMKitchenDemo1.Views;

namespace MVVMKitchenDemo1.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        UserService ObjUserService;
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region DisplayOperation
        private ObservableCollection<User> userList;

        public ObservableCollection<User> UserList
        {
            get { return userList; }
            set { userList = value; OnPropertyChanged("UserList"); }
        }

        public ICommand UpdateButtonCommand { get; }
        public ICommand DeleteButtonCommand { get; }
        public ICommand BackButtonCommand { get; }


        public UserViewModel()
        {
            ObjUserService = new UserService();
            LoadData();
            CurrentUser = new User();
         
            UpdateButtonCommand = new ViewModelCommand(ExecuteUpdateCommand, CanExecuteCommand);
            DeleteButtonCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteCommand);
        }

        private void ExecuteDeleteCommand(object obj)
        {
            Delete();
        }

        private void ExecuteUpdateCommand(object obj)
        {
            Update();
        }

        private bool CanExecuteCommand(object obj)
        {
            if (CurrentUser != null
                 && !string.IsNullOrEmpty(CurrentUser.FirstName)
                 && !string.IsNullOrEmpty(CurrentUser.LastName)
                 && !string.IsNullOrEmpty(CurrentUser.Username)
                 && !string.IsNullOrEmpty(CurrentUser.Password)
                 && !string.IsNullOrEmpty(CurrentUser.EmailAddress)
                 && !string.IsNullOrEmpty(CurrentUser.UserType))
                return true;

            return false;
        }

       
        private void LoadData()
        {
            UserList = new ObservableCollection<User>(ObjUserService.GetAll());
        }
        #endregion
        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; OnPropertyChanged("CurrentUser"); }
        }

        #region SaveOperation

   

        #endregion

        #region Update Operation


        public void Update()
        {
            try
            {
                var IsUpdated = ObjUserService.Update(CurrentUser);
                LoadData();
                if (IsUpdated)
                    Message = "User updated successfully";
                else
                    Message = "Update operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        #endregion

        #region Delete Operation
        public void Delete()
        {
            try
            {
                var IsDeleted = ObjUserService.Delete(CurrentUser.UserId);
                if (IsDeleted)
                {
                    Message = "Deleting user success";
                    LoadData();
                }
                else
                {
                    Message = "Delete operation failed";
                }
            }
            catch(Exception e)
            {
                Message = e.Message;
            }
        }

        #endregion

    }
}
