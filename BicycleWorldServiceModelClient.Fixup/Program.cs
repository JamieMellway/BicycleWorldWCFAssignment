using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace BicycleWorldServiceModelClient.Fixup
{
    class Program
    {
        static void Main(string[] args)
        {
            string proxyLocation = @"..\..\..\BicycleWorldServiceModelClient\BicycleWorldProxy.cs";
            string output = File.ReadAllText(proxyLocation);
            Dictionary<string, string> replacements = new Dictionary<string, string>();
            replacements.Add("bool Checkout();", "[TransactionFlow(TransactionFlowOption.Mandatory)]\n        bool Checkout();");
            replacements.Add("int GetSalesOrderListCount();", "[TransactionFlow(TransactionFlowOption.Mandatory)]\n        int GetSalesOrderListCount();");
            replacements.Add("System.Collections.Generic.List<BicycleWorldServiceModelClient.BicycleWorldService.SalesOrder> GetSalesOrderList(int take, int skip);", "[TransactionFlow(TransactionFlowOption.Mandatory)]\n        System.Collections.Generic.List<BicycleWorldServiceModelClient.BicycleWorldService.SalesOrder> GetSalesOrderList(int take, int skip);");
            replacements.Add("System.Collections.Generic.List<BicycleWorldServiceModelClient.BicycleWorldService.SalesHistoryData> GetSalesHistory(int take, int skip);", "[TransactionFlow(TransactionFlowOption.Mandatory)]\n        System.Collections.Generic.List<BicycleWorldServiceModelClient.BicycleWorldService.SalesHistoryData> GetSalesHistory(int take, int skip);");
            replacements.Add("using System.Runtime.Serialization;", "using System.Runtime.Serialization;\n    using System.ServiceModel;");
                
            foreach (var replacement in replacements)
            {
                output = output.Replace(replacement.Key, replacement.Value);
            }

            File.WriteAllText(proxyLocation, output);
        }
    }
}
