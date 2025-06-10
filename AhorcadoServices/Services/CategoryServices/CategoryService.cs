using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace AhorcadoServices.Services.CategoryServices
{
    public class CategoryService : ICategoryManager
    {
        public List<string> GetCategories()
        {
            var dao = new CategoryDAO();
            return dao.GetCategories();
        }
    }
}
