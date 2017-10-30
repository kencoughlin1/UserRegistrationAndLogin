using System;

namespace UserRegistrationAndLogin
{
    public class Register
    {
        private SqlLiteStore sqlLiteStore;

        public Register(SqlLiteStore sqlLiteStore)
        {
            if (sqlLiteStore == null)
            {
                throw new ArgumentNullException("sqlLiteStore");
            }
            this.sqlLiteStore = sqlLiteStore;
        }

        public void RegisterUser(string Username, string Password)
        {
            if (!GetDoesUserExistAlready(Username))
            {
                //hash password
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, 10);
                UserStateModel userModel = new UserStateModel(Username, HashedPassword, UserStateModel.RegistrationStateEnum.NotVerified);

                //Save Username and password
                sqlLiteStore.UpsertUser(userModel);
            }
        }

        public void VerifyUsernameEmailResponse(string Username)
        {
            var userModel = sqlLiteStore.GetUserModel(Username);

            sqlLiteStore.UpsertUser(userModel);
        }

        public bool GetDoesUserExistAlready(string Username)
        {
            return sqlLiteStore.GetUserModel(Username).UserFound;
        }
    }
}
