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
            //add good version to test?

        }


        /* "abc.png" is passed in which is valid
         */
        [Test]
        public void specimenImage_png_is_validtype()
        {
            specimen.specimenImagePath = "abc.png";

            //ensuring the path is stored
            var result = specimen.specimenImagePath.Length;
            //in future hopefully know how to verify this with validator
            Assert.IsTrue(result > 0, "Image specimen type must be png, jpeg, or jpg");
        }

        /* "abc.jpeg" is passed in which is valid
         */
        [Test]
        public void specimenImage_jpeg_is_validtype()
        {

            specimen.specimenImagePath = "abc.jpeg";

            //ensuring the path is stored
            var result = specimen.specimenImagePath.Length;
            Assert.IsTrue(result > 0, "Image specimen type must be png, jpeg, or jpg");
        }

        /* "abc.jpg" is passed in which is valid
         */
        [Test]
        public void specimenImage_jpg_is_validtype()
        {

            specimen.specimenImagePath = "abc.jpg";

            //ensuring the path is stored
            var result = specimen.specimenImagePath.Length;
            Assert.IsTrue(result > 0, "Image specimen type must be png, jpeg, or jpg");
        }

        /* "abcgfjdjfdpng" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImage_has_no_type()
        {

            specimen.specimenImagePath = "abcgfjdjfdpng";

            
            //ensuring the path is NOT stored
            var result = specimen.specimenImagePath.Length;
            Assert.IsTrue(result == 0, "Image specimen type must be png, jpeg, or jpg");
        }

        /* "abc.pn" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImage_is_not_validtype()
        {
            //.abc .webp .pn .jp abcabc should fail
            specimen.specimenImagePath = "abc.pn";

            //ensuring the path is NOT stored
            var result = specimen.specimenImagePath.Length;
            Assert.IsTrue(result == 0, "Image specimen type must be png, jpeg, or jpg");
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

            var result = specimen.specimenImagePath.Length;
            Assert.IsTrue(result == 0, "Image path is too large");
        }

        /* ".png" is passed in which is invalid
         * and an exception is thrown.
         */
        [Test]
        public void specimenImage_sourcename_is_too_small()
        {
            specimen.specimenImagePath = ".png";

            var result = specimen.specimenImagePath.Length;
            Assert.IsTrue(result == 0, "Image path is too small");
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