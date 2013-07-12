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
using System.Threading;

namespace BicycleWorldWPFClient
{
    public partial class ShoppingCartFormViewModel
    {
        public void SearchProducts(object item)
        {
            IEnumerable<ProductData> result;
            if (String.IsNullOrEmpty(DescriptionToFilterWith))
                result = productList;
            else
                result = productList.Where(p =>
                        p.ProductDescription.Contains(DescriptionToFilterWith, StringComparison.OrdinalIgnoreCase) ||
                        p.Name.Contains(DescriptionToFilterWith, StringComparison.OrdinalIgnoreCase) ||
                        p.ProductNumber.Contains(DescriptionToFilterWith, StringComparison.OrdinalIgnoreCase)
                    );

            if (SelectedCategory != null && SelectedCategory.ProductCategoryID != 0)
            {
                result = result.Where(c => c.CategoryID == SelectedCategory.ProductCategoryID);
            }

            FilteredProductList = result.ToList();
        }

        public void RefreshProducts(object item)
        {
            productList = ProductsClient.GetProductList().Where(p => p.IsActive == true && p.IsCategoryActive == true).ToList();
            SearchProducts(null);
        }

        public void AddItemToCart(object productNumberObject)
        {
            bool result = false;

            string productNumber = (string)productNumberObject;
            result = ShoppingCartClient.AddItemToCart(productNumber);
            if (!result) MessageBox.Show("Adding to Shopping Cart Failed.");
            else
            {
                ReduceProductQuantity((string)productNumberObject);
                RefreshShoppingCart();
            }
        }

        private bool CanAddItemToCart(object productNumberObject)
        {
            bool result = false;

            result = productList.Where(product => product.Quantity > 0 && product.ProductNumber == (string)productNumberObject).Count() > 0;
            return result;
        }

        private void RemoveFromCart(object productNumberObject)
        {
            bool result = false;

            string productNumber = (string)productNumberObject;
            result = ShoppingCartClient.RemoveItemFromCart(productNumber);
            if (!result) MessageBox.Show("Adding to Shopping Cart Failed.");
            else
            {
                IncreaseProductQuantity((string)productNumberObject);
                RefreshShoppingCart();
            }
        }

        private void ReduceProductQuantity(string productNumber)
        {
            var product = productList.Where(p => p.ProductNumber == productNumber).FirstOrDefault();
            if (product != null && product.Quantity > 0)
            {
                product.Quantity--;
                RaisePropertyChanged("FilteredProductList");
            }
        }

        private void IncreaseProductQuantity(string productNumber)
        {
            var product = productList.Where(p => p.ProductNumber == productNumber).FirstOrDefault();
            if (product != null)
            {
                product.Quantity++;
                RaisePropertyChanged("FilteredProductList");
            }
        }

        public void ClearCart(object item)
        {
            foreach (var cartItem in shoppingCart)
            {
                for (int i = 0; i < cartItem.Quantity; i++)
                {
                    RemoveFromCart(cartItem.ProductNumber);
                }
            }
        }

        public void Checkout(object item)
        {
            bool result = false;
            EnableView = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                result = ShoppingCartClient.Checkout();
            }
            finally
            {
                EnableView = true;
                Mouse.OverrideCursor = null;
            }

            if (!result) MessageBox.Show("Checkout failed.");
            else
            {
                MessageBox.Show("Checkout successful.");
                RefreshShoppingCart();
            }
        }

        private void RefreshShoppingCart()
        {
            try
            {
                ShoppingCart = ShoppingCartClient.GetShoppingCartList();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
