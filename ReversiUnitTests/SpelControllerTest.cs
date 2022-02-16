using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using ReversiRestApi;
using ReversiRestApi.Controllers;
using ReversiRestApi.DTO;
using ReversiRestApi.Repositories;

namespace ReversiUnitTests
{
    public class SpelControllerTest
    {
        [Test]
        public void SpelController_GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler_Werkt()
        {
            var controller = new SpelController(new SpelRepository());
            var result = controller.GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler();

            Assert.NotNull(result);

            Assert.That(result.Value.ToList(), Is.EquivalentTo(new List<string>
            {
                "Potje snel reveri, dus niet lang nadenken",
                "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander"
            }));
        }

        [Test]
        public void SpelController_MaakSpel_Maakt_Spel()
        {
            var repository = new SpelRepository();
            var controller = new SpelController(repository);

            var response = controller.MaakSpel(new SpelPostDataTransferObject
            {
                Omschrijving = "Test",
                Speler1Token = "Test"
            });

            var actualResult = (OkObjectResult)response;
            dynamic spel = JsonConvert.DeserializeObject((string) actualResult.Value);

            Assert.AreEqual(actualResult.Value, JsonConvert.SerializeObject(repository.GetSpel((string) spel.Token)));
        }
        
        [Test]
        public void SpelController_GetSpel_Verkrijgt_Spel()
        {
            var repository = new SpelRepository();
            var controller = new SpelController(repository);


            var spel = new Spel()
            {
                Token = "kaas",
            };
            repository.AddSpel(spel);

            var response = controller.GetSpel("kaas");
            
            Assert.AreEqual(((OkObjectResult)response).Value, spel.ToString());
        }
    }
}