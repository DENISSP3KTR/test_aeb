using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TestAEB.Models;

namespace TestAEB.Controllers
{
    /// <summary>
    /// Сontroller for the task list
    /// </summary>
    [Route("api/ToDo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Interface ToDo
        /// </summary>
        /// <param name="context">passes the context of the task list</param>
        /// <param name="mapper"></param>
        public ToDoController(ToDoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets the list of task
        /// </summary>
        /// <returns>Returns ToDoModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ToDoModel>>> GetTasks() 
        {
            return Ok(await _context.ToDoModels.ToListAsync());
        }

        /// <summary>
        /// Gets one task by id
        /// </summary>
        /// <param name="id">ToDoModel object id</param>
        /// <returns>Returns an element from ToDoModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ToDoModel>> GetTask(int id) 
        {
            var task = await _context.ToDoModels.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        /// <summary>
        /// Creates the task
        /// </summary>
        /// <param name="task">ToDoModel object</param>
        /// <returns>Returns ToDoModel the list</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ToDoModel>>> AddTask(ToDoModel task)
        {
            task.CreateTime = DateTime.UtcNow;
            task.Status = status.created;
            task.DueTime = task.DueTime.ToUniversalTime();
            _context.ToDoModels.Add(task);
            await _context.SaveChangesAsync();
            return Ok(await _context.ToDoModels.ToListAsync());
        }

        /// <summary>
        /// Update а task
        /// </summary>
        /// <param name="request">ToDo object</param>
        /// <returns>Returns ToDoModel the list</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ToDoModel>>> UpdateTask(ToDoModel request)
        {
            var task = await _context.ToDoModels.FindAsync(request.Id);
            if (task == null)
            {
                return NotFound("Задача с таким идентификатором не найдена");
            }
            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.CreateTime = request.CreateTime;
            task.DueTime = request.DueTime.ToUniversalTime();
            if (request.Status == status.completed)
            {
                task.CompletionTime = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return Ok(await _context.ToDoModels.ToListAsync());
        }

        /// <summary>
        /// Deleting а task
        /// </summary>
        /// <param name="id">ToDoModel object id</param>
        /// <returns>Returns ToDoModel the list</returns>
        /// <response code="204">Success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<ToDoModel>>> DeleteTask(int id)
        {
            var task = await _context.ToDoModels.FindAsync(id);
            if (task == null)
            {
                return NotFound("Задача с таким идентификатором не найдена");
            }
            _context.ToDoModels.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
