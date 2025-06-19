using AhorcadoServices.Services.MatchServices;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;

namespace GameServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameService : IGameManager
    {
        private readonly MatchService _matchService = new MatchService();

        private readonly Dictionary<int, MatchInfoWithCallbacks> _activeMatches = new Dictionary<int, MatchInfoWithCallbacks>();
        private readonly MatchDAO dao = new MatchDAO();

        public void JoinMatch(int matchId, PlayerInfoDTO player, WordInfoDTO word, int maxAttempts = 6)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IGameManagerCallback>();

            lock (_activeMatches)
            {
                if (!_activeMatches.TryGetValue(matchId, out var match))
                {
                    match = new MatchInfoWithCallbacks
                    {
                        MatchInfo = new MatchInfoDTO
                        {
                            MatchID = matchId,
                            Player1 = null,
                            Player2 = null,
                            Word = word,
                            RemainingAttempts = maxAttempts,
                            GuessedLetters = new List<string>(),
                            IsGameOver = false
                        }
                    };
                    _activeMatches[matchId] = match;
                }

                if (match.MatchInfo.Player1 == null)
                {
                    match.MatchInfo.Player1 = player;
                    match.Callback1 = callback;
                    callback.OnMatchReady(matchId, match.MatchInfo);
                }
                else if (match.MatchInfo.Player2 == null && match.MatchInfo.Player1.PlayerId != player.PlayerId)
                {
                    match.MatchInfo.Player2 = player;
                    match.Callback2 = callback;
                    match.Callback1?.OnMatchReady(matchId, match.MatchInfo);
                    match.Callback2?.OnMatchReady(matchId, match.MatchInfo);
                }
                else
                {
                    return;
                }
            }
        }

        public void LeaveMatch(int matchId, int playerId)
        {
            lock (_activeMatches)
            {
                if (_activeMatches.TryGetValue(matchId, out var match))
                {
                    bool isPlayer1 = match.MatchInfo.Player1?.PlayerId == playerId;
                    bool isPlayer2 = match.MatchInfo.Player2?.PlayerId == playerId;

                    if (isPlayer1 && match.MatchInfo.Player2 == null)
                    {
                        match.MatchInfo.Player1 = null;
                        match.Callback1 = null;
                        _activeMatches.Remove(matchId);
                        return;
                    }

                    if (!match.MatchInfo.IsGameOver && (isPlayer1 || isPlayer2))
                    {
                        bool forfeitSuccess = dao.ForfeitMatch(matchId, playerId);
                    }

                    if (isPlayer1 && match.Callback2 != null)
                    {
                        try
                        {
                            match.Callback2.OnPlayerLeft(matchId, playerId);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error notifying player 2 about leave: {ex.Message}");
                        }
                    }

                    else if (isPlayer2 && match.Callback1 != null)
                    {
                        try
                        {
                            match.Callback1.OnPlayerLeft(matchId, playerId);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error notifying player 1 about leave: {ex.Message}");
                        }
                    }

                    if (isPlayer1)
                    {
                        match.MatchInfo.Player1 = null;
                        match.Callback1 = null;
                    }
                    else if (isPlayer2)
                    {
                        match.MatchInfo.Player2 = null;
                        match.Callback2 = null;
                    }

                    if (match.MatchInfo.Player1 == null && match.MatchInfo.Player2 == null)
                    {
                        _activeMatches.Remove(matchId);
                    }
                }
            }
        }

        public void GuessLetter(int matchId, int playerId, string letter)
        {
            lock (_activeMatches)
            {
                if (_activeMatches.TryGetValue(matchId, out var match))
                {
                    if (match.MatchInfo.Player2?.PlayerId != playerId || match.MatchInfo.IsGameOver) return;

                    string word = match.MatchInfo.Word?.WordText?.ToUpper();
                    if (string.IsNullOrEmpty(word) || letter.Length != 1) return;

                    letter = letter.ToUpper();

                    if (match.MatchInfo.GuessedLetters.Contains(letter)) return;

                    match.MatchInfo.GuessedLetters.Add(letter);

                    bool isCorrect = word.Contains(letter);
                    if (!isCorrect)
                    {
                        match.MatchInfo.RemainingAttempts--;
                    }

                    bool wordIsGuessed = word.All(c => c == ' ' || match.MatchInfo.GuessedLetters.Contains(c.ToString(), StringComparer.OrdinalIgnoreCase));
                    match.MatchInfo.IsGameOver = wordIsGuessed || match.MatchInfo.RemainingAttempts <= 0;

                    if (match.MatchInfo.IsGameOver)
                    {
                        int winnerId = wordIsGuessed ? playerId : match.MatchInfo.Player1.PlayerId;

                        match.Callback1?.OnGameOver(matchId, winnerId);
                        match.Callback2?.OnGameOver(matchId, winnerId);
                    }

                    match.Callback1?.OnLetterGuessed(matchId, letter, isCorrect, match.MatchInfo.RemainingAttempts, match.MatchInfo.IsGameOver);
                    match.Callback2?.OnLetterGuessed(matchId, letter, isCorrect, match.MatchInfo.RemainingAttempts, match.MatchInfo.IsGameOver);
                }
            }
        }

    }
}
