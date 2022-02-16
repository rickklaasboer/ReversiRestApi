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
        public ActionResult<IEnumerable<string>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            var results = _repository.GetSpellen()
                .Where((s) => s.Speler2Token == null)
                .Select((s) => s.Omschrijving);

            return new ActionResult<IEnumerable<string>>(results);
        }

        [HttpPost]
        public IActionResult MaakSpel([FromBody] SpelPostDataTransferObject body)
        {
            if (body.Omschrijving == null || body.Speler1Token == null)
            {
                return BadRequest();
            }

            var spel = new Spel
            {
                Token = Guid.NewGuid().ToString(),
                Speler1Token = body.Speler1Token,
                Omschrijving = body.Omschrijving,
            };
            _repository.AddSpel(spel);
            
            return Ok(spel.ToString());
        }
        
        [HttpGet("{token}")]
        public IActionResult GetSpel(string token)
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
    }
}