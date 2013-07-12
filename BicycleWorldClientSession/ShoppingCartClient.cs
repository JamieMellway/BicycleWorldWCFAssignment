using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldServiceModelClient.BicycleWorldService;
using BicycleWorldServiceModelClient;
using BicycleWorldClientSession;
using System.ServiceModel;
using System.Transactions;

namespace BicycleWorldClientSession
{
    public static class ShoppingCartClient
    {
        public static bool AddItemToCart(string productNumber)
        {
            bool result = false;
            try
            {
                result = UserSession.Current.Client.AddItemToCart(productNumber);
            }
            catch (Exception) { throw; }
            return result;
        }

        public static bool RemoveItemFromCart(string productNumber)
        {
            bool result = false;
            try
            {
                result = UserSession.Current.Client.RemoveItemFromCart(productNumber);
            }
            catch (Exception) { throw; }
            return result;
        }

        public static List<ShoppingCartItem> GetShoppingCartList()
        {
            List<ShoppingCartItem> result = new List<ShoppingCartItem>();
            try
            {
                result = UserSession.Current.Client.GetShoppingCartList();
            }
            catch (Exception) { throw; }
            return result;
        }

        public static bool Checkout()
        {
            bool result = false;
            try
            {
                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = IsolationLevel.RepeatableRead;
                transactionOptions.Timeout = new TimeSpan(0, 1, 0);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions))
                {
                    result = UserSession.Current.Client.Checkout();
                    if (result)
                    {
                        scope.Complete();
                        UserSession.Current.CloseSession();
                    }
                }
            }
            catch (ProtocolException ex)
            {
                throw ex;
            }
            catch (Exception) { throw; }
            return result;
        }
    }
}
