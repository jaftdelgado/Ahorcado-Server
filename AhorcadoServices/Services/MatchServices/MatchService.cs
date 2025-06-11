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
    }
}
