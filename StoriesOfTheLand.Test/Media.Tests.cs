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
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Packaging;

namespace StoriesOfTheLand.Test
{
    public class MediaTests
    {
        private Media MediaObject;

        [SetUp]
        public void SetUp()
        {
            MediaObject = new Media()
            {
                SpecimenImagePath =  "abc.png,abc.jpg",
                SpecimenAudioPath = "abc.mp3"
            };

        }
        #region SpecimenAudioTests

        [Test]
        public void testInvalidAudioPathFileType()
        {
            MediaObject.SpecimenAudioPath = "abc.png";

            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Image path must have atleast 5 characters and be of type mp3 or m4a.", errors[0].ErrorMessage);
        }

        [Test]
        public void testInvalidAudioFileLengthTooShort()
        {
            MediaObject.SpecimenAudioPath = "a.mp3";
            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Image path length must be between 6 and 254.", errors[0].ErrorMessage);
        }

        [Test]
        public void testInvalidAudioFileLengthTooLong()
        {
            MediaObject.SpecimenAudioPath = new string('a', 251);
            MediaObject.SpecimenAudioPath += ".mp3";
            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Image path length must be between 6 and 254.", errors[0].ErrorMessage);
        }

        [Test]
        public void testInvalidAudioFileHasNoType()
        {
            MediaObject.SpecimenAudioPath = "abcdefg";
            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Image path must have atleast 5 characters and be of type mp3 or m4a.", errors[0].ErrorMessage);
        }

        [Test]
        public void testValidAudioFileTypeMp3()
        {
            MediaObject.SpecimenAudioPath = "plant.mp3";

            var errors = ValidationHelper.Validate(MediaObject);

            Assert.IsEmpty(errors);
        }

        [Test]
        public void testValidAudioFileTypeM4a()
        {
            MediaObject.SpecimenAudioPath = "plant.m4a";

            var errors = ValidationHelper.Validate(MediaObject);

            Assert.IsEmpty(errors);
        }

        [Test]
        public void testValidAudioFileLenghtLowerBoundary()
        {
            MediaObject.SpecimenAudioPath = "ab.mp3";

            var errors = ValidationHelper.Validate(MediaObject);

            Assert.IsEmpty(errors);

        }

        [Test]
        public void testValidAudioFileLengthUpperBoundary()
        {
            MediaObject.SpecimenAudioPath = new string('a', 250);
            MediaObject.SpecimenAudioPath += ".mp3";
            var errors = ValidationHelper.Validate(MediaObject);

            Assert.IsEmpty(errors);
        }

        #endregion


        #region SpecimenImagePath

        /* "abc.png" is passed in which is valid
        */ 
        [Test]
        public void specimenImagePngIsValidtype()
        {
            MediaObject.SpecimenImagePath = "abc.png";
            var errors = ValidationHelper.Validate(MediaObject);


            Assert.IsEmpty(errors);

        }

        /* "abc.jpeg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpegIsValidtype()
        {

            MediaObject.SpecimenImagePath = "abc.jpeg";

            var errors = ValidationHelper.Validate(MediaObject);


            Assert.IsEmpty(errors);
        }

        /* "abc.jpg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpgIsValidtype()
        {

            MediaObject.SpecimenImagePath="abc.jpg,abc.jpeg";

            var errors = ValidationHelper.Validate(MediaObject);


            Assert.IsEmpty(errors);
        }

        /* "abcgfjdjfdpng" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImageHasNoType()
        {
           MediaObject.SpecimenImagePath =  "abcgfjdjfdpng" ;


            var errors = ValidationHelper.Validate(MediaObject);

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
           MediaObject.SpecimenImagePath = "abc.pn";

            var errors = ValidationHelper.Validate(MediaObject);

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

            MediaObject.SpecimenImagePath = new string('a', 251);
            MediaObject.SpecimenImagePath += ".png";


            var errors = ValidationHelper.Validate(MediaObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path length must be between 6 and 254.", errors[0].ErrorMessage);
        }

        /* 254 ending/including ".png" is passed in which is just almost too big
        */
        [Test]
        public void specimenImageSourceNameIsOnMaxBoundaryCaseValid()
        {
            MediaObject.SpecimenImagePath = new string('a', 250);
            MediaObject.SpecimenImagePath += ".png";

            var errors = ValidationHelper.Validate(MediaObject);


            Assert.IsEmpty(errors);
        }

        /*".png" is passed in which is invalid
         * and an exception is thrown.
         */
        [Test]
        public void specimenImageSourceNameIsTooSmall()
        {
            MediaObject.SpecimenImagePath = "a.png";

            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image path length must be between 6 and 254.", errors[0].ErrorMessage);
        }

        #endregion


    }
}
