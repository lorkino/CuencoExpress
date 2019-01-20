using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Film.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Film.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : Controller
    {

        //public async Task<JsonResult> Profile([FromBody]  UserDates user)
        //{

        //}
    }
}