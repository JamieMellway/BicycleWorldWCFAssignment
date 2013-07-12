using System.Runtime.Serialization;

namespace BicycleWorldObjectModel
{
    [DataContract]
    public class ProductData
    {
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ProductNumber { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public decimal ListPrice { get; set; }
        [DataMember]
        public string ProductDescription { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public int CategoryID { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public int SalesOrderCount { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public bool IsCategoryActive { get; set; }
    }
}
