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
    }
}
