using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs.User
{
    public class UserModelDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Title { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public LocationModel? Location { get; set; }
        public ResumeModel? Resume { get; set; }
        public PhotoModel? ProfilePhoto { get; set; }
    }
}
