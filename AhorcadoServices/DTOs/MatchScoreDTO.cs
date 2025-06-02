using System.Runtime.Serialization;

namespace Services.DTOs
{
    [DataContract]
    public class MatchScoreDTO
    {
        [DataMember]
        public int PlayerID { get; set; }

        [DataMember]
        public int MatchID { get; set; }

        [DataMember]
        public int ResultID { get; set; }

        [DataMember]
        public int Points { get; set; }
    }
}