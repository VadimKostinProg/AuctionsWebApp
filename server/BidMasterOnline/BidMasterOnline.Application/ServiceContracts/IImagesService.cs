using Microsoft.AspNetCore.Http;

namespace BidMasterOnline.Application.ServiceContracts
{
    /// <summary>
    /// Service for managing images.
    /// </summary>
    public interface IImagesService
    {
        /// <summary>
        /// Adds new image to the image storage.
        /// </summary>
        /// <param name="image">Image file to add.</param>
        /// <returns>URL of the added image.</returns>
        Task<string> AddImageAsync(IFormFile image);
    }
}
