using AhorcadoServices.DTOs;
using AhorcadoServices.Services.CategoryServices;
using AhorcadoServices.Services.LanguageServices;
using AhorcadoServices.Services.MatchServices;
using AhorcadoServices.Services.WordServices;
using Services.DTOs;
using Services.MatchServices;
using Services.PlayerServices;
using Services.WordServices;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MainService : IMainManager
    {
        private readonly IPlayerManager _playerService;
        private readonly ICategoryManager _categoryService;
        private readonly IWordManager _wordService;
        private readonly IMatchManager _matchService;
        private readonly ILanguageManager _languageService;

        public MainService()
        {
            _playerService = new PlayerService();
            _categoryService = new CategoryService();
            _wordService = new WordService();
            _matchService = new MatchService();
            _languageService = new LanguageService();
        }

        public bool Ping() { return true; }

        #region PlayerServices
        public PlayerDTO LogIn(string username, string password) => _playerService.LogIn(username, password);

        public bool RegisterPlayer(PlayerDTO player) => _playerService.RegisterPlayer(player);

        public bool UpdatePlayerInfo(PlayerDTO player) => _playerService.UpdatePlayerInfo(player);
        #endregion


        #region CategoryServices
        public List<CategoryDTO> GetCategories() => _categoryService.GetCategories();
        #endregion


        #region WordServices
        public List<int> GetDifficults(int categoryId, int languageId) => _wordService.GetDifficults(categoryId,languageId);

        public List<WordDTO> GetWords() => _wordService.GetWords();
        #endregion


        #region MatchServices
        public MatchDTO CreateMatch(int player1Id, int wordId)
            => _matchService.CreateMatch(player1Id, wordId);

        public MatchDTO JoinMatch(int matchId, int player2Id)
            => _matchService.JoinMatch(matchId, player2Id);

        public List<MatchDTO> GetAvailableMatches(int playerId)
            => _matchService.GetAvailableMatches(playerId);

        public List<PlayerMatchHistoryDTO> GetPlayerMatchHistory(int playerId)
            => _matchService.GetPlayerMatchHistory(playerId);

        public bool ForfeitMatch(int matchId, int forfeitingPlayerId) 
            => _matchService.ForfeitMatch(matchId, forfeitingPlayerId);

        public bool DeclareVictoryForPlayer2(int matchId) 
            => _matchService.DeclareVictoryForPlayer2(matchId);

        public bool DeclareVictoryForPlayer1(int matchId) 
            => _matchService.DeclareVictoryForPlayer1(matchId);

        #endregion

        #region LanguageServices
        public List<LanguageDTO> GetLanguages() => _languageService.GetLanguages();

        #endregion
    }
}
