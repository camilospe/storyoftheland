using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Test
{
    public class Tests
    {


        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        // Cultural Significance Tests

        [Test]
        public void testThatCulturalSignificanceCanBeCorrectlyRetrieved()
        {
            // Test will create a new Specimen object with the name "Sage" and a cultural significance
            // It will then add the Specimen to the test SpecimenController

            Specimen testSpecimen = new Specimen();
            testSpecimen.name = "Sage";
            testSpecimen.cultural_significance = "This is the cultural significance for the sage specimen";
            testSpecController.addSpecimen(testSpecimen);

            // Test will then retrieve the Specimen with the name "Sage" from the SpecimenController
            // It will assert that the Specimen exists, it's name is "Sage", and it's Cultural Significance matches what was input 

            Specimen readSpecimen = testSpecController.getSpecimen("Sage");
            Assert.IsNotNull(readSpecimen);
            Assert.AreEqual(readSpecimen.name, "Sage");

            Assert.AreEqual(readSpecimen.cultural_significance, "This is the cultural significance for the sage specimen");

            // Result: Returns the Sage specimen's cultural significance 

        }

        [Test]
        public void testThatCulturalSignificanceDisplaysAMessageIfNothingIsEntered()
        {
            // Creates a new Specimen object with the name "Grass" and no cultural significance
            // Adds the Specimen to the test SpecimenController

            Specimen testSpecimen = new Specimen();
            testSpecimen.name = "Grass";
            testSpecController.addSpecimen(testSpecimen);

            // Test will then retrieve the Specimen with the name "Grass:
            // It will assert that the Specimen exists, it's name is grass, and it's cultural significance matches the default error message

            Specimen readSpecimen = testSpecController.getSpecimen("Grass");
            Assert.IsNotNull(readSpecimen);
            Assert.AreEqual(readSpecimen.name, "Grass");

            Assert.AreEqual(readSpecimen.cultural_significance, "Error: Cultural Significance has not been entered for this specimen");

            // Result: Returns an error message reading "Cultural Significance has not been entered for this specimen". 
        }

        [Test]
        public void testThatCulturalSignificanceDoesNotAllowInvalidInputs()
        {
            // Creates a new Specimen object named "Lavender" and a cultural significance that is longer than the allowed character limit
            // Adds it to the SpecimenController

            Specimen testSpecimen = new Specimen();
            testSpecimen.name = "Lavender";

            String sCult = "a";
            for (int i = 0; i < 3600; i++)
            {
                sCult += "a";
            }
            testSpecimen.cultural_significance = sCult;
            testSpecController.addSpecimen(testSpecimen);

            // Retrieves the specimen with the name "Lavender"
            // Asserts that it exists, has the right name, and matches the error message for too many characters

            Specimen readSpecimen = testSpecController.getSpecimen("Lavender");
            Assert.IsNotNull(readSpecimen);
            Assert.AreEqual(readSpecimen.name, "Lavender");

            Assert.AreEqual(readSpecimen.cultural_significance, "Error: Cultural Significance exceeds allowed character limit (3500 Characters)");

            // Result: Throws an exception reading "Cultural Significance exceeds allowed character limit (3500 Characters)"
        }
    }
}