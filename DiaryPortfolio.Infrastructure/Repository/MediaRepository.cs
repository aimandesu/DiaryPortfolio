using Dapper;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Location;
using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Application.DTOs.Photo;
using DiaryPortfolio.Application.DTOs.Space;
using DiaryPortfolio.Application.DTOs.Video;
using DiaryPortfolio.Application.DTOsSP.Media;
using DiaryPortfolio.Application.Helpers.Filter;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MediaRepository> _logger;

        public MediaRepository(
            ApplicationDbContext context,
            ILogger<MediaRepository> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Pagination<MediaModelDto>> GetAllMediaByUsername(
            QuerySearchObject querySearchObject,
            Guid userId)
        {
            _logger.LogInformation("Fetching media for user with ID: {UserId}", userId);

            var skip = (querySearchObject.PageNumber - 1) * querySearchObject.PageSize;

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@Skip", skip);
            parameters.Add("@PerPage", querySearchObject.PageSize);

            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var multi = await connection.QueryMultipleAsync(
                "dbo.sp_GetMediaByUserId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            // Result set 1
            var total = await multi.ReadFirstAsync<int>();

            // Result set 2 — read flat, then manually build nested models
            var flatRows = (await multi.ReadAsync<MediaFlatRow>()).ToList();

            var data = flatRows.Select(row => new MediaModelDto
            {
                Id = row.Id,
                Title = row.Title,
                Description = row.Description,
                CreatedAt = row.CreatedAt,
                MediaStatus = row.MediaStatus,
                MediaType = row.MediaType,

                SpaceModel = row.Space_Id.HasValue ? new SpaceModelDto
                {
                    Id = row.Space_Id.Value,
                    Title = row.Space_Title ?? "",
                    CreatedAt = row.Space_CreatedAt ?? DateTime.MinValue,
                    UserId = row.Space_UserId ?? Guid.Empty,
                } : null,

                LocationModel = row.Loc_Id.HasValue ? new LocationModelDto
                {
                    Id = row.Loc_Id.Value,
                    Name = row.Loc_Name ?? "",
                    Latitude = row.Loc_Latitude ?? "",
                    Longitude = row.Loc_Longitude ?? "",
                } : null,

                VideoModels = [],
                PhotoModels = [],

            }).ToList();

            // Result set 3 & 4
            var videos = (await multi.ReadAsync<MediaVideoModel>()).ToList();
            var photos = (await multi.ReadAsync<MediaPhotoModel>()).ToList();

            var videoLookup = videos
                .Where(v => v.Media != null)
                .GroupBy(v => v.Media!.Id)
                .ToDictionary(
                    g => g.Key, 
                    g => g.Select(v => v.Video).OfType<VideoModel>().ToList()
                );

            var photoLookup = photos
                .Where(p => p.Media != null)
                .GroupBy(p => p.Media!.Id)
                .ToDictionary(
                    g => g.Key, 
                    g => g.Select(p => p.Photo).OfType<PhotoModel>().ToList()
                );

            foreach (var media in data)
            {
                media.VideoModels = videoLookup.TryGetValue(media.Id, out var v) ? v : [];
                media.PhotoModels = photoLookup.TryGetValue(media.Id, out var p) ? p : [];
            }

            return new Pagination<MediaModelDto>
            {
                Data = data,
                CurrentPage = querySearchObject.PageNumber,
                PerPage = querySearchObject.PageSize,
                Total = total,
                LastPage = (int)Math.Ceiling(total / (double)querySearchObject.PageSize)
            };
        }
    }
}
