using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Film.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PublicKeyController : ControllerBase
    {
        private readonly PushNotificationsOptions _options;

        protected PublicKeyController(IOptions<PushNotificationsOptions> options)
        {
            _options = options.Value;
        }

        protected ContentResult Get()
        {
            return Content(_options.PublicKey, "text/plain");
        }
    }
}
