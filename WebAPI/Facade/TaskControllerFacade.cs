using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPI.Models;
using WebAPI.Models.UIModels;

namespace WebAPI.Facade
{
    public class TaskControllerFacade
    {
        DBEntities db = new DBEntities();
        public Task Map(TaskModel taskModel)
        {
            var task = new Task();
            //Add Parent Record if it has value
            var parentID = CreateParentTask(taskModel.ParentTask);
            if (taskModel != null)
            {
                task.TaskName = taskModel.TaskName;
                task.StartDate = taskModel.StartDate;
                task.EndDate = taskModel.EndDate;
                task.Priority = taskModel.Priority;
                task.ProjectID = taskModel.ProjectID;
                task.ParentID = parentID;
                task.TaskID = taskModel.TaskID;
                task.Status = taskModel.Status;
            }
            return task;
        }

        public IQueryable<TaskModel> ReverseMap(IEnumerable<Task> destination)
        {
            var taskModelList = new List<TaskModel>();
            if (destination != null)
            {
                foreach (var d in destination)
                {
                    var t = new TaskModel();
                    var parentTask = db.ParentTasks.Where(pt => pt.ParentID == d.ParentID).ToList();
                    var user = db.Users.Where(u => u.ProjectID == d.ProjectID).ToList();
                    var project = db.Projects.Where(p => p.ProjectID == d.ProjectID).ToList();
                    var IsParentTask = parentTask.Count > 0;

                    t.ProjectID = project[0].ProjectID;
                    t.ProjectName = project[0].ProjectName;
                    t.TaskID = d.TaskID;
                    t.TaskName = d.TaskName;
                    t.IsParentTask = IsParentTask;
                    t.Priority = d.Priority;
                    t.ParentTask = IsParentTask ? parentTask[0].ParentTaskName : "No Parent Task";
                    t.ParentTaskID = IsParentTask ? parentTask[0].ParentID : default(int);
                    t.StartDate = d.StartDate;
                    t.EndDate = d.EndDate;
                    t.UserID = user.Count > 0 ? user[0].UserID : default(int);
                    t.UserName = user.Count > 0 ? user[0].FirstName + ' ' + user[0].LastName : string.Empty;
                    t.Status = d.Status;
                    taskModelList.Add(t);
                }
            }
            return taskModelList.AsQueryable();
        }

        public int CreateParentTask(string parentTaskName)
        {
            if (!string.IsNullOrEmpty(parentTaskName))
            {
                var parentTask = new ParentTask();
                parentTask.ParentTaskName = parentTaskName;
                db.ParentTasks.Add(parentTask);
                db.SaveChanges();
                return parentTask.ParentID;
            }
            return default(int);//No parent task.

        }

        public void UpdateParentTask(TaskModel taskModel)
        {
            var parentTask = new ParentTask();
            parentTask.ParentTaskName = taskModel.ParentTask;
            db.ParentTasks.Add(parentTask);
            db.SaveChanges();

            var parentID = parentTask.ParentID;

            taskModel.ParentTaskID = parentID;
            db.Entry(taskModel).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateUser(int taskID, int projectID, int userID)
        {
            if (db.Users.Count(e => e.UserID == userID) > 0)
            {
                var user = db.Users.Where(u => u.UserID == userID).FirstOrDefault();
                user.ProjectID = projectID;
                user.TaskID = taskID;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}