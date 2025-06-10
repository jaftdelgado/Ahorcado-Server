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
        public List<int> GetDifficults(int categoryId)
        {
            var dao = new WordDAO();
            return dao.GetDifficults(categoryId);
        }

        public List<WordDTO> GetWords(int categoryId, int difficult)
        {
            var dao = new WordDAO();
            return dao.GetWords(categoryId, difficult);
        }
    }
}
