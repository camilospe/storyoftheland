using StorisOfTheLand.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoriesOfTheLand.Test
{
    public class Tests
    {
        private Specimen SpecimenObject;

        [SetUp]
        public void SetUp()
        {
            //Sets up the specimen object for use in the test
            SpecimenObject = new Specimen();
        }

        /// <summary>
        /// This will test if the Specimen's Description will reject less than 10 characters
        /// </summary>
        [Test]
        public void EnteringInLessThanTheMinimumAmount()
        {
            string descriptionStringTest = new string('a', 5);




        }

        /// <summary>
        /// This will test if the Specimen's Description will reject less than 10 characters
        /// </summary>
        [Test]
        public void EnteringInJustUnderTheMinimumAmount()
        {
            string descriptionStringTest = new string('a', 9);

        }

        /// <summary>
        /// This will test if the Specimen's description can accept exactly 10 characters
        /// </summary>
        [Test]
        public void EnteringInJustOverTheMinimumAmount()
        {
            string descriptionStringTest = new string('a', 10);

        }

        /// <summary>
        /// This will test if the Specimen's description can accept more than 10 characters
        /// </summary>
        [Test]
        public void EnteringInAcceptableNumberOfCharacters()
        {
            string descriptionStringTest = new string('a', 100);

        }

        /// <summary>
        /// This will test if the Specimen's description can accept under the maxumum amount of 5000 characers
        /// </summary>
        [Test]
        public void EnteringInJustUnderTheMaximumNumberOfCharacters()
        {
            string descriptionStringTest = new string('a', 5000);
        }

        /// <summary>
        /// This will test if the Specimen's description will reject just over the Maximum amount of 5000 characters
        /// </summary>
        [Test]
        public void EnteringInJustOverTheMaximumNumberOfCharacters()
        {

        }

        /// <summary>
        /// This will test if the Specimen's description will reject over the Maximum amount of 5000 characters
        /// </summary>
        [Test]
        public void EnteringInMoreThatTheMaximumAmount()
        {

        }

        /// <summary>
        /// This will test if the Specimen's description will reject the use of special characters
        /// </summary>
        [Test]
        public void EnteringInSpecialCharacters()
        {

        }

        /// <summary>
        /// This will test if the Specimen's description will reject if no value is put into it
        /// </summary>
        [Test]
        public void EnteringInNothing()
        {

        }

        /// <summary>
        /// This will test if the Specimen's description can accept both numbers and letters together
        /// </summary>
        [Test]
        public void EnteringInLettersAndNumbers()
        {

        }

        /// <summary>
        /// This will test if the Specimen's description can accept just numbers
        /// </summary>
        [Test]
        public void EnterInNumbers()
        {

        }
    }
}