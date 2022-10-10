using Application.Services.ImageService;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.ClodinaryImageService
{
    public class CloudinaryImageServiceAdapter : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageServiceAdapter(IConfiguration configuration)
        {
            Account account = configuration.GetSection("CloudinaryAccount").Get<Account>();
            _cloudinary = new Cloudinary(account);
        }

        public string Upload(IFormFile formFile)
        {
            ImageUploadParams imageUploadParams = new()
            {
                File = new(formFile.FileName, formFile.OpenReadStream()),
                UseFilename = false,
                UniqueFilename = true,
                Overwrite = false
            };
            ImageUploadResult imageUploadResult = _cloudinary.Upload(imageUploadParams);

            return imageUploadResult.Url.ToString();
        }

        public string Update(IFormFile formFile, string imageUrl)
        {
            Delete(imageUrl);
            return Upload(formFile);
        }

        public void Delete(string imageUrl)
        {
            DeletionParams deletionParams = new(GetPublicId(imageUrl));
            _cloudinary.Destroy(deletionParams);
        }

        private string GetPublicId(string imageUrl)
        {
            int startIndex = imageUrl.LastIndexOf('/') + 1;
            int endIndex = imageUrl.LastIndexOf('.');
            int length = endIndex - startIndex;
            return imageUrl.Substring(startIndex, length);
        }
    }
}
