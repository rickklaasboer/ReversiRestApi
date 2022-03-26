using System.Collections.Generic;
using System.Linq;
using ReversiRestApi.DatabaseContexts;
using ReversiRestApi.Repositories;
using ReversiRestApi.Utility;

namespace ReversiRestApi.DAL
{
    public class GameAccessLayer : IGameRepository
    {
        private readonly ReversiDbContext _context;

        public GameAccessLayer(ReversiDbContext context)
        {
            _context = context;
        }

        public void AddGame(Game game)
        {
            Tap.Capture(_context, ctx => ctx.Games.Add(game)).SaveChanges();
        }

        public List<Game> GetGames()
        {
            return _context.Games.ToList();
        }

        public Game GetGame(string token)
        {
            return _context.Games.First(x => x.Token == token);
        }

        public void UpdateGame(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }
    }
}