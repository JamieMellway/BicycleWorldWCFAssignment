using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using BicycleWorldEntityModel;
using BicycleWorldObjectModel;

namespace BicycleWorldCore
{
    public class SalesOrderCore
    {
        public static int GetSalesOrderListCount(string userName)
        {
            int salesOrderListCount = 0;

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    var customer = database.Customers.FirstOrDefault(c => c.Username == userName);

                    if (customer != null)
                    {
                        var salesorders = from salesorder in database.SalesOrders
                                              //.Include(so => so.SalesOrderItems.Select(soi => soi.Product.ProductCategory))
                                           .Where(so => so.CustomerID == customer.CustomerID)
                                          select salesorder;

                        salesOrderListCount = salesorders.Count();
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
                        SystemOperation = "Iterate through product history",
                        SystemReason = "Exception reading product history",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return salesOrderListCount;
        }

        public static List<SalesHistoryData> GetSalesHistory(string userName, int take, int skip)
        {
            List<SalesHistoryData> salesHistoryList = new List<SalesHistoryData>();
            List<SalesOrder> salesOrderList = new List<SalesOrder>();

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    var customer = database.Customers.FirstOrDefault(c => c.Username == userName);

                    if (customer != null)
                    {
                        var salesorders = from salesorder in database.SalesOrders
                                           .Include(so => so.SalesOrderItems.Select(soi => soi.Product.ProductCategory))
                                           .Where(so => so.CustomerID == customer.CustomerID)
                                          orderby salesorder.SalesOrderID ascending 
                                          select salesorder;

                        salesOrderList = salesorders.Skip(skip).Take(take).ToList();

                        foreach (var salesOrder in salesOrderList)
                        {
                            foreach (var salesOrderItem in salesOrder.SalesOrderItems.OrderBy(item=>item.OrderBy))
                            {
                                salesHistoryList.Add(new SalesHistoryData()
                                {
                                    SalesOrderNumber = salesOrder.SalesOrderID,
                                    OrderDate = salesOrder.OrderDate,
                                    ProductNumber = salesOrderItem.Product.ProductNumber,
                                    ProductName = salesOrderItem.Product.Name,
                                    Quantity = salesOrderItem.Quantity,
                                    PerItemCost = salesOrderItem.Product.ListPrice,
                                    ItemSubTotal = salesOrderItem.Quantity * salesOrderItem.Product.ListPrice,
                                    OrderTotal = salesOrder.SalesOrderItems.Sum(item => item.Quantity * item.Product.ListPrice),
                                    SortOrder = salesOrderItem.OrderBy
                                });
                            }
                        }

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
                        SystemOperation = "Iterate through product history",
                        SystemReason = "Exception reading product history",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return salesHistoryList;
        }

        public static List<SalesOrder> GetSalesOrderList(string userName, int take, int skip)
        {
            List<SalesOrder> salesOrderList = new List<SalesOrder>();

            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;

                    var customer = database.Customers.FirstOrDefault(c => c.Username == userName);

                    if (customer != null)
                    {
                        var salesorders = from salesorder in database.SalesOrders
                                           .Include(so => so.SalesOrderItems.Select(soi => soi.Product.ProductCategory))
                                           .Where(so => so.CustomerID == customer.CustomerID)
                                          orderby salesorder.SalesOrderID ascending
                                          select salesorder;

                        salesOrderList = salesorders.Skip(skip).Take(take).ToList();
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
                        SystemOperation = "Iterate through product history",
                        SystemReason = "Exception reading product history",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }

            return salesOrderList;
        }

        public static void SaveSalesOrder(List<ShoppingCartItem> shoppingCart, string username)
        {
            try
            {
                using (BicycleWorldDataModelContainer database = new BicycleWorldDataModelContainer())
                {
                    database.Configuration.ProxyCreationEnabled = false;
                    Customer customer = database.Customers.First(c => c.Username == username);

                    SalesOrder salesOrder = new SalesOrder()
                    {
                        CustomerID = customer.CustomerID,
                        Customer = customer,
                        SalesOrderID = 0,
                        SalesOrderItems = new List<SalesOrderItem>(),
                        OrderDate = DateTime.Now
                    };

                    foreach (ShoppingCartItem item in shoppingCart)
                    {
                        Product matchingProduct = database.Products.FirstOrDefault(p => p.ProductNumber == item.ProductNumber);

                        if (matchingProduct == null) throw new FaultException<SystemFault>(new SystemFault
                                                          {
                                                              SystemOperation = "Checking out",
                                                              SystemReason = "Shopping cart",
                                                              SystemMessage = String.Format("Product {0} does not exist.", item.ProductNumber)
                                                          });

                        SalesOrderItem salesOrderItem = new SalesOrderItem()
                        {
                            SalesOrderID = 0,
                            ProductID = matchingProduct.ProductID,
                            ActualCost = matchingProduct.ListPrice,
                            OrderBy = item.OrderBy,
                            ModifiedDate = DateTime.Now,
                            Quantity = item.Quantity,
                        };
                        salesOrder.SalesOrderItems.Add(salesOrderItem);
                    }
                    database.SalesOrders.Add(salesOrder);

                    database.SaveChanges();
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
                        SystemOperation = "Checking out",
                        SystemReason = "Exception checking out shopping cart",
                        SystemMessage = e.Message
                    };

                    throw new FaultException<SystemFault>(sf);
                }
            }
        }
    }
}
