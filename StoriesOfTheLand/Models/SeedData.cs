using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Data;
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

                // Look for any movies.
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Look for any Specimens.
                //if (context.Specimen.Any())
                //{
                //    return;   // DB has been seeded
                //}

                context.Specimen.AddRange(

                        new Specimen
                        {
                            SpecimenDescription = //Blueberry
                            @"A small shrub, 10-50cm tall, growing in sandy or gravel soils. It thrives in clearings of coniferous stands of the boreal forest. 
                             This woody plant can grow in dense clusters and is characterized by its soft, lance-shaped, velvety leaves. The spring flowers are shaped like delicate white urns,
                             which develop into the petite, blue fruit, familiar to all “pickers”!",
                            LatinName = "Vaccinium myrtilloides",
                            EnglishName = "Velvet Leaf Blueberry",
                            CreeName = "Idinimin",
                            SpecimenImagePath = "blueberry.png",
                            CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Horsetail
                            @"Horsetail plants tend to favour cool, moist, forested areas. Species grow from low to the ground to 1m tall. All horsetails are characterized by jointed, grooved, 
                            hollow stems with a honeycomb like top where the spores are housed. Horsetails reproduce by spores as apposed to seed. 
                           They are ancient primitive plants dating back over 300 million years!",
                            LatinName = "Equisetum species",
                            EnglishName = "Horsetail",
                            SpecimenImagePath = "Horsetail.png",
                            CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Labrador Tea
                            @"Labrador tea is a low shrub found in bogs, swamps, and moist lowland woods in nutrient poor soil. This plant keeps its leaves all year round though they 
                            often turn brownish orange in the winter. The leaves alternate around the stem like a spiral staircase. The leaves are thick and leathery with orange fuzzy hairs 
                            on the underside. White coloured flowers sit on top of the plant.",
                            LatinName = "Ledum groenlandicum",
                            EnglishName = "Labrador Tea",
                            CreeName = "Maskêkopakwa",
                            SpecimenImagePath = "LabradorTea.png",
                            CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Lungwort
                            @"Lungwort is an erect, perennial plant, (growing from 20-80cm tall) commonly found in moist woods, and meadows. 
                            It has wide pointed leaves that alternate up the stem and pink or blue bell-shaped flowers on bowing branches
                             Leaves are covered with short hairs making them feel rough to the touch. ",
                            LatinName = "Mertensia paniculata",
                            EnglishName = "Lungwort",
                            SpecimenImagePath = "Lungwort.png",
                            CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Mint
                           @"Wild mint is found in moist soil, on shorelines, stream banks and damp clearings. It can grow from 10-60cm tall, 
                            has serrated leaves in pairs around a square stem and small, purple-pink flowers in dense whorls at the base of the leaves. Walking on or 
                            disturbing mint releases the familiar mint smell.",
                            LatinName = "Mentha arvensis",
                            EnglishName = "Wild Mint",
                            CreeName = "Amiskowihkask",
                            SpecimenImagePath = "mint.png",
                            CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."

                        }/*,
                        new Specimen
                        {
                            SpecimenDescription = //Stinging Nettle
                            "Stinging Nettle is found in moist open areas around stream/riverbanks, open low areas, thickets, and disturbed sites. It grows tall, usually 0.5m-2.0m," +
                            " with square stems and narrow, toothed leaves in pairs around the stem. Tiny inconspicuous, greenish flowers form drooping clusters at the base of the leaves. " +
                            "The plant spreads through underground stems called rhizomes. \r\nNote: Stinging Nettle has hairs on the leaves and stems that contain formic acid. " +
                            "Handling or brushing up against any part of the plant can irritate the skin, causing a burning rash that can last for days!\r\n"
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Paper Birch
                            "Full grown Paper Birch trees can vary from 15-30m in height, and they are commonly found in moist, well drained, forested sites." +
                            " The most recognizable feature of mature trees is the easily-peelable white bark with pale or dark lenticels (horizontal, aerating structures). " +
                            "Bark colour can be dark or reddish brown on younger trees and branches. The leaves have a double sawtooth pattern around the edges. "
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Plantain
                            "An introduced species common to disturbed or cultivated areas, it is found throughout most of North America. " +
                            "It grows low to the ground in a cluster of strongly veined, egg-shaped leaves. Seed capsules are arranged in spikes from the center of the leaves. "
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Wild Rose
                            "Wild Rose is found in clearings, plains, and forested areas. It is a shrub with alternate leaves that are divided into 3-7 smaller leaflets. " +
                            "The flowers are reddish pink with 5 petals. After flowering, the fruits or rosehips develop, which vary in shape from round to oval depending on the species. " +
                            "The reddish stems are covered in prickles and thorns."
                        },
                        new Specimen
                        {
                            SpecimenDescription = //Willow
                            "Willows are shrubs and trees that grow in low moist areas near or around water. They are “dioecious” meaning that there are separate male and female plants. " +
                            "Willows have leaves that are two to more than ten times longer than they are wide. All willow species produce tiny flowers in clusters called catkins; " +
                            "these soft fluffy droops are what gave rise to the name “Pussy Willow”. There are thirty-two different species of willow in Saskatchewan and differentiating " +
                            "species often requires extensive analysis from an experienced taxonomist.  "

                        },
                        new Specimen
                        {
                            SpecimenDescription = //Common Yarrow
                            "Common Yarrow is found in clearings around wooded areas, and in meadows. The leaves alternate on the stem and are divided into segments which resemble fern leaves. " +
                            "It has both erect stems that grow 10-70cm tall and horizontal, underground stems called rhizomes that allow it to spread over an area.  "
                        }*/

                    );

                context.SaveChanges();
            }



        }
    }
}