using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using StoriesOfTheLand.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoriesOfTheLand.Test
{
   
    public class Tests
    {
        private Specimen testSpecimen;
        [SetUp]
        public void Setup()
        {
            testSpecimen = new Specimen() {
                EnglishName = "Tree",
                LatinName = "Valid Name"
            };
        }
        [Test]
        //test invlaid upper bounds by entering 51 characters
        public void testInvalidSpecimenEnlgishNameIsLongerThan50Characters()
        {
            testSpecimen.EnglishName = new string('a',51);

            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too long must be 50 characters or less", errors[0].ErrorMessage);
        }

        [Test]
        //test valid upper bound by enetering 50 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtUpperBoundary()
        {

            testSpecimen.EnglishName = new string('a', 50);
           
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.IsEmpty(errors);

        }
        [Test]
        //test invailid lower bound by entering 2 characters
        public void testInvalidSpecimenEnglishNameIsShoterThan3Characters()
        {
            testSpecimen.EnglishName = "aa";

            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too short must be a minimum of 3 characters", errors[0].ErrorMessage);
        }


        [Test]
        //test valid length lower bound by entering 3 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtLowerBoundary()
        {

            testSpecimen.EnglishName = "aaa";

            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.IsEmpty(errors);
        }


        [Test]
        //test if there are any non letter characters
        public void testInvalidSpecimenEnglishNameHasInvalidCharacters()
        {
            testSpecimen.EnglishName = "124@";
            
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name should not contain numbers or special characters.", errors[0].ErrorMessage);
        }

        


        [Test]
        // test invalid by entering an empty string
        public void testInvalidSpecimenEnglishNameNotEntered()
        {
            testSpecimen.EnglishName = null;

            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name is required", errors[0].ErrorMessage);
        }






        //Ethans tests

        [Test]
        public void testLatinNameIsLongerThan50Characters()
        {
            

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
    }
}