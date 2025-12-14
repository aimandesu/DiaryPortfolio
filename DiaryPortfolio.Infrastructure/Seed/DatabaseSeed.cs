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

using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Faker;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public static class DatabaseSeed
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.SetCommandTimeout(3600); // 1 hour timeout

        try
        {
            // Disable tracking for better performance
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Console.WriteLine("Starting seed process...");

            // =========================
            // USERS (100k)
            // =========================
            Console.WriteLine("Seeding Users...");
            var users = new UserFaker().Generate(10_000);
            context.BulkInsert(users, new BulkConfig
            {
                SetOutputIdentity = true,
                BatchSize = 10_000
            });
            Console.WriteLine($"✓ Inserted {users.Count:N0} users");

            // =========================
            // TEXTS (1)
            // =========================
            // Create 10 text style presets
            var texts = new TextFaker().Generate(10);
            context.BulkInsert(texts, new BulkConfig { SetOutputIdentity = true });
            var textIds = texts.Select(t => t.Id).ToList();

            // Each media picks one of the 10 styles

            // =========================
            // SPACES (500k)
            // =========================
            Console.WriteLine("Seeding Spaces...");
            var spaceBatchSize = 50_000;
            var totalSpaces = 50_000;

            for (int batch = 0; batch < totalSpaces / spaceBatchSize; batch++)
            {
                var spaces = new SpaceFaker().Generate(spaceBatchSize);

                for (int i = 0; i < spaces.Count; i++)
                {
                    spaces[i].UserId = users[(batch * spaceBatchSize + i) % users.Count].Id;
                }

                context.BulkInsert(spaces, new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 10_000
                });

                Console.WriteLine($"  Batch {batch + 1}/{totalSpaces / spaceBatchSize}: {(batch + 1) * spaceBatchSize:N0} spaces inserted");
            }

            // Retrieve all space IDs for media assignment
            var spaceIds = context.Set<SpaceModel>().Select(s => s.Id).ToList();
            Console.WriteLine($"✓ Total spaces: {spaceIds.Count:N0}");

            // =========================
            // COLLECTIONS (1.5M)
            // =========================
            Console.WriteLine("Seeding Collections...");
            var collectionBatchSize = 100_000;
            var totalCollections = 50_000;

            for (int batch = 0; batch < totalCollections / collectionBatchSize; batch++)
            {
                var collections = new CollectionFaker().Generate(collectionBatchSize);

                context.BulkInsert(collections, new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 20_000
                });

                Console.WriteLine($"  Batch {batch + 1}/{totalCollections / collectionBatchSize}: {(batch + 1) * collectionBatchSize:N0} collections inserted");
            }

            var collectionIds = context.Set<CollectionModel>().Select(c => c.Id).ToList();
            Console.WriteLine($"✓ Total collections: {collectionIds.Count:N0}");

            // =========================
            // MEDIAS (5M) - CRITICAL: Process in smaller batches
            // =========================
            Console.WriteLine("Seeding Medias (this will take a while)...");
            var mediaBatchSize = 25_000; // Smaller batches for large dataset
            var totalMedias = 50_000;
            var random = new Random();

            for (int batch = 0; batch < totalMedias / mediaBatchSize; batch++)
            {
                var medias = new MediaFaker().Generate(mediaBatchSize);

                // Assign foreign keys
                for (int i = 0; i < medias.Count; i++)
                {
                    medias[i].SpaceId = spaceIds[random.Next(spaceIds.Count)];
                    medias[i].CollectionId = collectionIds[random.Next(collectionIds.Count)];
                    medias[i].TextId = textIds[random.Next(textIds.Count)];
                }

                context.BulkInsert(medias, new BulkConfig
                {
                    SetOutputIdentity = true,
                    BatchSize = 5_000,
                    PreserveInsertOrder = false // Faster without order preservation
                });

                // Progress update every 10 batches
                if ((batch + 1) % 10 == 0 || batch == 0)
                {
                    var progress = (double)(batch + 1) / (totalMedias / mediaBatchSize) * 100;
                    Console.WriteLine($"  Progress: {progress:F1}% - {(batch + 1) * mediaBatchSize:N0}/{totalMedias:N0} medias inserted");
                }
            }

            var mediaIds = context.Set<MediaModel>().Select(m => m.Id).ToList();
            Console.WriteLine($"✓ Total medias: {mediaIds.Count:N0}");

            // =========================
            // LOCATIONS (20k)
            // =========================
            Console.WriteLine("Seeding Locations...");
            var locations = new LocationFaker().Generate(20_000);

            for (int i = 0; i < locations.Count; i++)
            {
                locations[i].MediaId = mediaIds[i]; // First 20k medias
            }

            context.BulkInsert(locations, new BulkConfig { BatchSize = 5_000 });
            Console.WriteLine($"✓ Inserted {locations.Count:N0} locations");

            // =========================
            // CONDITIONS (2.5k)
            // =========================
            Console.WriteLine("Seeding Conditions...");
            var conditions = new ConditionFaker().Generate(2_500);

            for (int i = 0; i < conditions.Count; i++)
            {
                conditions[i].MediaId = mediaIds[i]; // First 2.5k medias
            }

            context.BulkInsert(conditions, new BulkConfig { BatchSize = 1_000 });
            Console.WriteLine($"✓ Inserted {conditions.Count:N0} conditions");

            // =========================
            // PHOTOS (300k)
            // =========================
            Console.WriteLine("Seeding Photos...");
            var photoBatchSize = 50_000;
            var totalPhotos = 30_000;

            for (int batch = 0; batch < totalPhotos / photoBatchSize; batch++)
            {
                var photos = new PhotoFaker().Generate(photoBatchSize);
                context.BulkInsert(photos, new BulkConfig { BatchSize = 10_000 });
                Console.WriteLine($"  Batch {batch + 1}/{totalPhotos / photoBatchSize}: {(batch + 1) * photoBatchSize:N0} photos inserted");
            }

            context.ChangeTracker.AutoDetectChangesEnabled = true;
            Console.WriteLine("✅ Seed completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Seed failed: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}
