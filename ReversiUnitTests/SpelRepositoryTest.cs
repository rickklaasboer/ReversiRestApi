using System.Collections.Generic;
using NUnit.Framework;
using ReversiRestApi;
using ReversiRestApi.Repositories;

namespace ReversiUnitTests
{
    public class SpelRepositoryTest
    {
        [Test]
        public void SpelRepositorty_AddSpel_Voegt_Spel_Toe()
        {
            var respository = new GameRepository();
            var spel = new Game
            {
                // ID = -1,
                Description = "Test 1",
                Token = "Test_1_Token"
            };

            respository.AddGame(spel);

            Assert.AreEqual(spel, respository.GetGame("Test_1_Token"));
        }
        
        [Test]
        public void SpelRepositorty_GetSpellen_Haalt_Alle_Spellen_Op()
        {
            var respository = new GameRepository();

            Assert.NotNull(respository.GetGames());
            Assert.IsInstanceOf<List<Game>>(respository.GetGames());
        }

        [Test]
        public void SpelRepositorty_GetSpel_Haalt_Spel_Op()
        {
            var respository = new GameRepository();
            var spel = new Game
            {
                // ID = -1,
                Description = "Test 1",
                Token = "Test_1_Token"
            };

            respository.AddGame(spel);

            Assert.AreEqual(spel, respository.GetGame("Test_1_Token"));
        }
        
    }
}