using System.Security.Cryptography;
using System.Text;

namespace Core.Security.Hashing;

public class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (HMACSHA512 hmac = new())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (HMACSHA512 hmac = new(passwordSalt))
        {
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != passwordHash[i])
                    return false;
        }

        return true;
    }
}