using Services.DTOs;

namespace Services.PlayerServices
{
    public class PlayerService : IPlayerManager
    {
        public PlayerDTO LogIn(string username, string password)
        {
            var playerDAO = new PlayerDAO();
            PlayerDTO player = playerDAO.LogIn(username, password);

            if (player != null) return player;

            else return null;
        }
    }
}
