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

        public ToDoController(ToDo_Context context)
        {
            _context = context;
        }

        //private static List<ToDo_model> Task = new List<ToDo_model>
        //    {
        //        new ToDo_model { Id = 1,
        //            Title="Сделай тестовое задание",
        //            Description="Тестовое задание для АЭБ",
        //            Create_Time = DateTime.Now,
        //            Due_Time = new DateTime(2023, 10, 2),
        //            Completion_Time = new DateTime(2023, 10, 1),
        //            status = Status.in_work
        //        }
        //    };
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
                return BadRequest("Задача с таким идентификатором не найдена");
            }
            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<List<ToDo_model>>> AddTask(ToDo_model task)
        {
            task.Create_Time = DateTime.UtcNow;
            task.status = Status.created;
            task.Due_Time = task.Due_Time.ToUniversalTime();
            task.Completion_Time = task.Completion_Time.ToUniversalTime();
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
                return BadRequest("Задача с таким идентификатором не найдена");
            }
            task.Title = request.Title;
            task.Description = request.Description;
            task.status = request.status;
            task.Create_Time = request.Create_Time;
            task.Due_Time = request.Due_Time.ToUniversalTime();
            task.Completion_Time = request.Completion_Time.ToUniversalTime();

            await _context.SaveChangesAsync();

            return Ok(await _context.ToDo_models.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ToDo_model>>> DeleteTask(int id)
        {
            var task = await _context.ToDo_models.FindAsync(id);
            if (task == null)
            {
                return BadRequest("Задача с таким идентификатором не найдена");
            }
            _context.ToDo_models.Remove(task);
            await _context.SaveChangesAsync();
            return await _context.ToDo_models.ToListAsync();
        }
    }
}
