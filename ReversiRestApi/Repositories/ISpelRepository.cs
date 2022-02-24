using System.Collections.Generic;

namespace ReversiRestApi.Repositories
{
    public interface ISpelRepository
    {
        void AddSpel(Spel spel);

        public List<Spel> GetSpellen();

        Spel GetSpel(string spelToken);
    }
}