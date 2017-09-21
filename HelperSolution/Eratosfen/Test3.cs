//using System;
//using System.Text;
//public class Config
//{
//    public static int N = 1000000;
//    public static int MAX_LEN = 50000;
//    public static void print(int[] a)
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.Append("---------------------------------------------\n");
//        for (int i = 0; i < a.Length && a[i] != 0; i++)
//            sb.Append(a[i] + " ");
//        Console.WriteLine(sb.ToString());
//    }
// 28
//}
//public class CSieve
//{
//    // Функция, реализующая стандартный алгоритм "Решето
//    //Эратосфена"
//    private int[] SynchronousSieve(int[] ar)
//    {
//        if (ar == null || ar.Length == 0)
//            return new int[0];
//        int[] primes = (int[])Array.CreateInstance(typeof(int),
//        Config.MAX_LEN);
//        int ind = 0;
//        primes[0] = ar[0];
//        for (int i = 1; i < ar.Length; i++)
//        {
//            if (isPrime(ar[i], primes))
//                primes[++ind] = ar[i];
//        }
//        return primes;
//    }
//    // Функция,проверяющая число на простоту
//    private bool isPrime(int n, int[] primes)
//    {
//        bool isPrime = true;
//        for (int j = 0; isPrime && j < primes.Length &&
//        primes[j] != 0 && primes[j] * primes[j] <= n; j++)
//            isPrime = (n % primes[j] != 0);
//        return isPrime;
//    }
//    async Sieve(handler int[]() getNatPack, channel (int[])
//    sendPrimesPack)
//    {
//        // Получаем первый пакет из входного потока и
//        // извлекаем из него подпакет простых чисел
//        int[] head = SynchronousSieve((int[])getNatPack());
//        // Посылаем пакет простых чисел в выходной поток
//        sendPrimesPack(head);
//        // Фильтруем оставшиеся пакеты входного потока
//        // относительно пакета head
//        if (head.Length != 0)
//        {
//            new CSieve().Sieve(inter, sendPrimesPack);
//            filter(head, getNatPack, cout);
//        }
//        29
//    }
//    handler inter int[] () & channel cout(int[] p)
//    {
//        return (x);
//    }
//    // Фильтрация потоков
//    void filter(int[] head, handler int[] () getNatPack, channel
//    (int[]) cfiltered)
//    {
//        int[] al =
//        (int[])Array.CreateInstance(typeof(int), Config.MAX_LEN);
//        int ind = 0;
//        // Для каждого пакета из входного потока
//        for (int[] p; (p = (int[])getNatPack()).Length != 0;)
//        {
//            // Выбираем простые числа, формируя новый пакет
//            for (int i = 0; i < p.Length && p[i] != 0; i++)
//            {
//                if (isPrime(p[i], head))
//                {
//                    al[ind++] = p[i];
//                    // Если пакет заполнен, то посылаем его
//                    // в поток отфильтрованных пакетов
//                    if (ind == Config.MAX_LEN)
//                    {
//                        ind = 0;
//                        cfiltered(al);
//                        al =
//                        (int[])Array.CreateInstance(typeof(int), Config.MAX_LEN);
//                    }
//                }
//            }
//        }
//        // Посылаем последний пакет в поток
//        // отфильтрованных пакетов
//        if (al[0] != 0)
//            cfiltered(al);
//        // Посылаем маркер конца потока пакетов
//        cfiltered(new int[0]);
//    }
//    class Eratosthenes2
//    {
//        public static void Main(String[] args)
//        {
//            Eratosthenes2 er2 = new Eratosthenes2();
//            CSieve csieve = new CSieve();
//            // Запускаем метод Sieve
//            csieve.Sieve(er2.getNatPack, er2.sendPrimePack);
//            30
//        int[] al =
//        (int[])Array.CreateInstance(typeof(int), Config.MAX_LEN);
//            // Создаем поток пакетов натуральных чисел
//            for (int i = 2; i <= Config.N; i++)
//            {
//                int ind = (i - 2) % Config.MAX_LEN;
//                al[ind] = i;
//                if (ind == Config.MAX_LEN - 1)
//                {
//                    er2.Nats(al);
//                    al =
//                    (int[])Array.CreateInstance(typeof(int), Config.MAX_LEN);
//                }
//            }
//            if (al[0] != 0)
//                er2.Nats(al);
//            er2.Nats(new int[0]);
//            int[] p;
//            // Распечатываем результирующие пакеты простых чисел
//            while ((p = (int[])er2.getPrimePack()).Length != 0)
//                Config.print(p);
//        }
//        handler getNatPack int[] () & channel Nats(int[] p)
//        {
//            return (p);
//        }
//        handler getPrimePack int[] () & channel sendPrimePack(int[] p)
//        {
//            return (p);
//        }
//    }
