﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using SQLitePCL;
using StoriesOfTheLand.Controllers;
using StoriesOfTheLand.Data;
using StoriesOfTheLand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesOfTheLand.Test
{
    public class SponsorTests
    {
        private Sponsor SponsorObject;
        private HomeController _controller;
        private StoriesOfTheLandContext _context;

        [SetUp]
        public void SetUp()
        {

            

            SponsorObject = new Sponsor()
            {
                SponsorName = "test",
                SponsorURL = "test",
                SponsorImagePath = "Saskatchewan_Polytechnic_Logo.png"
            };



            //Necesarry for functionally testing, sets up the db
            var options = new DbContextOptionsBuilder<StoriesOfTheLandContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
            //Create a context based on options
            _context = new StoriesOfTheLandContext(options);
            //Create a controller based on the context
            _controller = new HomeController(_context);

            _context.Sponsor.AddRange(
                SponsorObject,
                new Sponsor
                {
                    SponsorName = "test2",
                    SponsorURL = "test2",
                    SponsorImagePath = "Saskatchewan_Polytechnic_Logo.png"
                }
                );
            _context.SaveChanges();
             
        }

        [TestFixture]
        public class HtmlTest
        {
            private static readonly HttpClient httpClient = new HttpClient();

            [Test]
            public void testSponsorListNotEmpty()
            {
                string url = "https://storiesoftheland-app-20231206210.greenrock-f1e4ba9c.eastus.azurecontainerapps.io/";
                //put in enviroment variable or config file, no hard card url

                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                // Ensure the request was successful
                Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

                // Read the HTML content from the response
                string htmlContent = response.Content.ReadAsStringAsync().Result;

                Assert.IsTrue(htmlContent.Contains("<div id=\"partnerships\" class=\"partnerships\" data-bs-ride=\"partnerships\">"), "Expected content not found in HTML");
            }

        }

        [Test]
        public async Task testHomeControllerIndexReturnsView()
        {
            var result = await _controller.Index();
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task TestHomeControllerIndexViewContainsListOfSponsorsAsync()
        {
            var result = await _controller.Index();
            Assert.IsInstanceOf<ViewResult>(result);
            var model = ((ViewResult)result).Model;
            
            Assert.IsNotNull(model);
            Assert.IsAssignableFrom<List<Sponsor>>(model);
        }

       

        #region sponsormodel
        [Test]
        public void testSponsorName0CharIsInvalid()
        {
            SponsorObject.SponsorName = "";

            var errors = ValidationHelper.Validate(SponsorObject);

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Sponsor Name is required", errors[0].ErrorMessage);
        }

        [Test]
        public void testSponsorName1CharIsValid()
        {
            SponsorObject.SponsorName = "a";

            var errors = ValidationHelper.Validate(SponsorObject);
            Assert.IsEmpty(errors);

        }

        [Test]
        public void testSponsorName50CharIsValid()
        {
            SponsorObject.SponsorName = new string('a', 50);

            var errors = ValidationHelper.Validate(SponsorObject);
            Assert.IsEmpty(errors);

        }

        [Test]
        public void testSponsorName51CharIsInvalid()
        {
            SponsorObject.SponsorName = new string('a', 51);

            var errors = ValidationHelper.Validate(SponsorObject);

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Sponsor Name length must be between 1 and 50", errors[0].ErrorMessage);
        }

        [Test]
        public void testSponsorURL0CharIsInvalid()
        {
            SponsorObject.SponsorURL = "";

            var errors = ValidationHelper.Validate(SponsorObject);

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Sponsor URL is required", errors[0].ErrorMessage);
        }

        [Test]
        public void testSponsorURL1CharIsValid()
        {
            SponsorObject.SponsorURL = "a";

            var errors = ValidationHelper.Validate(SponsorObject);
            Assert.IsEmpty(errors);

        }
        [Test]
        public void testSponsorURL100CharIsValid()
        {
            SponsorObject.SponsorURL = new string('a', 100);

            var errors = ValidationHelper.Validate(SponsorObject);
            Assert.IsEmpty(errors);

        }

        [Test]
        public void testSponsorURL101CharIsInvalid()
        {
            SponsorObject.SponsorURL = new string('a', 101);

            var errors = ValidationHelper.Validate(SponsorObject);

            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("Sponsor Link length must be between 1 and 100", errors[0].ErrorMessage);
        }

        #endregion

        





    }
}
