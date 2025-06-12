using Services.DTOs;
using System.ServiceModel;

namespace CallbackServices
{
    [ServiceContract(CallbackContract = typeof(IAhorcadoCallback))]
    public interface IAhorcadoManager
    {
        [OperationContract]
        MatchDTO CreateMatch(int playerId, int wordId);

        [OperationContract]
        MatchDTO JoinMatch(int playerId, int matchId);
    }

    public interface IAhorcadoCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMatchCreated(MatchDTO match);

        [OperationContract(IsOneWay = true)]
        void OnPlayerJoined(MatchDTO match);
    }
}
