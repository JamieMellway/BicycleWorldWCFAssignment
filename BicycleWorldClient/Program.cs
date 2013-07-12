using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using BicycleWorldServiceModelClient.BicycleWorldService;

namespace ProductsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Press ENTER when the service has started");
            //Console.ReadLine();
            WaitForService.Wait();

            CategoryTest.RunTest();
            //ProductTest.RunTest();
            

            //ShoppingCartTest.RunTest();
            ShoppingCartTest2.RunTestAddItems();
            //ShoppingCartTest2.RunTestRemoveAndCheckoutItems();
            //ShoppingCartTest2.RunTestAddItems();
            //ShoppingCartTest2.RunTestAddItems();

            //SalesTest.RunTest();

            Console.WriteLine("Press ENTER to finish");
            Console.ReadLine();
        }
      }
}

