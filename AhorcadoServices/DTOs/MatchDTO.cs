using System;
using System.Runtime.Serialization;

namespace Services.DTOs
{
    [DataContract]
    public class MatchDTO
    {
        [DataMember]
        public int MatchID { get; set; }

        [DataMember]
        public int Player1ID { get; set; }

        [DataMember]
        public int? Player2ID { get; set; }

        [DataMember]
        public int WordID { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public int StatusID { get; set; }

        [DataMember]
        public WordDTO Word { get; set; }

        [DataMember]
        public PlayerDTO Player1 { get; set; }

        [DataMember]
        public PlayerDTO Player2 { get; set; }

    }
}