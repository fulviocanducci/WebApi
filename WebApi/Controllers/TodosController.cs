using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DataServices;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class TodosController : ControllerBase
    {
        private readonly RepositoryTodoImplementation Repository;

        public TodosController(RepositoryTodoImplementation repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public IAsyncEnumerable<Todo> GetTodo()
        {
            return Repository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            Todo todo = await Repository.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            bool status;
            try
            {
                status = await Repository.EditAsync(todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status, todo });
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            await Repository.AddAsync(todo);
            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            Todo todo = await Repository.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            await Repository.DeleteAsync(todo);
            return todo;
        }

        private bool TodoExists(int id)
        {
            return Repository.Any(e => e.Id == id);
        }
    }
}
