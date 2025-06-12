using AhorcadoServices.Services.MatchServices;
using Model;
using AhorcadoServices.DTOs;
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

            WordDTO wordDTO;
            using (var context = new ahorcadoDBEntities())
            {
                var word = context.Words.FirstOrDefault(w => w.WordID == wordId);

                wordDTO = new WordDTO
                {
                    WordID = word.WordID,
                    Word = word.Word,
                    LanguageID = word.LanguageID,
                    CategoryID = word.CategoryID,
                    Difficult = word.Difficult
                };
            }

            var dto = new MatchDTO
            {
                MatchID = match.MatchID,
                Player1ID = match.Player1,
                Player2ID = match.Player2,
                WordID = match.WordID,
                Word = wordDTO,
                CreateDate = match.CreateDate,
                EndDate = match.EndDate,
                StatusID = match.StatusID
            };

            return dto;
        }

        public MatchDTO JoinMatch(int matchId, int player2Id)
        {
            var dao = new MatchDAO();
            var match = dao.JoinMatch(matchId, player2Id);
            
            if (match == null) return null;

            using (var context = new ahorcadoDBEntities())
            {
                var player1Entity = context.Players.Find(match.Player1);
                var player2Entity = context.Players.Find(match.Player2 ?? 0);
                var wordEntity = context.Words.Find(match.WordID);

                var player1DTO = player1Entity == null ? null : new PlayerDTO
                {
                    PlayerID = player1Entity.PlayerID,
                    FirstName = player1Entity.FirstName,
                    LastName = player1Entity.LastName,
                    BirthDay = player1Entity.BirthDay,
                    ProfilePic = player1Entity.ProfilePic,
                    TotalScore = player1Entity.TotalScore,
                    Username = player1Entity.Username,
                };

                var player2DTO = player2Entity == null ? null : new PlayerDTO
                {
                    PlayerID = player2Entity.PlayerID,
                    FirstName = player2Entity.FirstName,
                    LastName = player2Entity.LastName,
                    BirthDay = player2Entity.BirthDay,
                    ProfilePic = player2Entity.ProfilePic,
                    TotalScore = player2Entity.TotalScore,
                    Username = player2Entity.Username,
                };

                var wordDTO = wordEntity == null ? null : new WordDTO
                {
                    WordID = wordEntity.WordID,
                    Word = wordEntity.Word,
                    LanguageID = wordEntity.LanguageID,
                    CategoryID = wordEntity.CategoryID,
                    Difficult = wordEntity.Difficult,
                    Description = wordEntity.Description
                };

                return new MatchDTO
                {
                    MatchID = match.MatchID,
                    Player1ID = match.Player1,
                    Player2ID = match.Player2,
                    WordID = match.WordID,
                    CreateDate = match.CreateDate,
                    EndDate = match.EndDate,
                    StatusID = match.StatusID,
                    Player1 = player1DTO,
                    Player2 = player2DTO,
                    Word = wordDTO
                };
            }
        }

        public List<MatchDTO> GetAvailableMatches()
        {
            var matches = dao.GetAvailableMatches();

            var result = new List<MatchDTO>();

            foreach (var match in matches)
            {
                var word = match.Words;
                var player1 = match.Players;

                var wordDTO = word == null ? null : new WordDTO
                {
                    WordID = word.WordID,
                    Word = word.Word,
                    LanguageID = word.LanguageID,
                    CategoryID = word.CategoryID,
                    Difficult = word.Difficult,
                    Description = word.Description
                };

                var playerDTO = player1 == null ? null : new PlayerDTO
                {
                    PlayerID = player1.PlayerID,
                    Username = player1.Username,
                    FirstName = player1.FirstName,
                    LastName = player1.LastName,
                };

                result.Add(new MatchDTO
                {
                    MatchID = match.MatchID,
                    Player1ID = match.Player1,
                    Player2ID = match.Player2,
                    WordID = match.WordID,
                    CreateDate = match.CreateDate,
                    EndDate = match.EndDate,
                    StatusID = match.StatusID,
                    Word = wordDTO,
                    Player1 = playerDTO
                });
            }

            return result;
        }

        public List<PlayerMatchHistoryDTO> GetPlayerMatchHistory(int playerId)
        {
            return dao.GetPlayerMatchHistory(playerId);
        }

        public bool ForfeitMatch(int matchId)
        {
            return dao.ForfeitMatch(matchId);
        }

        public bool DeclareVictoryForPlayer2(int matchId)
        {
            return dao.DeclareVictoryForPlayer2(matchId);
        }

        public bool DeclareVictoryForPlayer1(int matchId)
        {
            return dao.DeclareVictoryForPlayer1(matchId);
        }


    }
}
