using Services.DTOs;

namespace Services.PlayerServices
{
    public class PlayerService : IPlayerManager
    {
        PlayerDAO playerDAO = new PlayerDAO();

        public PlayerDTO LogIn(string username, string password)
        {
            PlayerDTO player = playerDAO.LogIn(username, password);

            if (player != null) return player;

            else return null;
        }

        public bool RegisterPlayer(PlayerDTO player)
        {
            return playerDAO.RegisterPlayer(player);
        }

        public bool UpdatePlayerInfo(PlayerDTO player)
        {
            return playerDAO.UpdatePlayerInfo(player);
        }
    }
}
