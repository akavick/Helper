using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;



namespace CsLearningLibrary
{



    public class NetLesson001 : ILesson
    {

        public async Task Run()
        {
            var uri = new Uri("http://world.episerver.com/cms/?q=pagetype");

            Console.WriteLine($"Scheme: {uri.Scheme}");
            Console.WriteLine($"Port: {uri.Port}");
            Console.WriteLine($"Host: {uri.Host}");
            Console.WriteLine($"Path: {uri.AbsolutePath}");
            Console.WriteLine($"Query: {uri.Query}");


            IPHostEntry entry = Dns.GetHostEntry(uri.Host);
            Console.WriteLine($"{entry.HostName} has the following IP addresses:");

            foreach (IPAddress address in entry.AddressList)
            {
                Console.WriteLine($" {address}");
            }


            var ping = new Ping();
            PingReply reply = ping.Send(uri.Host);

            Console.WriteLine($"{uri.Host} was pinged, and replied: {reply?.Status}."); //timeout - protection from DDoS

            if (reply?.Status == IPStatus.Success)
            {
                Console.WriteLine($"Reply from {reply.Address} took{ reply.RoundtripTime:N0}ms");
            }

        }
    }



}