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
        #region Commands

        private DelegateCommand<object> searchProducts;

        public ICommand SearchProductsCommand
        {
            get
            {
                return searchProducts ?? (searchProducts = new DelegateCommand<object>(SearchProducts));
            }
        }

        private DelegateCommand<object> refreshProductsCommand;

        public ICommand RefreshProductsCommand
        {
            get
            {
                return refreshProductsCommand ?? (refreshProductsCommand = new DelegateCommand<object>(RefreshProducts));
            }
        }

        private RelayCommand addItemToCartCommand;

        public RelayCommand AddItemToCartCommand
        {
            get
            {
                if (addItemToCartCommand == null)
                {
                    addItemToCartCommand = new RelayCommand(param => this.AddItemToCart(param), param => this.CanAddItemToCart(param));
                }
                return addItemToCartCommand;
            }
        }

        private RelayCommand removeFromCartCommand;

        public RelayCommand RemoveFromCartCommand
        {
            get
            {
                if (removeFromCartCommand == null)
                {
                    removeFromCartCommand = new RelayCommand(param => this.RemoveFromCart(param));
                }
                return removeFromCartCommand;
            }
        }

        private DelegateCommand<object> clearCartCommand;

        public ICommand ClearCartCommand
        {
            get
            {
                return clearCartCommand ?? (clearCartCommand = new DelegateCommand<object>(ClearCart));
            }
        }

        private DelegateCommand<object> checkoutCommand;

        public ICommand CheckoutCommand
        {
            get
            {
                return checkoutCommand ?? (checkoutCommand = new DelegateCommand<object>(Checkout));
            }
        }
        #endregion
    }
}