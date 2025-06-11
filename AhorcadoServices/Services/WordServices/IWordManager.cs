using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Services.DTOs;

namespace AhorcadoServices.Services.WordServices
{
    [ServiceContract]
    public interface IWordManager
    {
        [OperationContract]
        List<int> GetDifficults(int categoryId, int languageId);

        [OperationContract]
        List<WordDTO> GetWords(int categoryId, int difficult, int languageId);
    }
}
