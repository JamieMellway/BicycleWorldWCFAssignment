using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace BicycleWorldWPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            bool? exit = false;
            bool? authenticated = null;
            Login loginWindow;
            ShoppingCartForm shoppingCartForm;

            while (authenticated != true)
            {
                loginWindow = new Login();
                exit = loginWindow.ShowDialog();
                authenticated = loginWindow.Authenicated;
                if (exit == false) break;

                if (authenticated == true)
                {
                    shoppingCartForm = new ShoppingCartForm();
                    try
                    {
                        exit = shoppingCartForm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        exit = true;
                    }
                    authenticated = shoppingCartForm.Authenticated;
                    if (exit == false) break;
                }
            }
            this.Shutdown();
        }
    }
}
