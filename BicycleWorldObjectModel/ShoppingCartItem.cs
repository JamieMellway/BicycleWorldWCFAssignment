using System;
using System.Runtime.Serialization;

namespace BicycleWorldObjectModel
{
    // Shopping cart item
        [Serializable]
        [DataContract]
        public class ShoppingCartItem
        {
            [DataMember]
            public int ProductID { get; set; }
            [DataMember]
            public string ProductNumber { get; set; }
            [DataMember]
            public string ProductName { get; set; }
            [DataMember]
            public decimal Cost { get; set; }
            [DataMember]
            public int Quantity { get; set; }
            [DataMember]
            public int OrderBy { get; set; }
        }
    }
