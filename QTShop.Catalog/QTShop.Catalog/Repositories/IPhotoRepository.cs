using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QTShop.Catalog.Repositories
{
    public interface IPhotoRepository
    {
        Task<string> InsertPhotoAsync(IFormFile fileStream);
    }
}