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
        List<string> GetCategories();

        [OperationContract]
        List<int> GetDifficults(int categoryId);

        [OperationContract]
        List<WordDTO> GetWords(int categoryId, int difficult);

        [OperationContract]
        bool CreateMatch(int player1Id, int wordId);
    }
}
