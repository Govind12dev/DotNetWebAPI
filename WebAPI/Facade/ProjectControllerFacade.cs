using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Models.UIModels;

namespace WebAPI.Facade
{
    public class ProjectControllerFacade
    {
        DBEntities db = new DBEntities();
        UserController userController = new UserController();
        public Project Map(ProjectModel projectModel)
        {
            var project = new Project();
            if (projectModel != null)
            {
                project.ProjectName = projectModel.ProjectName;
                project.StartDate = projectModel.StartDate;
                project.EndDate = projectModel.EndDate;
                project.Priority = projectModel.Priority;
                project.ProjectID = projectModel.ProjectID;
            }
            return project;
        }

        public IQueryable<ProjectModel> ReverseMap(IEnumerable<Project> destination)
        {
            var projectModelList = new List<ProjectModel>();
            if (destination != null)
            {
                foreach (var d in destination)
                {
                    var p = new ProjectModel();
                    var user = db.Users.Where(u => u.ProjectID == d.ProjectID).ToList();
                    var task = db.Tasks.Where(t => t.ProjectID == d.ProjectID).ToList();
                    var status = task.Any(t => t.Status == "Completed") ? task.Count(t => t.Status.Contains("Completed")).ToString() : "0";
                    p.ProjectID = d.ProjectID;
                    p.ProjectName = d.ProjectName;
                    p.StartDate = d.StartDate;
                    p.EndDate = d.EndDate;
                    p.Priority = d.Priority;
                    p.ProjectManager = user.Count > 0 ? user[0].FirstName + " " + user[0].LastName : string.Empty;
                    p.UserID = user.Count > 0 ? user[0].UserID : 0;
                    p.NumberOfTasks = task.Count;
                    p.Status = status;
                    projectModelList.Add(p);
                }
            }
            return projectModelList.AsQueryable();
        }

        public void UpdateUser(int userid, int projectID)
        {
            if (db.Users.Count(e => e.UserID == userid) > 0)
            {
                var user = db.Users.Where(u => u.UserID == userid).FirstOrDefault();
                user.ProjectID = projectID;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}