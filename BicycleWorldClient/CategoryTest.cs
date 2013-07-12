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
    public static class CategoryTest
    {
        public static void RunTest()
        {
            PermissiveCertificatePolicy.Enact("CN=HTTPS-Server");
            Console.Write("Initializing Proxy for Category Tests...");
            BicycleWorldServiceClient proxy = new BicycleWorldServiceClient("DefaultBinding_BicycleWorldService_BicycleWorldService");
            Console.WriteLine(" done");

            proxy.ClientCredentials.UserName.UserName = "Fred";
            proxy.ClientCredentials.UserName.Password = "Pa$$w0rd";


            try
            {
                Console.WriteLine(proxy.Test());
                
                Console.WriteLine("Test 1: List all categories");
                var categories = proxy.CategoriesList();
                foreach (var category in categories)
                {
                    Console.WriteLine(" Category ID: {0:000}, Name: {1}", category.ProductCategoryID, category.Name);
                }

                Console.WriteLine("Test 2: Removing all 'TEST' categories");
                foreach (var category in categories.Where(c => c.Name == "TEST"))
                {
                    Console.Write(" Removing ProductCategoryID {0:000}... ", category.ProductCategoryID);
                    if (proxy.RemoveCategory(category.ProductCategoryID)) Console.WriteLine("Removed.");
                    else Console.WriteLine("Not removed.");
                }

                Console.WriteLine("Test 3: Add 'TEST' category");
                int productCategoryID = proxy.AddCategory(new ProductCategory()
                {
                    Name = "TEST",
                });
                Console.WriteLine(" New category added.  ProductCategoryID: {0}", productCategoryID);

                Console.WriteLine(" Test 4: Update 'TEST' category");
                ProductCategory categoryToUpdate = proxy.GetCategory(productCategoryID);
                Console.WriteLine(" Name was: {0}", categoryToUpdate.Name);
                categoryToUpdate.Name = "test";
                categoryToUpdate = proxy.UpdateCategory(categoryToUpdate);
                categoryToUpdate = proxy.GetCategory(productCategoryID);
                Console.WriteLine(" Name is now: {0}", categoryToUpdate.Name);

                Console.WriteLine("Test 4: Remove 'TEST' category");
                bool removalSuccessful = proxy.RemoveCategory(productCategoryID);
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
