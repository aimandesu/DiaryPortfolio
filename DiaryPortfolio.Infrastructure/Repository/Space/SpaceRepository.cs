using Azure.Core;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository.ISpaceRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository.Space
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SpaceRepository(
            ApplicationDbContext context,
            IUserService userService
            )
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ResultResponse<SpaceModel>> AddSpace(string title)
        {
            var userId = _userService.UserId!.Value;

            var userModel = await _context.Users
                .Include(e => e.DiaryProfile)
                    .ThenInclude(s => s.SpaceModels)
                .FirstOrDefaultAsync( u => u.Id == userId );

            if (userModel?.DiaryProfile == null) {
                return ResultResponse<SpaceModel>.Failure(
                    new Error(
                        HttpStatusCode.NotFound, 
                        "User does not sign up for diary profile services yet.")
                );
            }

            if (userModel.DiaryProfile.SpaceModels
                .Any(s => s.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase)))
            {
                return ResultResponse<SpaceModel>.Failure(
                    new Error(
                        HttpStatusCode.BadRequest,
                        "Space with the same title already exists."
                    )
                );
            }

            var space = new SpaceModel
            {
                Title = title,
                CreatedAt = DateTime.UtcNow,
                DiaryProfileId = userModel.DiaryProfile.Id,
            };

            _context.Spaces.Add(space);

            return ResultResponse<SpaceModel>.Success(space);

        }
    }
}
