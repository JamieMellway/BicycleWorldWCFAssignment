using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using BicycleWorldServiceModelClient.BicycleWorldService;

namespace BicycleWorldClientSession
{
    public static class CategoriesClient
    {
        public static List<CategoryData> GetCategoryList()
        {
            List<CategoryData> result = null;
            BicycleWorldServiceClient client = new BicycleWorldServiceClient();
            try
            {
                client.ClientCredentials.UserName.UserName = LoginUser.Current.Username;
                client.ClientCredentials.UserName.Password = LoginUser.Current.Password;

                List<ProductCategory> productsList = client.CategoriesList();
                var q = from product in productsList
                         select new CategoryData()
                         {
                             ProductCategoryID = product.ProductCategoryID,
                             Name = product.Name,
                             IsActive = product.IsActive,
                             ProductCount = product.ProductCount
                         };
                result = q.ToList();
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


        public static ProductCategory UpdateCategory(CategoryData category)
        {
            ProductCategory result = null;
            BicycleWorldServiceClient client = new BicycleWorldServiceClient();
            try
            {
                client.ClientCredentials.UserName.UserName = LoginUser.Current.Username;
                client.ClientCredentials.UserName.Password = LoginUser.Current.Password;
                result = client.UpdateCategory(new ProductCategory()
                {
                    Name = category.Name,
                    IsActive = category.IsActive,
                    ProductCategoryID = category.ProductCategoryID
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

        public static bool DeleteCategory(int categoryID)
        {
            bool result = false;
            BicycleWorldServiceClient client = new BicycleWorldServiceClient();
            try
            {
                client.ClientCredentials.UserName.UserName = LoginUser.Current.Username;
                client.ClientCredentials.UserName.Password = LoginUser.Current.Password;
                result = client.RemoveCategory(categoryID);
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
