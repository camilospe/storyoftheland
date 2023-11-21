using StoriesOfTheLand.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Data;

namespace StoriesOfTheLand.Test
{
   
    public class Tests
    {
        private Specimen SpecimenObject;
        private string SpecimenDescriptionError = "SpecimenDescription length must be between 10 and 5000";


        [SetUp]
        public void SetUp()
        {
            //Sets up the specimen object for use in the test
            SpecimenObject = new Specimen()
            {
                EnglishName = "Tree",
                SpecimenDescription = new string('a', 11),
                LatinName = new string('a', 10),
                CreeName = "Name",
                CulturalSignificance = "Something Valid",
                
            };
        }
       
        #region EnglishName
        [Test]
        //test invlaid upper bounds by entering 51 characters
        public void testInvalidSpecimenEnglishNameIsLongerThan50Characters()
        {
            SpecimenObject.EnglishName = new string('a', 51);

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too long must be 50 characters or less", errors[0].ErrorMessage);
        }

        [Test]
        //test valid upper bound by enetering 50 characters
        public void testValidSpecimenEnglishNameIsAcceptableLenghtUpperBoundary()
        {

            SpecimenObject.EnglishName = new string('a', 50);

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(errors);

        }
        [Test]
        //test invailid lower bound by entering 2 characters
        public void testInvalidSpecimenEnglishNameIsShoterThan3Characters()
        {
            SpecimenObject.EnglishName = "aa";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too short must be a minimum of 3 characters", errors[0].ErrorMessage);
        }


        [Test]
        //test valid length lower bound by entering 3 characters
        public void testValidSpecimenEnglishNameIsAcceptableLenghtLowerBoundary()
        {

            SpecimenObject.EnglishName = "aaa";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(errors);
        }


        [Test]
        //test if there are any non letter characters
        public void testInvalidSpecimenEnglishNameHasInvalidCharacters()
        {
            SpecimenObject.EnglishName = "124@";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name should not contain numbers or special characters.", errors[0].ErrorMessage);
        }




        [Test]
        // test invalid by entering an empty string
        public void testInvalidSpecimenEnglishNameNotEntered()
        {
            SpecimenObject.EnglishName = null;

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name is required", errors[0].ErrorMessage);
        }

        #endregion

        #region Description
        /// <summary>
        /// This will test if the Specimen's Description will reject less than 10 characters
        /// </summary>
        [Test]
        public void EnteringInLessThanTheMinimumAmount()
        {
            string descriptionStringTest = new string('a', 5); //create a new string with 5 a's

            SpecimenObject.SpecimenDescription = descriptionStringTest; //set the SpecimenObject's Description field to that value
            var errors = ValidationHelper.Validate(SpecimenObject); //use the ValidationHelper class to see if there is errors

            Assert.AreEqual(errors.Count, 1); //Check to see if something is in the errors variable
            Assert.AreEqual(SpecimenDescriptionError, errors[0].ErrorMessage); //check to see if the ErrorMessage is correct 

        }

        /// <summary>
        /// This will test if the Specimen's Description will reject less than 10 characters
        /// </summary>
        [Test]
        public void EnteringInJustUnderTheMinimumAmount()
        {
            string descriptionStringTest = new string('a', 9);

            SpecimenObject.SpecimenDescription = descriptionStringTest;
            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(SpecimenDescriptionError, errors[0].ErrorMessage);

        }

        /// <summary>
        /// This will test if the Specimen's description can accept exactly 10 characters
        /// </summary>
        [Test]
        public void EnteringInJustOverTheMinimumAmount()
        {
            string descriptionStringTest = new string('a', 10);

            SpecimenObject.SpecimenDescription = descriptionStringTest;
            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.IsEmpty(errors);

        }

        /// <summary>
        /// This will test if the Specimen's description can accept more than 10 characters
        /// </summary>
        [Test]
        public void EnteringInAcceptableNumberOfCharacters()
        {
            string descriptionStringTest = new string('a', 11);

            SpecimenObject.SpecimenDescription = descriptionStringTest;
            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.IsEmpty(errors);
        }

        /// <summary>
        /// This will test if the Specimen's description can accept under the maxumum amount of 5000 characers
        /// </summary>
        [Test]
        public void EnteringInJustUnderTheMaximumNumberOfCharacters()
        {
            string descriptionStringTest = new string('a', 5000);

            SpecimenObject.SpecimenDescription = descriptionStringTest;
            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.IsEmpty(errors);
        }

        /// <summary>
        /// This will test if the Specimen's description will reject just over the Maximum amount of 5000 characters
        /// </summary>
        [Test]
        public void EnteringInJustOverTheMaximumNumberOfCharacters()
        {
            string descriptionStringTest = new string('a', 5001);

            SpecimenObject.SpecimenDescription = descriptionStringTest;
            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(SpecimenDescriptionError, errors[0].ErrorMessage);
        }

        /// <summary>
        /// This will test if the Specimen's description will reject over the Maximum amount of 5000 characters
        /// </summary>
        [Test]
        public void EnteringInMoreThatTheMaximumAmount()
        {
            string descriptionStringTest = new string('a', 5500);

            SpecimenObject.SpecimenDescription = descriptionStringTest;
            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual(SpecimenDescriptionError, errors[0].ErrorMessage);
        }


        /// <summary>
        /// This will test if the Specimen's description will reject if no value is put into it
        /// </summary>
        [Test]
        public void EnteringInNothing()
        {
            string descriptionStringTest = null;

            SpecimenObject.SpecimenDescription = descriptionStringTest;
            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("SpecimenDescription cannot be blank", errors[0].ErrorMessage);

        }
        #endregion

        #region Latin Name

        /*
         * The database inserts 70 letter A's into the Latin Name Field and fails,
         * resulting in an exception that is thrown
         */
        [Test]
        public void testLatinNameIsLongerThan50Characters()
        {
            Specimen SpecimenObject = new Specimen();

            //Set the latin name
            SpecimenObject.LatinName = new string('A', 100);


            //Test that nothing was inserted
            var errors = ValidationHelper.Validate(SpecimenObject);
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
            SpecimenObject.LatinName = new string('A', 51);

            //Test that false is returned when specimen is unable to be added
            var errors = ValidationHelper.Validate(SpecimenObject);
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
            SpecimenObject.LatinName = "Begonia";

            //Test that true is returned when specimen is able to be added
            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(errors);
        }

        /*
         * The database isnerts exactly 50 E's into the database, resulting in no 
         * errors, and data being successfully inserted.
         */
        [Test]
        public void testLatinNameIsExactly50CharactersLong()
        {
            SpecimenObject.LatinName = new string('E', 50);

            //Test that true is returned when specimen is able to be added
            var errors = ValidationHelper.Validate(SpecimenObject);
            //There should be nothing passed into .ErrorMessage and the result should be null
            Assert.IsEmpty(errors);
        }

        [Test]
        public void testLatinNameDoesNotExist()
        {
            Specimen SpecimenObject = new Specimen();
            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual("Latin Name is required", errors[0].ErrorMessage);
        }
        #endregion

        #region CreeName

        [Test]
        public void TestThatCreeNamNotProvidedIsValid()
        {
            string EmptyString = string.Empty;
            SpecimenObject.CreeName = EmptyString;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatCreeNameCantBe91Characters()
        {
            string TextOf91Char = "Jumbled letters and numbers create a unique and intriguing pattern of characters that form this sentence";
            SpecimenObject.CreeName = TextOf91Char;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, Errors.Count);
            Assert.AreEqual("Cree name must be up to 90 characters", Errors[0].ErrorMessage);
        }

        [Test]
        public void TestThatCreeNameCantBeMoreThan90Characters()
        {
            Random Rand = new Random();
            int RandomNumber = Rand.Next(91, 300);
            string TextOfMoreThan91Characters = new string('o', RandomNumber);
            SpecimenObject.CreeName = TextOfMoreThan91Characters;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, Errors.Count);
            Assert.AreEqual("Cree name must be up to 90 characters", Errors[0].ErrorMessage);
        }

        [Test]
        public void TestThatCreeNameOf90CharactersIsValid()
        {
            string TextCreeOf90Char = new string('o', 90);
            SpecimenObject.CreeName = TextCreeOf90Char;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatCreeNameOfExactly4CharactersIsValid()
        {
            string S4Char = "oooo";
            SpecimenObject.CreeName = S4Char;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatCreeOfMoreThan4CharactersAreValid()
        {
            Random Rand = new Random();
            int RandomNumber = Rand.Next(5, 90);
            string RandomText = new string('o', RandomNumber);
            SpecimenObject.CreeName = RandomText;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }



        [Test]
        public void TestThatLatinCreeNameIsValid()
        {
            string LongLatinAlphabet = "eēiīoōaāpepēpipīpopōpapātetētitītotōtatākekēkikīkokōkakāchechēchichī";
            SpecimenObject.CreeName = LongLatinAlphabet;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatInvalidCharactersFails()
        {

            string LongStrangeString = "Я木दΩあҳღតޒع*{} ";

            SpecimenObject.CreeName = LongStrangeString;

            var Errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(1, Errors.Count);

            Assert.AreEqual("Characters are not valid", Errors[0].ErrorMessage);
        }



        #endregion

        #region Cultural Significance
        [Test]
        public void testThatCulturalSignificanceCanBeCorrectlyRetrieved()
        {
            // Adds a cultural significance which is valid
            SpecimenObject.CulturalSignificance = "This is the cultural significance for the sage specimen";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(errors); // Tests that there are no errors
        }

        [Test]
        public void testThatCulturalSignificanceMustBeSet()
        {
            // Sets cultural significance to null
            SpecimenObject.CulturalSignificance = null;

            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(1, errors.Count); // Tests that there is only one error
            Assert.AreEqual("Cultural Significance is required", errors[0].ErrorMessage); // Tests that there is a StringLength error
        }

        [Test]
        public void testThatCulturalSignificanceCannotExceed3500Characters()
        {
            // Adds a cultural significance which is string of 3501 characters
            string testString = new string('a', 3501);
            SpecimenObject.CulturalSignificance = testString;

            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(1, errors.Count); // Tests that there is only one error
            Assert.AreEqual("Cultural Significance must have between 1 and 3500 characters", errors[0].ErrorMessage); // Tests that there is a StringLength error
        }

        [Test]
        public void testThatCulturalSignificanceCanGoUpTo3500Characters()
        {
            // Adds a cultural significance which is string of 3500 characters
            string testString = new string('a', 3500);
            SpecimenObject.CulturalSignificance = testString;

            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.IsEmpty(errors); // Tests that there are no errors
        }

        #endregion

       


    }

}