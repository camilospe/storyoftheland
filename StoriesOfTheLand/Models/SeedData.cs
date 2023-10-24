using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Models;
using StoriesOfTheLand.Data;
using System;
using System.Linq;
using StoriesOfTheLand.Models;

namespace StoriesOfTheLand.Models;

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
            /*            // Look for any Specimens.
                        if (context.Specimen.Any())
                        {
                            return;   // DB has been seeded
                        }*/
            context.Specimen.AddRange(
                new Specimen
                {
                    LatinName = "Plantago Major",
                    SpecimenImagePath = "mint.png"
                },
                new Specimen
                {
                    LatinName = "Vaccinium myrtilloides",
                    SpecimenImagePath = "blueberry.png"
                },
                new Specimen
                {
                    LatinName = "Ledum groenlandicum",
                    SpecimenImagePath = "LabradorTea.png"
                },
                new Specimen
                {
                    LatinName = "Mertensia paniculata.",
                    SpecimenImagePath = "mint.png"
                }
            );
            context.SaveChanges();
        }
    }
}