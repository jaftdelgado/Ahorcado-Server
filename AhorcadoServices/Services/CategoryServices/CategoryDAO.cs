using System.Collections.Generic;
using System.Linq;
using Model;

namespace AhorcadoServices.Services.CategoryServices
{
    public class CategoryDAO
    {
        public List<string> GetCategories()
        {
            using (var context = new ahorcadoDBEntities())
            {
                return context.Categories.Select(c => c.CategoryName).ToList();
            }
        }
    }
}
