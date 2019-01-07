using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace CsLearningLibrary
{

    public static class RsaExtensions
    {
        public static string ToXmlStringExt(this RSA rsa, bool includePrivateParameters)
        {
            var p = rsa.ExportParameters(includePrivateParameters);
            XElement xml;

            if (includePrivateParameters)
            {
                xml = new XElement("RSAKeyValue"
                                 , new XElement("Modulus", Convert.ToBase64String(p.Modulus))
                                 , new XElement("Exponent", Convert.ToBase64String(p.Exponent))
                                 , new XElement("P", Convert.ToBase64String(p.P))
                                 , new XElement("Q", Convert.ToBase64String(p.Q))
                                 , new XElement("DP", Convert.ToBase64String(p.DP))
                                 , new XElement("DQ", Convert.ToBase64String(p.DQ))
                                 , new XElement("InverseQ", Convert.ToBase64String(p.InverseQ)));
            }
            else
            {
                xml = new XElement("RSAKeyValue"
                                 , new XElement("Modulus", Convert.ToBase64String(p.Modulus))
                                 , new XElement("Exponent", Convert.ToBase64String(p.Exponent)));
            }

            return xml.ToString();
        }



        public static void FromXmlStringExt(this RSA rsa, string parametersAsXml)
        {
            var xml = XDocument.Parse(parametersAsXml);
            var root = xml.Element("RSAKeyValue");
            var rsaParameters = new RSAParameters();

            if (root != null)
            {
                rsaParameters.Modulus = Convert.FromBase64String(root.Element("Modulus").Value);
                rsaParameters.Exponent = Convert.FromBase64String(root.Element("Exponent").Value);

                if (root.Element("P") != null)
                {
                    rsaParameters.P = Convert.FromBase64String(root.Element("P").Value);
                    rsaParameters.Q = Convert.FromBase64String(root.Element("Q").Value);
                    rsaParameters.DP = Convert.FromBase64String(root.Element("DP").Value);
                    rsaParameters.DQ = Convert.FromBase64String(root.Element("DQ").Value);
                    rsaParameters.InverseQ = Convert.FromBase64String(root.Element("InverseQ").Value);
                }
            }

            rsa.ImportParameters(rsaParameters);
        }



    }



    public class CryptographyLesson004 : ILesson
    {
        public static string PublicKey;



        public static string GenerateSignature(string data)
        {
            var rsa = RSA.Create();
            var hashedData = GetHashedData(data);

            PublicKey = rsa.ToXmlStringExt(false); // исключение закрытого ключа

            var signedHash = rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            var signature = Convert.ToBase64String(signedHash);

            return signature;
        }



        public static bool ValidateSignature(string data, string signature)
        {
            var rsa = RSA.Create();
            var hashedData = GetHashedData(data);
            var signatureBytes = Convert.FromBase64String(signature);

            rsa.FromXmlStringExt(PublicKey);

            var hashIsVerified = rsa.VerifyHash(hashedData, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return hashIsVerified;
        }



        private static byte[] GetHashedData(string data)
        {
            var dataBytes = Encoding.Unicode.GetBytes(data);
            var sha = SHA256.Create();
            var hashedData = sha.ComputeHash(dataBytes);

            return hashedData;
        }



        public async Task Run()
        {
            Console.Write("Enter some text to sign: ");

            var data = Console.ReadLine();
            var signature = GenerateSignature(data);

            Console.WriteLine($"Signature: {signature}");
            Console.WriteLine("Public key used to check signature:");
            Console.WriteLine(PublicKey);

            Console.WriteLine(ValidateSignature(data, signature)
                                  ? "Correct! Signature is valid."
                                  : "Invalid signature.");

            // создаем поддельную подпись, заменив первый символ на X
            var fakeSignature = signature.Replace(signature[0], 'X');

            Console.WriteLine(ValidateSignature(data, fakeSignature)
                                  ? "Correct! Signature is valid."
                                  : $"Invalid signature: {fakeSignature}");
        }



    }

}