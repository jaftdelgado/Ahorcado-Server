using System.Runtime.Serialization;

namespace Services.DTOs
{
    [DataContract]
    public class CategoryDTO
    {
        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public string CategoryName { get; set; }
    }
}