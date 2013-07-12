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
    public class AdminCategoryViewModel : INotifyPropertyChanged
    {
        public AdminCategoryViewModel()
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                CategoryList = new ObservableCollection<CategoryData>(CategoriesClient.GetCategoryList());
            }
        }

        private ObservableCollection<CategoryData> categoryList = null;
        public ObservableCollection<CategoryData> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; RaisePropertyChanged("CategoryList"); }
        }

        private CategoryData selectedCategory = null;
        public CategoryData SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                CategoryName = (selectedCategory != null) ? value.Name : String.Empty;
                CategoryIsActive = (selectedCategory != null) ? value.IsActive : false;
                RaisePropertyChanged("CategoryID");
                RaisePropertyChanged("SelectedCategory");
                deleteCategoryCommand.RaiseCanExecuteChanged();
            }
        }


        public string CategoryID
        {
            get { return (SelectedCategory != null) ? SelectedCategory.ProductCategoryID.ToString() : "New"; }
        }

        private string categoryName = String.Empty;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                RaisePropertyChanged("CategoryName");
                editCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        private bool categoryIsActive = false;
        public bool CategoryIsActive
        {
            get { return categoryIsActive; }
            set
            {
                categoryIsActive = value;
                RaisePropertyChanged("CategoryIsActive");
            }
        }

        #region Methods
        public void CreateNewCategory(object item)
        {
            SelectedCategory = null;
        }

        public void EditCategory(object item)
        {
            if (CanEditCategory(null))
            {
                if (SelectedCategory != null)
                {
                    CategoryData categoryItem = new CategoryData()
                    {
                        ProductCategoryID = SelectedCategory.ProductCategoryID,
                        Name = CategoryName,
                        IsActive = CategoryIsActive
                    };
                    ProductCategory updatedProductCategory = CategoriesClient.UpdateCategory(categoryItem);

                    CategoryData categoryInList = CategoryList.FirstOrDefault(cat => cat.ProductCategoryID == updatedProductCategory.ProductCategoryID);
                    categoryInList.Name = updatedProductCategory.Name;
                    categoryInList.IsActive = updatedProductCategory.IsActive;
                }
                else
                {
                    try
                    {
                        CategoryData categoryItem = new CategoryData()
                        {
                            ProductCategoryID = 0,
                            Name = CategoryName,
                            IsActive = CategoryIsActive
                        };
                        ProductCategory updatedProductCategory = CategoriesClient.UpdateCategory(categoryItem);

                        if (updatedProductCategory != null)
                        {
                            CategoryList.Add(new CategoryData()
                            {
                                ProductCategoryID = updatedProductCategory.ProductCategoryID,
                                Name = updatedProductCategory.Name,
                                IsActive = updatedProductCategory.IsActive,
                                ProductCount = updatedProductCategory.ProductCount
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

        public bool CanEditCategory(object item)
        {
            return (CategoryName.Length > 0);
        }

        public void DeleteCategory(object item)
        {
            if (CanDeleteCategory(null))
            {
                bool result = false;
                result = CategoriesClient.DeleteCategory(SelectedCategory.ProductCategoryID);
                if (result)
                {
                    CategoryData categoryJustDeleted = CategoryList.First(c => c.ProductCategoryID == SelectedCategory.ProductCategoryID);
                    CategoryList.Remove(categoryJustDeleted);
                    MessageBox.Show("Delete successful");
                }
                else { MessageBox.Show("Delete failed"); }
            }
        }

        public bool CanDeleteCategory(object item)
        {
            return (SelectedCategory != null && SelectedCategory.ProductCount == 0);
        }

        #endregion

        #region Commands

        private DelegateCommand<object> createNewCategoryCommand;

        public ICommand CreateNewCategoryCommand
        {
            get
            {
                return createNewCategoryCommand ?? (createNewCategoryCommand = new DelegateCommand<object>(CreateNewCategory));
            }
        }

        private DelegateCommand<object> editCategoryCommand;

        public ICommand EditCategoryCommand
        {
            get
            {
                return editCategoryCommand ?? (editCategoryCommand = new DelegateCommand<object>(EditCategory, CanEditCategory));
            }
        }

        private DelegateCommand<object> deleteCategoryCommand;

        public ICommand DeleteCategoryCommand
        {
            get
            {
                return deleteCategoryCommand ?? (deleteCategoryCommand = new DelegateCommand<object>(DeleteCategory, CanDeleteCategory));
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
