using System.Collections.Generic;
using System.Linq;

namespace ReversiRestApi.Repositories
{
    public class GameRepository : IGameRepository
    {
        // Lijst met tijdelijke spellen
        public List<Game> Spellen { get; set; }

        public GameRepository()
        {
            Game spel1 = new Game();
            Game spel2 = new Game();
            Game spel3 = new Game();
            
            spel1.Player1Token = "abcdef";
            spel1.Description = "Potje snel reveri, dus niet lang nadenken";
            spel2.Player1Token = "ghijkl";
            spel2.Player2Token = "mnopqr";
            spel2.Description = "Ik zoek een gevorderde tegenspeler!";
            spel3.Player1Token = "stuvwx";
            spel3.Description = "Na dit spel wil ik er nog een paar spelen tegen zelfde tegenstander";

            Spellen = new List<Game> { spel1, spel2, spel3 };
        }

        public void AddGame(Game game)
        {
            Spellen.Add(game);
        }

        public List<Game> GetGames()
        {
            return Spellen;
        }

        public Game GetGame(string spelToken)
        {
            return Spellen.First(s => s.Token == spelToken);
        }

        public void UpdateGame(Game game)
        {
            throw new System.NotImplementedException();
        }
    }
}