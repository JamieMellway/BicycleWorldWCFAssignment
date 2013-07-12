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
    public partial class ShoppingCartFormViewModel
    {
        public string Username { get { return LoginUser.Current.Username; } }

        public List<CategoryData> CategoryList { get; set; }

        private CategoryData selectedCategory = null;
        public CategoryData SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged("SelectedCategory");
            }
        }

        public Visibility AdminVisibility
        {
            get
            {
                return LoginUser.Current.IsUserAdmin ? Visibility.Visible : Visibility.Collapsed;
            }
        }


        private bool enableView = true;
        public bool EnableView
        {
            get { return enableView; }
            set
            {
                enableView = value;
                RaisePropertyChanged("EnableView");
            }
        }


        private List<ProductData> productList { get; set; }

        public string DescriptionToFilterWith { get; set; }
        private List<ProductData> filteredProductList = null;
        public List<ProductData> FilteredProductList
        {
            get
            {
                if (filteredProductList == null)
                    return productList.ToList<ProductData>();

                return filteredProductList;
            }
            set
            {
                filteredProductList = value;
                RaisePropertyChanged("FilteredProductList");
            }
        }

        private List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>();

        public List<ShoppingCartItem> ShoppingCart
        {
            get { return shoppingCart; }
            set
            {
                shoppingCart = value;
                RaisePropertyChanged("ShoppingCart");
                RaisePropertyChanged("ShoppingCartTotal");
                RaisePropertyChanged("ShoppingCartCount");
            }
        }

        public decimal ShoppingCartTotal
        {
            get { return shoppingCart.Sum(item => (item.Quantity * item.Cost)); }
        }

        public decimal ShoppingCartCount
        {
            get { return shoppingCart.Sum(item => item.Quantity); }
        }
    }
}
