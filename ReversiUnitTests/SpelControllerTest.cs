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
        public void SpelController_MaakSpel_Maakt_Spel()
        {
            var repository = new SpelRepository();
            var controller = new SpelController(repository);

            var response = controller.Create(new SpelPostDataTransferObject
            {
                Omschrijving = "Test",
                Speler1Token = "Test"
            });

            var actualResult = (OkObjectResult)response;
            dynamic spel = JsonConvert.DeserializeObject((string)actualResult.Value);

            Assert.AreEqual(actualResult.Value, JsonConvert.SerializeObject(repository.GetSpel((string)spel.Token)));
        }

        [Test]
        public void SpelController_GetSpel_Verkrijgt_Spel()
        {
            var repository = new SpelRepository();
            var controller = new SpelController(repository);


            var spel = new Game()
            {
                Token = "kaas",
            };
            repository.AddSpel(spel);

            var response = controller.Get("kaas");

            Assert.AreEqual(((OkObjectResult)response).Value, spel.ToString());
        }
    }
}