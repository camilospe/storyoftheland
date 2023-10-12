using StoriesOfTheLand.Controllers;
using StorisOfTheLand.Models;
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
            specController = new SpecimensController();
            //add good version with valid attributes
            //to test once all attributes added to specimen model

        }


        /* "abc.png" is passed in which is valid
         */
        [Test]
        public void specimenImage_png_is_validtype()
        {
            specimen.specimenImagePath = "abc.png";
            var errors = ValidationHelper.Validate(specimen);

            Assert.IsEmpty(errors);
 
        }

        /* "abc.jpeg" is passed in which is valid
         */
        [Test]
        public void specimenImage_jpeg_is_validtype()
        {

            specimen.specimenImagePath = "abc.jpeg";

            var errors = ValidationHelper.Validate(specimen);

            Assert.IsEmpty(errors);
        }

        /* "abc.jpg" is passed in which is valid
         */
        [Test]
        public void specimenImage_jpg_is_validtype()
        {

            specimen.specimenImagePath = "abc.jpg";

            var errors = ValidationHelper.Validate(specimen);

            Assert.IsEmpty(errors);
        }

        /* "abcgfjdjfdpng" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImage_has_no_type()
        {
            specimen.specimenImagePath = "abcgfjdjfdpng";


            var errors = ValidationHelper.Validate(specimen);

            Assert.Equals("Image path is not correct file type, must be png, jpg, or jpeg", errors[0].ErrorMessage);
        }

        /* "abc.pn" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImage_is_not_validtype()
        {
            //.abc .webp .pn .jp abcabc should fail
            specimen.specimenImagePath = "abc.pn";

            var errors = ValidationHelper.Validate(specimen);

            Assert.Equals("Image path is not correct file type, must be png, jpg, or jpeg", errors[0].ErrorMessage);
        }

        /* 255 ending/including ".png" is passed in which is too large
         * and an exception is thrown
         */
        [Test]
        public void specimenImage_sourcename_is_too_big()
        {
            /*
             abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde
             abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde
             abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcde */
            //255char

            specimen.specimenImagePath = "abcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdea.png";
            //this is 255char including .png

            var errors = ValidationHelper.Validate(specimen);
            Assert.Equals("Image path length must be between 5 and 254", errors[0].ErrorMessage);
        }

        [Test]
        public void specimenImage_sourcename_is_almost_too_big_but_valid()
        {
           
            specimen.specimenImagePath = "bcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdeabcdea.png";
            //this is 254char including .png

            var errors = ValidationHelper.Validate(specimen);
            Assert.IsEmpty(errors);
        }

        /* ".png" is passed in which is invalid
         * and an exception is thrown.
         */
        [Test]
        public void specimenImage_sourcename_is_too_small()
        {
            specimen.specimenImagePath = ".png";

            var errors = ValidationHelper.Validate(specimen);
            Assert.Equals("Image path length must be between 5 and 254", errors[0].ErrorMessage);
        }



        //Controller tests below

        /* A test path is assigned,
         * if Specimen method .getImagePath returns the same test path
         * Yippie test successful
         */
        [Test]
        public void specimenGetImagePathIsRetrieved()
        {
            const string testPath = "Test.png";
            specimen.specimenImagePath = testPath;
            var result = specimen.getImagePath();

            //if result = Test.png is retrieved then yay
            Assert.AreEqual(result, testPath);
        }


        /* A test path is assigned,
        * if Specimen Controller method .getSpecimenImagePath 
        * returns the same test path Yippie test successful
        */
        [Test]
        public void specimenGetSpecimenImagePathIsRetrieved()
        {
            const string testPath = "Test.png";
            specimen.specimenImagePath = testPath;
            var result = specController.getSpecimenImagePath();

            Assert.AreEqual(specController.getSpecimenImagePath(), testPath);
        }



    }
}