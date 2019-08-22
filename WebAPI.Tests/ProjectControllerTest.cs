using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Tests
{
    [TestFixture]
    public class ProjectControllerTest
    {
        ProjectController projectController = new ProjectController();

        [Test]
        public void GetProjects_Test()
        {
            var projects = projectController.GetProjects();
            Assert.IsNotNull(projects);
            Assert.IsTrue(projects.Count() > 0);
        }


        [Test]
        public void GetProject_Test()
        {
            int projId1 = 12;
            var actionResult1 = projectController.GetProject(projId1);
            Assert.IsNotNull(actionResult1);
            int projId2 = 880;
            var actionResult2 = projectController.GetProject(projId2);
            Assert.IsNotNull(actionResult2);

        }

        [Test]
        public void PostProject()
        {
            var mockProject = new Project();
            mockProject.ProjectName = "Test Case Project1";
            mockProject.Priority = 5;
            mockProject.StartDate = DateTime.Now;
            mockProject.EndDate = DateTime.Now.AddDays(30);

            var actionResult = projectController.PostProject(mockProject);
            Assert.IsNotNull(actionResult);
        }

        [Test]
        public void DeleteProject_Test()
        {
            int id1 = 5;
            int id2 = default(int);
            var actionResult = projectController.DeleteProject(id1);

            var projects = projectController.GetProjects();
            foreach (var project in projects)
            {
                id2 = project.ProjectID;
                break;
            }
            var actionResult1 = projectController.DeleteProject(id2);
            Assert.IsNotNull(actionResult1);

            if (actionResult == null)
            {
                Assert.IsNull(actionResult);
            }
            else
            {
                Assert.IsNotNull(actionResult);
            }
        }

        [Test]
        public void PutProject_Test()
        {
            int id1 = default(int);
            var updateProject = new Project();
            var updateProject1 = new Project();

            updateProject1.ProjectID = 1789;
            updateProject1.ProjectName = "Update test project";
            updateProject1.StartDate = DateTime.Now;
            updateProject1.EndDate = DateTime.Now.AddDays(35);
            updateProject1.Priority = 60;

            var projects = projectController.GetProjects();
            foreach (var project in projects)
            {
                id1 = project.ProjectID;
                updateProject.ProjectName = "Update test project";
                updateProject.StartDate = DateTime.Now;
                updateProject.EndDate = DateTime.Now.AddDays(35);
                updateProject.Priority = 60;
                break;
            }
            var actionResult1 = projectController.PutProject(id1, updateProject);
            var actionResult2 = projectController.PutProject(1, updateProject);
            var actionResult3 = projectController.PutProject(updateProject1.ProjectID, updateProject1);
            Assert.IsNotNull(actionResult1);
            Assert.IsInstanceOf<BadRequestResult>(actionResult2);
            Assert.IsInstanceOf<NotFoundResult>(actionResult3);
        }
    }
}
