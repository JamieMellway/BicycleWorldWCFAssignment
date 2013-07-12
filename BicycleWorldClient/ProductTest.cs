using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldServiceModelClient.BicycleWorldService;
using System.ServiceModel;
using BicycleWorldServiceModelClient;
using BicycleWorldClientSession;

namespace ProductsClient
{
    public static class ProductTest
    {
        public static void RunTest()
        {
             // Create a proxy object and connect to the service
            PermissiveCertificatePolicy.Enact("CN=HTTPS-Server");
            Console.Write("Initializing Product Proxy...");
            BicycleWorldServiceClient proxy = new BicycleWorldServiceClient("DefaultBinding_BicycleWorldService_BicycleWorldService");
            Console.WriteLine(" done");

            proxy.ClientCredentials.UserName.UserName = "Fred";
            proxy.ClientCredentials.UserName.Password = "Pa$$w0rd";


            // Test the operations in the service

            try
            {
                Console.WriteLine(proxy.Test());
                
                // Obtain a list of all products
                Console.WriteLine("Test 1: List all products");
                var products = proxy.ProductList();
                foreach (var product in products)
                {
                    Console.WriteLine(" Product ID: {0:000}, ProductNumber: {1}", product.ProductID, product.ProductNumber);
                }

                Console.WriteLine("Test 2: Removing all 'TEST' products");
                foreach (var product in products.Where(p=>p.ProductNumber=="TEST"))
                {
                    Console.Write(" Removing productID {0:000}... ", product.ProductID);
                    if (proxy.RemoveProduct(product.ProductID)) Console.WriteLine("Removed.");
                    else Console.WriteLine("Not removed.");
                }

                Console.WriteLine("Test 3: Add 'TEST' product");
                int productID = proxy.AddProduct(new Product()
                {
                    Name = "Test",
                    Color = "RED",
                    ListPrice = (decimal)3.99,
                    ProductCategoryID = 1,
                    ProductDescription = "Test Mountain bike",
                    ProductNumber = "TEST",
                    Quantity = 5,
                });
                Console.WriteLine(" New producted added.  ProductID: {0}", productID);

                Console.WriteLine(" Test 4: Update 'TEST' product");
                Product productToUpdate = proxy.GetProduct(productID);
                Console.WriteLine(" Price was: {0}", productToUpdate.ListPrice);
                productToUpdate.ListPrice = (decimal)7.99;
                productToUpdate = proxy.UpdateProduct(productToUpdate);
                productToUpdate = proxy.GetProduct(productID);
                Console.WriteLine(" Price is now: {0}", productToUpdate.ListPrice);

                Console.WriteLine("Test 4: Remove 'TEST' product");
                bool removalSuccessful = proxy.RemoveProduct(productID);
                Console.WriteLine(" Removal Successful: {0}", removalSuccessful);


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
