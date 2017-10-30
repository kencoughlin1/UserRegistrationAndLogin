using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistrationAndLogin;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        SqlLiteStore sqlLite;
        [TestInitialize]
        public void Setup()
        {
            sqlLite = new SqlLiteStore();
            sqlLite.CreateSqlLiteStore();

        }


        [TestMethod]
        public void TestEmptyListIsNotNull()
        {
            Assert.IsNotNull(sqlLite.GetUserModel("Dosent Exist User"));
        }





    }
}
