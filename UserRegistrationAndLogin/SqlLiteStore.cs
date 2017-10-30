using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;


namespace UserRegistrationAndLogin
{
    public class SqlLiteStore
    {
        private SQLiteConnection dbConnection;



        public void CreateSqlLiteStore()
        {

            var file = new DirectoryInfo(Environment.CurrentDirectory) + "Users.sqlite";
            dbConnection = new SQLiteConnection("Data Source=" + file +";Version=3;");

            if (!File.Exists(file))
            {
                SQLiteConnection.CreateFile(file);


                dbConnection.Open();

                string sql = "CREATE TABLE Users (UserName VARCHAR(100),  HashedPassword VARCHAR(100), RegistrationState INT)";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

            }


        }

        public UserStateModel GetUserModel(string Username)
        {


            UserStateModel UserState = new UserStateModel();

            try
            {
                using (dbConnection)
                {
                    dbConnection.Open();
                    string sql = "select UserName, HashedPassword, RegistrationState from Users";

                    using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    UserState.Username = reader["UserName"].ToString();
                                    UserState.HashedPassword = reader["HashedPassword"].ToString();
                                    UserState.RegistrationState = (UserStateModel.RegistrationStateEnum)(Int32.Parse(reader["RegistrationState"].ToString()));
                                    UserState.UserFound = true;
                                }
                            }
                            {
                                UserState.UserFound = false;
                            }
                        }
                        dbConnection.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
            }

            return new UserStateModel();
        }

        public void UpsertUser(UserStateModel UserStateModel)
        {

            try
            {
                using (dbConnection)
                {
                    dbConnection.Open();
                    string sql = "INSERT OR IGNORE INTO Users(UserName, HashedPassword, RegistrationState ) VALUES('" + UserStateModel.Username + "', '" + UserStateModel.HashedPassword + "', " + UserStateModel.RegistrationState + ")" +
                    "UPDATE Users SET RegistrationState = " + UserStateModel.RegistrationState + " WHERE UserName = '" + UserStateModel.Username + "'";
                    using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                    {
                        command.ExecuteNonQuery();
                        dbConnection.Close();
                    }
                }
            }
            catch (SQLiteException e)
            {
            }

        }

    }
   
}
