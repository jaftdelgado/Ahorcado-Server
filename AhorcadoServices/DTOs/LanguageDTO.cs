using System.Runtime.Serialization;

namespace Services.DTOs
{
    [DataContract]
    public class LanguageDTO
    {
        [DataMember]
        public int LanguageID { get; set; }

        [DataMember]
        public string LanguageName { get; set; }
    }
}