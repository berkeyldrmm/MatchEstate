using BusinessLayer.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace BusinessLayer.Concrete
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task DeleteListingImages(List<string> listingIds)
        {
            foreach (var id in listingIds)
            {
                await _cloudinary.DeleteResourcesByPrefixAsync(id);
                await _cloudinary.DeleteFolderAsync(id);
            }
        }

        public async Task<List<string>> UpdateListingImages(string listingId, List<IFormFile> files, List<string> deletingImageIds)
        {
            foreach (var id in deletingImageIds)
            {
                var imageId = id.Split(".")[0];
                var publicId = $"{listingId}/{imageId}";
                var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));
                Debug.WriteLine("");
            }
            return await UploadImages(listingId, files);
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
                var path = $"{uploadResult.PublicId}.{uploadResult.Format}";
                return path.Split("/")[1];
            }

            throw new Exception($"An error occured while uploading images. {uploadResult.Error}");
        }

        public async Task<List<string>> UploadImages(string listingId, List<IFormFile> files)
        {
            List<string> images = new List<string>();

            if(files == null || files.Count == 0)
                return images;

            foreach (var img in files)
            {
                images.Add(await UploadImage(listingId, img));
            }

            return images;
        }

        //public async Task<List<string>> GetPublicIdsFromFolderAsync(string folderName)
        //{
        //    var publicIds = new List<string>();

        //    //var searchResult = await _cloudinary.Search()
        //    //    .Expression($"folder={folderName}")
        //    //    .SortBy("public_id", "asc")
        //    //    .ExecuteAsync();

        //    var result = await _cloudinary.Folder

        //    if (result.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        foreach (var resource in result.Resources)
        //        {
        //            publicIds.Add(resource.PublicId);
        //        }
        //    }

        //    return publicIds;
        //}
    }
}
