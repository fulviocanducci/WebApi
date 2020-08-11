using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DataServices;

namespace WebApi.Controllers
{
    /// <summary>
    /// Todos Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController()]
    [Authorize()]
    public class TodosController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RepositoryTodoImplementation Repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public TodosController(RepositoryTodoImplementation repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Todos - Get
        /// </summary>
        /// <returns>List of Todos</returns>
        [HttpGet]
        public IAsyncEnumerable<Todo> GetTodo()
        {
            return Repository.GetAsync();
        }

        /// <summary>
        /// Todos - Get By Id
        /// </summary>
        /// <param name="id">param int id</param>
        /// <returns>Todo class</returns>
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

        /// <summary>
        /// Todos - Put
        /// </summary>
        /// <param name="id">param int id</param>
        /// <param name="todo">param todo class</param>
        /// <returns>Todo class</returns>
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

        /// <summary>
        /// Todo - Post
        /// </summary>
        /// <param name="todo">Todo class</param>
        /// <returns>Todo class</returns>
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            await Repository.AddAsync(todo);
            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        /// <summary>
        /// Todo - Delete
        /// </summary>
        /// <param name="id">param int id</param>
        /// <returns>Todo class</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [NonAction()]
        private bool TodoExists(int id)
        {
            return Repository.Any(e => e.Id == id);
        }
    }
}
