using Model;
using Services.DTOs;
using System;
using System.Linq;

namespace Services.PlayerServices
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

        public bool RegisterPlayer(PlayerDTO playerDTO)
        {
            using (var context = new ahorcadoDBEntities())
            {
                bool exists = context.Players.Any(p =>
                    p.Username == playerDTO.Username || p.EmailAddress == playerDTO.EmailAddress);

                if (exists) return false;

                var newPlayer = new Players
                {
                    FirstName = playerDTO.FirstName,
                    LastName = playerDTO.LastName,
                    BirthDay = playerDTO.BirthDay,
                    PhoneNumber = playerDTO.PhoneNumber,
                    EmailAddress = playerDTO.EmailAddress,
                    ProfilePic = playerDTO.ProfilePic,
                    TotalScore = 0,
                    Username = playerDTO.Username,
                    Password = playerDTO.Password,
                    SelectedLanguageID = 1
                };

                context.Players.Add(newPlayer);
                context.SaveChanges();

                return true;
            }
        }

        public bool UpdatePlayerInfo(PlayerDTO playerDTO)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var existingPlayer = context.Players.FirstOrDefault(p => p.PlayerID == playerDTO.PlayerID);

                if (existingPlayer == null) return false;

                bool emailConflict = context.Players.Any(p =>
                    p.EmailAddress == playerDTO.EmailAddress && p.PlayerID != playerDTO.PlayerID);

                if (emailConflict) return false;

                existingPlayer.FirstName = playerDTO.FirstName;
                existingPlayer.LastName = playerDTO.LastName;
                existingPlayer.BirthDay = playerDTO.BirthDay;
                existingPlayer.PhoneNumber = playerDTO.PhoneNumber;
                existingPlayer.ProfilePic = playerDTO.ProfilePic;
                existingPlayer.SelectedLanguageID = playerDTO.SelectedLanguageID;
                existingPlayer.Password = playerDTO.Password;
                existingPlayer.Username = playerDTO.Username;

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Console.WriteLine($"Entidad: {eve.Entry.Entity.GetType().Name}, Estado: {eve.Entry.State}");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine($"- Propiedad: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                        }
                    }
                    throw;
                }
                return true;
            }
        }
        public PlayerDTO GetPlayerById(int playerId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var player = context.Players.FirstOrDefault(p => p.PlayerID == playerId);

                if (player == null)
                    return null;

                return new PlayerDTO
                {
                    PlayerID = player.PlayerID,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Username = player.Username,
                    Password = player.Password,
                    PhoneNumber = player.PhoneNumber,
                    BirthDay = player.BirthDay,
                    ProfilePic = player.ProfilePic,
                    TotalScore = player.TotalScore,
                    SelectedLanguageID = player.SelectedLanguageID
                };
            }
        }


    }
}

