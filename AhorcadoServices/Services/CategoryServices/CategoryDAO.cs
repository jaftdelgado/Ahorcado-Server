using Model;
using Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace AhorcadoServices.Services.CategoryServices
{
    public class CategoryDAO
    {
        public List<CategoryDTO> GetCategories()
        {
            using (var context = new ahorcadoDBEntities())
            {
                return context.Categories
                    .Select(c => new CategoryDTO
                    {
                        CategoryID = c.CategoryID,
                        CategoryName = c.CategoryName
                    })
                    .ToList();
            }
        }
    }
}
