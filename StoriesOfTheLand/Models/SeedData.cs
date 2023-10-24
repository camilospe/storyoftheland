using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Models;
using StoriesOfTheLand.Data;
using System;
using System.Linq;
using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new StoriesOfTheLandContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<StoriesOfTheLandContext>>()))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
/*            // Look for any Specimens.
            if (context.Specimen.Any())
            {
                return;   // DB has been seeded
            }*/
            context.Specimen.AddRange(
                new Specimen
                {
                    LatinName = "Plantago Major",
                    CulturalSignificance = "This plant is magical! Often found near nettle which is natures convenient way to rid the nettles nasty burn by using the leaves of plantain, you can use a spit poultice or simply rub the plant and its liquid on the effected area. Plantain is also very convenient in this area with our abundance of mosquitos as it will help take the itch away. Chewing a piece of the root and keeping it on the affected area can help draw out infection and reduce the discomfort of a toothache."

                },
                new Specimen
                {
                    LatinName = "Vaccinium myrtilloides",
                    CulturalSignificance = "Another powerhouse antioxidant, from the roots and stems to the berry itself, this plant is worth knowing and utilizing, mostly used for consumption, even drying them for winter use. the roots, branches and leaves are perfect for a vitamin packed delicate but delicious tea."

                },
                new Specimen
                {
                    LatinName = "Ledum groenlandicum",
                    CulturalSignificance = "Labrador tea or ‘muskeg tea – maskêkopakwa. This is an all time favourite, it can be picked year-round but easiest in the spring through summer months, most commonly used as a tea and often mixed with wild mint to add a pleasant flavour and aid with coughs, fevers, and diarrhea. Any\r\n\r\nleftover tea is a great face wash and astringent to help clear up acne and acne scarring. Another interesting use ive heard of in the north is “dusting powder” like baby powder made from grinding the leaves to prevent rashes. Imagine seeing the sea of muskeg and a field of white flowering Labrador tea as a child, how magical the spongey earth beneath you feels, its comforting, you can fall, and she catches you in her soft gentle embrace. I once heard my mother talking about my capan / great grandmother looking into the muskeg with fear and longing, and that they would go into the forest to gather in secret."

                },
                new Specimen
                {
                    LatinName = "Mertensia paniculata.",
                    CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
                }
            );
            context.SaveChanges();
        }
    }
}