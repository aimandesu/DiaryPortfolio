using DiaryPortfolio.Application.Helpers.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.Update
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.ProjectFileStream)
               .MustBeDocument();

            RuleForEach(x => x.MediaFileStreams)
                .MustBeMedia();
        }
    }
}
