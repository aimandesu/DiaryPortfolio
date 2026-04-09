using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper.User;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Profile.Create
{
    internal class CreateProfileHandler : IRequestHandler<CreateProfileRequest, ResultResponse<UserModelDto>>
    {
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly UserManager<UserModel> _userManager;

        public CreateProfileHandler(
            IFileHandlerRepository fileHandlerRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork
            //,
            //UserManager<UserModel> userManager
        )
        {
            _fileHandlerRepository = fileHandlerRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            //_userManager = userManager;
        }

        public async ValueTask<ResultResponse<UserModelDto>> Handle(
            CreateProfileRequest request, 
            CancellationToken cancellationToken)
        {

            var mediaType = MediaType.Profile;

            var streams = new List<MediaStream>();

            if (request.ProfileUpload?.ResumeFileStream is not null)
                streams.Add(request.ProfileUpload.ResumeFileStream);

            if (request.ProfileUpload?.ProfileFileSteam is not null)
                streams.Add(request.ProfileUpload.ProfileFileSteam);

            // Operations

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                streams,
                mediaType
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<UserModelDto>.Failure(uploadResult.Error);
            }

            var uploadProfileResult = await _userRepository.UploadProfile(
                profileUpload: request.ProfileUpload,
                profilePhoto: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.Image))
                    .Select(e => e[MediaSubType.Image].Photos)
                    .ToList().First(),
                resumeFile: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.File))
                    .Select(e => e[MediaSubType.File].Files)
                    .ToList().First()
            );

            try
            {
                //await _unitOfWork.SaveChanges(cancellationToken);
                //await _userManager.UpdateAsync(uploadProfileResult.Result);
                return ResultResponse<UserModelDto>.Success(uploadProfileResult.Result.ToPortfolioProfileDto()); //ni pun kene terangkan profile mana
            }
            catch (Exception ex) {

                _fileHandlerRepository.DeleteFiles(
                    [
                    .. uploadResult.Result
                        .Where(e => e.ContainsKey(MediaSubType.Image))
                        .Select(e => e[MediaSubType.Image].Photos?.Url ?? ""),
                    .. uploadResult.Result
                        .Where(e => e.ContainsKey(MediaSubType.File))
                        .Select(e => e[MediaSubType.File].Files?.Url ?? "")
                    ]);

                return ResultResponse<UserModelDto>.Failure(
                    new Error(System.Net.HttpStatusCode.Conflict, ex.ToString())
                );

            }

        }
    }
}
