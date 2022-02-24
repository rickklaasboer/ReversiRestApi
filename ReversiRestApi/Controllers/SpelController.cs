using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReversiRestApi.DTO;
using ReversiRestApi.Repositories;

namespace ReversiRestApi.Controllers
{
    [ApiController]
    [Route("/api/spel")]
    public class SpelController : ControllerBase
    {
        private readonly ISpelRepository _repository;

        public SpelController(ISpelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return null;
        }

        [HttpGet("/{token}")]
        public IActionResult Get(string token)
        {
            try
            {
                var result = _repository.GetSpel(token);
                return Ok(result.ToString());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("/player/{token}")]
        public IActionResult GetByPlayer()
        {
            return null;
        }

        [HttpGet("/turn")]
        public IActionResult GetTurn()
        {
            return null;
        }

        [HttpPut("/turn")]
        public IActionResult ExecuteTurn()
        {
            return null;
        }

        [HttpPut("/turn/abandon")]
        public IActionResult AbandonTurn()
        {
            return null;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SpelPostDataTransferObject body)
        {
            if (body.Omschrijving == null || body.Speler1Token == null)
            {
                return BadRequest();
            }

            var spel = new Game
            {
                Token = Guid.NewGuid().ToString(),
                Player1Token = body.Speler1Token,
                Description = body.Omschrijving,
            };
            _repository.AddSpel(spel);

            return Ok(spel.ToString());
        }
    }
}