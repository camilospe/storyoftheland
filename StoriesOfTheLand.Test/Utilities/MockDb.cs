using Microsoft.EntityFrameworkCore;
using StoriesOfTheLand.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StoriesOfTheLand.Test.Utilities
{
    public class MockDb : IDbContextFactory<StoriesOfTheLandContext>
    {
        public StoriesOfTheLandContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<StoriesOfTheLandContext>()
                .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
                .Options;

            return new StoriesOfTheLandContext(options);
        }
    }
}
