using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StorisOfTheLand.Models;

namespace StorisOfTheLand.Data
{
    public class StorisOfTheLandContext : DbContext
    {
        public StorisOfTheLandContext (DbContextOptions<StorisOfTheLandContext> options)
            : base(options)
        {
        }

        public DbSet<StorisOfTheLand.Models.Specimen> Specimen { get; set; } = default!;
    }
}
