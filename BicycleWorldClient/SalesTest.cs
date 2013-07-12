using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldServiceModelClient.BicycleWorldService;
using System.ServiceModel;
using System.Transactions;
using BicycleWorldServiceModelClient;
using BicycleWorldClientSession;

namespace ProductsClient
{
    public static class SalesTest
    {
        public static void RunTest()
        {
            PermissiveCertificatePolicy.Enact("CN=HTTPS-Server");
            Console.Write("Initializing Sales Proxy...");
            BicycleWorldSalesServiceClient proxy = new BicycleWorldSalesServiceClient("wsHttpBinding_BicycleWorldSalesService");
            Console.WriteLine(" done");

            proxy.ClientCredentials.UserName.UserName = "Fred";
            proxy.ClientCredentials.UserName.Password = "Pa$$w0rd";

            // Test the operations in the service

            try
            {
                Console.WriteLine(proxy.Test());

                // The encrypted list will timeout for slightly large lists (in the hundreds), so the list is 
                // being grabbed in smaller chunks.  Transaction logic is used to lock the list while the list is being
                // grabbed.
                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = IsolationLevel.RepeatableRead;
                transactionOptions.Timeout = new TimeSpan(0, 2, 0);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
                {
                    Console.WriteLine("Test 1: List all Sales Orders");
                    int take = 5;
                    int count = proxy.GetSalesOrderListCount(); 
                    for (int skip = 0; skip < count; skip = skip + take)
                    {
                        var salesOrders = proxy.GetSalesOrderList(take, skip);
                        foreach (var saleOrder in salesOrders)
                        {
                            Console.WriteLine(" Sales Order ID: {0:000}", saleOrder.SalesOrderID);
                            foreach (var item in saleOrder.SalesOrderItems)
                            {
                                Console.WriteLine("     Item Price: {0}, Item Quantity: {1}", item.ActualCost, item.Quantity);
                                Console.WriteLine("     Name: {0}, Product Number: {1}", item.Product.Name, item.Product.ProductNumber);
                            }
                        }
                    }

                    //No scope.Complete here as no write tasks are being scoped.
                    
                    scope.Dispose();
                }

                Console.WriteLine();

                // Disconnect from the service
                proxy.Close();
            }
            catch (FaultException<SystemFault> sf)
            {
                Console.WriteLine("SystemFault {0}: {1}\n{2}",
                    sf.Detail.SystemOperation, sf.Detail.SystemMessage,
                    sf.Detail.SystemReason);
            }
            catch (FaultException<DatabaseFault> dbf)
            {
                Console.WriteLine("DatabaseFault {0}: {1}\n{2}",
                    dbf.Detail.DbOperation, dbf.Detail.DbMessage,
                    dbf.Detail.DbReason);
            }
            catch (FaultException e)
            {
                Console.WriteLine("{0}: {1}", e.Code.Name, e.Reason);
            }
            catch (Exception e)
            {
                Console.WriteLine("General exception: {0}", e.Message);
                Console.WriteLine("Inner Exception: {0}", e.InnerException);
            }

            //Console.WriteLine("Press ENTER to continue");
            //Console.ReadLine();

        }
    }
}
