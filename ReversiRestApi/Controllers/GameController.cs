using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReversiRestApi.Repositories;
using ReversiRestApi.Requests.Game;

namespace ReversiRestApi.Controllers
{
    [ApiController]
    [Route("/api/game")]
    public class GameController : Controller
    {
        private readonly IGameRepository _repository;

        public GameController(IGameRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get list of all games that are waiting for player(s)
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var result = _repository.GetGames()
                    .Where(s => s.Player1Token == null || s.Player2Token == null);
                return JsonResponse(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get game by token
        /// </summary>
        /// <param name="token"></param>
        [HttpGet("/{token}")]
        public IActionResult Get(string token)
        {
            try
            {
                var result = _repository.GetGame(token);
                return JsonResponse(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get game by player token
        /// </summary>
        [HttpGet("/player/{token}")]
        public IActionResult GetByPlayer(string token)
        {
            try
            {
                var result = _repository
                    .GetGames()
                    .First(s => s.Player1Token == token || s.Player2Token == token);
                return JsonResponse(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get player who's turn it is
        /// </summary>
        [HttpGet("/turn/{token}")]
        public IActionResult GetTurn(string token)
        {
            try
            {
                var result = _repository.GetGame(token);
                return JsonResponse(new
                {
                    Color = result.PlayerTurn
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] CreateGameRequest request)
        {
            if (request.Description == null || request.Player1Token == null)
            {
                return BadRequest();
            }

            var game = new Game
            {
                Token = Guid.NewGuid().ToString(),
                Player1Token = request.Player1Token,
                Description = request.Description,
            };
            _repository.AddGame(game);

            return JsonResponse(game);
        }

        /// <summary>
        /// Let player do a turn
        /// </summary>
        [HttpPut("/turn")]
        public IActionResult DoTurn([FromBody] DoTurnRequest request)
        {
            try
            {
                var result = _repository.GetGame(request.GameToken);
                // TODO: check if player that calls is actually the player who's turn it is
                result.MakeMove(request.Position.row, request.Position.column);
                
                return JsonResponse(new
                {
                    Success = true
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Let player skip a turn (if allowed)
        /// </summary>
        [HttpPut("/turn/abandon")]
        public IActionResult AbandonTurn([FromBody] AbandonTurnRequest request)
        {
            try
            {
                var result = _repository.GetGame(request.GameToken);
                // TODO: check if player that calls is actually the player who's turn it is
                result.Skip();
                
                return JsonResponse(new
                {
                    Success = true
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}