using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    internal class ResumeRepository : IResumeRepository
    {
        private readonly IPortfolioProfileRepository _portfolioProfileRepository;
        private readonly IRazorViewRenderer _razorRenderer;
        private readonly IPdfGeneratorService _pdfGenerator;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ISelectionHelper _selectionHelper;

        public ResumeRepository(
            IPortfolioProfileRepository portfolioProfileRepository,
            IRazorViewRenderer razorRenderer,
            IPdfGeneratorService pdfGenerator,
            ApplicationDbContext context,
            IUserService userService,
            ISelectionHelper selectionHelper)
        {
            _portfolioProfileRepository = portfolioProfileRepository;
            _razorRenderer = razorRenderer;
            _pdfGenerator = pdfGenerator;
            _context = context;
            _userService = userService;
            _selectionHelper = selectionHelper;
        }

        public async Task<ResultResponse<ResumeModel>> DeleteResume(string resumeId)
        {

            var resume = await _context.Resume
                .Include(f => f.ResumeFile)
                .FirstOrDefaultAsync(r => r.Id == new Guid(resumeId));

            var response = new ResumeModel
            {
                Id = new Guid(resumeId),
                ResumeFile = resume?.ResumeFile
            };

            if (resume == null)
                return ResultResponse<ResumeModel>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.NotFound,
                        "No Resume with the resume ID provided found"));

            if (resume.ResumeFile != null)
                _context.Files.Remove(resume.ResumeFile);

            var profile = await _context.PortfolioProfile
                .FirstOrDefaultAsync(p => p.UserId == _userService.UserId);

            //profile.ResumeId = null;

            _context.Resume.Remove(resume);

            return ResultResponse<ResumeModel>.Success(response);
        }

        public async Task<ResultResponse<List<ResumeTemplateModel>>> GetResumeTemplates()
        {
            var response = await _context.ResumeTemplate
                .ToListAsync();
            
            return ResultResponse<List<ResumeTemplateModel>>.Success(response);
        }

        public async Task<ResultResponse<ResumeModel?>> GetResume()
        {
            var profile =  await _context.PortfolioProfile
                .Include(r => r.Resume)
                .FirstOrDefaultAsync(p => p.UserId == _userService.UserId);
            var response = profile?.Resume;

            return ResultResponse<ResumeModel?>.Success(response);
        }

        public async Task<byte[]> GenerateResumeReport(
            string userId, 
            string templateId)
        {
            var response = await _portfolioProfileRepository.GenerateResume(userId);

            var photo = response?.Result?.User?.ProfilePhoto;

            if (photo != null && !string.IsNullOrEmpty(photo.Url))
            {
                photo.Url = await _razorRenderer.RenderFileToBase64ImageAsync(photo.Url);
            }

            var selectedResume = await _context.ResumeTemplate
                .Where(e => e.Id == new Guid(templateId))
                .FirstOrDefaultAsync();
            
            var html = await _razorRenderer.RenderViewToStringAsync($"Resume/Templates/{selectedResume?.Url}", response.Result);

            return await _pdfGenerator.GenerateFromHtmlAsync(html);
        }

        public async Task<ResultResponse<ResumeModel>> UploadResume(
            string templateId, FileModel? file)
        {
            var typeSelection = await _selectionHelper.GetSelectionIdAsync(FilesEnum.Resume);
            
            var profile =  await _context.PortfolioProfile
                .Include(r => r.Resume)
                .FirstOrDefaultAsync(p => p.UserId == _userService.UserId);

            if (profile.Resume != null)
            {
                _context.Resume.Remove(profile.Resume);
            }

            var resumeFile = new FileModel
            {
                Url = file?.Url ?? "",
                SelectionId = typeSelection,
            };
            
            var selectedResume = await _context.ResumeTemplate
                .Where(e => e.Id == new Guid(templateId))
                .FirstOrDefaultAsync();

            var resume = new ResumeModel
            {
                PortfolioProfileId = _userService.PortfolioProfileId ?? Guid.Empty,
                TemplateId = new Guid(templateId),
                ResumeFile = resumeFile,
                ResumeTemplate = templateId == "" ? null : selectedResume
                //ni we dont have bcs we need to do
                //like query with template id well to get this 
            };

            _context.Resume.Add(resume);

            return ResultResponse<ResumeModel>.Success(resume);
        }
    }
}
