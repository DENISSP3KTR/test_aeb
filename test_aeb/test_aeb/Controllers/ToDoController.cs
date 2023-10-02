using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using test_aeb.Models;

namespace test_aeb.Controllers
{
    [Route("api/ToDo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDo_Context _context;
        private readonly IMapper _mapper;
        public ToDoController(ToDo_Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Gets the list of task
        /// </summary>
        /// <returns>Returns ToDo_model</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ToDo_model>>> GetTasks() 
        {
            return Ok(await _context.ToDo_models.ToListAsync());
        }

        /// <summary>
        /// Gets one task by id
        /// </summary>
        /// <param name="id">ToDo_model object id</param>
        /// <returns>Returns an element from ToDo_model</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ToDo_model>> GetTask(int id) 
        {
            var task = await _context.ToDo_models.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        /// <summary>
        /// Creates the task
        /// </summary>
        /// <param name="task">ToDo_model object</param>
        /// <returns>Returns ToDo_model the list</returns>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ToDo_model>>> AddTask(ToDo_model task)
        {
            task.Create_Time = DateTime.UtcNow;
            task.status = Status.created;
            task.Due_Time = task.Due_Time.ToUniversalTime();
            _context.ToDo_models.Add(task);
            await _context.SaveChangesAsync();
            return Ok(await _context.ToDo_models.ToListAsync());
        }

        /// <summary>
        /// Update а task
        /// </summary>
        /// <param name="request">ToDo object</param>
        /// <returns>Returns ToDo_model the list</returns>
        /// <response code="204">Success</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ToDo_model>>> UpdateTask(ToDo_model request)
        {
            var task = await _context.ToDo_models.FindAsync(request.Id);
            if (task == null)
            {
                return NotFound("Задача с таким идентификатором не найдена");
            }
            task.Title = request.Title;
            task.Description = request.Description;
            task.status = request.status;
            task.Create_Time = request.Create_Time;
            task.Due_Time = request.Due_Time.ToUniversalTime();
            if (request.status == Status.completed)
            {
                task.Completion_Time = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return Ok(await _context.ToDo_models.ToListAsync());
        }

        /// <summary>
        /// Deleting а task
        /// </summary>
        /// <param name="id">ToDo_model object id</param>
        /// <returns>Returns ToDo_model the list</returns>
        /// <response code="204">Success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<ToDo_model>>> DeleteTask(int id)
        {
            var task = await _context.ToDo_models.FindAsync(id);
            if (task == null)
            {
                return NotFound("Задача с таким идентификатором не найдена");
            }
            _context.ToDo_models.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
