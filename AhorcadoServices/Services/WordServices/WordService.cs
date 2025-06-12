using System.Collections.Generic;
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

        public List<WordDTO> GetWords()
        {
            var dao = new WordDAO();
            return dao.GetWords();
        }
    }
}
