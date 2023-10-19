using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                if(context.Specimen.Any())
                {
                    //context.Specimen.ExecuteDelete();
                    return;
                }
                context.Specimen.AddRange(
                    new StoriesOfTheLand.Models.Specimen
                    {
                        SpecimenID = 1,
                        SpecimenImagePath = "mint.png"
                    }
                    ) ;
                context.SaveChanges();
            }


        }

    }
}
