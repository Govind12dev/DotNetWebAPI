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
using WebAPI.Facade;
using WebAPI.Models;
using WebAPI.Models.UIModels;

namespace WebAPI.Controllers
{
    public class TaskController : ApiController
    {
        private DBEntities db = new DBEntities();

        private TaskControllerFacade tm = new TaskControllerFacade();

        // GET: api/Task
        public IEnumerable<TaskModel> GetTasks()
        {
            var abc = tm.ReverseMap(db.Tasks);
            return abc;
        }

        // GET: api/Task/5     
        [ResponseType(typeof(TaskModel))]
        public IHttpActionResult GetTask(int id)
        {
            var tasks = db.Tasks.Where(t => t.ProjectID == id).ToList();
            if (tasks.Count == 0)
            {
                return null;
            }
            var taskModel = tm.ReverseMap(tasks);           
            return Ok(taskModel);
        }

        // PUT: api/Task/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, TaskModel taskModel)
        {
            if (id != taskModel.TaskID)
            {
                return BadRequest();
            }
            if (!TaskExists(id))
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(taskModel.ParentTask))
            {
                tm.UpdateParentTask(taskModel);
            }
            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Task
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(TaskModel taskModel)
        {
            if (taskModel.IsParentTask)
            {
                tm.CreateParentTask(taskModel.TaskName);
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                var task = tm.Map(taskModel);
                //Add Task Record
                db.Tasks.Add(task);
                db.SaveChanges();
                //Update User Record
                tm.UpdateUser(task.TaskID, taskModel.ProjectID, taskModel.UserID);
                return CreatedAtRoute("DefaultApi", new { id = task.TaskID }, task);
            }

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

            //db.Tasks.Remove(task);
            task.Status = "Completed";
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();


            return Ok(task);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskID == id) > 0;
        }
    }
}