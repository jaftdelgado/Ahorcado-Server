using System.Runtime.Serialization;

namespace Services.DTOs
{
    [DataContract]
    public class MatchResultDTO
    {
        [DataMember]
        public int ResultID { get; set; }

        [DataMember]
        public string ResultName { get; set; }
    }
}