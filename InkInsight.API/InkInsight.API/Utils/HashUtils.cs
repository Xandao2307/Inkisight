using System.Security.Cryptography;
using System.Text;

namespace InkInsight.API.Utils
{
    public static class HashUtils
    {
        public static string CreateHash(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            using SHA256 sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bytes);
            var builder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
