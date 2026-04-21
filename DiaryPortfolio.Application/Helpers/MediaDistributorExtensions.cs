using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.FileDistributor;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers
{
    public static class MediaDistributorExtensions
    {
        public static DistributedMediaResult ExtractMedia(
            this IEnumerable<Dictionary<MediaSubType, MediaDistributor>> source)
        {
            var distributors = source.SelectMany(e => e.Values);

            var videos = distributors
                .Select(d => d.Videos)
                .Where(v => v is not null)
                .Select(v => v!)
                .ToList();

            var photos = distributors
                .Select(d => d.Photos)
                .Where(p => p is not null)
                .Select(p => p!)
                .ToList();

            var files = distributors
                .Select(d => d.Files)
                .Where(p => p is not null)
                .Select(p => p!)
                .ToList();

            return new DistributedMediaResult
            {
                Videos = videos,
                Photos = photos,
                Files = files
            };
        }
    }
}
