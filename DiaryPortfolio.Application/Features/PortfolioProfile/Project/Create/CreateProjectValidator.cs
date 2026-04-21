using DiaryPortfolio.Application.Helpers.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Create
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.ProjectFileStream)
                .MustBeDocument();

            RuleForEach(x => x.MediaFileStreams)
                .MustBeMedia();
        }
    }
}
