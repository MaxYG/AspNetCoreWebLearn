using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace AspNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class OtherController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<OtherController> _logger;
        private readonly IFileProvider _fileProvider;
        public string[] LoadedHostingStartupAssemblies { get; private set; }

        public OtherController(IConfiguration config,ILogger<OtherController> logger,IFileProvider fileProvider)
        {
            _config = config;
            _logger = logger;
            _fileProvider = fileProvider;
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
            _logger.LogInformation("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            return LoadedHostingStartupAssemblies;
        }

        [HttpGet("files")]
        public List<string> GetAllFiles()
        {
            var directoryNames=new List<string>();
            var contests=_fileProvider.GetDirectoryContents("");
            foreach (var item in contests)
            {
                if (item.IsDirectory)
                {
                    directoryNames.Add(item.Name);
                }
                else
                {
                    directoryNames.Add(item.Name+" - "+item.Length+"bytes");
                }
            }
            return directoryNames;

            // return contests.ToArray();
        }
    }
}