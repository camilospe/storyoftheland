using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Data;
using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StoriesOfTheLandContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StoriesOfTheLandContext>>()))
            {
                // Look for any movies.
                if (context.Specimen.Any())
                {
                    return;   // DB has been seeded
                }
                
                context.Specimen.AddRange(
                    new Specimen
                    {
                        SpecimenName = "The_Plantacus",
                        SpecimenDescription = "DON'T DO IT"
                    },
                    new Specimen
                    {
                        SpecimenName = "The_Other_One",
                        SpecimenDescription = "DO IT"
                    }

                );
                context.SaveChanges();
            }
        }


    }
}
