using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using test_aeb.Models;

namespace test_aeb.Controllers
{
    [Route("api/[controller]")]
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ToDo_model>>> GetTasks() 
        {
            return Ok(await _context.ToDo_models.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo_model>> GetTask(int id) 
        {
            var task = await _context.ToDo_models.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<List<ToDo_model>>> AddTask(ToDo_model task)
        {
            task.Create_Time = DateTime.UtcNow;
            task.status = Status.created;
            task.Due_Time = task.Due_Time.ToUniversalTime();
            _context.ToDo_models.Add(task);
            await _context.SaveChangesAsync();
            return Ok(await _context.ToDo_models.ToListAsync());
        }
        [HttpPut]
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ToDo_model>>> DeleteTask(int id)
        {
            var task = await _context.ToDo_models.FindAsync(id);
            if (task == null)
            {
                return NotFound("Задача с таким идентификатором не найдена");
            }
            _context.ToDo_models.Remove(task);
            await _context.SaveChangesAsync();
            return await _context.ToDo_models.ToListAsync();
        }
    }
}
