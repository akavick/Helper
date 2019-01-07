using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class CryptographyLesson002 : ILesson
    {
        // размер соли должен составлять не менее восьми байт,
        // мы будем использовать 16 байт
        private static readonly byte[] _salt = Encoding.Unicode.GetBytes("7BANANAS");

        // число итераций должно быть не меньше 1000, мы будем использовать
        // 2000 итераций
        private const int _iterations = 2000;



        public static string Encrypt(string plainText, string password)
        {
            var plainBytes = Encoding.Unicode.GetBytes(plainText);
            var aes = Aes.Create();

            var pbkdf2 = new Rfc2898DeriveBytes(password, _salt, _iterations);

            aes.Key = pbkdf2.GetBytes(32); // установить 256-битный ключ
            aes.IV = pbkdf2.GetBytes(16);  // установить 128-битный вектор

            // инициализации
            var ms = new MemoryStream();

            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
            }

            return Convert.ToBase64String(ms.ToArray());
        }



        public static string Decrypt(string cryptoText, string password)
        {
            var cryptoBytes = Convert.FromBase64String(cryptoText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, _salt, _iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            var ms = new MemoryStream();

            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cryptoBytes, 0, cryptoBytes.Length);
            }

            return Encoding.Unicode.GetString(ms.ToArray());
        }



        public async Task Run()
        {
            Console.Write("Enter a message that you want to encrypt: ");
            var message = Console.ReadLine();
            Console.Write("Enter a password: ");
            var password = Console.ReadLine();
            var cryptoText = Encrypt(message, password);
            Console.WriteLine($"Encrypted text: {cryptoText}");
            Console.Write("Enter the password: ");
            var password2 = Console.ReadLine();

            try
            {
                var clearText = Decrypt(cryptoText, password2);
                Console.WriteLine($"Decrypted text: {clearText}");
            }
            catch
            {
                Console.WriteLine("Enable to decrypt because you entered the wrong password!");
            }

        }
    }

}



