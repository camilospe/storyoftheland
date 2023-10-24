using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Models;
using StoriesOfTheLand.Data;
using System;
using System.Linq;

namespace StoriesOfTheLand.Models;

public static class SeedData
{
    public class SeedData
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
                    LatinName = "Plantago Major"

                },
                new Specimen
                {
                    LatinName = "Vaccinium myrtilloides"
                        SpecimenID = 1,
                        EnglishName = "Wild Mint"

                },
                new Specimen
                {
                    LatinName = "Ledum groenlandicum"
                        SpecimenID = 2,
                        EnglishName ="Velvet Leaf Blueberry"
      
                },
                new Specimen
                {
                    LatinName = "Mertensia paniculata."
                }
            );
            context.SaveChanges();
        }
    }
}