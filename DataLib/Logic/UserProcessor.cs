using DataLib.DataHandlers;
using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    public class UserProcessor
    {
        private readonly CryptoHandler _cryptoHandler = new CryptoHandler(); //create an instance of our cryptohandler
        public int CreateUser(string LoFirstName, string LoLastName, string LoEmailAddress, string LoPassword)
        {
            UserDataModel data = new UserDataModel
            { //map our data model and frontend model together
                FirstName = LoFirstName,
                LastName = LoLastName,
                EmailAddress = LoEmailAddress,
                Password = _cryptoHandler.Encrypt(LoPassword), //encrypt password before it gets to DB
                Salt = _cryptoHandler.SaltUsed //grab and store the last salt used to create the password for this user
            };


            // @ symbol allows multiple lines and then to insert properties 
            string sql = @"INSERT INTO dbo.Users (FirstName, LastName, EmailAddress, Password, Salt) 
                            VALUES (@FirstName, @LastName, @EmailAddress, @Password, @Salt)";

            return SqlDataAccess.SaveData(sql, data);
        }

        public string LoadSalt(string LoEmailAddress) //this will gather the salt from the DB for a matching email address
        {
            UserSaltModel data = new UserSaltModel
            { //map our models together
                EmailAddress = LoEmailAddress
            };

            string sql = "select Salt from dbo.Users where EmailAddress = @emailAddress";
            var salt = SqlDataAccess.LoadSalt(sql, data);
            return salt[0].salt.ToString(); 
        }
        public int CheckCredentials(string LoEmailAddress, string LoPassword, string salt) //uses the salt from "LoadSalt" to compare the two complete hashes
        {
            UserSignInModel data = new UserSignInModel
            { //map our data model and frontend model together
                EmailAddress = LoEmailAddress,
                Password = _cryptoHandler.EncryptWithSalt(LoPassword, salt)
            };

            // @ symbol allows multiple lines and then to insert properties 
            string sql = @"SELECT EmailAddress from dbo.Users WHERE
                            EmailAddress=@EmailAddress AND Password=@Password";

            return SqlDataAccess.CheckUser(sql, data);
        }
    }
}
