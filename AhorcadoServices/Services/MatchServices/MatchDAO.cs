using System;
using Services.DTOs;
using Model;

namespace AhorcadoServices.Services.MatchServices
{
    public class MatchDAO
    {
        public MatchDTO CreateMatch(int player1Id, int wordId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var match = new Matches
                {
                    Player1 = player1Id,
                    Player2 = null,
                    WordID = wordId,
                    CreateDate = DateTime.Now,
                    EndDate = null,
                    StatusID = 1 // O el status que corresponda
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
    }
}