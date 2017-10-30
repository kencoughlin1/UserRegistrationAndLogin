using System;
using System.Collections.Generic;
using System.Text;

namespace UserRegistrationAndLogin
{
    public class Login
    {

        private SqlLiteStore sqlLiteStore;

        public Login(SqlLiteStore sqlLiteStore)
        {
            if (sqlLiteStore == null)
            {
                throw new ArgumentNullException("sqlLiteStore");
            }
            this.sqlLiteStore = sqlLiteStore;

        }
        

        public bool DoesUserPasswordMatch(string Password, string StoredHash)
        {     
            return BCrypt.Net.BCrypt.Verify(Password, StoredHash);
        }

        public bool LoginUser(string Username, string Password)
        {
            UserStateModel UserModel = sqlLiteStore.GetUserModel(Username);
            //is username in DB
            //verify registation state
            //Does the password hash match
            return (UserModel.UserFound && (UserModel.RegistrationState == UserStateModel.RegistrationStateEnum.Verified) && DoesUserPasswordMatch(Password, UserModel.HashedPassword));
        }
    }
}
