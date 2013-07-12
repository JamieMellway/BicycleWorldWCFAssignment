using System;
using System.Runtime.Serialization;

namespace BicycleWorldObjectModel
{
    [DataContract]
    public class SalesHistoryData
    {
        [DataMember]
        public int SalesOrderNumber { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public string ProductNumber { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        
        [DataMember]
        public decimal PerItemCost { get; set; }
        [DataMember]
        public decimal ItemSubTotal { get; set; }
        [DataMember]
        public decimal OrderTotal { get; set; }

        [DataMember]
        public int SortOrder { get; set; }
    }
}
