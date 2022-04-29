using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ConfigTestController : ControllerBase
    {
        private IConfiguration _config;

        public ConfigTestController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return _config["sqlConnectionString"];
        }
    }
}
