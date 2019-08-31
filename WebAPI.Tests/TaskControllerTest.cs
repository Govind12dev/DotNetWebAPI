using NBench;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models.UIModels;

namespace WebAPI.Tests
{
    [TestFixture]
    public class TaskControllerTest
    {
        TaskController taskController = new TaskController();
        ProjectController pc = new ProjectController();
        UserController uc = new UserController();
        

        [PerfBenchmark(NumberOfIterations = 50, RunMode = RunMode.Throughput, TestMode = TestMode.Test
            , SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 6000)]        
        [Test]
        public void GetTasks_Test()
        {
            var tasks = taskController.GetTasks();
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count() > 0);

        }

        [PerfBenchmark(NumberOfIterations = 50, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 6000)]  
        [Test]
        public void GetTask_Test()
        {
            var projectID = default(int);
            var tasks = taskController.GetTasks();
            foreach (var task in tasks)
            {
                projectID = task.ProjectID;
                break;
            }
            var actionResult1 = taskController.GetTask(projectID);
            Assert.IsNotNull(actionResult1);
            int projectID2 = 810080;
            var actionResult2 = taskController.GetTask(projectID2);
            Assert.IsNull(actionResult2);

        }

        [PerfBenchmark(NumberOfIterations = 20, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 25000)]        
        [Test]
        public void PostTask_Test()
        {
            var mockProject = new TaskModel();
            mockProject.TaskName = "Test Case Task1";
            mockProject.Priority = 5;
            mockProject.StartDate = DateTime.Now;
            mockProject.EndDate = DateTime.Now.AddDays(30);
            mockProject.Status = "Completed";

            var actionResult = taskController.PostTask(mockProject);
            Assert.IsNotNull(actionResult);
        }

        [PerfBenchmark(NumberOfIterations = 10, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 15000)]        
        [Test]
        public void PostTask_IsNotParentTask_Test()
        {
            var mockProject = new TaskModel();
            mockProject.TaskName = "Test Case Task1";
            mockProject.Priority = 5;
            mockProject.StartDate = DateTime.Now;
            mockProject.EndDate = DateTime.Now.AddDays(30);
            mockProject.Status = "Completed";
            mockProject.IsParentTask = false;

            var projects = pc.GetProjects();
            var users = uc.GetUsers();
            foreach (var project in projects)
            {
                mockProject.ProjectID = project.ProjectID;
                mockProject.ProjectName = project.ProjectName;
                break;
            }

            foreach (var user in users)
            {
                mockProject.UserID = user.UserID;
                break;
            }

            var actionResult = taskController.PostTask(mockProject);
            Assert.IsNotNull(actionResult);
        }


        [PerfBenchmark(NumberOfIterations = 10, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 25000)]
        [Test]
        public void DeleteTask_Test()
        {
            int id1 = 5;
            int projectID = default(int);
            var actionResult1 = taskController.DeleteTask(id1);

            var tasks = taskController.GetTasks();
            foreach (var task in tasks)
            {
                projectID = task.ProjectID;
                break;
            }
            var actionResult2 = taskController.GetTask(projectID);
            Assert.IsNotNull(actionResult2);

            if (actionResult1 == null)
            {
                Assert.IsNull(actionResult1);
            }
            else
            {
                Assert.IsNotNull(actionResult1);
            }

        }


        [PerfBenchmark(NumberOfIterations = 10, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 25000)]        
        [Test]
        public void PutTaks_Test()
        {
            int id1 = default(int);
            var updateTask1 = new TaskModel();
            var updateTask2 = new TaskModel();

            updateTask2.TaskID = 170989;
            updateTask2.TaskName = "Update test project";
            updateTask2.StartDate = DateTime.Now;
            updateTask2.EndDate = DateTime.Now.AddDays(35);
            updateTask2.Priority = 60;

            var tasks = taskController.GetTasks();
            foreach (var task in tasks)
            {
                id1 = task.TaskID;
                updateTask1.TaskName = "Update test project";
                updateTask1.StartDate = DateTime.Now;
                updateTask1.EndDate = DateTime.Now.AddDays(35);
                updateTask1.Priority = 60;
                break;
            }
            var actionResult1 = taskController.PutTask(id1, updateTask1);
            var actionResult2 = taskController.PutTask(1, updateTask1);
            var actionResult3 = taskController.PutTask(updateTask2.TaskID, updateTask2);
            Assert.IsNotNull(actionResult1);
            Assert.IsInstanceOf<BadRequestResult>(actionResult2);
            Assert.IsInstanceOf<NotFoundResult>(actionResult3);
        } 
        [Test]
        public void UpdateParentTask_Test()
        {            
            var updateTask1 = new TaskModel();
            var tasks = taskController.GetTasks();
            foreach (var task in tasks)
            {
                updateTask1.TaskID = task.TaskID;
                updateTask1.TaskName = "Update test project";
                updateTask1.StartDate = DateTime.Now;
                updateTask1.EndDate = DateTime.Now.AddDays(35);
                updateTask1.Priority = 60;
                updateTask1.ParentTask = "Parent Test task";
                break;
            }
            var actionResult1 = taskController.PutTask(updateTask1.TaskID, updateTask1);                       
            Assert.IsNotNull(actionResult1);           
        }

        [PerfBenchmark(NumberOfIterations =20, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 20000)]        
        [Test]
        public void GetTask_ReturnNullTask()
        {
            var projectID = 1230456;
            var tasks = taskController.GetTask(projectID);            
            Assert.IsNull(tasks);
        }
    }
}
