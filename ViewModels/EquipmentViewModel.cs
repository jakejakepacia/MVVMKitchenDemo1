using MVVMKitchenDemo1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MVVMKitchenDemo1.ViewModels
{
    public class EquipmentViewModel : ViewModelBase
    {
        private Equipment _currentEquipment;
        private EquipmentService _equipmentService;
        private User _loggedInUser;
        private UserService _userService;
        private SiteService _siteService;
        private string _message;
        private bool _isViewEnable = true;
        public string Message { get { return _message; } set { _message = value; OnPropertyChanged("Message"); } }
        public bool IsViewEnable { get { return _isViewEnable; } set { _isViewEnable = value; OnPropertyChanged("IsViewEnable"); } }

        private IDictionary<int, string> site;

        public IDictionary<int, string> SiteList
        {
            get { return site; }
            set { site = value; OnPropertyChanged("SiteList"); }
        }


        public Equipment CurrentEquipment
        {
            get { return _currentEquipment; }
            set { _currentEquipment = value; OnPropertyChanged("CurrentEquipment"); }
        }

        private ObservableCollection<Equipment> _equipmentList;

        public ObservableCollection<Equipment> EquipmentList
        {
            get { return _equipmentList; }
            set { _equipmentList = value; OnPropertyChanged("EquipmentList"); }
        }

        public ICommand DeleteButtonCommand { get; }
        public ICommand AddButtonCommand { get; }
        public ICommand UpdateButtonCommand { get; }
        public ICommand ClearButtonCommand { get; }


        public EquipmentViewModel()
        {
           _equipmentService = new EquipmentService();
            CurrentEquipment = new Equipment();
            _userService = new UserService();
            _siteService= new SiteService();

            _loggedInUser = _userService.GetUserByUsername(Thread.CurrentPrincipal.Identity.Name);
            LoadEquipmentData();
            PopulateSiteOptions();

            DeleteButtonCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteCommand);
            AddButtonCommand = new ViewModelCommand(ExecuteAddCommand, CanExectueAddCommand);
            UpdateButtonCommand = new ViewModelCommand(ExecuteUpdateCommand, CanExecuteCommand);
            ClearButtonCommand = new ViewModelCommand(ExecuteClearCommand);
        }

        private void PopulateSiteOptions()
        {
            var siteList = _siteService.GetSitesByUser(_loggedInUser.UserId);
          
            SiteList = new Dictionary<int, string>();
            foreach (var site in siteList)
            {
                SiteList.Add(site.SiteId, site.Description);
            }

        }

        private bool CanExecuteCommand(object obj)
        {
            if (CurrentEquipment != null && CurrentEquipment.EquipmentId != 0
                  && !string.IsNullOrEmpty(CurrentEquipment.Description)
                  && !string.IsNullOrEmpty(CurrentEquipment.Condition)
                  && CurrentEquipment.SerialNumber > 0)
            {
                IsViewEnable = false;
                return true;
            }


            IsViewEnable = true;
            return false;
        }

        private void ExecuteUpdateCommand(object obj)
        {
            var isUpdated = _equipmentService.Update(CurrentEquipment);

            if(isUpdated)
            {
                Message = "Updated successfully!";
                LoadEquipmentData();
            }
            else
            {
                Message = "Update operation failed!";

            }
        }

        private void ExecuteClearCommand(object obj)
        {
            CurrentEquipment = new Equipment();
        }

        private bool CanExectueAddCommand(object obj)
        {
            if(CurrentEquipment != null && CurrentEquipment.EquipmentId == 0 
                && !string.IsNullOrEmpty(CurrentEquipment.Description) 
                && !string.IsNullOrEmpty(CurrentEquipment.Condition)
                && CurrentEquipment.SerialNumber > 0
                && CurrentEquipment.SiteId != 0)
                return true;

            return false;
        }

        private void ExecuteAddCommand(object obj)
        {
            var isAdded = _equipmentService.Add(CurrentEquipment);

            if (isAdded)
            {
                LoadEquipmentData();
                Message = "Adding equipment successfully!";
            }
        }

        private void ExecuteDeleteCommand(object obj)
        {
            var isDeleted = _equipmentService.Delete(CurrentEquipment.EquipmentId);

            if(isDeleted)
            {
                LoadEquipmentData();
                Message = "Delete operation success!";
            }
            else
            {
                Message = "Delete operation failed!";
            }
        }

        private void LoadEquipmentData()
        {
            CurrentEquipment.UserId = _loggedInUser.UserId;
            EquipmentList = new ObservableCollection<Equipment>(_equipmentService.GetEquipmentByUserId(CurrentEquipment.UserId));
        }
    }
}
