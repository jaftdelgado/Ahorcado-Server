using System.Runtime.Serialization;

namespace AhorcadoServices.DTOs
{
    [DataContract]
    public class MatchStatusDTO
    {
        [DataMember]
        public int StatusID { get; set; }

        [DataMember]
        public string StatusName { get; set; }
    }
}