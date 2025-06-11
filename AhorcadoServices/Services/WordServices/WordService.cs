using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AhorcadoServices.Services.WordServices;
using Services.DTOs;

namespace Services.WordServices
{
    public class WordService : IWordManager
    {
        public List<int> GetDifficults(int categoryId, int languageId)
        {
            var dao = new WordDAO();
            return dao.GetDifficults(categoryId, languageId);
        }

        public List<WordDTO> GetWords(int categoryId, int difficult, int languageId)
        {
            var dao = new WordDAO();
            return dao.GetWords(categoryId, difficult, languageId);
        }
    }
}
