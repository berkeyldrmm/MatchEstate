using BusinessLayer.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImage(string listingId, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Folder = listingId
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return $"{uploadResult.PublicId}.{uploadResult.Format}";
            }

            throw new Exception($"An error occured while uploading images. {uploadResult.Error}");
        }

        public async Task<List<string>> UploadImages(string listingId, List<IFormFile> files)
        {
            List<string> images = new List<string>();
            foreach (var img in files)
            {
                images.Add(await UploadImage(listingId, img));
            }

            return images;
        }
    }
}
