using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreApiData;
using AspNetCoreData;
using Microsoft.AspNetCore.Mvc;
using TodoItem = AspNetCoreApiData.TodoItem;

namespace AspNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ApiDbcontext _dbContext;
        public TodoController(ApiDbcontext dbContext)
        {
            _dbContext = dbContext;
            if (!_dbContext.TodoItems.Any())
            {
                _dbContext.TodoItems.Add(new TodoItem { Name = "Item1" });
                _dbContext.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            var result = _dbContext.TodoItems.ToList();

            return result;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var result = _dbContext.TodoItems.FirstOrDefault(x => x.Id == id);
            if (result==null)
            {
                return NotFound();
            }
            return new ObjectResult(result);
        }


    }
}