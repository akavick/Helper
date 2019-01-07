using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class CryptographyLesson003 : ILesson
    {
        private class User
        {
            public string Name { get; set; }

            public string Salt { get; set; }

            public string SaltedHashedPassword { get; set; }
        }



        private static readonly Dictionary<string, User> _users = new Dictionary<string, User>();



        private static User Register(string username, string password)
        {
            // генерация соли
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            var saltedhashedPassword = GenerateSaltedHashedPassword(password, saltText);

            var user = new User
            {
                Name = username,
                Salt = saltText,
                SaltedHashedPassword = saltedhashedPassword
            };

            _users.Add(user.Name, user);

            return user;
        }



        private static string GenerateSaltedHashedPassword(string password, string saltText)
        {
            // генерация соленого и хешированного пароля
            var sha = SHA256.Create();
            var saltedPassword = password + saltText;
            var bytes = Encoding.Unicode.GetBytes(saltedPassword);
            var hash = sha.ComputeHash(bytes);
            var saltedhashedPassword = Convert.ToBase64String(hash);

            return saltedhashedPassword;
        }



        private static bool CheckPassword(string username, string password)
        {
            if (!_users.ContainsKey(username))
            {
                return false;
            }

            var user = _users[username];
            var saltedhashedPassword = GenerateSaltedHashedPassword(password, user.Salt);
            var passwordIsCorrect = saltedhashedPassword == user.SaltedHashedPassword;

            return passwordIsCorrect;
        }



        public async Task Run()
        {
            Console.WriteLine("A user named Alice has been registered with Pa$$w0rd as her password.");

            var alice = Register("Alice", "Pa$$w0rd");

            Console.WriteLine($"Name: {alice.Name}");
            Console.WriteLine($"Salt: {alice.Salt}");
            Console.WriteLine($"Salted and hashed password: {alice.SaltedHashedPassword}");
            Console.WriteLine();
            Console.Write("Enter a different username to register: ");

            var username = Console.ReadLine();

            Console.Write("Enter a password to register: ");

            var password = Console.ReadLine();
            var user = Register(username, password);

            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Salt: {user.Salt}");
            Console.WriteLine($"Salted and hashed password: {user.SaltedHashedPassword}");

            var passwordIsCorrect = false;

            while (!passwordIsCorrect)
            {
                Console.Write("Enter a username to log in: ");

                var loginUsername = Console.ReadLine();

                Console.Write("Enter a password to log in: ");

                var loginPassword = Console.ReadLine();

                passwordIsCorrect = CheckPassword(loginUsername, loginPassword);

                var outputMessage = passwordIsCorrect
                                        ? $"Correct! {loginUsername} has been logged in."
                                        : "Invalid username or password. Try again.";

                Console.WriteLine(outputMessage);
            }

        }
    }

}