using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;
using System.ServiceModel;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;

namespace BicycleWorldCore
{
    public static class CategoryCore
    {
        public static List<ProductCategory> CategoriesList()
        {
            List<ProductCategory> categoriesList = new List<ProductCategory>();

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    var categories = from category in database.ProductCategories
                                     select category;

                    foreach (var cat in categories)
                    {
                        var q = from category in database.ProductCategories
                                         .Include(c => c.Products)
                                where category.ProductCategoryID == cat.ProductCategoryID
                                select category.Products.Count;
                        cat.ProductCount = q.FirstOrDefault();
                    }

                    categoriesList = categories.ToList();
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Iterate through categories",
                        SystemReason = "Exception reading categories",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return categoriesList;
        }

        public static ProductCategory GetCategory(int categoryID)
        {
            ProductCategory categoryToGet = new ProductCategory();

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    var categories = from category in database.ProductCategories
                                     where category.ProductCategoryID == categoryID
                                     select category;
                    categoryToGet = categories.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Iterate through categories",
                        SystemReason = "Exception reading categories",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return categoryToGet;
        }

        public static int AddCategory(ProductCategory category)
        {
            int result = 0;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;
                    category.ModifiedDate = DateTime.Now;
                    category.rowguid = new Guid();

                    database.ProductCategories.Add(category);

                    database.SaveChanges();

                    ProductCategory newCategory = database.ProductCategories.FirstOrDefault(c => c.ProductCategoryID == category.ProductCategoryID);
                    result = (newCategory == null) ? 0 : newCategory.ProductCategoryID;
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Adding category",
                        SystemReason = "Exception adding category",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return result;
        }

        public static bool RemoveCategory(int categoryID)
        {
            bool result = false;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    ProductCategory categoryToRemove = database.ProductCategories.FirstOrDefault(c => c.ProductCategoryID == categoryID);

                    if (categoryToRemove != null)
                    {
                        database.ProductCategories.Remove(categoryToRemove);
                        database.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Removing category",
                        SystemReason = "Removing category",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return result;
        }

        public static ProductCategory UpdateCategory(ProductCategory category)
        {
            ProductCategory result = null;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    ProductCategory categoryToUpdate = database.ProductCategories.FirstOrDefault(c => c.ProductCategoryID == category.ProductCategoryID);
                    if (categoryToUpdate == null)
                    {
                        categoryToUpdate = new ProductCategory()
                        {
                            ProductCategoryID = 0,
                            Name = category.Name,
                            IsActive = category.IsActive,
                            ModifiedDate = DateTime.Now,
                            rowguid = Guid.NewGuid()
                        };
                        database.ProductCategories.Add(categoryToUpdate);
                    }
                    else
                    {
                        categoryToUpdate.Name = category.Name;
                        categoryToUpdate.IsActive = category.IsActive;
                        categoryToUpdate.ModifiedDate = DateTime.Now;
                        categoryToUpdate.rowguid = Guid.NewGuid();
                    }

                    database.SaveChanges();
                    result = database.ProductCategories.FirstOrDefault(c => c.rowguid == categoryToUpdate.rowguid);
                }
            }
            catch (Exception e)
            {
                if (e.InnerException is System.Data.SqlClient.SqlException)
                {
                    DatabaseFault dbf = new DatabaseFault
                    {
                        DbOperation = "Connect to database",
                        DbReason = "Exception accessing database",
                        DbMessage = e.InnerException.Message
                    };

                    throw new FaultException<DatabaseFault>(dbf);
                }
                else
                {
                    SystemFault sf = new SystemFault
                    {
                        SystemOperation = "Updating category",
                        SystemReason = "Updating category",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }
            return result;
        }
    }
}
