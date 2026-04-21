using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Common
{
    public static class AllowedContentTypes
    {
        public static readonly string[] Images =
        [
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg"
        ];

        public static readonly string[] Videos =
        [
            ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv"
        ];

        public static readonly string[] Documents =
        [
            ".pdf"
        ];

        public static readonly string[] Media = [.. Images, .. Videos];
    }
}
