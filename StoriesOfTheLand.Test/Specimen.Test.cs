using StorisOfTheLand.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoriesOfTheLand.Test
{
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            //Sets up the specimen object for use in the test
            Specimen SpecimenObject = new Specimen();
        }

        /// <summary>
        /// This will test if the Specimen's Description is more than the minimum amount of 10
        /// </summary>
        [Test]
        public void EnteringInLessThanTheMinimumAmount()
        {

        }

        /// <summary>
        /// This will test if the Specimen's Descriptions is less than 10
        /// This is the edge case 
        /// </summary>
        [Test]
        public void EnteringInJustUnderTheMinimumAmount()
        {

        }

        /// <summary>
        /// This will test if the Specimen's description 
        /// </summary>
        [Test]
        public void EnteringInJustOverTheMinimumAmount()
        {

        }

        [Test]
        public void EnteringInAcceptableNumberOfCharacters()
        {

        }

        [Test]
        public void EnteringInJustUnderTheMaximumNumberOfCharacters()
        {

        }

        [Test]
        public void EnteringInJustOverTheMaximumNumberOfCharacters()
        {

        }

        [Test]
        public void EnteringInMoreThatTheMaximumAmount()
        {

        }

        [Test]
        public void EnteringInSpecialCharacters()
        {

        }

        [Test]
        public void EnteringInNothing()
        {

        }

        [Test]
        public void EnteringInLettersAndNumbers()
        {

        }

        [Test]
        public void EnterInNumbers()
        {

        }
    }
}