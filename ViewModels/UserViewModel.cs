using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MVVMKitchenDemo1.Models;
using MVVMKitchenDemo1.Commands;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using MVVMKitchenDemo1.Views;

namespace MVVMKitchenDemo1.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        UserService ObjUserService;
        
        public UserViewModel()
        {
            ObjUserService = new UserService();
            LoadData();
            CurrentUser = new User();
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }
      

        public ICommand BackButtonCommand { get; }
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

        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

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
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

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
