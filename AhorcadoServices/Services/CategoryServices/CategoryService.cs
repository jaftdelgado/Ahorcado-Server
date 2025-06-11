using Model;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhorcadoServices.Services.CategoryServices
{
    public class CategoryService : ICategoryManager
    {
        public List<CategoryDTO> GetCategories()
        {
            var dao = new CategoryDAO();
            return dao.GetCategories();
        }
    }
}
