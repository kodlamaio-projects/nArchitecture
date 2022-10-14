using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ImageService
{
    public interface IImageService
    {
        string Upload(IFormFile formFile);
        string Update(IFormFile formFile, string imageUrl);
        void Delete(string imageUrl);
    }
}
