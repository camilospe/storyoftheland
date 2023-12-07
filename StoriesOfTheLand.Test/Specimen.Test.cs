using StoriesOfTheLand.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System.Numerics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.DependencyInjection;
using StoriesOfTheLand.Data;
using StoriesOfTheLand.Controllers;
using Moq;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace StoriesOfTheLand.Test
{

    public class Tests
    {
        private Specimen SpecimenObject;
        private string SpecimenDescriptionError = "SpecimenDescription length must be between 10 and 5000";
        private SpecimenController _controller;
        private StoriesOfTheLandContext _context;
        private List<Specimen> Specimens;
        private Specimen NewSpecimen;
        private string detailUrl;

        [SetUp]
        public void SetUp()
        {
            //Sets up the specimen object for use in the test
            SpecimenObject = new Specimen()
            {
                EnglishName = "Tree",
                SpecimenDescription = new string('a', 11),
                LatinName = new string('a', 10),
                CreeName = "Name",
                CulturalSignificance = "Something Valid",
                
            };

            NewSpecimen = new Specimen()
            {
                EnglishName = "English Name",
                SpecimenDescription = "Valid Description",
                LatinName = "Latin Name",
                CreeName = "Cree Name",
                CulturalSignificance = "Valid Significance",
            };
            //Necesarry for functionally testing, sets up the db
            var options = new DbContextOptionsBuilder<StoriesOfTheLandContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
            //Create a context based on options
            _context = new StoriesOfTheLandContext(options);
            //Create a controller based on the context
            _controller = new SpecimenController(_context);
            Specimens = new List<Specimen>();
            Specimens.Add(
                new Specimen
                {
                    SpecimenDescription = //Blueberry
                            @"A small shrub, 10-50cm tall, growing in sandy or gravel soils. It thrives in clearings of coniferous stands of the boreal forest. 
                             This woody plant can grow in dense clusters and is characterized by its soft, lance-shaped, velvety leaves. The spring flowers are shaped like delicate white urns,
                             which develop into the petite, blue fruit, familiar to all “pickers”!",
                    LatinName = "Vaccinium myrtilloides",
                    EnglishName = "Velvet Leaf Blueberry",
                    CreeName = "Idinimin",
                    SpecimenMedia = new Media
                    {
                        SpecimenImagePath = "blueberry.png"
                      
                    },
                    CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
                });
            Specimens.Add(
            new Specimen
            {
                SpecimenDescription = //Horsetail
                            @"Horsetail plants tend to favour cool, moist, forested areas. Species grow from low to the ground to 1m tall. All horsetails are characterized by jointed, grooved, 
                            hollow stems with a honeycomb like top where the spores are housed. Horsetails reproduce by spores as apposed to seed. 
                           They are ancient primitive plants dating back over 300 million years!",
                LatinName = "Equisetum species",
                EnglishName = "Horsetail",
                SpecimenMedia = new Media
                {
                    SpecimenImagePath = "Horsetail.png"

                },
                CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
            });
            Specimens.Add(
            new Specimen
            {
                SpecimenDescription = //Labrador Tea
                            @"Labrador tea is a low shrub found in bogs, swamps, and moist lowland woods in nutrient poor soil. This plant keeps its leaves all year round though they 
                            often turn brownish orange in the winter. The leaves alternate around the stem like a spiral staircase. The leaves are thick and leathery with orange fuzzy hairs 
                            on the underside. White coloured flowers sit on top of the plant.",
                LatinName = "Ledum groenlandicum",
                EnglishName = "Labrador Tea",
                CreeName = "Maskêkopakwa",
                SpecimenMedia = new Media
                {
                    SpecimenImagePath = "LabradorTea.png"

                }, 
                CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
            });
            Specimens.Add(
            new Specimen
            {
                SpecimenDescription = //Lungwort
                @"Lungwort is an erect, perennial plant, (growing from 20-80cm tall) commonly found in moist woods, and meadows. 
                It has wide pointed leaves that alternate up the stem and pink or blue bell-shaped flowers on bowing branches
                Leaves are covered with short hairs making them feel rough to the touch. ",
                LatinName = "Mertensia paniculata",
                EnglishName = "Lungwort",
                SpecimenMedia = new Media
                {
                    SpecimenImagePath = "Lungwort.png"

                },
                CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."
            });
            Specimens.Add(
            new Specimen
            {
                SpecimenDescription = //Mint
                @"Wild mint is found in moist soil, on shorelines, stream banks and damp clearings. It can grow from 10-60cm tall, 
                has serrated leaves in pairs around a square stem and small, purple-pink flowers in dense whorls at the base of the leaves. Walking on or 
                disturbing mint releases the familiar mint smell.",
                LatinName = "Mentha arvensis",
                EnglishName = "Wild Mint",
                CreeName = "Amiskowihkask",
                SpecimenMedia = new Media
                {
                    SpecimenImagePath = "mint.png"

                },
                CulturalSignificance = "When you stumble on her you may see a pretty wildflower, but she is so much more, strong, beautiful and healing in nature the lungwort plant offers relief from stomach ailments, diarrhea, wounds healing and most commonly like its name its used for coughs, colds and irritation of the lungs."

             }
            );
        }
       
        #region EnglishName
        [Test]
        //test invlaid upper bounds by entering 51 characters
        public void testInvalidSpecimenEnglishNameIsLongerThan50Characters()
        {
            SpecimenObject.EnglishName = new string('a', 51);

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too long must be 50 characters or less", errors[0].ErrorMessage);
        }

        [Test]
        //test valid upper bound by enetering 50 characters
        public void testValidSpecimenEnglishNameIsAcceptableLenghtUpperBoundary()
        {

            SpecimenObject.EnglishName = new string('a', 50);

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(errors);

        }
        [Test]
        //test invailid lower bound by entering 2 characters
        public void testInvalidSpecimenEnglishNameIsShoterThan3Characters()
        {
            SpecimenObject.EnglishName = "aa";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English name is too short must be a minimum of 3 characters", errors[0].ErrorMessage);
        }


        [Test]
        //test valid length lower bound by entering 3 characters
        public void testValidSpecimenEnglishNameIsAcceptableLenghtLowerBoundary()
        {

            SpecimenObject.EnglishName = "aaa";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(errors);
        }


        [Test]
        //test if there are any non letter characters
        public void testInvalidSpecimenEnglishNameHasInvalidCharacters()
        {
            SpecimenObject.EnglishName = "124@";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name should not contain numbers or special characters.", errors[0].ErrorMessage);
        }




        [Test]
        // test invalid by entering an empty string
        public void testInvalidSpecimenEnglishNameNotEntered()
        {
            SpecimenObject.EnglishName = null;

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, errors.Count);
            Assert.AreEqual("English Name is required", errors[0].ErrorMessage);
        }

        #endregion

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
            Specimen SpecimenObject = new Specimen();

            //Set the latin name
            SpecimenObject.LatinName = new string('A', 100);


            //Test that nothing was inserted
            var errors = ValidationHelper.Validate(SpecimenObject);
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
            SpecimenObject.LatinName = new string('A', 51);

            //Test that false is returned when specimen is unable to be added
            var errors = ValidationHelper.Validate(SpecimenObject);
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
            Specimen SpecimenObject = new Specimen();
            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual("Latin Name is required", errors[0].ErrorMessage);
        }
        #endregion

        #region CreeName

        [Test]
        public void TestThatCreeNamNotProvidedIsValid()
        {
            string EmptyString = string.Empty;
            SpecimenObject.CreeName = EmptyString;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatCreeNameCantBe91Characters()
        {
            string TextOf91Char = "Jumbled letters and numbers create a unique and intriguing pattern of characters that form this sentence";
            SpecimenObject.CreeName = TextOf91Char;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, Errors.Count);
            Assert.AreEqual("Cree name must be up to 90 characters", Errors[0].ErrorMessage);
        }

        [Test]
        public void TestThatCreeNameCantBeMoreThan90Characters()
        {
            Random Rand = new Random();
            int RandomNumber = Rand.Next(91, 300);
            string TextOfMoreThan91Characters = new string('o', RandomNumber);
            SpecimenObject.CreeName = TextOfMoreThan91Characters;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.AreEqual(1, Errors.Count);
            Assert.AreEqual("Cree name must be up to 90 characters", Errors[0].ErrorMessage);
        }

        [Test]
        public void TestThatCreeNameOf90CharactersIsValid()
        {
            string TextCreeOf90Char = new string('o', 90);
            SpecimenObject.CreeName = TextCreeOf90Char;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatCreeNameOfExactly4CharactersIsValid()
        {
            string S4Char = "oooo";
            SpecimenObject.CreeName = S4Char;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatCreeOfMoreThan4CharactersAreValid()
        {
            Random Rand = new Random();
            int RandomNumber = Rand.Next(5, 90);
            string RandomText = new string('o', RandomNumber);
            SpecimenObject.CreeName = RandomText;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }



        [Test]
        public void TestThatLatinCreeNameIsValid()
        {
            string LongLatinAlphabet = "eēiīoōaāpepēpipīpopōpapātetētitītotōtatākekēkikīkokōkakāchechēchichī";
            SpecimenObject.CreeName = LongLatinAlphabet;
            var Errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(Errors);
        }

        [Test]
        public void TestThatInvalidCharactersFails()
        {

            string LongStrangeString = "Я木दΩあҳღតޒع*{} ";

            SpecimenObject.CreeName = LongStrangeString;

            var Errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(1, Errors.Count);

            Assert.AreEqual("Characters are not valid", Errors[0].ErrorMessage);
        }



        #endregion

        #region Cultural Significance
        [Test]
        public void testThatCulturalSignificanceCanBeCorrectlyRetrieved()
        {
            // Adds a cultural significance which is valid
            SpecimenObject.CulturalSignificance = "This is the cultural significance for the sage specimen";

            var errors = ValidationHelper.Validate(SpecimenObject);
            Assert.IsEmpty(errors); // Tests that there are no errors
        }

        [Test]
        public void testThatCulturalSignificanceMustBeSet()
        {
            // Sets cultural significance to null
            SpecimenObject.CulturalSignificance = null;

            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(1, errors.Count); // Tests that there is only one error
            Assert.AreEqual("Cultural Significance is required", errors[0].ErrorMessage); // Tests that there is a StringLength error
        }

        [Test]
        public void testThatCulturalSignificanceCannotExceed3500Characters()
        {
            // Adds a cultural significance which is string of 3501 characters
            string testString = new string('a', 3501);
            SpecimenObject.CulturalSignificance = testString;

            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.AreEqual(1, errors.Count); // Tests that there is only one error
            Assert.AreEqual("Cultural Significance must have between 1 and 3500 characters", errors[0].ErrorMessage); // Tests that there is a StringLength error
        }

        [Test]
        public void testThatCulturalSignificanceCanGoUpTo3500Characters()
        {
            // Adds a cultural significance which is string of 3500 characters
            string testString = new string('a', 3500);
            SpecimenObject.CulturalSignificance = testString;

            var errors = ValidationHelper.Validate(SpecimenObject);

            Assert.IsEmpty(errors); // Tests that there are no errors
        }

        #endregion

       

        #region ViewSpecimenIndex
        /*
         * Check to see that the Specimen Index for English Common Name displays in Alphabetical Order
         * When English Common Name is selected
         */
        [Test]
        public void TestSpecimenEnglishCommonNameDisplaysInAlphabeticalOrder()
        {
            Specimens = Specimens.OrderBy(s => s.EnglishName).ToList();
            Assert.AreEqual("Horsetail", Specimens[0].EnglishName);
            Assert.AreEqual("Wild Mint", Specimens[4].EnglishName);

        }

        /*
         * Check to see that the Specimen Index for English Common Name displays in Reverse Alphabetical Order
         * When English Common Name is selected
         */
        [Test]
        public void TestSpecimenEnglishCommonNameDisplaysInReverseAlphabeticalOrder()
        {
            Specimens = Specimens.OrderByDescending(s => s.EnglishName).ToList();
            Assert.AreEqual("Wild Mint", Specimens[0].EnglishName);
            Assert.AreEqual("Horsetail", Specimens[4].EnglishName);
        }

        /*
         * Check to see that the Specimen Index for Latin Name displays in Reverse Alphabetical Order
         * When Latin Name is selected
         */
        [Test]
        public void TestSpecimenLatinNameDisplaysInAlphabeticalOrder()
        {
            Specimens = Specimens.OrderBy(s => s.LatinName).ToList();
            Assert.AreEqual("Equisetum species", Specimens[0].LatinName);
            Assert.AreEqual("Vaccinium myrtilloides", Specimens[4].LatinName);
        }

        /*
         * Check to see that the Specimen Index for Latin Name displays in Reverse Alphabetical Order
         * When Latin Name is selected
         */
        [Test]
        public void TestSpecimenLatinNameDisplaysInReverseAlphabeticalOrder()
        {
            Specimens = Specimens.OrderByDescending(s => s.LatinName).ToList();
            Assert.AreEqual("Vaccinium myrtilloides", Specimens[0].LatinName);
            Assert.AreEqual("Equisetum species", Specimens[4].LatinName);
        }

        /*
         * Check to see that the Specimen Index for Cree Name displays in Reverse Alphabetical Order
         * When Cree Name is selected
         */
        [Test]
        public void TestSpecimenCreeNameDisplaysInAlphabeticalOrder()
        {
            Specimens = Specimens.OrderBy(s => s.CreeName).ToList();
            Assert.AreEqual(null, Specimens[0].CreeName);
            Assert.AreEqual("Maskêkopakwa", Specimens[4].CreeName);
        }

        /*
         * Check to see that the Specimen Index for Cree Name displays in Reverse Alphabetical Order
         * When Cree Name is selected
        */
        [Test]
        public void TestSpecimenCreeNameDisplaysInReverseAlphabeticalOrder()
        {

            Specimens = Specimens.OrderByDescending(s => s.CreeName).ToList();
            Assert.AreEqual("Maskêkopakwa", Specimens[0].CreeName);
            Assert.AreEqual(null, Specimens[4].CreeName);
        }
        /*
         * Check to see that the index method in the mock controller returns an appropriate
         * view. If it does not return a view, it fails.
         * Source: https://stackoverflow.com/questions/27428796/how-do-i-get-the-result-or-return-value-of-a-task
         */
        [Test]
        public void TestHttpPostFunctionality()
        {
            // Obtain the Controller's index method results after being called. 
            var result = _controller.Index("Search", "Sort");
            
            //If the result returns a View, it passes and it it doesn't return the view it fails. 
            Assert.That(result.Result, Is.InstanceOf<ViewResult>());
        }
        #endregion

        #region QR Code Functional Tests

        private static readonly HttpClient httpClient = new HttpClient();

        [Test]
        public void TestThatViewQRCodeButtonDisplaysQRCode()
        {
            // Eventually want this URL as a global variable
            string url = "https://storiesoftheland.azurewebsites.net/Specimen/Details/1";

            // Make a request to the URL
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            // Ensure the request was successful
            Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

            // Read the HTML content from the response
            string htmlContent = response.Content.ReadAsStringAsync().Result;

            // Perform assertions or checks on the HTML content
            // May have to change this
            Assert.IsTrue(htmlContent.Contains("class=\"border\""), "Expected content not found in HTML");
            Assert.IsTrue(htmlContent.Contains("alt=\"Velvet Leaf Blueberry\""), "Expected content not found in HTML");

        }

        #endregion
        #region CreateSpecimen

        [Test]

        public void TestOpenCreatePage()
        {
            // Eventually want this URL as a global variable
            string url = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";

            // Make a request to the URL
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            // Ensure the request was successful
            Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {url}. Status code: {response.StatusCode}");

            // Read the HTML content from the response
            string htmlContent = response.Content.ReadAsStringAsync().Result;

            // Check for form and its elements
            Assert.IsTrue(htmlContent.Contains("form"), "Create form not found");
            Console.WriteLine(htmlContent);
            Assert.IsTrue(htmlContent.Contains(" <input class=\"form-control\" type=\"text\" data-val=\"true\" data-val-length=\"Name cannot be more than 50 characters\" data-val-length-max=\"50\" data-val-required=\"Latin Name is required\" id=\"LatinName\" maxlength=\"50\" name=\"LatinName\" value=\"\" />"), "Latin Name label text is incorrect or not found");
        }

        [Test]
        public void TestThatRepeatedLatinNameSpecimenWontBeSavedIntoTheDatabase()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string url = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";
                driver.Navigate().GoToUrl(url);

                void FillForm()
                {
                    driver.FindElement(By.Id("LatinName")).SendKeys("Vaccinium myrtilloides");
                    driver.FindElement(By.Id("EnglishName")).SendKeys("Test English Name");
                    driver.FindElement(By.Id("CreeName")).SendKeys("Test Cree Name");
                    driver.FindElement(By.Id("SpecimenDescription")).SendKeys("Test Specimen Description");
                    driver.FindElement(By.Id("CulturalSignificance")).SendKeys("Test Cultural Significance");
                }

                FillForm();

                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                string currentUrl = driver.Url; // Get the current URL before clicking the button

                var attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                Thread.Sleep(1000);

                // Check if the URL has changed
                Assert.AreEqual(currentUrl, driver.Url, "The page did not redirect after form submission.");

                driver.Quit();
            }
        }

        [Test]
        public void TestThatRepeatedEnglishNameSpecimenWontBeSavedIntoTheDatabase()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string url = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";
                driver.Navigate().GoToUrl(url);

                void FillForm()
                {
                    driver.FindElement(By.Id("LatinName")).SendKeys("Test English Name");
                    driver.FindElement(By.Id("EnglishName")).SendKeys("Lungwort");
                    driver.FindElement(By.Id("CreeName")).SendKeys("Test Cree Name");
                    driver.FindElement(By.Id("SpecimenDescription")).SendKeys("Test Specimen Description");
                    driver.FindElement(By.Id("CulturalSignificance")).SendKeys("Test Cultural Significance");
                }

                FillForm();

                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                string currentUrl = driver.Url; // Get the current URL before clicking the button

                var attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                Thread.Sleep(1000);

                // Check if the URL has changed
                Assert.AreEqual(currentUrl, driver.Url, "The page did not redirect after form submission.");

                driver.Quit();
            }
        }

        [Test]
        public void TestThatRepeatedCreeNameSpecimenWontBeSavedIntoTheDatabase()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string url = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";
                driver.Navigate().GoToUrl(url);

                void FillForm()
                {
                    driver.FindElement(By.Id("LatinName")).SendKeys("Test English Name");
                    driver.FindElement(By.Id("EnglishName")).SendKeys("Test English");
                    driver.FindElement(By.Id("CreeName")).SendKeys("Amiskowihkask");
                    driver.FindElement(By.Id("SpecimenDescription")).SendKeys("Test Specimen Description");
                    driver.FindElement(By.Id("CulturalSignificance")).SendKeys("Test Cultural Significance");
                }

                FillForm();

                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                string currentUrl = driver.Url; // Get the current URL before clicking the button

                var attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                Thread.Sleep(1000);

                // Check if the URL has changed
                Assert.AreEqual(currentUrl, driver.Url, "The page did not redirect after form submission.");

                driver.Quit();
            }
        }

        [Test]
        public void TestAddValidSpecimenRedirectsToIndex()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string url = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";
                driver.Navigate().GoToUrl(url);

                // Function to generate a random 4-character string
                string GenerateRandomString()
                {
                    var random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                }

                // Function to fill the form with random data
                void FillFormWithRandomData()
                {
                    driver.FindElement(By.Id("LatinName")).SendKeys(NewSpecimen.LatinName + GenerateRandomString());
                    driver.FindElement(By.Id("EnglishName")).SendKeys(NewSpecimen.EnglishName + GenerateRandomString());
                    driver.FindElement(By.Id("CreeName")).SendKeys(NewSpecimen.CreeName + GenerateRandomString());
                    driver.FindElement(By.Id("SpecimenDescription")).SendKeys(NewSpecimen.SpecimenDescription + GenerateRandomString());
                    driver.FindElement(By.Id("CulturalSignificance")).SendKeys(NewSpecimen.CulturalSignificance + GenerateRandomString());
                }

                FillFormWithRandomData();

                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                string currentUrl = driver.Url; // Get the current URL before clicking the button

                var attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                Thread.Sleep(1000);

                // Check if the URL has changed
                Assert.AreNotEqual(currentUrl, driver.Url, "The page did not redirect after form submission.");

                driver.Quit();
            }
        }
        [Test]
        public void TestAddValidSpecimenCountNumberOfSpecimens()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string listUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/";
                string createUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";

                // Navigate to the Specimen list page and count the current number of specimens
                driver.Navigate().GoToUrl(listUrl);
                var specimenRows = driver.FindElements(By.CssSelector("table tbody tr"));
                int initialCount = specimenRows.Count;

                // Navigate to the Create Specimen page
                driver.Navigate().GoToUrl(createUrl);

                // Function to generate a random 4-character string
                string GenerateRandomString()
                {
                    var random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                }

                // Function to fill the form with random data
                void FillFormWithRandomData()
                {
                    driver.FindElement(By.Id("LatinName")).SendKeys("Latin" + GenerateRandomString());
                    driver.FindElement(By.Id("EnglishName")).SendKeys("English" + GenerateRandomString());
                    driver.FindElement(By.Id("CreeName")).SendKeys("Cree" + GenerateRandomString());
                    driver.FindElement(By.Id("SpecimenDescription")).SendKeys("Description" + GenerateRandomString());
                    driver.FindElement(By.Id("CulturalSignificance")).SendKeys("Significance" + GenerateRandomString());
                }

                FillFormWithRandomData();

                // Submit the form
                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                var attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                // Wait for the redirect to complete or for a confirmation message
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.Url != createUrl);

                // Navigate back to the Specimen list page and count the specimens again
                driver.Navigate().GoToUrl(listUrl);
                specimenRows = driver.FindElements(By.CssSelector("table tbody tr"));
                int finalCount = specimenRows.Count;

                // Check if the count has increased by 1
                Assert.AreEqual(initialCount + 1, finalCount, "The count of specimens did not increase as expected.");

                driver.Quit();
            }
        }


        [Test]
        public void TestAddMultipleSpecimensCountNumberOfSpecimens()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string listUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/";
                string createUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";

                // Navigate to the Specimen list page and count the current number of specimens
                driver.Navigate().GoToUrl(listUrl);
                var specimenRows = driver.FindElements(By.CssSelector("table tbody tr"));
                int initialCount = specimenRows.Count;

                // Navigate to the Create Specimen page
                driver.Navigate().GoToUrl(createUrl);

                // Function to generate a random 4-character string
                string GenerateRandomString()
                {
                    var random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                }

                // Function to fill the form with random data
                void FillFormWithRandomData()
                {
                    driver.FindElement(By.Id("LatinName")).SendKeys("Latin" + GenerateRandomString());
                    driver.FindElement(By.Id("EnglishName")).SendKeys("English" + GenerateRandomString());
                    driver.FindElement(By.Id("CreeName")).SendKeys("Cree" + GenerateRandomString());
                    driver.FindElement(By.Id("SpecimenDescription")).SendKeys("Description" + GenerateRandomString());
                    driver.FindElement(By.Id("CulturalSignificance")).SendKeys("Significance" + GenerateRandomString());
                }

                FillFormWithRandomData();

                // Submit the form
                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                var attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                // Wait for the redirect to complete or for a confirmation message
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.Url != createUrl);

                //Go back to the form
                driver.Navigate().GoToUrl(createUrl);

                // Function to fill the form with random data


                FillFormWithRandomData();

                // Submit the form
                button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                // Wait for the redirect to complete or for a confirmation message
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.Url != createUrl);

                // Navigate back to the Specimen list page and count the specimens again
                driver.Navigate().GoToUrl(listUrl);
                specimenRows = driver.FindElements(By.CssSelector("table tbody tr"));
                int finalCount = specimenRows.Count;

                // Check if the count has increased by 1
                Assert.AreEqual(initialCount + 2, finalCount, "The count of specimens did not increase as expected.");

                driver.Quit();
            }
        }


        [Test]
        public void TestIntegrityRecordsSpecimen()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string listUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/";
                string createUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";

                // Navigate to the Specimen list page and count the current number of specimens
                driver.Navigate().GoToUrl(listUrl);
                var specimenRows = driver.FindElements(By.CssSelector("table tbody tr"));
                int FinalCount = specimenRows.Count + 1; // Assuming new ID will be the next sequential number

                // Navigate to the Create Specimen page
                driver.Navigate().GoToUrl(createUrl);

                // Function to generate a random 4-character string
                string GenerateRandomString()
                {
                    var random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                }

                NewSpecimen.LatinName = "Latin" + GenerateRandomString();
                NewSpecimen.EnglishName = "English" + GenerateRandomString();
                NewSpecimen.CreeName = "Cree" + GenerateRandomString();
                NewSpecimen.SpecimenDescription = "Description" + GenerateRandomString();
                NewSpecimen.CulturalSignificance = "Significance" + GenerateRandomString();

                // Function to fill the form with random data
                void FillFormWithRandomData()
                {
                    driver.FindElement(By.Id("LatinName")).SendKeys(NewSpecimen.LatinName);
                    driver.FindElement(By.Id("EnglishName")).SendKeys(NewSpecimen.EnglishName);
                    driver.FindElement(By.Id("CreeName")).SendKeys(NewSpecimen.CreeName);
                    driver.FindElement(By.Id("SpecimenDescription")).SendKeys(NewSpecimen.SpecimenDescription);
                    driver.FindElement(By.Id("CulturalSignificance")).SendKeys(NewSpecimen.CulturalSignificance);
                }

                FillFormWithRandomData();

                // Submit the form
                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                int attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }

                // Wait for the redirect to complete or for a confirmation message
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.Url != createUrl);

                // Navigate to the detail page of the newly added specimen
                detailUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/specimen/Details/" + FinalCount;

                driver.Quit();
            }

            // Use HttpClient to check the details of the created specimen
            HttpResponseMessage response = httpClient.GetAsync(detailUrl).Result;

            // Ensure the request was successful
            Assert.IsTrue(response.IsSuccessStatusCode, $"Failed to retrieve content from {detailUrl}. Status code: {response.StatusCode}");

            // Read the HTML content from the response
            string htmlContent = response.Content.ReadAsStringAsync().Result;

            // Perform assertions or checks on the HTML content
            Assert.IsTrue(htmlContent.Contains(NewSpecimen.EnglishName), "English Name was changed found in HTML");
            Assert.IsTrue(htmlContent.Contains(NewSpecimen.CreeName), "Cree Name was changed or not found in HTML");
            Assert.IsTrue(htmlContent.Contains(NewSpecimen.LatinName), "Latin Name was changed or not found in HTML");
            Assert.IsTrue(htmlContent.Contains(NewSpecimen.CulturalSignificance), "Cultural Significance was changed or not found in HTML");
            Assert.IsTrue(htmlContent.Contains(NewSpecimen.SpecimenDescription), "Description was changed or not found in HTML");
        }
        [Test]
        public void testThatEmptyFormIsNotValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                string createUrl = "https://storiesoftheland-app-20231207012.blackstone-8fc98515.canadacentral.azurecontainerapps.io/Specimen/Create";

                // Navigate to the Create Specimen page
                driver.Navigate().GoToUrl(createUrl);
                // Submit the form

                var button = driver.FindElement(By.CssSelector("button.btn.btn-secondary[type='submit']"));
                int attempts = 0;
                while (attempts < 3)
                {
                    try
                    {
                        button.Click();
                        break;
                    }
                    catch (ElementClickInterceptedException)
                    {
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        attempts++;
                    }
                }
                Thread.Sleep(1000);

                // Check if the URL has changed
                Assert.AreEqual(createUrl, driver.Url, "The page is not supposed to change");

                driver.Quit();
            }
        }

        #endregion
    }

}