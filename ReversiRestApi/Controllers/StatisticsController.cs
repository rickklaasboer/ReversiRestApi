using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReversiRestApi.Repositories;

namespace ReversiRestApi.Controllers
{
    [ApiController]
    [Route("/api/statistics")]
    public class StatisticsController : Controller
    {
        private readonly IGameRepository _repository;

        public StatisticsController(IGameRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return JsonResponse(_repository.GetGames().Where(g => g.DidFinish).Select(g => new
            {
                g.Token,
                g.WinningPlayer,
                g.Player1Token,
                g.Player2Token
            }));
        }
    }
}