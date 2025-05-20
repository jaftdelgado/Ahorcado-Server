using System.Runtime.Serialization;

namespace AhorcadoServices.DTOs
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