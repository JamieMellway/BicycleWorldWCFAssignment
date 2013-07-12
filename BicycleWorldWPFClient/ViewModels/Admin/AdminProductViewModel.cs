using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using BicycleWorldServiceModelClient.BicycleWorldService;
using BicycleWorldClientSession;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BicycleWorldWPFClient
{
    public class AdminProductViewModel : INotifyPropertyChanged
    {
        public AdminProductViewModel()
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                ProductList = new ObservableCollection<ProductData>(ProductsClient.GetProductList());
                CategoryList = new ObservableCollection<CategoryData>(CategoriesClient.GetCategoryList());
            }
        }

        public ObservableCollection<CategoryData> CategoryList { get; set; }

        private ObservableCollection<ProductData> productList = null;
        public ObservableCollection<ProductData> ProductList
        {
            get { return productList; }
            set { productList = value; RaisePropertyChanged("ProductList"); }
        }

        private ProductData selectedProduct = null;
        public ProductData SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                try
                {
                    ProductName = (selectedProduct != null) ? selectedProduct.Name : String.Empty;
                    ProductNumber = (selectedProduct != null) ? selectedProduct.ProductNumber : String.Empty;
                    Color = (selectedProduct != null) ? selectedProduct.Color : String.Empty;
                    Quantity = (selectedProduct != null) ? selectedProduct.Quantity : 0;
                    ListPrice = (selectedProduct != null) ? selectedProduct.ListPrice : 0;
                    ProductDescription = (selectedProduct != null) ? selectedProduct.ProductDescription : String.Empty;
                    CategoryID = (selectedProduct != null) ? selectedProduct.CategoryID : 0;
                    ProductIsActive = (selectedProduct != null) ? value.IsActive : false;
                }
                catch (Exception)
                { throw; }
                RaisePropertyChanged("ProductID");
                RaisePropertyChanged("SelectedProduct");
                deleteProductCommand.RaiseCanExecuteChanged();
            }
        }


        public string ProductID
        {
            get { return (SelectedProduct != null) ? SelectedProduct.ProductID.ToString() : "New"; }
        }

        private string productName = String.Empty;
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                RaisePropertyChanged("ProductName");
                editProductCommand.RaiseCanExecuteChanged();
            }
        }

        private string productNumber;
        public string ProductNumber { get { return productNumber; } set { productNumber = value; RaisePropertyChanged("ProductNumber"); } }

        private string color = String.Empty;
        public string Color { get { return color; } set { color = value; RaisePropertyChanged("Color"); } }

        private decimal listPrice;
        public decimal ListPrice { get { return listPrice; } set { listPrice = value; RaisePropertyChanged("ListPrice"); } }

        private string productDescription;
        public string ProductDescription { get { return productDescription; } set { productDescription = value; RaisePropertyChanged("ProductDescription"); } }

        private int quantity;
        public int Quantity { get { return quantity; } set { quantity = value; RaisePropertyChanged("Quantity"); } }

        public CategoryData SelectedCategory
        {
            get { return CategoryList.FirstOrDefault(c => c.ProductCategoryID == CategoryID); }
            set
            {
                CategoryID = (value != null) ? value.ProductCategoryID : 0;
                RaisePropertyChanged("SelectedCategory");
            }
        }

        private int categoryID;
        public int CategoryID { get { return categoryID; } set { categoryID = value; RaisePropertyChanged("CategoryID"); RaisePropertyChanged("SelectedCategory"); } }

        private bool productIsActive = false;
        public bool ProductIsActive
        {
            get { return productIsActive; }
            set
            {
                productIsActive = value;
                RaisePropertyChanged("ProductIsActive");
            }
        }

        #region Methods
        public void CreateNewProduct(object item)
        {
            SelectedProduct = null;
        }

        public void EditProduct(object item)
        {
            if (CanEditProduct(null))
            {
                if (SelectedProduct != null)
                {
                    ProductData productItem = new ProductData()
                    {
                        ProductID = SelectedProduct.ProductID,
                        Name = ProductName,
                        ProductNumber = ProductNumber,
                        Color = Color,
                        ListPrice = ListPrice,
                        ProductDescription = ProductDescription,
                        Quantity = Quantity,
                        CategoryID = CategoryID,
                        IsActive = ProductIsActive
                    };
                    Product updatedProduct = ProductsClient.UpdateProduct(productItem);

                    ProductData productInList = ProductList.First(p => p.ProductID == updatedProduct.ProductID);
                    productInList.Name = updatedProduct.Name;
                    productInList.Color = updatedProduct.Color;
                    productInList.ListPrice = updatedProduct.ListPrice;
                    productInList.ProductNumber = updatedProduct.ProductNumber;
                    productInList.ProductDescription = updatedProduct.ProductDescription;
                    productInList.Quantity = updatedProduct.Quantity;
                    productInList.IsActive = updatedProduct.IsActive;
                    productInList.SalesOrderCount = updatedProduct.SalesOrderItems.Count;

                    productInList.CategoryID = (int)updatedProduct.ProductCategoryID;
                    CategoryData category = CategoryList.FirstOrDefault(c => c.ProductCategoryID == (int)updatedProduct.ProductCategoryID);
                    productInList.CategoryName = (category != null) ? category.Name : String.Empty;
                    productInList.IsCategoryActive = (category != null) ? category.IsActive : false;
                }
                else
                {
                    try
                    {
                        ProductData productItem = new ProductData()
                        {
                            ProductID = 0,
                            Name = ProductName,
                            CategoryID = CategoryID,
                            Color = Color,
                            ListPrice = ListPrice,
                            ProductDescription = ProductDescription,
                            ProductNumber = ProductNumber,
                            Quantity = Quantity,
                            IsActive = ProductIsActive
                        };
                        Product updatedProduct = ProductsClient.UpdateProduct(productItem);

                        if (updatedProduct != null)
                        {
                            ProductList.Add(new ProductData()
                            {
                                ProductID = updatedProduct.ProductID,
                                Name = updatedProduct.Name,
                                CategoryID = (int)updatedProduct.ProductCategoryID,
                                Color = updatedProduct.Color,
                                ListPrice = updatedProduct.ListPrice,
                                ProductDescription = updatedProduct.ProductDescription,
                                ProductNumber = updatedProduct.ProductNumber,
                                Quantity = updatedProduct.Quantity,
                                IsActive = updatedProduct.IsActive,
                                SalesOrderCount = 0
                            });
                        }
                        else
                        { MessageBox.Show("Save failed."); }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        public bool CanEditProduct(object item)
        {
            return (ProductName.Length > 0);
        }

        public void DeleteProduct(object item)
        {
            if (CanDeleteProduct(null))
            {
                bool result = false;
                result = ProductsClient.DeleteProduct(SelectedProduct.ProductID);
                if (result)
                {
                    ProductData productJustDeleted = ProductList.First(c => c.ProductID == SelectedProduct.ProductID);
                    ProductList.Remove(productJustDeleted);
                    MessageBox.Show("Delete successful");
                }
                else { MessageBox.Show("Delete failed"); }
            }
        }

        public bool CanDeleteProduct(object item)
        {
            return (SelectedProduct != null && SelectedProduct.SalesOrderCount == 0);
        }

        #endregion

        #region Commands

        private DelegateCommand<object> createNewProductCommand;

        public ICommand CreateNewProductCommand
        {
            get
            {
                return createNewProductCommand ?? (createNewProductCommand = new DelegateCommand<object>(CreateNewProduct));
            }
        }

        private DelegateCommand<object> editProductCommand;

        public ICommand EditProductCommand
        {
            get
            {
                return editProductCommand ?? (editProductCommand = new DelegateCommand<object>(EditProduct, CanEditProduct));
            }
        }

        private DelegateCommand<object> deleteProductCommand;

        public ICommand DeleteProductCommand
        {
            get
            {
                return deleteProductCommand ?? (deleteProductCommand = new DelegateCommand<object>(DeleteProduct, CanDeleteProduct));
            }
        }
        #endregion

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
