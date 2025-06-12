using Services.DTOs;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace GameServices
{
    [ServiceContract(CallbackContract = typeof(IGameManagerCallback))]
    public interface IGameManager
    {
        [OperationContract]
        void JoinMatch(int matchId, PlayerInfoDTO player, WordInfoDTO word, int maxAttempts);

        [OperationContract]
        void LeaveMatch(int matchId, int playerId);

        [OperationContract]
        void GuessLetter(int matchId, int playerId, string letter);
    }

    [ServiceContract]
    public interface IGameManagerCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnPlayerJoined(int matchId, PlayerInfoDTO player);

        [OperationContract(IsOneWay = true)]
        void OnPlayerLeft(int matchId, int playerId);

        [OperationContract(IsOneWay = true)]
        void OnMatchReady(int matchId, MatchInfoDTO matchInfo);

        [OperationContract(IsOneWay = true)]
        void OnLetterGuessed(int matchId, string letter, bool isCorrect, int remainingAttempts, bool isGameOver);

        [OperationContract(IsOneWay = true)]
        void OnGameOver(int matchId, int winnerPlayerId);
    }
}