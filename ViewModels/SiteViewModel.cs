using MVVMKitchenDemo1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMKitchenDemo1.ViewModels
{
    public class SiteViewModel : ViewModelBase
    {
        private string _siteId;
        private string _description;
        private bool _isActive;
        private string _message;
        private SiteService _siteService;
        private UserService _userService;
        private User _loggedInUser;
        private Site _currentSite;

        public string SiteId { get { return _siteId; } set { _siteId = value; OnPropertyChanged("SiteId"); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged("IsActive"); } }
        public string Message { get { return _message; } set { _message = value; OnPropertyChanged("Message"); } }

        private ObservableCollection<Site> _sites;
        public ObservableCollection<Site> SiteList
        {
            get { return _sites; }
            set { _sites = value; OnPropertyChanged("SiteList"); }
        }


        public Site CurrentSite
        {
            get { return _currentSite; }
            set { _currentSite = value; OnPropertyChanged("CurrentSite"); }
        }

        public ICommand AddButtonCommand { get; }
        public ICommand ClearButtonCommand { get; }
        public ICommand DeleteButtonCommad { get; }
        public ICommand UpdateButtonCommand { get; }

        public SiteViewModel()
        {
            _siteService = new SiteService();
            _userService = new UserService();
            CurrentSite = new Site();
            _loggedInUser = _userService.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name);
            CurrentSite.UserId = _loggedInUser.UserId;
            LoadSiteData();
            AddButtonCommand = new ViewModelCommand(ExecuteAddbuttonCommand, CanExecuteAddButtonCommand);
            ClearButtonCommand = new ViewModelCommand(ExecuteClearButtonCommand);
            DeleteButtonCommad = new ViewModelCommand(ExecuteDeleteButtonCommand, CanExecuteDeleteCommand);
            UpdateButtonCommand = new ViewModelCommand(ExecuteUpdateCommand, CanExecuteUpdateButtonCommand);
        }

        private bool CanExecuteUpdateButtonCommand(object obj)
        {
            if (CurrentSite != null
              && !string.IsNullOrEmpty(CurrentSite.Description)
              && CurrentSite.SiteId != 0)
                return true;

            return false;
        }

        private void ExecuteUpdateCommand(object obj)
        {
            var isUpdate = _siteService.Update(CurrentSite);

            if (isUpdate)
            {
                Message = "Update site successfully";
                LoadSiteData();
            }
            else
            {
                Message = "Update operation faield";
            }
        }

        private bool CanExecuteDeleteCommand(object obj)
        {
            if (CurrentSite != null && CurrentSite.SiteId != 0)
                return true;

            return false;
        }

        private void ExecuteDeleteButtonCommand(object obj)
        {
            var isDeleted = _siteService.Delete(CurrentSite.SiteId);

            if(isDeleted)
            {
                Message = "Deleting Site successfully!";
                LoadSiteData();
            }
            else
            {
                Message = "Delete operation failed";
            }
        }

        private void ExecuteClearButtonCommand(object obj)
        {
            ClearSitForm();
        }

        private bool CanExecuteAddButtonCommand(object obj)
        {
            if (CurrentSite != null
                && !string.IsNullOrEmpty(CurrentSite.Description)
                && CurrentSite.SiteId == 0)
                return true;

            return false;
        }

        private void ExecuteAddbuttonCommand(object obj)
        {
            var isAdded = _siteService.Add(CurrentSite);

            if (isAdded)
            {
                Message = "Added successfully!";
                LoadSiteData();
                ClearSitForm();
            }
        }

        private void LoadSiteData()
        {
            SiteList = new ObservableCollection<Site>(_siteService.GetSitesByUser(_loggedInUser.UserId));
        }

        private void ClearSitForm()
        {
            CurrentSite = new Site();
            CurrentSite.UserId = _loggedInUser.UserId;
        }
    }
}
