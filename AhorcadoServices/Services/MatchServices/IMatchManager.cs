using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Services.DTOs;

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
        List<MatchDTO> GetAvailableMatches();
    }
}
