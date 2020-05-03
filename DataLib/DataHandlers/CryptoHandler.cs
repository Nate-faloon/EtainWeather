using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.DataHandlers
{
    public class CryptoHandler
    {
        public string SaltUsed = ""; //used for storing salt when a user signs up
        public string Encrypt(string password)
        {

            // generate a 128-bit salt using a secure RNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            SaltUsed = ConvertSaltToString(salt); //store our salt for this password

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public string EncryptWithSalt(string password, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            byte[] convertedSalt = ConvertStringToSalt(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: convertedSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private string ConvertSaltToString(byte[] originalSalt) //turns our salt intro a 64base string for storage in a DB
        {
            string result = Convert.ToBase64String(originalSalt);
            return result; 
        }

        private byte[] ConvertStringToSalt(string originalString) //converts our string salt back into a byte array for hasing
        {
            byte[] bytes = Convert.FromBase64String(originalString);
            return bytes;
        }
    }
      
}

