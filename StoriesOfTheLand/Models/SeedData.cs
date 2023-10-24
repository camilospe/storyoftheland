using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Models;
using StoriesOfTheLand.Data;
using System;
using System.Linq;
using StorisOfTheLand.Models;

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
                    LatinName = "Plantago Major"

                },
                new Specimen
                {
                    LatinName = "Vaccinium myrtilloides"
                        SpecimenID = 1,
                        SpecimenImagePath = "mint.png"
                    }
                    ) ;
                context.SaveChanges();
            }

                },
                new Specimen
                {
                    LatinName = "Ledum groenlandicum"
      
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