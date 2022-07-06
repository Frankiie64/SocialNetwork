using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Helper
{
    public class GeneratePass
    {
        public static string NewPassword()
        {
            Random rdn = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int space = chars.Length;
            char letter;
            int SpacePass = 10;
            string randomPass = string.Empty;
            for (int i = 0; i < SpacePass; i++)
            {
                letter = chars[rdn.Next(space)];
                randomPass += letter.ToString();
            }
            return randomPass;
        }
    }
}
