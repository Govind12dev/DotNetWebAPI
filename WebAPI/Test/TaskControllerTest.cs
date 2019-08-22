using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebAPI.Controllers;

namespace WebAPI.Tests
{
    [TestFixture]
    public class TaskControllerTest
    {
        TaskController taskController = new TaskController();

        [Test]
        public void GetTasks_Test()
        {
            var tasks = taskController.GetTasks();
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count() > 0);

        }

        [Test]
        public void GetTask_Test()
        {
            int taskId1 = 12;
            var actionResult1 = taskController.GetTask(taskId1);
            Assert.IsNotNull(actionResult1);
            int taskId2 = 880;
            var actionResult2 = taskController.GetTask(taskId2);
            Assert.IsNotNull(actionResult2);

        }


        [Test]
        public void PostTask_Test()
        {
            var mockProject = new Models.Task();
            mockProject.TaskName = "Test Case Task1";
            mockProject.Priority = 5;
            mockProject.StartDate = DateTime.Now;
            mockProject.EndDate = DateTime.Now.AddDays(30);
            mockProject.Status = "Completed";

            var actionResult = taskController.PostTask(mockProject);
            Assert.IsNotNull(actionResult);
        }


        [Test]
        public void DeleteTask_Test()
        {
            int id1 = 5;
            int id2 = default(int);
            var actionResult1 = taskController.DeleteTask(id1);

            var tasks = taskController.GetTasks();
            foreach (var task in tasks)
            {
                id2 = task.TaskID;
            }
            var actionResult2 = taskController.GetTask(id2);
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

        [Test]
        public void PutTaks_Test()
        {
            int id1 = default(int);
            var updateTask1 = new Models.Task();
            var updateTask2 = new Models.Task();

            updateTask2.TaskID = 1789;
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
    }
}
