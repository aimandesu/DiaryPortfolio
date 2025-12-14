using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers.Filter
{
   public sealed class QuerySearchObject : QueryObject
    {
        public string? SearchTerm { get; set; }
    }
}
