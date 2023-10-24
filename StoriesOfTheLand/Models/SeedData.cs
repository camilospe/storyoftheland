using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Models;
using StoriesOfTheLand.Data;
using System;
using System.Linq;

namespace StoriesOfTheLand.Models
{
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StoriesOfTheLandContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<StoriesOfTheLandContext>>()))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            // Look for any Specimens.
            if (context.Specimen.Any())
            {
                    //context.Specimen.ExecuteDelete();
                    return;
            }
            context.Specimen.AddRange(
                new Specimen
                {
                        LatinName = "Vaccinium myrtilloides",
                        SpecimenID = 1,
                        SpecimenImagePath = "mint.png"
                }
                );
             context.SaveChanges();
            }

        }

    }
}