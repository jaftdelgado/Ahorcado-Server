using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace GameServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameService : IGameManager
    {
        private readonly Dictionary<int, MatchInfoWithCallbacks> _activeMatches = new Dictionary<int, MatchInfoWithCallbacks>();

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
                    bool player1Left = match.MatchInfo.Player1?.PlayerId == playerId;
                    bool player2Left = match.MatchInfo.Player2?.PlayerId == playerId;

                    if (player1Left)
                    {
                        match.MatchInfo.Player1 = null;
                        match.Callback1 = null;
                    }

                    if (player2Left)
                    {
                        match.MatchInfo.Player2 = null;
                        match.Callback2 = null;
                    }

                    // Notificar al otro jugador sobre la desconexión
                    if (player1Left && match.Callback2 != null)
                    {
                        match.Callback2.OnPlayerLeft(matchId, playerId);
                    }
                    else if (player2Left && match.Callback1 != null)
                    {
                        match.Callback1.OnPlayerLeft(matchId, playerId);
                    }

                    // Eliminar partida si no hay jugadores
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

                    bool isWordGuessed = word.All(c => c == ' ' || match.MatchInfo.GuessedLetters.Contains(c.ToString()));
                    match.MatchInfo.IsGameOver = isWordGuessed || match.MatchInfo.RemainingAttempts <= 0;

                    match.Callback1?.OnLetterGuessed(matchId, letter, isCorrect, match.MatchInfo.RemainingAttempts, match.MatchInfo.IsGameOver);
                    match.Callback2?.OnLetterGuessed(matchId, letter, isCorrect, match.MatchInfo.RemainingAttempts, match.MatchInfo.IsGameOver);
                }
            }
        }
    }
}
