using Services.DTOs;
using Model;
using System.Linq;

namespace Services
    .PlayerServices
{
    public class PlayerDAO
    {
        public PlayerDTO LogIn(string username, string password)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var player = context.Players
                    .FirstOrDefault(p => p.Username == username && p.Password == password);
                if (player != null)
                {
                    return new PlayerDTO
                    {
                        PlayerID = player.PlayerID,
                        FirstName = player.FirstName,
                        LastName = player.LastName,
                        BirthDay = player.BirthDay,
                        PhoneNumber = player.PhoneNumber,
                        Username = player.Username,
                        EmailAddress = player.EmailAddress,
                        ProfilePic = player.ProfilePic,
                        TotalScore = player.TotalScore,
                        SelectedLanguageID = player.SelectedLanguageID
                    };
                }
                return null;
            }
        }
    }
}
