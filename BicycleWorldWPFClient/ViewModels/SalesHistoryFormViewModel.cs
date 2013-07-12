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
    public partial class SalesHistoryFormViewModel : INotifyPropertyChanged
    {
        public SalesHistoryFormViewModel()
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                SalesHistoryList = SalesHistoryClient.GetSalesHistory();
                if (SalesHistoryList.Count == 0) MessageBox.Show(String.Format("{0} has no orders on record.", Username));
            }
        }

        public string Username { get { return LoginUser.Current.Username; } }

        private List<SalesHistoryData> salesHistoryList = new List<SalesHistoryData>();
        public List<SalesHistoryData> SalesHistoryList
        {
            get { return salesHistoryList; }
            set
            {
                salesHistoryList = value;
                RaisePropertyChanged("SalesHistoryList");
            }
        }

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
