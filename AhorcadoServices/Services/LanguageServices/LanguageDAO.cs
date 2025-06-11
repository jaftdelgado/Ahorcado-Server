using Model;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServices.Services.LanguageServices
{
    public class LanguageDAO
    {
        public List<LanguageDTO> GetLanguages()
        {
            using (var context = new ahorcadoDBEntities())
            {
                return context.Languages
                    .Select(l => new LanguageDTO
                    {
                        LanguageID = l.LanguageID,
                        LanguageName = l.LanguageName
                    })
                    .ToList();
            }
        }
    }
}
