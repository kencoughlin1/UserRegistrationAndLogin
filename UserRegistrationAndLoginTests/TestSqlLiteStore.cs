using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistrationAndLogin;

namespace UserRegistrationAndLoginTests
{
    [TestClass]
    public class TestSqlLiteStore
    {
        SqlLiteStore sqlLite;
        [TestInitialize]
        public void Setup()
        {
            sqlLite = new SqlLiteStore();
            sqlLite.CreateSqlLiteStore();
            string HashedPassword = BCrypt.Net.BCrypt.HashPassword("TestPassword", 10);
            UserStateModel userModel = new UserStateModel("newuser@newuser.com", HashedPassword, UserStateModel.RegistrationStateEnum.NotVerified);
            sqlLite.UpsertUser(userModel);

        }

        [TestCleanup]
        public void ClearDown()
        {
            sqlLite.CleanDownSqlLiteStore();
        }

        [TestMethod]
        public void TestUserStateModelIsNotNull()
        {
            Assert.IsNotNull(sqlLite.GetUserModel("Dosent Exist User"));
        }

        [TestMethod]
        public void TestUserStateModelIsNotFound()
        {
            Assert.IsFalse(sqlLite.GetUserModel("Dosent Exist User").UserFound);
        }

        [TestMethod]
        public void TestUserStateModelIsFound()
        {
            Assert.IsTrue(sqlLite.GetUserModel("newuser@newuser.com").UserFound);
        }

        [TestMethod]
        public void TestUserStateModelIsFoundAndVerified()
        {
            string HashedPassword = BCrypt.Net.BCrypt.HashPassword("TestPassword", 10);
            UserStateModel userModel = new UserStateModel("newuser@newuser.com", HashedPassword, UserStateModel.RegistrationStateEnum.Verified);
            sqlLite.UpsertUser(userModel);
            Assert.IsTrue(sqlLite.GetUserModel("newuser@newuser.com").RegistrationState == UserStateModel.RegistrationStateEnum.Verified);
        }



    }
}
