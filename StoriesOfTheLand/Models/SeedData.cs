using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Data;
using StorisOfTheLand.Models;
using System;
using System.Linq;

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
                if (context.Specimen.Any())
                {
                    return;
                }

                context.Specimen.AddRange(
                    new Specimen()
                    {
                        SpecimenID = 1,
                        EnglishName = "Wild Mint"

                    },
                    new Specimen()
                    {
                        SpecimenID = 2,
                        EnglishName ="Velvet Leaf Blueberry"
                   
                    });
                context.SaveChanges();
            }
        }
    }
}
