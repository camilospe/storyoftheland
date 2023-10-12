using StorisOfTheLand.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace StoriesOfTheLand.Test
{

    class ValidationHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var vc = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, vc, results, true);

            if (model is IValidatableObject) (model as IValidatableObject).Validate(vc);

            return results;
        }
    }
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

        /*
         * The database inserts 70 letter A's into the Latin Name Field and fails,
         * resulting in an exception that is thrown
         */
        [Test]
        public void testLatinNameIsLongerThan50Characters()
        {
            Specimen testSpecimen = new Specimen();
            
            //Set the latin name
            testSpecimen.latinName = new string('A', 100);


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
            testSpecimen.latinName = new string('A', 51);

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
            testSpecimen.latinName = "Begonia";

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

            testSpecimen.latinName = new string('E', 50);

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
    }
}