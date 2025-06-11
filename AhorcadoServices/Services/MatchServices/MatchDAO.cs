using Model;
using Services.DTOs;
using System;
using System.Linq;

namespace AhorcadoServices.Services.MatchServices
{
    public class MatchDAO
    {
        public MatchDTO CreateMatch(int player1Id, int wordId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                int lastMatchId = context.Matches.Any()
                ? context.Matches.Max(m => m.MatchID)
                : 0;

                var match = new Matches
                {
                    MatchID = lastMatchId + 1,
                    Player1 = player1Id,
                    Player2 = null,
                    WordID = wordId,
                    CreateDate = DateTime.Now,
                    EndDate = null,
                    StatusID = 1 
                };

                context.Matches.Add(match);
                context.SaveChanges();

                return new MatchDTO
                {
                    MatchID = match.MatchID,
                    Player1 = match.Player1,
                    Player2 = match.Player2,
                    WordID = match.WordID,
                    CreateDate = match.CreateDate,
                    EndDate = match.EndDate,
                    StatusID = match.StatusID
                };
            }
        }

        public MatchDTO JoinMatch(int matchId, int player2Id)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var match = context.Matches.Find(matchId);
                if (match == null || match.Player2 != null)
                    return null; 

                match.Player2 = player2Id;
                match.StatusID = 2;

                context.SaveChanges();

                return new MatchDTO
                {
                    MatchID = match.MatchID,
                    Player1 = match.Player1,
                    Player2 = match.Player2,
                    WordID = match.WordID,
                    CreateDate = match.CreateDate,
                    EndDate = match.EndDate,
                    StatusID = match.StatusID
                };
            }
        }
    }
}