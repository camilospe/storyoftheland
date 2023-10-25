using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Data;
using NUnit.Framework;
using StoriesOfTheLand.Models;
using Humanizer;
using static System.Net.Mime.MediaTypeNames;

namespace StoriesOfTheLand.Test
{
    public class DatabaseFixture
    {
        // The service provider which can resolve dependencies
        protected ServiceProvider ServiceProvider { get; private set; }

        public DatabaseFixture()
        {
            // Setup Dependency Injection
            var serviceCollection = new ServiceCollection();

            // Add in-memory database support
            serviceCollection.AddDbContext<StoriesOfTheLandContext>(options => options.UseInMemoryDatabase("TestDb"), ServiceLifetime.Transient);

            // Build the service provider
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Populate the database with test data
            PopulateTestData();
        }

        private void PopulateTestData()
        {
            using var scope = ServiceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StoriesOfTheLandContext>();

            if (!dbContext.Specimen.Any()) // check if data already exists to avoid duplicate entries
            {
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "alder nipisi" });
                dbContext.Specimen.Add(new Specimen { EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "okiniyak" });
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "wapismooniawoosit" });
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "okiniyak" });
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "waskway(ak)" });
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "maskêkopakwa" });
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "Amiskowihkask" });
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "asam" });
                dbContext.Specimen.Add(new Specimen {EnglishName = "ABDCD", LatinName = "aaaaaa", SpecimenDescription = "aaaaaaaaaaa", CreeName = "idinimin" });
                dbContext.SaveChanges();
            }
        }
        
        [OneTimeTearDown]
        public void Cleanup()
        {
            // Cleanup resources, if necessary
            ServiceProvider.Dispose();
        }
    }
}
