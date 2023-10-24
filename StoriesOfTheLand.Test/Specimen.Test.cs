using StoriesOfTheLand.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                SpecimenDescription = new string('a', 11),
                LatinName = new string('a', 10)
            };
        }

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
            Specimen testSpecimen = new Specimen();
            var errors = ValidationHelper.Validate(testSpecimen);
            Assert.AreEqual("Latin Name is required", errors[0].ErrorMessage);
        }
        #endregion

    }

}