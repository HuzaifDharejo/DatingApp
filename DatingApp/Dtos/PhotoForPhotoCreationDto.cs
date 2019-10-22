using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Dtos
{
    public class PhotoForPhotoCreationDto
    {
        public IFormFile File { get; set; }
        public string Url { get; set; }
        public string Discription { get; set; }
        public DateTime DateAdded { get; set; }
        
        public string PublicId { get; set; }

        public PhotoForPhotoCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}
