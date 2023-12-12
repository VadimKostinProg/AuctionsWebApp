using BidMasterOnline.Application.ServiceContracts;
using Microsoft.AspNetCore.Http;

namespace BidMasterOnline.Application.Services
{
   public class ImagesService : IImagesService
    {
        public Task<string> AddImageAsync(IFormFile image)
        {
            throw new NotImplementedException();
        }
    }
}
