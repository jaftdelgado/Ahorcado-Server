using System.Runtime.Serialization;

namespace Services.DTOs
{
    [DataContract]
    public class WordDTO
    {
        [DataMember]
        public int WordID { get; set; }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        public int LanguageID { get; set; }

        [DataMember]
        public string Word { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Difficult { get; set; }
    }

}