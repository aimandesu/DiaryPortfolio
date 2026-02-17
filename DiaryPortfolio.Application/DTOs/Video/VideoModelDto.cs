using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs.Video
{
    public class VideoModelDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Mime { get; set; } = string.Empty;
        public double Size { get; set; }

    }
}
