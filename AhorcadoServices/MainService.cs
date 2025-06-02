using Services.DTOs;
using Services.PlayerServices;
using System.ServiceModel;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MainService : IMainManager
    {
        private readonly IPlayerManager _playerManager;

        public MainService()
        {
            _playerManager = new PlayerService();
        }

        public bool Ping() { return true; }

        #region PlayerServices
        public PlayerDTO LogIn(string username, string password) => _playerManager.LogIn(username, password);
        #endregion
    }
}
