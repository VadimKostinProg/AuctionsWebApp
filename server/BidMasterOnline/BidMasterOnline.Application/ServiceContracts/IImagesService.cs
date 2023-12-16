using CloudinaryDotNet.Actions;
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
        /// <returns>Result of upload.</returns>
        Task<ImageUploadResult> AddImageAsync(IFormFile image);

        /// <summary>
        /// Deletes the existant image from the storage.
        /// </summary>
        /// <param name="publicId">Public id of the image.</param>
        /// <returns>Result of the deletion.</returns>
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
