using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using StoriesOfTheLand.Data;
using Humanizer;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Extensions.FileSystemGlobbing;
using NuGet.Packaging.Signing;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace StoriesOfTheLand.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StoriesOfTheLandContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<StoriesOfTheLandContext>>()))
        {
            // Look for any specimens
            if (context.Specimen.Any())
            {
                return; // DB has been seeded
            }
            context.Specimen.AddRange(
                new StorisOfTheLand.Models.Specimen
                {
                    CulturalSignificance = "found along the shores of lakes and creeks, you’ll often smell her before you see her, once you add this beauty to your tea cabinet it’ll be a staple, " +
                    "not only tasty but abundant in nutrients and healing properties.This herb is great in teas, on its own or used with Labrador, and chaga." +
                    "Its said to help reduce pain in breast feeding, and relieve menstrual cramps. On the land as kids we’d gather old coals from the fire pit" +
                    " and mix it with mint to brush our teeth along the water. Be sure the stem is square."
                }
            );
            context.SaveChanges();
        }
    }
}
