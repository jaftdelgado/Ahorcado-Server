using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameServices
{
    public class MatchInfoWithCallbacks
    {
        public MatchInfoDTO MatchInfo { get; set; } = new MatchInfoDTO();
        public IGameManagerCallback Callback1 { get; set; }
        public IGameManagerCallback Callback2 { get; set; }
    }

    [DataContract]
    public class PlayerInfoDTO
    {
        [DataMember]
        public int PlayerId { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public byte[] ProfilePic { get; set; }
    }

    [DataContract]
    public class MatchInfoDTO
    {
        [DataMember]
        public int MatchID { get; set; }

        [DataMember]
        public PlayerInfoDTO Player1 { get; set; }

        [DataMember]
        public PlayerInfoDTO Player2 { get; set; }

        [DataMember]
        public WordInfoDTO Word { get; set; }

        [DataMember]
        public int RemainingAttempts { get; set; } = 6;

        [DataMember]
        public List<string> GuessedLetters { get; set; } = new List<string>();

        [DataMember]
        public bool IsGameOver { get; set; }
    }

    [DataContract]
    public class WordInfoDTO
    {
        [DataMember]
        public int WordID { get; set; }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public int LanguageID { get; set; }

        [DataMember]
        public string WordText { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Difficult { get; set; }
    }
}
