﻿using StoriesOfTheLand.Models;
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
        [SetUp]
        public void SetUp()
        {
            SponsorObject = new Sponsor()
            {
                SponsorName = "test",
                SponsorURL = "test",
            };
        }


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

    }
}