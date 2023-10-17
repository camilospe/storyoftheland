using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Models;
using StoriesOfTheLand.Data;
using System;
using System.Linq;
using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StoriesOfTheLandContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<StoriesOfTheLandContext>>()))
        {
            // Look for any Specimens.
            if (context.Specimen.Any())
            {
                return;   // DB has been seeded
            }
            context.Specimen.AddRange(
                new Specimen
                {
                    LatinName = "Latin Name 4",

                },
                new Specimen
                {
                    LatinName = "Latin Name 2 ",

                },
                new Specimen
                {
                    LatinName = "Latin Name 3",
      
                },
                new Specimen
                {
                    LatinName = "Latin Name 4",
                }
            );
            context.SaveChanges();
        }
    }
}