using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Data
{
    public class StoriesOfTheLandContext : DbContext
    {
        public StoriesOfTheLandContext (DbContextOptions<StoriesOfTheLandContext> options)
            : base(options)
        {
        }

        public DbSet<StorisOfTheLand.Models.Specimen> Specimen { get; set; } = default!;
    }
}
