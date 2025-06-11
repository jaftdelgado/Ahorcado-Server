using Services.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace AhorcadoServices.Services.CategoryServices
{
    [ServiceContract]
    public interface ICategoryManager
    {
        [OperationContract]
        List<CategoryDTO> GetCategories();
    }
}
