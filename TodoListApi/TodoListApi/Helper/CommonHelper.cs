using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TodoListApi.Helper
{
    public class CommonHelper
    {
        public static string HashUsingPbkdf2(string password, string salt)
        {
            using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }
        public static string GenerateSalt()
        {
            var salt = Guid.NewGuid().ToString().Replace("-", "");
            using var bytes = new Rfc2898DeriveBytes("", Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }

        
        
    }
    public class ConstMessage
    {
       public const string Register = "Register Successfully",
            Inserted = "Inserted Successfully",
            Updated = "Updated Successfully",
            Deleted = "Deleted Successfully";
    }

    
    
}
