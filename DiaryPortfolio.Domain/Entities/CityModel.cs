using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class CityModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        
        public Guid StateId { get; set; }
        public StateModel? State { get; set; }

        public ICollection<PostalCodeModel> PostalCodes { get; set; } = [];
    }
}
