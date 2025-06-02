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
        public int Player1 { get; set; }

        [DataMember]
        public int? Player2 { get; set; }

        [DataMember]
        public int WordID { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public int StatusID { get; set; }
    }
}