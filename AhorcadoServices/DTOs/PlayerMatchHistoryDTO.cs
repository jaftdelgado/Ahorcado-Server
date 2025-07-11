﻿using System;
using System.Runtime.Serialization;

namespace AhorcadoServices.DTOs
{
    [DataContract]
    public class PlayerMatchHistoryDTO
    {
        [DataMember]
        public int MatchID { get; set; }

        [DataMember]
        public string OpponentName { get; set; }

        [DataMember]
        public string PlayedWord { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public int ResultID { get; set; }

        [DataMember]
        public int Points { get; set; }
    }
}
