using System;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace QTShop.Catalog.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IConfiguration _configuration;

        public PhotoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> InsertPhotoAsync(IFormFile fileStream)
        {
            var container = new BlobContainerClient(_configuration.GetSection("StorageAccountConnectionString").Value, 
                _configuration.GetSection("ProductContainer").Value);
            try
            {
                var blob = container.GetBlobClient(Guid.NewGuid()+ ".jpg");
                await using (var stream = fileStream.OpenReadStream())
                {
                    await blob.UploadAsync(stream);
                }

                var fileUrl = blob.Uri.AbsoluteUri;
                var result = fileUrl;
                return result;
            }
            catch (Exception ex)
            {
                // ignored
                throw new ApplicationException();
            }
        }
    }
}