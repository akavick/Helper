using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eratosfen
{
    class Test5
    {
        public static void Run()
        {
            var sNum = 10000000;
            var size = (int)(sNum * Math.Log(sNum) + sNum * Math.Log(Math.Log(sNum)));
            if (size < 12)
                size = 12;

            var bools = new bool[size];

            for (var i = 2; i < size; i++)
                bools[i] = true;

            for (var number = 2; number * number < size; ++number)
            {
                if (!bools[number])
                    continue;
                for (var j = number * number; j < size; j += number)
                    bools[j] = false;
            }

            var prime = 0;

            for (int number = 2, counter = 1; number < size; number++)
            {
                if (bools[number] && counter++ == sNum)
                {
                    prime = number;
                    break;
                }
            }

            Console.WriteLine(prime);



        }


    }
}


//private Task<(int prime, TimeSpan time)> CalculateE(int sNum)
//{
//    return Task.Run(() =>
//    {
//        var sw = new Stopwatch();
//        sw.Start();

//        var size = (int)(sNum * Math.Log(sNum) + sNum * Math.Log(Math.Log(sNum)));
//        if (size < 12)
//            size = 12;

//        #region woOptimization

//        //var bools = new BitArray(size);
//        ////var bools = new bool[size];

//        //for (var i = 2; i < size; i++)
//        //    bools[i] = true;

//        //for (var number = 2; number * number < size; ++number)
//        //{
//        //    if (!bools[number])
//        //        continue;
//        //    for (var j = number * number; j < size; j += number)
//        //        bools[j] = false;
//        //}

//        //var prime = 0;

//        //for (int number = 2, counter = 1; number < size; number++)
//        //{
//        //    if (bools[number] && counter++ == sNum)
//        //    {
//        //        prime = number;
//        //        break;
//        //    }
//        //}

//        #endregion


//        #region Optimization

//        var bools = new BitArray(size);
//        //var bools = new bool[size];

//        for (var i = 1; i < size; i++)
//            bools[i] = true;

//        for (var number = 1; (2 * number + 1) * (2 * number + 1) < 2 * size + 1; ++number)
//        {
//            if (!bools[number])
//                continue;
//            for (var j = 3 * number + 1; j < size; j += 2 * number + 1)
//                bools[j] = false;
//        }

//        var prime = 2;

//        for (int num = 1, counter = 1; num < size; num++)
//        {
//            if (bools[num] && counter++ == sNum - 1)
//            {
//                prime = (2 * num + 1);
//                break;
//            }
//        }

//        #endregion


//        sw.Stop();

//        return (prime, sw.Elapsed);
//    });
//}