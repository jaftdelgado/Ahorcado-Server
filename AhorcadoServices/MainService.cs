using Services.DTOs;
using Services.PlayerServices;
using System.ServiceModel;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MainService : IMainManager
    {
        private readonly IPlayerManager _playerService;

        public MainService()
        {
            _playerService = new PlayerService();
        }

        public bool Ping() { return true; }

        #region PlayerServices
        public PlayerDTO LogIn(string username, string password) => _playerService.LogIn(username, password);

        public bool RegisterPlayer(PlayerDTO player) => _playerService.RegisterPlayer(player);

        public bool UpdatePlayerInfo(PlayerDTO player) => _playerService.UpdatePlayerInfo(player);
        #endregion
    }
}
