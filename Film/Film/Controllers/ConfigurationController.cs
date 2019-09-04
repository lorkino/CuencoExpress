using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Film.Controllers
{
    
    [ApiController, Route("api/config")]
    public class ConfigurationController : Controller
    {

        private readonly IConfiguration _config;

        public ConfigurationController(IConfiguration config)
        {
            _config = config;
        }
        // GET: /<controller>/
        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok(new { tokenDate = _config.GetSection("ServerInfo:ServerDate").Value });
        }
    }
}
