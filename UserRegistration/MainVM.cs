using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectModels;
using System.Security.Cryptography;
using Database;
using System.ComponentModel;

namespace UserRegistration
{
    class MainVM : INotifyPropertyChanged
    {
        #region private fields
        private List<User> _userList;
        private DBController _dbController;
        #endregion

        #region public properties
        public List<User> UserList
        {
            get
            {
                return _userList;
            }
            set
            {
                _userList = value;
                NotifyPropertyChanged();
            }
        }

        public DBController DbController { get; set; }
        #endregion

        #region Constructor
        public MainVM()
        {
            UserList = new List<User>();

            DbController = new DBController();
        }
        #endregion

        #region public methods
        public bool registerUser(string userName, string password)
        {
            User user = new User();
            using (SHA512 sha512Hash = SHA512.Create())
            {
                string hash = GetHash(sha512Hash, password);
                user.Username = userName;
                user.PasswordHash = hash;
            }


            DbController.WriteUser(user);

            UserList = DbController.ReadUsers();

            return true;
        }
        #endregion

        #region private methods
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
