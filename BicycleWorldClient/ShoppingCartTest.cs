using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldServiceModelClient.BicycleWorldService;
using System.ServiceModel;
using System.Transactions;
using System.ServiceModel.Channels;
using BicycleWorldServiceModelClient;
using BicycleWorldClientSession;

namespace ProductsClient
{
    public static class ShoppingCartTest
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

                Console.WriteLine("Press ENTER to start shopping cart");
                Console.ReadLine();

                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = IsolationLevel.RepeatableRead;
                transactionOptions.Timeout = new TimeSpan(0, 1, 0);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
                {
                    IDictionary<string, string> context = null;
                    IContextManager contextManager = proxy.InnerChannel.GetProperty<IContextManager>();
                    if (context != null) { contextManager.SetContext(context); }

                    proxy.AddItemToCart("BK-M18B-42");

                    if (context == null)
                    {
                        context = contextManager.GetContext();
                    }

                    proxy.AddItemToCart("BK-M18B-42");
                    proxy.AddItemToCart("BB-7421");
                    proxy.AddItemToCart("BK-R19B-44");
                    proxy.RemoveItemFromCart("BK-R19B-44");

                    Console.WriteLine(proxy.GetShoppingCart());

                    if (proxy.Checkout())
                    {
                        scope.Complete();
                        Console.WriteLine("Goods purchased.");
                    }
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

            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();

        }
    }
}
