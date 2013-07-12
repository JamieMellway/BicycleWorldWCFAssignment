using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldServiceModelClient.BicycleWorldService;
using System.ServiceModel;
using System.ServiceModel.Security;
using BicycleWorldServiceModelClient;
using BicycleWorldClientSession;

namespace ProductsClient
{
    public static class WaitForService
    {
        public static void Wait()
        {
            PermissiveCertificatePolicy.Enact("CN=HTTPS-Server");

            bool productServiceIsRunning = false;

            Console.Write("Waiting for Product Service... ");
            while (!productServiceIsRunning)
            {
                System.Threading.Thread.Sleep(500);

                try
                {
                    BicycleWorldServiceClient proxy = new BicycleWorldServiceClient("DefaultBinding_BicycleWorldService_BicycleWorldService");
                    proxy.ClientCredentials.UserName.UserName = "Fred";
                    proxy.ClientCredentials.UserName.Password = "Pa$$w0rd";
                    proxy.Login();
                    productServiceIsRunning = true;
                    proxy.Abort();

                    Console.WriteLine("Connected.");
                }
                catch (ServerTooBusyException)
                {
                    Console.Write(".");
                }
                catch (SecurityAccessDeniedException ex)
                {
                    Console.WriteLine("\n {0}", ex.Message);
                    break;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            bool salesServiceIsRunning = false;

            Console.Write("Waiting for Sales Service... ");
            while (!salesServiceIsRunning)
            {
                System.Threading.Thread.Sleep(500);

                try
                {
                    BicycleWorldSalesServiceClient proxy = new BicycleWorldSalesServiceClient("wsHttpBinding_BicycleWorldSalesService");
                    proxy.ClientCredentials.UserName.UserName = "Fred";
                    proxy.ClientCredentials.UserName.Password = "Pa$$w0rd";
                    proxy.Login();
                    salesServiceIsRunning = true;
                    proxy.Abort();

                    Console.WriteLine("Connected.");
                }
                catch (ServerTooBusyException)
                {
                    Console.Write(".");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }
    }
}