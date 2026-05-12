using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Persistence
{
    public static class StoredProcedures
    {
        public static class Profile
        {
            public const string GetPortfolioProfile = "spGetPortfolioProfile";
            public const string UpsertPortfolioProfile = "sp_UpsertPortfolioProfile";
        }

        public static class Media
        {
            public const string GetMediaByUserId = "sp_GetMediaByUserId";
        }
    }
}
