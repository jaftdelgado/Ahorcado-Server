using Model;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public Matches JoinMatch(int matchId, int player2Id)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var match = context.Matches.Find(matchId);
                if (match == null || match.Player2 != null) return null;

                match.Player2 = player2Id;
                match.StatusID = 2;

                context.SaveChanges();

                return match;
            }
        }

        public List<Matches> GetAvailableMatches()
        {
            using (var context = new ahorcadoDBEntities())
            {
                return context.Matches
                              .Where(m => m.StatusID == 1 && m.Player2 == null)
                              .Include("Words")
                              .Include("Players")
                              .ToList();
            }
        }
    }
}