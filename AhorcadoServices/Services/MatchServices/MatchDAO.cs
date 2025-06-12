using AhorcadoServices.DTOs;
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

        public List<Matches> GetAvailableMatches(int playerId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                return context.Matches
                      .Where(m => m.StatusID == 1 && m.Player2 == null && m.Player1 != playerId)
                      .Include("Words")
                      .Include("Players")
                      .ToList();
            }
        }

        public List<PlayerMatchHistoryDTO> GetPlayerMatchHistory(int playerId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var finishedOrCancelled = new[] { 3, 4 };

                var query = from m in context.Matches
                            where (m.Player1 == playerId || m.Player2 == playerId)
                                  && finishedOrCancelled.Contains(m.StatusID)
                            join w in context.Words on m.WordID equals w.WordID
                            join ms in context.MatchScores on new { MatchID = m.MatchID, PlayerID = playerId }
                                equals new { ms.MatchID, ms.PlayerID }
                            join r in context.MatchResults on ms.ResultID equals r.ResultID
                            join p1 in context.Players on m.Player1 equals p1.PlayerID
                            join p2 in context.Players on m.Player2 equals p2.PlayerID into p2Join
                            from p2 in p2Join.DefaultIfEmpty()
                            select new PlayerMatchHistoryDTO
                            {
                                MatchID = m.MatchID,
                                OpponentName = m.Player1 == playerId
                                    ? (p2 != null ? p2.Username : "N/A")
                                    : p1.Username,
                                PlayedWord = w.Word,
                                EndDate = m.EndDate,
                                ResultName = r.ResultName,
                                Points = ms.Points
                            };

                return query.ToList();
            }
        }

        public bool ForfeitMatch(int matchId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var match = context.Matches.Find(matchId);
                if (match == null || match.StatusID == 3 || match.StatusID == 4)
                    return false;

                match.StatusID = 4;
                match.EndDate = DateTime.Now;

                var player1 = context.Players.Find(match.Player1);
                if (player1 == null) return false;

                if (match.Player2 != null)
                    player1.TotalScore -= 3;

                var forfeitResult = new MatchScores
                {
                    MatchID = match.MatchID,
                    PlayerID = player1.PlayerID,
                    ResultID = 3,
                    Points = -3
                };
                context.MatchScores.Add(forfeitResult);

                context.SaveChanges();
                return true;
            }
        }

        public bool DeclareVictoryForPlayer2(int matchId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var match = context.Matches.Find(matchId);
                if (match == null || match.StatusID == 3 || match.StatusID == 4 || match.Player2 == null)
                    return false;

                match.StatusID = 3;
                match.EndDate = DateTime.Now;

                var player2 = context.Players.Find(match.Player2.Value);
                if (player2 == null) return false;

                player2.TotalScore += 10;

                var score = new MatchScores
                {
                    MatchID = match.MatchID,
                    PlayerID = player2.PlayerID,
                    ResultID = 1,
                    Points = 10
                };
                context.MatchScores.Add(score);

                context.SaveChanges();
                return true;
            }
        }

        public bool DeclareVictoryForPlayer1(int matchId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var match = context.Matches.Find(matchId);
                if (match == null || match.StatusID == 3 || match.StatusID == 4 || match.Player2 == null)
                    return false;

                match.StatusID = 3;
                match.EndDate = DateTime.Now;

                var player1 = context.Players.Find(match.Player1);
                if (player1 == null) return false;

                player1.TotalScore += 5;

                var score = new MatchScores
                {
                    MatchID = match.MatchID,
                    PlayerID = player1.PlayerID,
                    ResultID = 1,
                    Points = 5
                };
                context.MatchScores.Add(score);

                context.SaveChanges();
                return true;
            }
        }

    }
}