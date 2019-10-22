using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Dtos
{
    public class PhotoForPhotoReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Discription { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
    }
}
