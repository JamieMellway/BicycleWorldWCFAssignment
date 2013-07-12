using System;
using System.Collections.Generic;
using System.ServiceModel;
using BicycleWorldServiceModelClient.BicycleWorldService;

namespace BicycleWorldClientSession
{
    public static class ProductsClient
    {
        public static List<ProductData> GetProductList()
        {
            List<ProductData> result = null;
            BicycleWorldServiceClient client = new BicycleWorldServiceClient();
            try
            {
                client.ClientCredentials.UserName.UserName = LoginUser.Current.Username;
                client.ClientCredentials.UserName.Password = LoginUser.Current.Password;
                result = client.ProductList();
                client.Close();
            }
            catch (FaultException)
            {
                client.Abort();
            }
            catch (CommunicationException)
            {
                client.Abort();
            }
            catch (TimeoutException)
            {
                client.Abort();
            }
            catch { throw; }
            return result;
        }


        public static Product UpdateProduct(ProductData product)
        {
            Product result = null;
            BicycleWorldServiceClient client = new BicycleWorldServiceClient();
            try
            {
                client.ClientCredentials.UserName.UserName = LoginUser.Current.Username;
                client.ClientCredentials.UserName.Password = LoginUser.Current.Password;
                result = client.UpdateProduct(new Product()
                {
                    ProductID = product.ProductID,
                    Name = product.Name,
                    ProductNumber = product.ProductNumber,
                    Color = product.Color,
                    ListPrice = product.ListPrice,
                    ProductDescription = product.ProductDescription,
                    Quantity = product.Quantity,
                    ProductCategoryID = product.CategoryID,
                    IsActive = product.IsActive
                });
                client.Close();
            }
            catch (FaultException)
            {
                client.Abort();
            }
            catch (CommunicationException)
            {
                client.Abort();
            }
            catch (TimeoutException)
            {
                client.Abort();
            }
            catch { throw; }
            return result;
        }

        public static bool DeleteProduct(int productID)
        {
            bool result = false;
            BicycleWorldServiceClient client = new BicycleWorldServiceClient();
            try
            {
                client.ClientCredentials.UserName.UserName = LoginUser.Current.Username;
                client.ClientCredentials.UserName.Password = LoginUser.Current.Password;
                result = client.RemoveProduct(productID);
                client.Close();
            }
            catch (FaultException)
            {
                client.Abort();
            }
            catch (CommunicationException)
            {
                client.Abort();
            }
            catch (TimeoutException)
            {
                client.Abort();
            }
            catch { throw; }
            return result;
        }
    }
}
