using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using StorisOfTheLand.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoriesOfTheLand.Test
{
   
    public class Tests
    {
        private Specimen newSpecimen;
        [SetUp]
        public void Setup()
        {
           newSpecimen = new Specimen() {
                EnglishName = "Tree"
            };

        }
        [Test]
        //test invlaid upper bounds by entering 51 characters
        public void testInvalidSpecimenEnlgishNameIsLongerThan50Characters()
        {
            newSpecimen.EnglishName = new string('a',51);

            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too long must be 50 characters or less", errors[0].ErrorMessage);
        }

        [Test]
        //test valid upper bound by enetering 50 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtUpperBoundary()
        {

            newSpecimen.EnglishName = new string('a', 50);
           
            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.IsEmpty(errors);

        }
        [Test]
        //test invailid lower bound by entering 2 characters
        public void testInvalidSpecimenEnglishNameIsShoterThan3Characters()
        {
            newSpecimen.EnglishName = "aa";

            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too short must be a minimum of 3 characters", errors[0].ErrorMessage);
        }


        [Test]
        //test valid length lower bound by entering 3 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtLowerBoundary()
        {

            newSpecimen.EnglishName = "aaa";

            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.IsEmpty(errors);
        }


        [Test]
        //test if there are any non letter characters
        public void testInvalidSpecimenEnglishNameHasInvalidCharacters()
        {
            newSpecimen.EnglishName = "124@";
            
            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name should not contain numbers or special characters.", errors[0].ErrorMessage);
        }


        [Test]
        // test invalid by entering an empty string
        public void testInvalidSpecimenEnglishNameNotEntered()
        {
            newSpecimen.EnglishName = null;

            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name is required", errors[0].ErrorMessage);

        }

    }
}