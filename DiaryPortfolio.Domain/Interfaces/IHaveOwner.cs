using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Interfaces
{
    public interface IHaveOwner
    {
        Guid OwnerId { get; }
    }
}
