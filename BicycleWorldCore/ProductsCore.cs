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
    public static class ProductsCore
    {
        //public static int ProductListCount()
        //{
        //    int productsListCount = 0;

        //    try
        //    {
        //        using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
        //        {
        //            var products = from product in database.Products
        //                                .Include(p => p.ProductCategory)
        //                           orderby product.ProductID
        //                           select product;

        //            productsListCount = products.Count();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        if (e.InnerException is System.Data.SqlClient.SqlException)
        //        {
        //            DatabaseFault dbf = new DatabaseFault
        //            {
        //                DbOperation = "Connect to database",
        //                DbReason = "Exception accessing database",
        //                DbMessage = e.InnerException.Message
        //            };

        //            throw new FaultException<DatabaseFault>(dbf);
        //        }
        //        else
        //        {
        //            SystemFault sf = new SystemFault
        //            {
        //                SystemOperation = "Counting products",
        //                SystemReason = "Exception count products",
        //                SystemMessage = e.Message
        //            };

        //            throw new FaultException<SystemFault>(sf);
        //        }
        //    }

        //    return productsListCount;
        //}

        public static Product GetProduct(int productID) 
        {
            Product productToGet = null;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    var products = from product in database.Products
                                        .Include(p => p.ProductCategory)
                                        where product.ProductID == productID
                                   select product;

                    productToGet = products.FirstOrDefault();
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
                        SystemOperation = "Getting product",
                        SystemReason = "Exception getting product",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return productToGet;
        }

        public static List<ProductData> ProductList() // (int take, int skip)
        {
            List<ProductData> productsList = new List<ProductData>();

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    var products = from product in database.Products
                                        .Include(p => p.ProductCategory)
                                        .Include(so=>so.SalesOrderItems)
                                   orderby product.ProductID
                                   select new ProductData()
                                   {
                                       ProductNumber = product.ProductNumber,
                                       ProductID = product.ProductID,
                                       CategoryID= product.ProductCategory.ProductCategoryID,
                                       CategoryName = product.ProductCategory.Name,
                                       Color = product.Color,
                                       ListPrice = product.ListPrice,
                                       Name = product.Name,
                                       ProductDescription = product.ProductDescription,
                                       Quantity = product.Quantity,
                                       SalesOrderCount = product.SalesOrderItems.Count,
                                       IsActive= product.IsActive,
                                       IsCategoryActive = product.ProductCategory.IsActive
                                   };
                                   //select product;

                    //productsList = products.Skip(skip).Take(take).ToList();
                    productsList = products.ToList();
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
                        SystemOperation = "Iterate through products",
                        SystemReason = "Exception reading products",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return productsList;
        }

        public static int AddProduct(Product product)
        {
            int result = 0;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    product.ModifiedDate = DateTime.Now;
                    product.rowguid = new Guid();
                    database.Products.Add(product);
                    //database.Products.AddObject(product);
                    database.SaveChanges();

                    Product newProduct = database.Products.FirstOrDefault(c => c.ProductNumber == product.ProductNumber);
                    result = (newProduct == null) ? 0 : newProduct.ProductID;
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
                        SystemOperation = "Adding product",
                        SystemReason = "Exception adding product",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return result;
        }

        public static bool RemoveProduct(int productID)
        {
            bool result = false;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    Product productToRemove = database.Products.FirstOrDefault(c => c.ProductID == productID);

                    if (productToRemove != null)
                    {
                        database.Products.Remove(productToRemove);
                        //productToRemove.MarkAsDeleted();

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
                        SystemOperation = "Removing product",
                        SystemReason = "Removing product",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return result;
        }

        public static Product UpdateProduct(Product product)
        {
            Product result = null;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    Product productToUpdate = database.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                    productToUpdate.ProductNumber = product.ProductNumber;
                    productToUpdate.Name = product.Name;
                    productToUpdate.Color = product.Color;
                    productToUpdate.ListPrice = product.ListPrice;
                    productToUpdate.ProductDescription = product.ProductDescription;
                    productToUpdate.ProductCategoryID = product.ProductCategoryID;
                    productToUpdate.Quantity = product.Quantity;
                    productToUpdate.IsActive = product.IsActive;

                    database.SaveChanges();
                    result = productToUpdate;
                    //result = database.Products
                    //    .Include(c=>c.ProductCategory)
                    //    .Include(so=>so.SalesOrderItems)
                    //    .FirstOrDefault(c => c.ProductID == productToUpdate.ProductID);
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
                        SystemOperation = "Iterate through catagories",
                        SystemReason = "Exception reading catagories",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return result;
        }
    }
}