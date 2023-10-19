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


    }
}