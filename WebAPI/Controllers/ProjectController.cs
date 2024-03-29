﻿using System;
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
    public class ProjectController : ApiController
    {
        private DBEntities db = new DBEntities();

        private ProjectControllerFacade pm = new ProjectControllerFacade();
        private UserController uc = new UserController();

        // GET: api/Project
        public IEnumerable<ProjectModel> GetProjects()
        {
            var abc = pm.ReverseMap(db.Projects);
            return abc;
        }

        // GET: api/Project/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Project/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, ProjectModel projectModel)
        {
            if (id != projectModel.ProjectID)
            {
                return BadRequest();
            }

            if (!ProjectExists(id))
            {
                return NotFound();
            }
            var project = pm.Map(projectModel);
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            pm.UpdateUser(projectModel.UserID, project.ProjectID);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Project
        //[ResponseType(typeof(ProjectModel))]
        public IHttpActionResult PostProject(ProjectModel projectModel)
        {
            var project = pm.Map(projectModel);
            db.Projects.Add(project);
            db.SaveChanges();

            pm.UpdateUser(projectModel.UserID, project.ProjectID);

            return CreatedAtRoute("DefaultApi", new { id = project.ProjectID }, project);
        }

        // DELETE: api/Project/5
        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            project.Priority = default(int);//Consider suspended with 0 priority
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(project);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ProjectID == id) > 0;
        }
    }
}