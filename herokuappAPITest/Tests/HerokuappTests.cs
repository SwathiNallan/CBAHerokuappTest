using System;
using System.Configuration;
using System.Linq;
using System.Text;
using herokuapp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace herokuappAPITest
{
    [TestClass]
    public class HerokuappTests
    {
        HerokuappAPIHelper helper;
        public HerokuappTests()
        {
            helper = new HerokuappAPIHelper();
            var env = ConfigurationManager.AppSettings["env"];
            switch (env)
            {
                case "prod":
                    helper.InitRestClient(ConfigurationManager.AppSettings["produrl"]);
                    break;

                case "local":
                    helper.InitRestClient(ConfigurationManager.AppSettings["localurl"]);
                    break;

                default:
                    break;
            }
        }

        [TestMethod]
        public void ValidateGetUsers()
        {
            var usersList = helper.GetUsers();
            Assert.IsTrue(usersList.Count > 0, "failed to get users list");
        }

        private string GetRandomUserName()
        {
            var builder = new StringBuilder(10);
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < 10; i++)
            {
                var @char = (char)new Random().Next('a', 'a' + lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString();
        }

        [TestMethod]
        public void ValidateSaveUsers()
        {
            string randomName = GetRandomUserName();
            var user = new User()
            {
                username = randomName,
                score = new Random().Next(100, 1000)
            };

            var users = helper.SaveUser(user);
            Assert.AreEqual("success", users["status"], "failed to validate status on save user");
            Assert.AreEqual("User added.", users["message"], "failed to validate message on save user");
        }

        [TestMethod]
        public void ValidateUpdateUsers()
        {
            User useroldValue = helper.GetUsers()[0];
            User user = helper.GetUsers()[0];
            user.score = new Random().Next(100, 1000);
            Assert.IsTrue(helper.UpdateUser(user),"Failed to update user");
            var updatedUser = helper.GetUsers().Where(a=> a.user_id == user.user_id).FirstOrDefault();
            Assert.AreNotEqual(useroldValue.score, updatedUser.score, "Failed to update score");
            Assert.AreEqual(user.score, updatedUser.score, "Failed to update score");
        }
    }
}
