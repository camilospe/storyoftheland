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
using Microsoft.AspNetCore.Http.HttpResults
using StoriesOfTheLand.Test.Utilities;
using StoriesOfTheLand.Controllers;
using StoriesOfTheLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoriesOfTheLand.Test
{
    public  class _29StoryFunctionalTests : IClassFixture<WebApplicationFactory<Program>>
    {

        protected readonly StoriesOfTheLandContext _context;
        public _29StoryFunctionalTests(StoriesOfTheLandContext context)
        {
            _context = context;
        }

        [Fact]
        public async Task GetIndexReturnsSponsors()
        {
            // Arrange
            var mockRepo = new MockDb<StoriesOfTheLandContext>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetTestSessions());

            await using var context = new MockDb().CreateDbContext();
            var controller = new HomeController(mockRepo.Object);

            context.Add(new Sponsor
            { SponsorID = 1,
              SponsorName = "Test1",
              SponsorURL = "Test1.ca"
            });

            context.Add(new Sponsor
            {
                SponsorID = 1,
                SponsorName = "Test2",
                SponsorURL = "Test3.ca"
            });

            // Act
            var result = await HomeController.Index();

            //Assert
            var viewResult = Assert.IsInstanceOf<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.AreEqual(2, model.Count());
        }





    }
}
