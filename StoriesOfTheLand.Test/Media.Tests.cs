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
                SpecimenImagePath = new string[]
                {
                    "abc.png"
                },
                SpecimenAudioPath = "abc.mp3"

            };

        }
        #region mediamodel
        

        #endregion
    }
}
