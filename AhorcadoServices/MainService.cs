using AhorcadoServices.Services.CategoryServices;
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

        public MainService()
        {
            _playerService = new PlayerService();
            _categoryService = new CategoryService();
            _wordService = new WordService();
            _matchService = new MatchService();
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
        public List<int> GetDifficults(int categoryId) => _wordService.GetDifficults(categoryId);

        public List<WordDTO> GetWords(int categoryId, int difficult) => _wordService.GetWords(categoryId, difficult);
        #endregion


        #region MatchServices
        public MatchDTO CreateMatch(int player1Id, int wordId)
            => _matchService.CreateMatch(player1Id, wordId);
        #endregion
    }
}
