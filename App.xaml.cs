using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVVMKitchenDemo1.ViewModels;
using MVVMKitchenDemo1.Views;
namespace MVVMKitchenDemo1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                //if (loginView.IsVisible == false)
                //{
                //    if (loginView.IsVisible == false && loginView.IsLoaded)
                //    {
                //        var mainView = new AdminView();
                //        mainView.Show();
                //        loginView.Close();
                //    }
                //}
            };
        }
    }
}
