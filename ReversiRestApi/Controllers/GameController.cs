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
                    .Where(s => !s.DidFinish)
                    .Where(s => s.Player1Token == null || s.Player2Token == null);
                return JsonResponse(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Get game by token
        /// </summary>
        /// <param name="token"></param>
        [HttpGet("{token}")]
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
        /// Join a game
        /// </summary>
        /// <param name="token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{token}/join")]
        public IActionResult Join(string token, [FromBody] JoinGameRequest request)
        {
            var game = _repository.GetGame(token);

            if (game.DidFinish)
            {
                return BadRequest();
            }

            if (game.Player1Token == request.PlayerToken || game.Player2Token == request.PlayerToken)
            {
                return JsonResponse(game);
            }

            if (!(game.Player1Token == null || game.Player2Token == null))
            {
                return BadRequest("This game is full");
            }

            if (game.Player1Token == null)
            {
                game.Player1Token = request.PlayerToken;
            }
            else
            {
                game.Player2Token = request.PlayerToken;
            }

            _repository.UpdateGame(game);

            return JsonResponse(game);
        }

        /// <summary>
        /// Leave a game
        /// </summary>
        /// <param name="token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{token}/leave")]
        public IActionResult Leave(string token, [FromBody] LeaveGameRequest request)
        {
            var game = _repository.GetGame(token);

            // Cancel leave when game is finished
            if (game.DidFinish) return JsonResponse(game);

            if (!(game.Player1Token == request.PlayerToken || game.Player2Token == request.PlayerToken))
            {
                return BadRequest("You are not in this game");
            }

            if (game.Player1Token == request.PlayerToken)
            {
                game.Player1Token = null;
            }
            else
            {
                game.Player2Token = null;
            }

            _repository.UpdateGame(game);

            return JsonResponse(game);
        }

        /// <summary>
        /// Let player do a turn
        /// </summary>
        [HttpPut("turn")]
        public IActionResult DoTurn([FromBody] DoTurnRequest request)
        {
            try
            {
                var game = _repository.GetGame(request.GameToken);

                if (game.DidFinish)
                {
                    return JsonResponse(game);
                }

                if (game.PlayerTurn == Color.None)
                {
                    game.PlayerTurn = Color.White;
                }

                if (request.PlayerToken == game.Player1Token && game.PlayerTurn == Color.White)
                {
                    game.MakeMove(request.Row, request.Column);
                }

                if (request.PlayerToken == game.Player2Token && game.PlayerTurn == Color.Black)
                {
                    game.MakeMove(request.Row, request.Column);
                }
                
                if (game.Finished())
                {
                    var winningPlayerColor = game.WinningColor();

                    game.DidFinish = true;
                    game.WinningPlayerColor = winningPlayerColor;
                    game.WinningPlayer = winningPlayerColor switch
                    {
                        Color.None => null,
                        Color.White => game.Player1Token,
                        _ => game.Player2Token
                    };
                }

                _repository.UpdateGame(game);

                return JsonResponse(game);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Let player skip a turn (if allowed)
        /// </summary>
        [HttpPut("turn/abandon")]
        public IActionResult AbandonTurn([FromBody] AbandonTurnRequest request)
        {
            try
            {
                var game = _repository.GetGame(request.GameToken);

                if (!game.DidFinish)
                {
                    if (request.PlayerToken == game.Player1Token && game.PlayerTurn == Color.White)
                    {
                        game.Skip();
                    }

                    if (request.PlayerToken == game.Player2Token && game.PlayerTurn == Color.Black)
                    {
                        game.Skip();
                    }
                }

                _repository.UpdateGame(game);

                return JsonResponse(game);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}