using Services.DTOs;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace CallbackServices
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AhorcadoService : IAhorcadoManager
    {
        private readonly List<MatchDTO> _matches = new List<MatchDTO>();
        private readonly Dictionary<int, IAhorcadoCallback> _playerCallbacks = new Dictionary<int, IAhorcadoCallback>();
        private int _nextMatchId = 1;

        public MatchDTO CreateMatch(int playerId, int wordId)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IAhorcadoCallback>();

            if (!_playerCallbacks.ContainsKey(playerId))
                _playerCallbacks.Add(playerId, callback);

            var match = new MatchDTO
            {
                MatchID = _nextMatchId++,
                Player1ID = playerId,
                WordID = wordId,
                CreateDate = DateTime.Now,
                StatusID = 1,
                // Puedes obtener WordDTO y PlayerDTO desde tu lógica
                Word = GetWord(wordId),
                Player1 = GetPlayer(playerId)
            };

            _matches.Add(match);

            // Notificar al jugador creador (opcional)
            callback.OnMatchCreated(match);

            return match;
        }

        public MatchDTO JoinMatch(int playerId, int matchId)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IAhorcadoCallback>();

            if (!_playerCallbacks.ContainsKey(playerId))
                _playerCallbacks.Add(playerId, callback);

            var match = _matches.Find(m => m.MatchID == matchId);
            if (match == null || match.Player2ID.HasValue)
                throw new Exception("Partida no disponible.");

            match.Player2ID = playerId;
            match.Player2 = GetPlayer(playerId);
            match.StatusID = 2; // Actualizar estado a "en curso", por ejemplo

            // Notificar a ambos jugadores
            if (_playerCallbacks.TryGetValue(match.Player1ID, out var callback1))
            {
                callback1.OnPlayerJoined(match);
            }

            callback.OnMatchCreated(match); // Notificar al que se une

            return match;
        }

        // Simulaciones de obtención de datos
        private WordDTO GetWord(int wordId) => new WordDTO
        {
            WordID = wordId,
            Word = "DINOSAURIO", // Simulado
            CategoryID = 1,
            LanguageID = 1,
            Difficult = 2,
            Description = "Un animal prehistórico"
        };

        private PlayerDTO GetPlayer(int playerId) => new PlayerDTO
        {
            PlayerID = playerId,
            Username = $"Jugador{playerId}"
        };
    }
}
