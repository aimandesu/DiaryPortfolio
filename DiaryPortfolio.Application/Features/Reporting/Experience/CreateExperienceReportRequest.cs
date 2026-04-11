using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Reporting.Experience
{
    public sealed record class CreateExperienceReportRequest(
        string Id) : IRequest<byte[]>;
}
