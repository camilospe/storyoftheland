using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Specimen newSpecimen = new Specimen();
            newSpecimen.EnlgishName = "Tree";
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        [Test]
        //test invlaid upper bounds by entering 51 characters
        public void testInvalidSpecimenEnlgishNameIsLongerThan50Characters()
        {
        
            

            String englishName = "";
            for (int i = 0; i <= 51; i++)
            {
                englishName += "a";
            }
            
            Assert.That(newSpecimen.EnlgishName.Length<=50, Is.False);
        }

        [Test]
        //test valid upper bound by enetering 50 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtUpperBoundary()
        {

            String englishName = "";

            for (int i = 0; i <= 50; i++)
            {
                englishName += "a";
            }
            Specimen newSpecimen = new Specimen(englishName);

            
        }


        [Test]
        //test invailid lower bound by entering 2 characters
        public void testInvalidSpecimenEnglishNameIsShoterThan3Characters()
        {
            string englishName = "aa";

            Specimen newSpecimen = new Specimen(englishName);
        }


        [Test]
        //test valid length lower bound by entering 3 characters
        public void testValidSpecimenEnlgishNameIsAcceptableLenghtLowerBoundary()
        {

            string englishName = "aaa";

            Specimen newSpecimen = new Specimen(englishName);
        }


        [Test]
        public void testInvalidSpecimenEnglishNameHasInvalidCharacters()
        {
            string englishName = "124a";
            Specimen newSpecimen = new Specimen(englishName);
            bool result = ContainsNonLetterCharacters(newSpecimen.EnlgishName);

            Assert.That(result, Is.EqualTo(true));
        }


        [Test]
        // test invalid by entering an empty string
        public void testInvalidSpecimenEnglishNameIsEmpty()
        {
            String englishName = "";

            Specimen newSpecimen = new Specimen(englishName);

           
        }


        // contains a helper method to check if there are any non letter characters in the name
        private bool ContainsNonLetterCharacters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }

    }
}