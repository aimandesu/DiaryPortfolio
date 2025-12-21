using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers.FileDistributor
{
    public class MediaMetadata
    {
        public string Mime { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
        public string Height { get; set; } = string.Empty;
        public long Size { get; set; } // in bytes
        public double? Duration { get; set; } // in seconds
    }
}
