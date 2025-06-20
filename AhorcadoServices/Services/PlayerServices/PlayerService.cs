using Services.DTOs;
using System;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;



namespace Services.PlayerServices
{
    public class PlayerService : IPlayerManager
    {
        PlayerDAO playerDAO = new PlayerDAO();
        private readonly DbContext dbContext;

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

        public bool UpdatePlayerInfo(PlayerDTO playerdto)
        {
            return playerDAO.UpdatePlayerInfo(playerdto);
        }
        public PlayerDTO GetPlayerById(int playerId)
        {
            var playerDAO = new PlayerDAO();
            return playerDAO.GetPlayerById(playerId);
        }


    }
}
