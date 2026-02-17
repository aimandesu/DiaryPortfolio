using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs.Photo
{
    public class PhotoModelDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Mime { get; set; } = string.Empty;
        public double Width { get; set; }
        public double Height { get; set; }
        public double Size { get; set; }
    }
}
