using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class CryptographyLesson001 : ILesson
    {

        public async Task Run()
        {
            const string pwd1 = "mypassword";

            // Create a byte array to hold the random value. 
            var salt1 = new byte[8];

            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(salt1);
            }

            //data1 can be a string or contents of a file.
            const string data1 = "Some test data";

            //The default iteration count is 1000 so the two methods use the same iteration count.
            const int myIterations = 1000;

            try
            {
                var k1 = new Rfc2898DeriveBytes(pwd1, salt1, myIterations);
                var k2 = new Rfc2898DeriveBytes(pwd1, salt1);

                // Encrypt the data.
                var encAlg = TripleDES.Create();
                encAlg.Key = k1.GetBytes(16);
                var encryptionStream = new MemoryStream();
                var encrypt = new CryptoStream(encryptionStream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);
                var utfD1 = new UTF8Encoding(false).GetBytes(data1);

                encrypt.Write(utfD1, 0, utfD1.Length);
                encrypt.FlushFinalBlock();
                encrypt.Close();
                var edata1 = encryptionStream.ToArray();
                k1.Reset();

                // Try to decrypt, thus showing it can be round-tripped.
                var decAlg = TripleDES.Create();
                decAlg.Key = k2.GetBytes(16);
                decAlg.IV = encAlg.IV;
                var decryptionStreamBacking = new MemoryStream();

                var decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);

                decrypt.Write(edata1, 0, edata1.Length);
                decrypt.Flush();
                decrypt.Close();
                k2.Reset();

                var data2 = new UTF8Encoding(false).GetString(decryptionStreamBacking.ToArray());

                if (!data1.Equals(data2))
                {
                    Console.WriteLine("Error: The two values are not equal.");
                }
                else
                {
                    Console.WriteLine("The two values are equal.");
                    Console.WriteLine("k1 iterations: {0}", k1.IterationCount);
                    Console.WriteLine("k2 iterations: {0}", k2.IterationCount);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }

}