using System.Collections.Generic;

namespace ReversiRestApi.Repositories
{
    public interface IGameRepository
    {
        void AddGame(Game game);

        public List<Game> GetGames();

        Game GetGame(string token);

        void UpdateGame(Game game);
    }
}