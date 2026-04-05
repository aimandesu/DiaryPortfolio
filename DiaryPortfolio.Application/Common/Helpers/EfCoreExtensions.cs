using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Common.Helpers
{
    public static class EfCoreExtensions
    {
        public static void SyncMediaCollection<TJunction, TMedia>(
        this DbContext context,  // <-- extension method
        ICollection<TJunction> existingJunctions,
        List<TMedia> incomingMedia,
        Func<TJunction, string?> getExistingUrl,
        Func<TJunction, TMedia?> getMediaFromJunction,
        Func<TMedia, string?> getIncomingUrl,
        Func<TMedia, TJunction> createJunction,
        DbSet<TMedia> dbSet
    ) where TJunction : class where TMedia : class
        {
            var newUrls = incomingMedia.Select(getIncomingUrl).ToHashSet();
            var existingUrls = existingJunctions.Select(getExistingUrl).ToHashSet();

            var toDelete = existingJunctions
                .Where(j => !newUrls.Contains(getExistingUrl(j)))
                .ToList();

            dbSet.RemoveRange(toDelete.Select(getMediaFromJunction).OfType<TMedia>());
            toDelete.ForEach(j => existingJunctions.Remove(j));

            incomingMedia
                .Where(m => !existingUrls.Contains(getIncomingUrl(m)))
                .Select(createJunction)
                .ToList()
                .ForEach(j => existingJunctions.Add(j));
        }
    }
}
