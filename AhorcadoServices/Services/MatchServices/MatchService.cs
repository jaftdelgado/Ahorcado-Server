using AhorcadoServices.DTOs;
using AhorcadoServices.Services.MatchServices;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MatchServices
{
    public class MatchService : IMatchManager
    {
        private readonly MatchDAO dao = new MatchDAO();

        public MatchDTO CreateMatch(int player1Id, int wordId)
        {
            var match = dao.CreateMatch(player1Id, wordId);

            var dto = new MatchDTO
            {
                MatchID = match.MatchID,
                Player1 = match.Player1,
                Player2 = match.Player2,
                WordID = match.WordID,
                CreateDate = match.CreateDate,
                EndDate = match.EndDate,
                StatusID = match.StatusID
            };

            return dto;
        }

        public MatchDTO JoinMatch(int matchId, int player2Id)
        {
            var dao = new MatchDAO();
            return dao.JoinMatch(matchId, player2Id);
        }

        public List<AvailableMatchDTO> GetAvailableMatches()
        {
            var dao = new MatchDAO();
            return dao.GetAvailableMatches();
        }

        public List<PlayerMatchHistoryDTO> GetPlayerMatchHistory(int playerId)
        {
            return dao.GetPlayerMatchHistory(playerId);
        }
    }
}
