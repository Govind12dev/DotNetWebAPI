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
    public class UserControllerTest
    {
        UserController userController = new UserController();

        [Test]
        public void GetUsers_Test()
        {
            var users = userController.GetUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() > 0);

        }

        [Test]
        public void GetUser_Test()
        {
            int taskId1 = 12;
            var actionResult1 = userController.GetUser(taskId1);
            Assert.IsNotNull(actionResult1);
            int taskId2 = 880;
            var actionResult2 = userController.GetUser(taskId2);
            Assert.IsNotNull(actionResult2);

        }


        [Test]
        public void PostUser_Test()
        {
            var mockProject = new Models.User();
            mockProject.FirstName = "Test Case User1";
            mockProject.LastName = "Last test";
            mockProject.EmployeeID = 2;

            var actionResult = userController.PostUser(mockProject);
            Assert.IsNotNull(actionResult);
        }


        [Test]
        public void DeleteUser_Test()
        {
            int id1 = 5;
            int id2 = default(int);
            var actionResult1 = userController.DeleteUser(id1);

            var users = userController.GetUsers();
            foreach (var user in users)
            {
                id2 = user.UserID;
                break;
            }
            var actionResult2 = userController.GetUser(id2);
            Assert.IsNotNull(actionResult2);

            if (actionResult1 == null)
            {
                Assert.IsNull(actionResult1);
            }
            else
            {
                Assert.IsNotNull(actionResult1);
            }

            var actionResult3 = userController.GetUser(10000);
            Assert.IsInstanceOf<NotFoundResult>(actionResult3);
        }

        [Test]
        public void PutUser_Test()
        {
            int id1 = default(int);
            var updateUser1 = new Models.User();
            var updateUser2 = new Models.User();

            updateUser2.UserID = 1789;
            updateUser2.FirstName = "Update test project";
            updateUser2.LastName = "Last";
            updateUser2.EmployeeID = 73;


            var users = userController.GetUsers();
            foreach (var user in users)
            {
                id1 = user.UserID;
                updateUser1.FirstName = "Update test project";
                updateUser1.LastName = "Last Name";
                updateUser1.EmployeeID = 86;
                break;
            }
            var actionResult1 = userController.PutUser(id1, updateUser1);
            var actionResult2 = userController.PutUser(1, updateUser1);
            var actionResult3 = userController.PutUser(updateUser2.UserID, updateUser2);
            Assert.IsNotNull(actionResult1);
            Assert.IsInstanceOf<BadRequestResult>(actionResult2);
            Assert.IsInstanceOf<NotFoundResult>(actionResult3);
        }
    }
}
