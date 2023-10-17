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

            }
        }


    }
}
