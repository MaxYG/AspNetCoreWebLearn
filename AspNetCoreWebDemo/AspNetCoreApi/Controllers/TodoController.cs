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

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            
            _dbContext.TodoItems.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _dbContext.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _dbContext.TodoItems.Update(todo);
            _dbContext.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _dbContext.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _dbContext.TodoItems.Remove(todo);
            _dbContext.SaveChanges();
            return new NoContentResult();
        }
    }
}