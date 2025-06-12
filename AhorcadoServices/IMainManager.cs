using Services.PlayerServices;
using System.ServiceModel;
using AhorcadoServices.Services.MatchServices;
using AhorcadoServices.Services.WordServices;
using AhorcadoServices.Services.CategoryServices;
using AhorcadoServices.Services.LanguageServices;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPlayerManager, IMatchManager, ICategoryManager, IWordManager, ILanguageManager
    {
        [OperationContract]
        bool Ping();
    }
}
