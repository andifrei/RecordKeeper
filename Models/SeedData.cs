using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace RecordKeeper.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RecordKeeperContext(serviceProvider.GetRequiredService<DbContextOptions<RecordKeeperContext>>()))
            {
                //look for any records
                if (context.RecordItem.Any())
                {
                    return; //DB has been seeded
                }

                context.RecordItem.AddRange
                (
                    new RecordItem
                    {
                        Artist = "Elder",
                        Album = "test",
                        Label = "lspds",
                        Description = "sdaf",
                        StoreLocation = "sdfg",
                        Type = "LP",
                        Price = 22.00M,
                        AsOf = DateTime.Parse("	11/02/2018 13:00:00"),
                        Store = "Reckless",
                        UserID = 1
                    },
                    new RecordItem
                    {
                        Artist = "Napalm Death",
                        Album = "Scum",
                        Label = "lspds",
                        Description = "asd",
                        StoreLocation = "sdf",
                        Type = "LP",
                        Price = 22.00M,
                        AsOf = DateTime.Parse("	11/02/2018 13:00:00"),
                        Store = "Shuga",
                        UserID = 1
                    }
                );

                context.SaveChanges();
            }
        }
    }
}