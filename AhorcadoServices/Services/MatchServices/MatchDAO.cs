using Model;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AhorcadoServices.Services.MatchServices
{
    public class MatchDAO
    {
        public Matches CreateMatch(int player1Id, int wordId)
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
                    StatusID = 1
                };

                context.Matches.Add(match);
                context.SaveChanges();

                return match;
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

        public List<AvailableMatchDTO> GetAvailableMatches()
        {
            using (var context = new ahorcadoDBEntities())
            {
                var query = from m in context.Matches
                            where m.StatusID == 1 && m.Player2 == null
                            join p in context.Players on m.Player1 equals p.PlayerID
                            join w in context.Words on m.WordID equals w.WordID
                            join c in context.Categories on w.CategoryID equals c.CategoryID
                            select new AvailableMatchDTO
                            {
                                MatchID = m.MatchID,
                                CreatorName = p.FirstName + " " + p.LastName,
                                WordCategory = c.CategoryName,
                                Difficulty = w.CategoryID,
                                CreateDate = m.CreateDate
                            };

                return query.ToList();
            }
        }
    }
}