using Microsoft.AspNetCore.Mvc;
using NHibernate;
using TaskAPI.Models;
using Task = TaskAPI.Models.Task;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {

        public TaskController()
        {
        }

        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<Task>> GetTasks(string keyword, bool? isCompleted)
        {
            using (var session = FluentHibernateHelper.TaskSession())
            {
                var query = session.Query<Task>();

                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(t => (t.Title != null) && t.Title.Contains(keyword));
                }

                if (isCompleted.HasValue)
                {
                    query = query.Where(t => t.IsComplete == isCompleted.Value);
                }

                return Ok(query.ToList());
            }
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult<Task> CreateTask(Task task)
        {
            using (var session = FluentHibernateHelper.TaskSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(task);
                transaction.Commit();
                return CreatedAtAction(nameof(GetTaskDetail), new { id = task.Id }, task);
            }
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public ActionResult<Task> DeleteTask(int id)
        {
            using (var session = FluentHibernateHelper.TaskSession())
            using (var transaction = session.BeginTransaction())
            {
                var task = session.Get<Task>(id);
                if (task == null)
                    return NotFound();

                session.Delete(task);
                transaction.Commit();
                return Ok(task);
            }
        }

        
        // GET: api/tasks/{id}
        [HttpGet("{Id}")]
        public ActionResult<Task> GetTaskDetail(int id)
        {
            using (var session = FluentHibernateHelper.TaskSession())
            {
                var task = session.Get<Task>(id);
                if (task == null)
                    return NotFound();


                return Ok(task);
            }
        }
    }
}
