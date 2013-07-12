using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BicycleWorldServiceModelClient;
using BicycleWorldServiceModelClient.BicycleWorldService;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using BicycleWorldWPFClient.Commands;
using BicycleWorldClientSession;

namespace BicycleWorldWPFClient
{
    public partial class AdminFormViewModel : INotifyPropertyChanged
    {
        public AdminFormViewModel()
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
            }
        }

        public string Username { get { return LoginUser.Current.Username; } }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}
