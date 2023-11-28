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
using StoriesOfTheLand.Controllers;
using Moq;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace StoriesOfTheLand.Test
{

    public class Tests
    {
        private Specimen SpecimenObject;
        private string SpecimenDescriptionError = "SpecimenDescription length must be between 10 and 5000";
        private SpecimenController _controller;
        private StoriesOfTheLandContext _context;
        private List<Specimen> Specimens;

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
                SpecimenImagePath = "abc.png",
                CulturalSignificance = "Something Valid"
            };
            //Necesarry for functionally testing, sets up the db
            var options = new DbContextOptionsBuilder<StoriesOfTheLandContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
            //Create a context based on options
            _context = new StoriesOfTheLandContext(options);
            //Create a controller based on the context
            _controller = new SpecimenController(_context);
            //Specimen Testing 
            Specimens = new List<Specimen>();
            _context.Add(
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
                });
            _context.Add(
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
            });
            _context.Add(
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
            });
            _context.Add(
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
            });
            _context.Add(
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

             }
            );

            _context.SaveChanges();
            var dbSpecs = from s in _context.Specimen select s;
            Specimens = dbSpecs.ToList();
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

        #region SpecimenImagePath

        /* "abc.png" is passed in which is valid
        */
        [Test]
        public void specimenImagePngIsValidtype()
        {
            SpecimenObject.SpecimenImagePath = "abc.png";
            var errors = ValidationHelper.Validate(SpecimenObject);


            Assert.IsEmpty(errors);

        }

        /* "abc.jpeg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpegIsValidtype()
        {

            SpecimenObject.SpecimenImagePath = "abc.jpeg";

            var errors = ValidationHelper.Validate(SpecimenObject);


            Assert.IsEmpty(errors);
        }

        /* "abc.jpg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpgIsValidtype()
        {

            SpecimenObject.SpecimenImagePath = "abc.jpg";

            var errors = ValidationHelper.Validate(SpecimenObject);


            Assert.IsEmpty(errors);
        }

        /* "abcgfjdjfdpng" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImageHasNoType()
        {
            SpecimenObject.SpecimenImagePath = "abcgfjdjfdpng";


            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path must have atleast 5 characters and be of type png, jpg, or jpeg.", errors[0].ErrorMessage);
        }

        /* "abc.pn" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImageIsNotValidtype()
        {
            //.abc .webp .pn .jp abcabc should fail
            SpecimenObject.SpecimenImagePath = "abc.pn";

            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path must have atleast 5 characters and be of type png, jpg, or jpeg.", errors[0].ErrorMessage);
        }

        /* 255 ending/including ".png" is passed in which is too large
         * and an exception is thrown
         */
        [Test]
        public void specimenImageSourceNameIsTooBig()
        {
            /*
             abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde
             abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde
             abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde */
            //255char

            SpecimenObject.SpecimenImagePath = new string('a', 251);
            SpecimenObject.SpecimenImagePath += ".png";


            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path length must be between 5 and 254.", errors[0].ErrorMessage);
        }

        /* 254 ending/including ".png" is passed in which is just almost too big
        */
        [Test]
        public void specimenImageSourceNameIsOnMaxBoundaryCaseValid()
        {
            SpecimenObject.SpecimenImagePath = new string('a', 250);
            SpecimenObject.SpecimenImagePath += ".png";

            var errors = ValidationHelper.Validate(SpecimenObject);


            Assert.IsEmpty(errors);
        }

        /* ".png" is passed in which is invalid
         * and an exception is thrown.
         */
        [Test]
        public void specimenImageSourceNameIsTooSmall()
        {
            SpecimenObject.SpecimenImagePath = ".jpeg";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path must have atleast 5 characters and be of type png, jpg, or jpeg.", errors[0].ErrorMessage);
        }

        #endregion

        #region ViewSpecimenIndex
        /*
         * Check to see that the Specimen Index for English Common Name displays in Alphabetical Order
         * When English Common Name is selected
         */
        [Test]
        public void TestSpecimenEnglishCommonNameDisplaysInAlphabeticalOrder()
        {
            var specsInEnglishAZ  = Specimens.OrderBy(s => s.EnglishName);
            Specimens = specsInEnglishAZ.ToList();
            Assert.AreEqual("Horsetail", Specimens[0].EnglishName);
            Assert.AreEqual("Wild Mint", Specimens[4].EnglishName);

        }

        /*
         * Check to see that the Specimen Index for English Common Name displays in Reverse Alphabetical Order
         * When English Common Name is selected
         */
        [Test]
        public void TestSpecimenEnglishCommonNameDisplaysInReverseAlphabeticalOrder()
        {
            var specsInEnglishZA = Specimens.OrderByDescending(s => s.EnglishName);
            Specimens = specsInEnglishZA.ToList();
            Assert.AreEqual("Wild Mint", Specimens[0].EnglishName);
            Assert.AreEqual("Horsetail", Specimens[4].EnglishName);
        }

        /*
         * Check to see that the Specimen Index for Latin Name displays in Reverse Alphabetical Order
         * When Latin Name is selected
         */
        [Test]
        public void TestSpecimenLatinNameDisplaysInAlphabeticalOrder()
        {
            var specsInLatinAZ = Specimens.OrderBy(s => s.LatinName);
            Specimens = specsInLatinAZ.ToList();
            Assert.AreEqual("Equisetum species", Specimens[0].LatinName);
            Assert.AreEqual("Vaccinium myrtilloides", Specimens[4].LatinName);
        }

        /*
         * Check to see that the Specimen Index for Latin Name displays in Reverse Alphabetical Order
         * When Latin Name is selected
         */
        [Test]
        public void TestSpecimenLatinNameDisplaysInReverseAlphabeticalOrder()
        {
            var specsInLatinZA = Specimens.OrderByDescending(s => s.LatinName);
            Specimens = specsInLatinZA.ToList();
            Assert.AreEqual("Vaccinium myrtilloides", Specimens[0].LatinName);
            Assert.AreEqual("Equisetum species", Specimens[4].LatinName);
        }

        /*
         * Check to see that the Specimen Index for Cree Name displays in Reverse Alphabetical Order
         * When Cree Name is selected
         */
        [Test]
        public void TestSpecimenCreeNameDisplaysInAlphabeticalOrder()
        {
            var specsInCreeAZ = Specimens.OrderBy(s => s.CreeName);
            Specimens = specsInCreeAZ.ToList();
            Assert.AreEqual("Amiskowihkask", Specimens[0].CreeName);
            Assert.AreEqual("Maskêkopakwa", Specimens[4].CreeName);
        }

        /*
         * Check to see that the Specimen Index for Cree Name displays in Reverse Alphabetical Order
         * When Cree Name is selected
        */
        [Test]
        public void TestSpecimenCreeNameDisplaysInReverseAlphabeticalOrder()
        {
            var specsInCreeZA = Specimens.OrderBy(s => s.CreeName);
            Specimens = specsInCreeZA.ToList();
            Assert.AreEqual("Maskêkopakwa", Specimens[0].CreeName);
            Assert.AreEqual("Amiskowihkask", Specimens[4].CreeName);
        }
        #endregion
    }

}