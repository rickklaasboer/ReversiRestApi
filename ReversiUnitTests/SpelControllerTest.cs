using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using ReversiRestApi;
using ReversiRestApi.Controllers;
using ReversiRestApi.Repositories;
using ReversiRestApi.Requests.Game;

namespace ReversiUnitTests
{
    public class SpelControllerTest
    {
        [Test]
        public void SpelController_MaakSpel_Maakt_Spel()
        {
            var repository = new GameRepository();
            var controller = new GameController(repository);

            var response = controller.Create(new CreateGameRequest()
            {
                Description = "Test",
                Player1Token = "Test"
            });

            var actualResult = (OkObjectResult)response;
            dynamic spel = JsonConvert.DeserializeObject((string)actualResult.Value);

            Assert.AreEqual(actualResult.Value, JsonConvert.SerializeObject(repository.GetGame((string)spel.Token)));
        }

        [Test]
        public void SpelController_GetSpel_Verkrijgt_Spel()
        {
            var repository = new GameRepository();
            var controller = new GameController(repository);


            var spel = new Game()
            {
                Token = "kaas",
            };
            repository.AddGame(spel);

            var response = controller.Get("kaas");

            Assert.AreEqual(((OkObjectResult)response).Value, spel.ToString());
        }
    }
}