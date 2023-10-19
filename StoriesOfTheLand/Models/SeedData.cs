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
                    LatinName = "Plantago Major"

                },
                new Specimen
                {
                    LatinName = "Vaccinium myrtilloides"

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