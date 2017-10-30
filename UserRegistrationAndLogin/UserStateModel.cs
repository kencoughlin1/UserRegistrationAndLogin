using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationAndLogin
{
    public class UserStateModel
    {

        public UserStateModel()
        { }

        public UserStateModel(string Username, string HashedPassword, RegistrationStateEnum RegistrationState)
        {
           this.Username = Username;

           this.HashedPassword = HashedPassword;

           this.RegistrationState = RegistrationState;

        }

        public enum RegistrationStateEnum
        {
            NotVerified,
            Verified
        }

        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public RegistrationStateEnum RegistrationState {get; set;}

        public bool UserFound { get; set; }
    }
}
