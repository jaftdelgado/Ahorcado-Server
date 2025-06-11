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
        public MatchDTO CreateMatch(int player1Id, int wordId)
        {
            var dao = new MatchDAO();
            return dao.CreateMatch(player1Id, wordId);
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
    }
}
