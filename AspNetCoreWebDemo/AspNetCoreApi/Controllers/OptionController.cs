using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspNetCoreApi.Options;
using AspNetCoreApiData;
using AspNetCoreData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TodoItem = AspNetCoreApiData.TodoItem;

namespace AspNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class OptionController : Controller
    {
        private readonly MyOptions _myOptions;
        private readonly MyOptionsWithDelegateConfig _myOptionsWithDelegateConfig;
        private readonly MySnapshotOptions _snapshotOptions;

        public OptionController(IOptions<MyOptions> optionAccessor,
            IOptions<MyOptionsWithDelegateConfig> myOptionsWithDelegateConfig,
            IOptionsSnapshot<MySnapshotOptions> snapshotOptionsAccessor)
        {
            _myOptions = optionAccessor.Value;
            _myOptionsWithDelegateConfig = myOptionsWithDelegateConfig.Value;
            _snapshotOptions = snapshotOptionsAccessor.Value;
        }

        [HttpGet]
        public IEnumerable<MyOptions> GetAll()
        {
            var myOptions=new List<MyOptions>(){_myOptions};
            
            return myOptions;
        }

       
        [HttpGet("delegate")]
        public IEnumerable<MyOptionsWithDelegateConfig> GetDelegateAll()
        {
            var myOptions = new List<MyOptionsWithDelegateConfig>() { _myOptionsWithDelegateConfig };

            return myOptions;
        }

        [HttpGet("snapshot")]
        public IEnumerable<MySnapshotOptions> GetSnapshotAll()
        {
            var myOptions = new List<MySnapshotOptions>() { _snapshotOptions };

            return myOptions;
        }


    }
}