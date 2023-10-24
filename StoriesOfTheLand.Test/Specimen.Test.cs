using StorisOfTheLand.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace StoriesOfTheLand.Test
{


    public class Tests
    {
        Specimen testSpecimen;
        [SetUp]
        public void Setup()
        {
            testSpecimen = new Specimen() { 
                LatinName = "Valid Name",
                CulturalSignificance = "This is the cultural significance for the sage specimen"
            };
            
        }

        /*
         * The database inserts 70 letter A's into the Latin Name Field and fails,
         * resulting in an exception that is thrown
         */
        [Test]
        public void testLatinNameIsLongerThan50Characters()
        {
            Specimen testSpecimen = new Specimen();
            
            //Set the latin name
            testSpecimen.LatinName = new string('A', 100);


            //Test that nothing was inserted
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual("Name cannot be more than 50 characters", errors[0].ErrorMessage);
        }

        /*
         * The database inserts exactly 51 characters into the Latin Name Field and fails,
         * resulting in an exception that is thrown from this boundary case
         */
        [Test]
        public void testLatinNameIsExactly51Characters()
        {
            Specimen testSpecimen = new Specimen();
            //Change specimen's Latin Name
            testSpecimen.LatinName = new string('A', 51);

            //Test that false is returned when specimen is unable to be added
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual("Name cannot be more than 50 characters", errors[0].ErrorMessage);
        }

        /*
         * The database isnerts the Latin Name "Begonia" into the database, resulting in no
         * errors, and data being successfully inserted. 
         */
        [Test]
        public void testLatinNameIsACorrectLength()
        {
            Specimen testSpecimen = new Specimen();
            //Change specimen's latin name
            testSpecimen.LatinName = "Begonia";

            //Test that true is returned when specimen is able to be added
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.IsEmpty(errors);
        }


        /*
         * The database isnerts exactly 50 E's into the database, resulting in no 
         * errors, and data being successfully inserted.
         */
        [Test]
        public void testLatinNameIsExactly50CharactersLong()
        {
            Specimen testSpecimen = new Specimen();
            //Change specimen's latin name

            testSpecimen.LatinName = new string('E', 50);

            //Test that true is returned when specimen is able to be added
            var errors = ValidationHelper.Validate(testSpecimen);
            //There should be nothing passed into .ErrorMessage and the result should be null
            Assert.IsEmpty(errors);
        }

        [Test]
        public void testLatinNameDoesNotExist()
        {
            Specimen testSpecimen = new Specimen();
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual("Latin Name is required", errors[0].ErrorMessage);
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
            testSpecimen.CulturalSignificance = testString;

            var errors = ValidationHelper.Validate(specimen);

            Assert.IsEmpty(errors); // Tests that there are no errors
        }
    }
}   