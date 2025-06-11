using System;

namespace Services.DTOs
{
    public class AvailableMatchDTO
    {
        public int MatchID { get; set; }
        public string CreatorName { get; set; }
        public string WordCategory { get; set; }
        public int Difficulty { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
