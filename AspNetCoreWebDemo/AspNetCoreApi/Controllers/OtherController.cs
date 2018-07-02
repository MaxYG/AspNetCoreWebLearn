using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class OtherController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        public string[] LoadedHostingStartupAssemblies { get; private set; }

        public OtherController(IConfiguration config,ILogger<OtherController> logger)
        {
            _config = config;
            _logger = logger;
            OnGet();
        }

        public void OnGet()
        {
            LoadedHostingStartupAssemblies =
                _config[WebHostDefaults.HostingStartupAssembliesKey]
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
        }

        [HttpGet]
        public string[] GetAll()
        {
            _logger.LogDebug("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            return LoadedHostingStartupAssemblies;
        }
    }
}