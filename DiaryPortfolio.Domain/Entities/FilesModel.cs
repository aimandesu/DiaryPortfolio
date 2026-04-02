using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class FilesModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //type
        public Guid SelectionId { get; set; }
        public SelectionModel? Selection { get; set; }
    }
}
