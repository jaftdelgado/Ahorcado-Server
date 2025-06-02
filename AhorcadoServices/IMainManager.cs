using Services.PlayerServices;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IMainManager : IPlayerManager
    {
        [OperationContract]
        bool Ping();
    }
}
