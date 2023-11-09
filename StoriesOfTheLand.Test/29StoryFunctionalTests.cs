using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using StoriesOfTheLand.Data;
using StoriesOfTheLand;

namespace StoriesOfTheLand.Test
{
    public  class _29StoryFunctionalTests : IClassFixture<WebApplicationFactory<Program>>
    {

        protected readonly StoriesOfTheLandContext _context;
        public _29StoryFunctionalTests(StoriesOfTheLandContext context)
        {
            _context = context;
        }
        




    }
}
