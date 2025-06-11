using Services.DTOs;
using Services.PlayerServices;
using System.Collections.Generic;
using System.ServiceModel;
using AhorcadoServices.Services.MatchServices;
using AhorcadoServices.Services.WordServices;
using AhorcadoServices.Services.CategoryServices;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPlayerManager
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        List<CategoryDTO> GetCategories();

        [OperationContract]
        List<int> GetDifficults(int categoryId, int languageId);

        [OperationContract]
        List<WordDTO> GetWords(int categoryId, int difficult, int languageId);

        [OperationContract]
        MatchDTO CreateMatch(int player1Id, int wordId);

        [OperationContract]
        MatchDTO JoinMatch(int matchId, int player2Id);

        [OperationContract]
        List<LanguageDTO> GetLanguages();
    }
}
