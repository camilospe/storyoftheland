using Microsoft.Identity.Client;
using StoriesOfTheLand.Controllers;
using StoriesOfTheLand.Models;
using System.Diagnostics.CodeAnalysis;

namespace StoriesOfTheLand.Test
{
    public class Tests
    {
        private Specimen specimen;
        SpecimensController specController;

        [SetUp]
        public void Setup()
        {
            specimen = new Specimen();
            specimen.SpecimenID = 1;
            specimen.SpecimenImagePath = "abc.png";
            //add good version with valid attributes
            //to test once all attributes added to specimen model

        }


        /* "abc.png" is passed in which is valid
         */
        [Test]
        public void specimenImagePngIsValidtype()
        {
            specimen.SpecimenImagePath = "abc.png";
            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(errors.Count, 1);
            Assert.IsEmpty(errors);
 
        }

        /* "abc.jpeg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpegIsValidtype()
        {

            specimen.SpecimenImagePath = "abc.jpeg";

            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(errors.Count, 1);
            Assert.IsEmpty(errors);
        }

        /* "abc.jpg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpgIsValidtype()
        {

            specimen.SpecimenImagePath = "abc.jpg";

            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(errors.Count, 1);
            Assert.IsEmpty(errors);
        }

        /* "abcgfjdjfdpng" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImageHasNoType()
        {
            specimen.SpecimenImagePath = "abcgfjdjfdpng";


            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path is not correct file type, must be png, jpg, or jpeg", errors[0].ErrorMessage);
        }

        /* "abc.pn" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImageIsNotValidtype()
        {
            //.abc .webp .pn .jp abcabc should fail
            specimen.SpecimenImagePath = "abc.pn";

            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path is not correct file type, must be png, jpg, or jpeg", errors[0].ErrorMessage);
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

            specimen.SpecimenImagePath = new string('a', 251);
            specimen.SpecimenImagePath += ".png";
          

            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path length must be between 5 and 254", errors[0].ErrorMessage);
        }

        /* 254 ending/including ".png" is passed in which is just almost too big
        */
        [Test]
        public void specimenImageSourceNameIsOnMaxBoundaryCaseValid()
        {
            specimen.SpecimenImagePath = new string('a', 250);
            specimen.SpecimenImagePath += ".png";
           
            var errors = ValidationHelper.Validate(specimen);

            Assert.AreEqual(errors.Count, 1);
            Assert.IsEmpty(errors);
        }

        /* ".png" is passed in which is invalid
         * and an exception is thrown.
         */
        [Test]
        public void specimenImageSourceNameIsTooSmall()
        {
            specimen.SpecimenImagePath = ".png";

            var errors = ValidationHelper.Validate(specimen);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path length must be between 5 and 254", errors[0].ErrorMessage);
        }


=======
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
                LatinName = "Valid Name"
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
>>>>>>> master
    }
}