using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServices.Services.LanguageServices
{
    public interface ILanguageManager
    {
        [OperationContract]
        List<LanguageDTO> GetLanguages();
    }
}
