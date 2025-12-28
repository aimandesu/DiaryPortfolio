// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using DiaryPortfolio.Infrastructure.Faker;
// using DiaryPortfolio.Infrastructure.Data;

// namespace DiaryPortfolio.Infrastructure.Seed
// {
//     public static class DatabaseSeed
//     {
//         public static void Seed(ApplicationDbContext context)
//         {
//             var users = new UserFaker().Generate(100000);
//             context.Users.AddRange(users);
//             context.SaveChanges();

//             var texts = new TextFaker().Generate(1);
//             context.TextStyle.AddRange(texts);
//             context.SaveChanges();


//             var spaces = new SpaceFaker().Generate(500000);
//             foreach (var space in spaces)
//             {
//                 space.UserId = users[Random.Shared.Next(users.Count)].Id;
//             }

//             var collections = new CollectionFaker().Generate(1500000);
//             context.Collections.AddRange(collections);
//             context.SaveChanges();

//             var medias = new MediaFaker().Generate(5000000);

//             foreach (var media in medias)
//             {
//                 media.SpaceId = spaces[Random.Shared.Next(spaces.Count)].Id;
//                 media.CollectionId = collections[Random.Shared.Next(collections.Count)].Id;
//             }

//             context.Medias.AddRange(medias);
//             context.SaveChanges();


//             var locations = new LocationFaker().Generate(20000);
//             foreach (var location in locations)
//             {
//                 location.MediaId = medias[Random.Shared.Next(medias.Count)].Id;
//             }

//             context.Locations.AddRange(locations);
//             context.SaveChanges();


//             var conditions = new ConditionFaker().Generate(2500);

//             foreach (var condition in conditions)
//             {
//                 condition.MediaId = medias[Random.Shared.Next(medias.Count)].Id;
//             }
//             context.Conditions.AddRange(conditions);
//             context.SaveChanges();


//             var photos = new PhotoFaker().Generate(300000);
//             context.Photos.AddRange(photos);
//             context.SaveChanges();


//         }

//     }
// }

using Bogus;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Faker;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public static class DatabaseSeed
{
    public static void Seed(ApplicationDbContext context)
    {
        // increase timeout for long-running bulk operations
        context.Database.SetCommandTimeout(3600); // 1 hour

        // keep original behavior safe: remember and restore QueryTrackingBehavior
        var originalQueryTracking = context.ChangeTracker.QueryTrackingBehavior;
        var originalAutoDetect = context.ChangeTracker.AutoDetectChangesEnabled;

        try
        {
            Console.WriteLine("Starting seed process...");

            // Disable tracking for better performance during inserts
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            // -----------------------
            // Configurable counts
            // -----------------------
            var usersTotal = 10_000;
            var usersBatch = 10_000;

            var textTotal = 10;

            var spacesTotal = 50_000;      // adjust to desired total (e.g. 500_000)
            var spacesBatch = 50_000;

            var collectionsTotal = 50_000; // adjust (e.g. 1_500_000)
            var collectionsBatch = 100_000;

            var mediasTotal = 50_000;      // adjust (e.g. 5_000_000)
            var mediasBatch = 25_000;

            var locationsTotal = 20_000;
            var conditionsTotal = 2_500;

            var photosTotal = 30_000;
            var photosBatch = 50_000;

            var random = new Random();

            // =========================
            // USERS
            // =========================
            Console.WriteLine("Seeding Users...");
            var users = new List<UserModel>(usersTotal);
            for (int offset = 0; offset < usersTotal; offset += usersBatch)
            {
                var take = Math.Min(usersBatch, usersTotal - offset);
                var batch = new UserFaker().Generate(take);
                users.AddRange(batch);
                context.BulkInsert(batch, new BulkConfig
                {
                    SetOutputIdentity = false,
                    BatchSize = Math.Min(10_000, take)
                });
                Console.WriteLine($"  Inserted {users.Count:N0} / {usersTotal:N0} users");
            }

            // =========================
            // TEXTS
            // =========================
            Console.WriteLine("Seeding Text styles...");
            var texts = new TextFaker().Generate(textTotal);
            context.BulkInsert(texts, new BulkConfig { SetOutputIdentity = false });
            var textIds = texts.Select(t => t.Id).ToList();

            // =========================
            // SPACES
            // =========================
            Console.WriteLine("Seeding Spaces...");
            var insertedSpaces = 0;
            for (int offset = 0; offset < spacesTotal; offset += spacesBatch)
            {
                var take = Math.Min(spacesBatch, spacesTotal - offset);
                var batch = new SpaceFaker(offset).Generate(take);
                for (int i = 0; i < batch.Count; i++)
                {
                    // Assign user by round-robin to ensure valid FK
                    batch[i].UserId = users[(insertedSpaces + i) % users.Count].Id;
                }

                context.BulkInsert(batch, new BulkConfig
                {
                    SetOutputIdentity = false,
                    BatchSize = Math.Min(10_000, take)
                });

                insertedSpaces += batch.Count;
                Console.WriteLine($"  Inserted {insertedSpaces:N0} / {spacesTotal:N0} spaces");
            }

            // Get space ids from DB (no tracking)
            var spaceIds = context.Set<SpaceModel>()
                .AsNoTracking()
                .Select(s => s.Id)
                .ToList();

            Console.WriteLine($"✓ Total spaces in DB: {spaceIds.Count:N0}");

            // =========================
            // COLLECTIONS
            // =========================
            Console.WriteLine("Seeding Collections...");
            var insertedCollections = 0;
            for (int offset = 0; offset < collectionsTotal; offset += collectionsBatch)
            {
                var take = Math.Min(collectionsBatch, collectionsTotal - offset);
                var batch = new CollectionFaker().Generate(take);
                context.BulkInsert(batch, new BulkConfig
                {
                    SetOutputIdentity = false,
                    BatchSize = Math.Min(20_000, take)
                });

                insertedCollections += batch.Count;
                Console.WriteLine($"  Inserted {insertedCollections:N0} / {collectionsTotal:N0} collections");
            }

            var collectionIds = context.Set<CollectionModel>().AsNoTracking().Select(c => c.Id).ToList();
            Console.WriteLine($"✓ Total collections in DB: {collectionIds.Count:N0}");

            // =========================
            // MEDIAS
            // =========================
            Console.WriteLine("Seeding Medias...");
            var insertedMedias = 0;
            var mediaFaker = new MediaFaker();

            for (int offset = 0; offset < mediasTotal; offset += mediasBatch)
            {
                var take = Math.Min(mediasBatch, mediasTotal - offset);
                var batch = mediaFaker.Generate(take);

                for (int i = 0; i < batch.Count; i++)
                {
                    batch[i].SpaceId = spaceIds[random.Next(spaceIds.Count)];
                    batch[i].CollectionId = collectionIds[random.Next(collectionIds.Count)];
                    batch[i].TextId = textIds[random.Next(textIds.Count)];
                }

                context.BulkInsert(batch, new BulkConfig
                {
                    SetOutputIdentity = false,
                    BatchSize = Math.Min(5_000, take),
                    PreserveInsertOrder = false
                });

                insertedMedias += batch.Count;
                if ((insertedMedias % (mediasBatch * 1)) == 0 || insertedMedias == mediasTotal)
                    Console.WriteLine($"  Inserted {insertedMedias:N0} / {mediasTotal:N0} medias");
            }

            // retrieve all media ids for dependent 1:1 relations
            var mediaIds = context.Set<MediaModel>().AsNoTracking().Select(m => m.Id).ToList();
            Console.WriteLine($"✓ Total medias in DB: {mediaIds.Count:N0}");

            // =========================
            // LOCATIONS (idempotent)
            // =========================
            Console.WriteLine("Seeding Locations...");
            // find media ids that do NOT already have a Location
            var existingLocationMediaIds = context.Set<LocationModel>().AsNoTracking().Select(l => l.MediaId).ToHashSet();
            var availableForLocation = mediaIds.Where(id => !existingLocationMediaIds.Contains(id)).ToList();

            if (availableForLocation.Count == 0)
            {
                Console.WriteLine("  No media available for new locations (all medias already have a location).");
            }
            else
            {
                var toInsertCount = Math.Min(locationsTotal, availableForLocation.Count);
                var locations = new LocationFaker().Generate(toInsertCount);

                // assign unique MediaId values from available set
                for (int i = 0; i < locations.Count; i++)
                {
                    locations[i].MediaId = availableForLocation[i];
                }

                context.BulkInsert(locations, new BulkConfig { BatchSize = 5_000 });
                Console.WriteLine($"✓ Inserted {locations.Count:N0} locations (skipped existing)");
            }

            // =========================
            // CONDITIONS (idempotent)
            // =========================
            Console.WriteLine("Seeding Conditions...");
            var existingConditionMediaIds = context.Set<ConditionModel>().AsNoTracking().Select(c => c.MediaId).ToHashSet();
            var availableForCondition = mediaIds.Where(id => !existingConditionMediaIds.Contains(id)).ToList();

            if (availableForCondition.Count == 0)
            {
                Console.WriteLine("  No media available for new conditions (all medias already have a condition).");
            }
            else
            {
                var toInsertConditions = Math.Min(conditionsTotal, availableForCondition.Count);
                var conditions = new ConditionFaker().Generate(toInsertConditions);

                for (int i = 0; i < conditions.Count; i++)
                {
                    conditions[i].MediaId = availableForCondition[i];
                }

                context.BulkInsert(conditions, new BulkConfig { BatchSize = 1_000 });
                Console.WriteLine($"✓ Inserted {conditions.Count:N0} conditions (skipped existing)");
            }

            // =========================
            // PHOTOS
            // =========================
            Console.WriteLine("Seeding Photos...");
            var insertedPhotos = 0;
            for (int offset = 0; offset < photosTotal && offset < mediaIds.Count; offset += photosBatch)
            {
                var take = Math.Min(photosBatch, photosTotal - offset);
                var batch = new PhotoFaker().Generate(take);

                // assign MediaId for each photo; cycle through mediaIds
                for (int i = 0; i < batch.Count; i++)
                    batch[i].MediaId = mediaIds[(offset + i) % mediaIds.Count];

                context.BulkInsert(batch, new BulkConfig { BatchSize = Math.Min(10_000, take) });
                insertedPhotos += batch.Count;
                Console.WriteLine($"  Inserted {insertedPhotos:N0} / {photosTotal:N0} photos");
            }

            Console.WriteLine("✅ Seed completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Seed failed: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
        finally
        {
            // restore tracking behavior to original values
            context.ChangeTracker.AutoDetectChangesEnabled = originalAutoDetect;
            context.ChangeTracker.QueryTrackingBehavior = originalQueryTracking;
        }
    }
}
