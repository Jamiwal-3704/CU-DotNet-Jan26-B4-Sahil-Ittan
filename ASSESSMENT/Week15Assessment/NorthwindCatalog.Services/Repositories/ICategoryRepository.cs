using NorthwindCatalog.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindCatalog.Services.Repositories
{
    public interface ICategoryRepository
    {
        //Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
