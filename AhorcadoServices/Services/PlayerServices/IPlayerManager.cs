using Services.DTOs;
using System.ServiceModel;

namespace Services.PlayerServices
{
    [ServiceContract]
    public interface IPlayerManager
    {
        [OperationContract]
        PlayerDTO LogIn(string username, string password);

        [OperationContract]
        bool RegisterPlayer(PlayerDTO player);

        [OperationContract]
        bool UpdatePlayerInfo(PlayerDTO player);

        [OperationContract]
        PlayerDTO GetPlayerById(int playerId);
    }
}
