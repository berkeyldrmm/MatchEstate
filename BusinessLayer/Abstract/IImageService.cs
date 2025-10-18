using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
    public interface IImageService
    {
        public Task<string> UploadImage(string listingId, IFormFile file);
        public Task<List<string>> UploadImages(string listingId, List<IFormFile> files);
        public Task DeleteListingImages(List<string> listingIds);
        public Task<List<string>> UpdateListingImages(string listingId, List<IFormFile> files, List<string> deletingImageIds);
    }
}
