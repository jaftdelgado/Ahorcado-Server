using AhorcadoServices.DTOs;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServices.Services.MatchServices
{
    [ServiceContract]
    public interface IMatchManager
    {
        [OperationContract]
        MatchDTO CreateMatch(int player1Id, int wordId);

        [OperationContract]
        MatchDTO JoinMatch(int matchId, int player2Id);

        [OperationContract]
        List<AvailableMatchDTO> GetAvailableMatches();

        [OperationContract]
        List<PlayerMatchHistoryDTO> GetPlayerMatchHistory(int playerId);
    }
}
