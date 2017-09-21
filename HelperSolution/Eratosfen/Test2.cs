//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Eratosfen
//{
//    class Eratosthenes
//    {
//        public static void Run()
//        {
//            int N = System.Convert.ToInt32(args[0]);
//            Eratosthenes E = new Eratosthenes();
//            new CSieve().Sieve(E.getNat, E.sendPrime);
//            for (int n = 2; n <= N; n++)
//                E.Nats(n);
//            E.Nats(-1);
//            while ((int p = E.getPrime() ) != -1 )
//            Console.WriteLine(p);
//        }
//        handler getNat int () & channel Nats(int n)
//        {
//            return (n);
//        }
//        handler getPrime int ()& channel sendPrime(int p)
//        {
//            return (n);
//        }
//    }
//    class CSieve
//    {
//        async Sieve(handler int() getList, channel (int) sendPrime)
//        {
//            int p = getList();
//            sendPrime(p);
//            if (p != -1)
//            {
//                new CSieve().Sieve(hin, sendPrime);
//                filter(p, getList, cout);
//            }
//        }
//        handler hin int () & channel cout(int x)
//        {
//            return (x);
//        }
//        void filter(int p, handler int() getList,
//                                         channel (int) cfiltered)
//        {
//            while ((int n = getList() ) != -1 )
//            if (n % p != 0)
//                cfiltered(n);
//            cfiltered(-1);
//        }
//    }
//}
