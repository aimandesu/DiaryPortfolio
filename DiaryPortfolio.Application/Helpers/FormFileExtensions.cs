using DiaryPortfolio.Application.Request;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers
{
    public static class FormFileExtensions
    {
        public static IEnumerable<MediaStream> ToMediaStreams(
            this IEnumerable<IFormFile> files)
        {
            return files.Select(f => new MediaStream
            {
                Stream = f.OpenReadStream(),
                FileName = f.FileName
            });
        }

        public static MediaStream ToMediaStream(this IFormFile file)
        {
            return new MediaStream
            {
                Stream = file.OpenReadStream(),
                FileName = file.FileName
            };
        }
    }
}
