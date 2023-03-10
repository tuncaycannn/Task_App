using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.ToolKit
{
    public class RandomPassword
    {
        public static string CreateRandomPassword(int length=14)
        {
            var validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            var random = new Random();
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0,validChars.Length)];
            }
            return new string(chars);
        }

        public static int RandomNumberGenerator(int min = 100000, int max = 999999)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
