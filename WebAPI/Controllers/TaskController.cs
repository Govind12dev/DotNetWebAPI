using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TaskController : ApiController
    {
        private DBEntities db = new DBEntities();

        // GET: api/Task
        public IQueryable<Task> GetTasks()
        {
            return db.Tasks;
        }

        // GET: api/Task/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Task/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            if (id != task.TaskID)
            {
                return BadRequest();
            }
            if (!TaskExists(id))
            {
                return NotFound();
            }

            db.Entry(task).State = EntityState.Modified;


            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Task
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
            db.Tasks.Add(task);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.TaskID }, task);
        }

        // DELETE: api/Task/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskID == id) > 0;
        }
    }
}