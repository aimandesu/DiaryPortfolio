using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.IRepository.IFileHandlerRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Enum;

namespace DiaryPortfolio.Infrastructure.Repository.FileHandler
{
    public class FilePathHandlerRepository : IFilePathHandlerRepository
    {
        IUserService _userService;

        public FilePathHandlerRepository(
            IUserService userService)
        {
            _userService = userService;
        }

        public string BuildPath(
            MediaType mediaType, 
            MediaSubType mediaSubType, 
            string fileName)
        {
            var date = DateTime.UtcNow;
            var userId = _userService.UserId!.Value.ToString();

            var datePath = Path.Combine(
                date.Year.ToString(),
                date.Month.ToString("D2")
            );

            return mediaType switch
            {
                MediaType.Profile => BuildPath(
                    mediaSubType,
                    userId ?? "", 
                    fileName,
                    "profile"
                ),

                MediaType.Post => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "posts",
                    datePath
                ),

                MediaType.Short => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "shorts",
                    datePath
                ),

                _ => throw new ArgumentOutOfRangeException(nameof(mediaType))
            };

        }

        private string BuildPath(
            MediaSubType mediaSubType,
            string userId,
            string originalFileName,
            string contentType,
            string? additionalPath = null
        )
        {
            var prefix = mediaSubType switch
            {
                MediaSubType.Image => "image",
                MediaSubType.Video => "video",
                _ => "media"
            };

            var ext = Path.GetExtension(originalFileName);
            var shortGuid = Guid.NewGuid().ToString("N")[..6];
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var fileName = $"{prefix}_{timestamp}_{shortGuid}{ext}";

            var shard = userId[..2];

            // Build path components
            var pathComponents = new List<string>
            {
                "uploads",
                "users",
                shard,
                userId,
                contentType
            };

            // Add additional path (date partition) if provided
            if (!string.IsNullOrEmpty(additionalPath))
            {
                pathComponents.Add(additionalPath);
            }

            // Add filename
            pathComponents.Add(fileName);

            return Path.Combine([.. pathComponents]);
        }

    }
}
