using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Services.DTOs;

namespace AhorcadoServices.Services.MatchServices
{
    [ServiceContract]
    public interface IMatchManager
    {
        [OperationContract]
        bool CreateMatch(int player1Id, int wordId);
    }
}
