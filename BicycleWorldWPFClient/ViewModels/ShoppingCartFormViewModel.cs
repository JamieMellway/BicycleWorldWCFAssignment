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
    public partial class ShoppingCartFormViewModel : INotifyPropertyChanged
    {
        public ShoppingCartFormViewModel()
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                RefreshProducts(null);
                
                ShoppingCart = ShoppingCartClient.GetShoppingCartList();
                List<CategoryData> list = new List<CategoryData>();
                list.Add(new CategoryData() { ProductCategoryID = 0, Name = "All Categories" });
                list.AddRange(CategoriesClient.GetCategoryList().Where(c=>c.IsActive == true).OrderBy(c=>c.Name).ToList());
                CategoryList = list;
                SelectedCategory = CategoryList.FirstOrDefault(c => c.ProductCategoryID == 0);
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
