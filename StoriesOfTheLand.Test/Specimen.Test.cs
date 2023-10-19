using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Test
{
    public class Tests
    {
        private Specimen specimen;


        [SetUp]
        public void Setup()
        {
            specimen = new Specimen() { CulturalSignificance= "This is the cultural significance for the sage specimen" };
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
            // Adds a cultural significance which is valid
            specimen.CulturalSignificance = "This is the cultural significance for the sage specimen";

            var errors = ValidationHelper.Validate(specimen);
            Assert.IsEmpty(errors); // Tests that there are no errors
        }

        [Test]
        public void testThatCulturalSignificanceMustBeSet()
        {
            // Sets cultural significance to null
            specimen.CulturalSignificance = null;

            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(1, errors.Count); // Tests that there is only one error
            Assert.AreEqual("Cultural Significance is required", errors[0].ErrorMessage); // Tests that there is a StringLength error
        }

        [Test]
        public void testThatCulturalSignificanceCannotExceed3500Characters()
        {
            // Adds a cultural significance which is string of 3501 characters
            string testString = new string('a', 3501);
            specimen.CulturalSignificance = testString;

            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(1, errors.Count); // Tests that there is only one error
            Assert.AreEqual("Cultural Significance must have between 1 and 3500 characters", errors[0].ErrorMessage); // Tests that there is a StringLength error
        }

        [Test]
        public void testThatCulturalSignificanceCanGoUpTo3500Characters()
        {
            // Adds a cultural significance which is string of 3500 characters
            string testString = new string('a', 3500);
            specimen.CulturalSignificance = testString;

            var errors = ValidationHelper.Validate(specimen);

            Assert.IsEmpty(errors); // Tests that there are no errors
        }
    }
}   