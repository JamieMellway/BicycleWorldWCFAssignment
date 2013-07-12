using System.Runtime.Serialization;

namespace BicycleWorldObjectModel
{
    [DataContract]
    public class CustomerData
    {
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }
    }
}
