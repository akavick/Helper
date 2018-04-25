using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Converts ASCII characters to bytes.
            // Displays the string's byte representation in the 
            // specified code page.
            // Code page 1252 represents Latin characters.
            PrintCPBytes("Hello, World!", 1252);
            // Code page 932 represents Japanese characters.
            PrintCPBytes("Hello, World!", 932);

            // Converts Japanese characters to bytes.
            PrintCPBytes("\u307b,\u308b,\u305a,\u3042,\u306d", 1252);
            PrintCPBytes("\u307b,\u308b,\u305a,\u3042,\u306d", 932);
        }



        public static void PrintCPBytes(string str, int codePage)
        {
            Encoding targetEncoding;
            byte[] encodedChars;

            // Gets the encoding for the specified code page.
            targetEncoding = Encoding.GetEncoding(codePage);

            // Gets the byte representation of the specified string.
            encodedChars = targetEncoding.GetBytes(str);

            // Prints the bytes.
            Console.WriteLine("Byte representation of '{0}' in Code Page  '{1}':", str, codePage);

            for (int i = 0; i < encodedChars.Length; i++)
            {
                Console.WriteLine("Byte {0}: {1}", i, encodedChars[i]);
            }
        }
    }
}
