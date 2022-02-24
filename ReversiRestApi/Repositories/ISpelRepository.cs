using System.Collections.Generic;

namespace ReversiRestApi.Repositories
{
    public interface ISpelRepository
    {
        void AddSpel(Game game);

        public List<Game> GetSpellen();

        Game GetSpel(string spelToken);
    }
}