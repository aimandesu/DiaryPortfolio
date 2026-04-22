using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Enum;

namespace DiaryPortfolio.Infrastructure.Repository
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
            string fileName,
            string? id = null)
        {
            var date = DateTime.UtcNow;
            var userId = _userService.UserId!.Value.ToString();

            var datePath = Path.Combine(
                date.Year.ToString(),
                date.Month.ToString("D2")
            );

            return mediaType switch
            {
                //modules
                MediaType.PortfolioProfile => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "portfolio-profile"
                ),

                MediaType.DiaryProfile => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "diary-profile"
                ),

                //for diary profile
                MediaType.Post => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "posts",
                    datePath,
                    subjectId: id
                ),

                MediaType.Short => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "shorts",
                    datePath,
                    subjectId: id
                ),

                //for portfolio profile
                MediaType.Education => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "educations"
                ),

                MediaType.Project => BuildPath(
                    mediaSubType,
                    userId ?? "",
                    fileName,
                    "projects",
                    subjectId: id
                ),

                _ => throw new ArgumentOutOfRangeException(nameof(mediaType))
            };

        }

        private string BuildPath(
            MediaSubType mediaSubType,
            string userId,
            string originalFileName,
            string contentType,
            string? additionalPath = null,
            string? subjectId = null
        )
        {
            var prefix = mediaSubType switch
            {
                MediaSubType.Image => "image",
                MediaSubType.Video => "video",
                _ => mediaSubType.ToString().ToLower(),
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

            if (!string.IsNullOrEmpty(subjectId))
            {
                pathComponents.Add(subjectId);
            }

            // Add filename
            pathComponents.Add(fileName);

            return Path.Combine([.. pathComponents]);
        }

    }
}
