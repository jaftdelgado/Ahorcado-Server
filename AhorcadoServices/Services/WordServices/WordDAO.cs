using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Services.DTOs;

namespace Services.WordServices
{
    public class WordDAO
    {
        public List<int> GetDifficults(int categoryId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                return context.Words
                    .Where(w => w.CategoryID == categoryId)
                    .Select(w => w.Difficult)
                    .Distinct()
                    .ToList();
            }
        }

        public List<WordDTO> GetWords(int categoryId, int difficult)
        {
            using (var context = new ahorcadoDBEntities())
            {
                return context.Words
                    .Where(w => w.CategoryID == categoryId && w.Difficult == difficult)
                    .Select(w => new WordDTO
                    {
                        WordID = w.WordID,
                        CategoryID = w.CategoryID,
                        LanguageID = w.LanguageID,
                        Word = w.Word,
                        Description = w.Description,
                        Difficult = w.Difficult
                    })
                    .ToList();
            }
        }
    }
}
