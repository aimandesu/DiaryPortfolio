using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class SelectionModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Selection { get; set; } = string.Empty;
        //FK
        public Guid TypeId { get; set; }
        public TypeModel? Type { get; set; }
    }
}
