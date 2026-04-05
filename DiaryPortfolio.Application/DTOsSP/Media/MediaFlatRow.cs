using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOsSP.Media
{
    public class MediaFlatRow
    {
        // Media
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string MediaStatus { get; set; } = string.Empty;
        public string MediaType { get; set; } = string.Empty;

        // Space
        public Guid? Space_Id { get; set; }
        public string? Space_Title { get; set; }
        public DateTime? Space_CreatedAt { get; set; }
        public Guid? Space_UserId { get; set; }

        // Location
        public Guid? Loc_Id { get; set; }
        public string? Loc_Name { get; set; }
        public string? Loc_Latitude { get; set; }
        public string? Loc_Longitude { get; set; }
    }
}
