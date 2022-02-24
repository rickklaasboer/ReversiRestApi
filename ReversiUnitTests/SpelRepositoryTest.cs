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
            var respository = new SpelRepository();
            var spel = new Spel
            {
                ID = -1,
                Omschrijving = "Test 1",
                Token = "Test_1_Token"
            };

            respository.AddSpel(spel);

            Assert.AreEqual(spel, respository.GetSpel("Test_1_Token"));
        }
        
        [Test]
        public void SpelRepositorty_GetSpellen_Haalt_Alle_Spellen_Op()
        {
            var respository = new SpelRepository();

            Assert.NotNull(respository.GetSpellen());
            Assert.IsInstanceOf<List<Spel>>(respository.GetSpellen());
        }

        [Test]
        public void SpelRepositorty_GetSpel_Haalt_Spel_Op()
        {
            var respository = new SpelRepository();
            var spel = new Spel
            {
                ID = -1,
                Omschrijving = "Test 1",
                Token = "Test_1_Token"
            };

            respository.AddSpel(spel);

            Assert.AreEqual(spel, respository.GetSpel("Test_1_Token"));
        }
        
    }
}