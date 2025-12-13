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

using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Faker;
using EFCore.BulkExtensions;

public static class DatabaseSeed
{
    public static void Seed(ApplicationDbContext context)
    {
        context.ChangeTracker.AutoDetectChangesEnabled = false;

        // =========================
        // USERS
        // =========================
        var users = new UserFaker().Generate(100_000);
        context.BulkInsert(users);

        // =========================
        // TEXTS
        // =========================
        var texts = new TextFaker().Generate(1);
        context.BulkInsert(texts);

        // =========================
        // SPACES
        // =========================
        var spaces = new SpaceFaker().Generate(500_000);

        for (int i = 0; i < spaces.Count; i++)
        {
            spaces[i].UserId = users[i % users.Count].Id;
        }

        context.BulkInsert(spaces);

        // =========================
        // COLLECTIONS
        // =========================
        var collections = new CollectionFaker().Generate(1_500_000);
        context.BulkInsert(collections);

        // =========================
        // MEDIAS
        // =========================
        var medias = new MediaFaker().Generate(5_000_000);

        for (int i = 0; i < medias.Count; i++)
        {
            medias[i].SpaceId = spaces[i % spaces.Count].Id;
            medias[i].CollectionId = collections[i % collections.Count].Id;
        }

        context.BulkInsert(medias, new BulkConfig
        {
            BatchSize = 50_000,
            PreserveInsertOrder = true,
            SetOutputIdentity = false
        });

        // =========================
        // LOCATIONS (1–1)
        // =========================
        var locations = new LocationFaker().Generate(20_000);

        for (int i = 0; i < locations.Count; i++)
        {
            locations[i].MediaId = medias[i].Id;
        }

        context.BulkInsert(locations);

        // =========================
        // CONDITIONS (1–1)
        // =========================
        var conditions = new ConditionFaker().Generate(2_500);

        for (int i = 0; i < conditions.Count; i++)
        {
            conditions[i].MediaId = medias[i].Id;
        }

        context.BulkInsert(conditions);

        // =========================
        // PHOTOS
        // =========================
        var photos = new PhotoFaker().Generate(300_000);
        context.BulkInsert(photos);

        context.ChangeTracker.AutoDetectChangesEnabled = true;
    }
}
