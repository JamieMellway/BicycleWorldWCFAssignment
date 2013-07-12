using System;
using System.ComponentModel;

namespace BicycleWorldClientSession
{
    public class CategoryData : INotifyPropertyChanged
    {
        private string name = String.Empty;
        private bool isActive = true;

        public int ProductCategoryID { get; set; }
        public string Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); } }
        public bool IsActive { get { return isActive; } set { isActive = value; RaisePropertyChanged("IsActive"); } }

        public int ProductCount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
