using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using StorisOfTheLand.Models;
using Intro.Tests.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoriesOfTheLand.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Specimen newSpecimen = new Specimen();
            newSpecimen.EnlgishName = "Tree";
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        [Test]
        //test invlaid upper bounds by entering 51 characters
        public void testInvalidSpecimenEnlgishNameIsLongerThan50Characters()
        {
            string englishName = "";
            for (int i = 0; i <= 51; i++)
            {
                englishName += "a";
            }
            Specimen newSpecimen = new Specimen();
            newSpecimen.EnlgishName = englishName;
            var errors = ValidationHelper.Validate(newSpecimen);

            Assert.Equals("English Name maximum is 50 characters", errors[0].ErrorMessage);
        }

        [Test]
        //test valid upper bound by enetering 50 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtUpperBoundary()
        {
            
            string englishName = "";

            for (int i = 0; i <= 50; i++)
            {
                englishName += "a";
            }
            Specimen newSpecimen = new Specimen();
            newSpecimen.EnlgishName=englishName;
            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.IsEmpty(errors);

        }
        [Test]
        //test invailid lower bound by entering 2 characters
        public void testInvalidSpecimenEnglishNameIsShoterThan3Characters()
        {
            string englishName = "aa";

            Specimen newSpecimen = new Specimen();
            newSpecimen.EnlgishName = englishName;
            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.Equals("English Name minimum is 3 characters", errors[0].ErrorMessage);
        }


        [Test]
        //test valid length lower bound by entering 3 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtLowerBoundary()
        {

            string englishName = "aaa";

            Specimen newSpecimen = new Specimen();
            newSpecimen.EnlgishName = englishName;
            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.IsEmpty(errors);
        }


        [Test]
        //test if there are any non letter characters
        public void testInvalidSpecimenEnglishNameHasInvalidCharacters()
        {
            string englishName = "124@";
            Specimen newSpecimen = new Specimen();
            newSpecimen.EnlgishName = englishName;
            var errors = ValidationHelper.Validate(newSpecimen);

            Assert.Equals("English Name should not contain numbers or special characters.", errors[0].ErrorMessage);
        }


        [Test]
        // test invalid by entering an empty string
        public void testInvalidSpecimenEnglishNameNotEntered()
        {
            Specimen newSpecimen = new Specimen();
            var errors = ValidationHelper.Validate(newSpecimen);
            Assert.Equals("English Name is required", errors[0].ErrorMessage);

        }

    }
}