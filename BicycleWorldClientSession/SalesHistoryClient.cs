using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BicycleWorldServiceModelClient.BicycleWorldService;
using BicycleWorldServiceModelClient;
using System.ServiceModel;
using System.Transactions;

namespace BicycleWorldClientSession
{
    public static class SalesHistoryClient
    {
        public static List<SalesHistoryData> GetSalesHistory()
        {
            List<SalesHistoryData> result = new List<SalesHistoryData>();
            try
            {
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
                    int count = UserSession.Current.Client.GetSalesOrderListCount(); 
                    for (int skip = 0; skip < count; skip = skip + take)
                    {
                        List<SalesHistoryData> salesOrders = UserSession.Current.Client.GetSalesHistory(take, skip).ToList();
                        result = result.Union(salesOrders).ToList();
                    }
                    result = result.OrderByDescending(so => so.SalesOrderNumber).ThenBy(so => so.SortOrder).ToList();
                    //No scope.Complete here as no write tasks are being scoped.

                    scope.Dispose();
                }
            }
            catch (Exception) { throw; }
            return result;
        }
    }
}
