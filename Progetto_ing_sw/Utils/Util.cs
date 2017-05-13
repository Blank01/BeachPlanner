using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_ing_sw.Utils
{
    public static class Util
    {
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }
        public static string GenerateShortID()
        {

            var sb = new StringBuilder(6);
            var random = new Random();
            char[] base62chars =
                "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
                .ToCharArray();
            for (int i = 0; i < 6; i++)
                sb.Append(base62chars[random.Next(36)]);

            return sb.ToString();

        }
    }
}
