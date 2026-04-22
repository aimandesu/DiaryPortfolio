using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Helpers;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Profile.Update
{
    internal class UpdateProfileHandler : IRequestHandler<UpdateProfileRequest, ResultResponse<UserModelDto>>
    {
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IPortfolioProfileRepository _portfolioProfileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly UserManager<UserModel> _userManager;

        public UpdateProfileHandler(
            IFileHandlerRepository fileHandlerRepository,
            IPortfolioProfileRepository portfolioProfileRepository,
            IUserRepository userRepository,
            IUserService userService
            //IUnitOfWork unitOfWork
            //,
            //UserManager<UserModel> userManager
        )
        {
            _fileHandlerRepository = fileHandlerRepository;
            _portfolioProfileRepository = portfolioProfileRepository;
            _userRepository = userRepository;
            _userService = userService;
            //_unitOfWork = unitOfWork;
            //_userManager = userManager;
        }

        public async ValueTask<ResultResponse<UserModelDto>> Handle(
            UpdateProfileRequest request, 
            CancellationToken cancellationToken)
        {

            var mediaType = MediaType.PortfolioProfile;

            var streams = new List<MediaStream>();

            var existingPortfolioProfile = await _userRepository
                .GetUserByUserId(
                    _userService.UserId ?? Guid.Empty,
                    ProfileType.Portfolio);

            //url
            var filesToDelete = new[]
            {
                existingPortfolioProfile?.PortfolioProfile?.Resume?.ResumeFile?.Url,
                existingPortfolioProfile?.PortfolioProfile?.ProfilePhoto?.Url
            }
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

            if (request.ProfileUpload?.ResumeFileStream is not null)
            {
                streams.Add(request.ProfileUpload.ResumeFileStream);
            }

            if (request.ProfileUpload?.ProfileFileSteam is not null)
            {
                streams.Add(request.ProfileUpload.ProfileFileSteam);
            }

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                streams,
                mediaType
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<UserModelDto>.Failure(uploadResult.Error);
            }

            var media = uploadResult.Result.ExtractMedia();

            var uploadProfileResult = await _portfolioProfileRepository.UploadProfile(
                userModel: existingPortfolioProfile,
                profileUpload: request.ProfileUpload,
                profilePhoto: media.Photos.FirstOrDefault(),
                resumeFile: media.Files.FirstOrDefault()
            );

            try
            {
                //await _unitOfWork.SaveChanges(cancellationToken);
                //await _userManager.UpdateAsync(uploadProfileResult.Result);

                if (filesToDelete != null && filesToDelete.Count > 0)
                {
                    _fileHandlerRepository.DeleteFiles(filesToDelete);
                } 

                return ResultResponse<UserModelDto>.Success(
                    uploadProfileResult.Result.ToPortfolioProfileDto()); 
            }
            catch (Exception ex) {

                return ResultResponse<UserModelDto>.Failure(
                    new Error(System.Net.HttpStatusCode.Conflict, ex.ToString())
                );

            }

        }
    }
}
