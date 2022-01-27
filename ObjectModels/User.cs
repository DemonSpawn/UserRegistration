using System;

namespace ObjectModels
{
    public class User
    {
        #region private fields
        private string _username;
        private string _passwordHash;
        #endregion

        #region public probperties
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        #endregion

        #region constructor
        public User(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        public User()
        {
            Username = "";
            PasswordHash = "";
        }
        #endregion


    }
}
