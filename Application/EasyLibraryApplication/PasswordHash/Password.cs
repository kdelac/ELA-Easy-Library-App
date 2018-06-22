using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHash
{
    /// <summary>
    /// Klasa za pretvaranje stringa lozinke u hash zapis i provjera hash zapisa s unesenom lozinkom
    /// </summary>
    public class Password
    {
        /// <summary>
        /// Metoda koja pretvara string lozinke i vraća hashirani string
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetHashString(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Usporedba upisane lozinke i hashiranog stringa i ako su isti vraća 1, a ako nisu vraća 0
        /// </summary>
        /// <param name="savedPasswordHash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int VerifyHashString(string savedPasswordHash, string password)
        {
            int flag = 0;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++) flag = hashBytes[i + 16] == hash[i] ? 1 : 0;

            return flag;
        }
    }
}
