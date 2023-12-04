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
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using StoriesOfTheLand.Controllers;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using Microsoft.AspNetCore.Components.Infrastructure;
using System.Net.Http;
using System.Net;

namespace StoriesOfTheLand.Test
{


   



    public class MediaTests
    {
        private Media MediaObject;
        private SpecimenController _controller;
        private StoriesOfTheLandContext _context;

        [SetUp]
        public void SetUp()
        {
            MediaObject = new Media()
            {
                SpecimenImagePath = "abc.png,abc.jpg",
                SpecimenAudioPath = "abc.mp3"
            };

           
            //set up database
            var options = new DbContextOptionsBuilder<StoriesOfTheLandContext>().UseInMemoryDatabase(databaseName: "newDB").Options;

            //create a new context
            _context = new StoriesOfTheLandContext(options);

            //create a new controller
            _controller = new SpecimenController(_context);


            _context.Specimen.AddRange(
                new Specimen
                {

                    SpecimenMedia= new Media
                    {
                        SpecimenImagePath = "abc.png,abc.jpg",
                        SpecimenAudioPath = "abc.mp3"
                    }
                }
                
             );
         
        }
        #region FunctionalTests
         [TestFixture]
    public class HtmlTest
    {
        private static readonly HttpClient httpClient = new HttpClient();

            [Test]
            public void TestCarouselHTMLIfThereIsMoreThanOneImage()
            {
                // Replace "your_url_here" with the actual URL you want to test
                string url = "https://storiesoftheland-app-20231204104.mangohill-c81df601.canadacentral.azurecontainerapps.io/Specimen/Details/1";

                // Make a request to the URL
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                // Ensure the request was successful
                Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

                // Read the HTML content from the response
                string htmlContent = response.Content.ReadAsStringAsync().Result;

                // Perform assertions or checks on the HTML content
                Assert.IsTrue(htmlContent.Contains("<div id=\"carouselExampleControls\" class=\"carousel slide\" data-bs-ride=\"carousel\">"), "Expected content not found in HTML");

                // You can perform more checks or validations here
            }

            [Test]
            public void TestNoCarouselHTMLIfThereIsOneImage()
            {
                // Replace "your_url_here" with the actual URL you want to test
                string url = "https://storiesoftheland-app-20231204104.mangohill-c81df601.canadacentral.azurecontainerapps.io/Specimen/Details/2";

                // Make a request to the URL
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                // Ensure the request was successful
                Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

                // Read the HTML content from the response
                string htmlContent = response.Content.ReadAsStringAsync().Result;

                // Perform assertions or checks on the HTML content
                Assert.IsTrue(htmlContent.Contains("<div id=\"oneImage\">"), "Expected content not found in HTML");

                // You can perform more checks or validations here
            }
            [Test]
            public void TestNoImageHTMLIfThereIsNoImage()
            {
                // Replace "your_url_here" with the actual URL you want to test
                string url = "https://storiesoftheland-app-20231204104.mangohill-c81df601.canadacentral.azurecontainerapps.io/Specimen/Details/6";

                // Make a request to the URL
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                // Ensure the request was successful
                Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

                // Read the HTML content from the response
                string htmlContent = response.Content.ReadAsStringAsync().Result;

                // Perform assertions or checks on the HTML content
                Assert.IsFalse(htmlContent.Contains("<div id=\"carouselExampleControls\" class=\"carousel slide\" data-bs-ride=\"carousel\">"), "Expected content not found in HTML");
                Assert.IsFalse(htmlContent.Contains("<div id=\"oneImage\">"), "Expected content not found in HTML");

                // You can perform more checks or validations here
            }

            [Test]
            public void TestAudioPlayerIfThereIsAnAudioFile()
            {
                // Replace "your_url_here" with the actual URL you want to test
                string url = "https://storiesoftheland-app-20231204104.mangohill-c81df601.canadacentral.azurecontainerapps.io/Specimen/Details/1";

                // Make a request to the URL
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                // Ensure the request was successful
                Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

                // Read the HTML content from the response
                string htmlContent = response.Content.ReadAsStringAsync().Result;

                // Perform assertions or checks on the HTML content
                Assert.IsTrue(htmlContent.Contains("<audio controls>"), "Expected content not found in HTML");

                // You can perform more checks or validations here
            }

            [Test]
            public void TestNoAudioPlayerIfThereIsNoAudioFile()
            {
                // Replace "your_url_here" with the actual URL you want to test
                string url = "https://storiesoftheland-app-20231204104.mangohill-c81df601.canadacentral.azurecontainerapps.io/Specimen/Details/2";

                // Make a request to the URL
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                // Ensure the request was successful
                Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

                // Read the HTML content from the response
                string htmlContent = response.Content.ReadAsStringAsync().Result;

                // Perform assertions or checks on the HTML content
                Assert.IsFalse(htmlContent.Contains("<audio controls>"), "Expected content not found in HTML");

                // You can perform more checks or validations here
            }
        }
        
        #endregion

        #region SpecimenAudioTests

        [Test]
        public void testInvalidAudioPathFileType()
        {
            MediaObject.SpecimenAudioPath = "Blueberry.png";

            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Audio file must be of type m4a or mp3", errors[0].ErrorMessage);
        }

        [Test]
        public void testInvalidAudioFileLengthTooShort()
        {
            MediaObject.SpecimenAudioPath = "a.mp3";
            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Audio path must be between 6 and 254 characters", errors[0].ErrorMessage);
        }

        [Test]
        public void testInvalidAudioFileLengthTooLong()
        {
            MediaObject.SpecimenAudioPath = new string('a', 251);
            MediaObject.SpecimenAudioPath += ".mp3";
            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Audio path must be between 6 and 254 characters", errors[0].ErrorMessage);
        }

        [Test]
        public void testInvalidAudioFileHasNoType()
        {
            MediaObject.SpecimenAudioPath = "Blueberrym4a";
            var errors = ValidationHelper.Validate(MediaObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Audio file must be of type m4a or mp3", errors[0].ErrorMessage);
        }

        [Test]
        public void testValidAudioFileTypeMp3()
        {
            MediaObject.SpecimenAudioPath = "Blueberry.mp3";

            var errors = ValidationHelper.Validate(MediaObject);

            Assert.IsEmpty(errors);
        }

        [Test]
        public void testValidAudioFileTypeM4a()
        {
            MediaObject.SpecimenAudioPath = "Blueberry.m4a";

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
            MediaObject.SpecimenImagePath = "Blueberry.png";
            var errors = ValidationHelper.Validate(MediaObject);


            Assert.IsEmpty(errors);

        }

        /* "abc.jpeg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpegIsValidtype()
        {

            MediaObject.SpecimenImagePath = "Blueberry.jpeg";

            var errors = ValidationHelper.Validate(MediaObject);


            Assert.IsEmpty(errors);
        }

        /* "abc.jpg" is passed in which is valid
         */
        [Test]
        public void specimenImageJpgIsValidtype()
        {

            MediaObject.SpecimenImagePath = "Blueberry.jpg";

            var errors = ValidationHelper.Validate(MediaObject);


            Assert.IsEmpty(errors);
        }

        /* "abcgfjdjfdpng" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImageHasNoType()
        {
            MediaObject.SpecimenImagePath = "Blueberrypng";


            var errors = ValidationHelper.Validate(MediaObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image file must be of type png, jpg, or jpeg", errors[0].ErrorMessage);


        }

        /* "abc.pn" is passed in which is invalid
         * and an exception is thrown
         */
        [Test]
        public void specimenImageIsNotValidtype()
        {
            //.abc .webp .pn .jp abcabc should fail
            MediaObject.SpecimenImagePath = "Blueberry.pn";

            var errors = ValidationHelper.Validate(MediaObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image file must be of type png, jpg, or jpeg", errors[0].ErrorMessage);
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
            Assert.AreEqual("Image path length must be between 6 and 254", errors[0].ErrorMessage);
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
            Assert.AreEqual("Image path length must be between 6 and 254", errors[0].ErrorMessage);
        }

        [Test]
        public void specimenImagePathContainsMoreThanOneImageValid()
        {
            MediaObject.SpecimenImagePath = "abc.png,abc.jpg";
            var errors = ValidationHelper.Validate(MediaObject);

            Assert.IsEmpty(errors);
        }

        [Test]
        public void specimenImagePathContainsMoreThanOneImageInvalid()
        {
            MediaObject.SpecimenImagePath = "abc.png,abc.mp3";
            var errors = ValidationHelper.Validate(MediaObject);

            Assert.AreEqual(errors.Count, 1);
            Assert.AreEqual("Image file must be of type png, jpg, or jpeg", errors[0].ErrorMessage);
        }

        #endregion

    }
}
