using System.Security.Cryptography;
using System.Text;

namespace RedSocial.Core.Application.Helper
{
    public class EncryptedPass
    {
        public static string ComputeSha256Hash(string pass)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                byte[] bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(pass));

                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
