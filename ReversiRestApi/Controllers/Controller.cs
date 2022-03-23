using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ReversiRestApi.Controllers
{
    public class Controller : ControllerBase
    {
        public IActionResult JsonResponse(object input)
        {
            Response.Headers.Add("Content-Type", "application/json");
            return Ok(input);
        }
    }
}