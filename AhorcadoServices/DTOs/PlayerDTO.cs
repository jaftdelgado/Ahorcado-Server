using System;
using System.Runtime.Serialization;

namespace AhorcadoServices.DTOs
{
    [DataContract]
    public class PlayerDTO
    {
        [DataMember]
        public int PlayerID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime BirthDay { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public byte[] ProfilePic { get; set; }

        [DataMember]
        public int TotalScore { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int? SelectedLanguageID { get; set; }
    }
}