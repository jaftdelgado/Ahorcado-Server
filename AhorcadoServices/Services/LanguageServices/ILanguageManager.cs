using Services.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace AhorcadoServices.Services.LanguageServices
{
    [ServiceContract]
    public interface ILanguageManager
    {
        [OperationContract]
        List<LanguageDTO> GetLanguages();
    }
}
