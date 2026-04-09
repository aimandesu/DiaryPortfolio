using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class DiaryProfileModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        //FK
        public Guid UserId { get; set; }  // PK + FK
        public UserModel User { get; set; } = null!;
        public List<SpaceModel> SpaceModels { get; set; } = [];
    }
}
