using StorisOfTheLand.Models;
using System.ComponentModel.DataAnnotations;

namespace StoriesOfTheLand.Test
{
    class ValidationHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
?
            var vc = new ValidationContext(model, null, null);
?
            Validator.TryValidateObject(model, vc, results, true);
?
            if (model is IValidatableObject) (model as IValidatableObject).Validate(vc);
?
            return results;
        }
    }

    public class Tests
    {
        

        [SetUp]
        public void Setup()
        {
            ;
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
        public void testDatabasesLatinNameIsLongerThan50Characters()
        {
            Specimen testSpecimen = new Specimen();
            //Set the latin name
            
            testSpecimen.latinName = new string('a',51);


            //Test that nothing was inserted
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.Equals("Name cannot be more than 50", errors[0].ErrorMessage);
        }

        /*
         * The database inserts exactly 51 characters into the Latin Name Field and fails,
         * resulting in an exception that is thrown from this boundary case
         */
        [Test]
        public void testDatabasesLatinNameIsExactly51Characters()
        {
            Specimen testSpecimen = new Specimen();
            //Change specimen's Latin Name
            testSpecimen.latinName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

            //Test that false is returned when specimen is unable to be added
            
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
           
        }


        /*
         * The database isnerts exactly 50 E's into the database, resulting in no 
         * errors, and data being successfully inserted.
         */
        [Test]
        public void testDatabasesLatinNameIsExactly50CharactersLong()
        {
            Specimen testSpecimen = new Specimen();
            //Change specimen's latin name

            testSpecimen.latinName = "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE";

            //Test that true is returned when specimen is able to be added
         
        }
    }
}