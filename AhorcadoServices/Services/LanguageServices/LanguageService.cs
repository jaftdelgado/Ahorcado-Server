using Services.DTOs;
using Services.WordServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServices.Services.LanguageServices
{
    public class LanguageService : ILanguageManager
    {
        public List<LanguageDTO> GetLanguages()
        {
            var dao = new LanguageDAO();
            return dao.GetLanguages();
        }
    }
}
