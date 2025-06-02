using System.Runtime.Serialization;

namespace Services.DTOs
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