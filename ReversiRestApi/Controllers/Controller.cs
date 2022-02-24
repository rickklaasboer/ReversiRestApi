﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ReversiRestApi.Controllers
{
    public class Controller : ControllerBase
    {
        public IActionResult JsonResponse(object input)
        {
            return Ok(JsonConvert.SerializeObject(input));
        }
    }
}