using DiaryPortfolio.Application.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers.Validation
{
    public static class FileValidatorExtensions
    {
        public static IRuleBuilderOptions<T, IFormFile?> MustBeDocument<T>(
            this IRuleBuilder<T, IFormFile?> rule)
        {
            return rule
                .Must(file => file == null ||
                    AllowedContentTypes.Documents.Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage($"File must be one of: {string.Join(", ", AllowedContentTypes.Documents)}");
        }

        public static IRuleBuilderOptions<T, IFormFile> MustBeImage<T>(
            this IRuleBuilder<T, IFormFile> rule)
        {
            return rule
                .Must(file => AllowedContentTypes.Images.Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage($"File must be an image. Allowed: {string.Join(", ", AllowedContentTypes.Images)}");
        }

        public static IRuleBuilderOptions<T, IFormFile> MustBeVideo<T>(
            this IRuleBuilder<T, IFormFile> rule)
        {
            return rule
                .Must(file => AllowedContentTypes.Videos.Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage($"File must be a video. Allowed: {string.Join(", ", AllowedContentTypes.Videos)}");
        }

        public static IRuleBuilderOptions<T, IFormFile> MustBeMedia<T>(
            this IRuleBuilder<T, IFormFile> rule)
        {
            return rule
                .Must(file => AllowedContentTypes.Media.Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage($"File must be an image or video. Allowed: {string.Join(", ", AllowedContentTypes.Media)}");
        }
    }
}
