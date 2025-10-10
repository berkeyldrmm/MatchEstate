using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IImageService
    {
        public Task<string> UploadImage(string listingId, IFormFile file);
        public Task<List<string>> UploadImages(string listingId, List<IFormFile> files);
        public Task DeleteListingImages(List<string> listingIds);
        public Task<List<string>> UpdateListingImages(string listingId, List<IFormFile> files, List<string> deletingImageIds);
        //public Task<List<string>> GetPublicIdsFromFolderAsync(string folderName);
    }
}
