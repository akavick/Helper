using System;
using System.Threading;

namespace Eratosfen
{
    /// <summary>
    /// Finds prime numbers starting from 2 and up to a given value
    /// </summary>
    class ParallelE
    {

        /*         
        1. Create a list of natural numbers 2, 3, 4, 5, ….., n.  None of which is marked. Each process 
        creates its share of lists. 
        2. Set k to 2, the first unmarked number on the list. Each process does this. 
        3. Repeat: Each process marks its share of list 
        a. Mark all multiples of k between k² and n. 
        b. Find the smallest number greater than k that is unmarked. Set  k to this new 
        value 
        c. Process 0 broadcasts k to rest of processes. 
        Until k² > n. 
        4. The unmarked numbers are primes. 
        5. Reduction to determine number of primes
        */

        /// <summary>
        /// possible status of each thread used by this program
        /// </summary>
        enum ThreadStatus
        {
            DoingStuff = 0,
            Waiting = 1,
            Dead = 2
        };

        /// <summary>
        /// Statuses for all the threads started
        /// </summary>
        static ThreadStatus[] _threadStatuses;

        /// <summary>
        /// array that stores wether a number is prime or not true = prime, false = not prime
        /// this array should be as long as the maximum number up to which you want to find primes
        /// </summary>
        static bool[] _nums;

        /// <summary>
        /// k from the algorithm above
        /// </summary>
        static int _k;

        /// <summary>
        /// n from the algorithm above
        /// </summary>
        static int _n;

        /// <summary>
        /// nr of threads that compute primes - this is actually the number of worker threads because one 
        /// extra thread will handle just finding the smallest remainig prime and broadcasting it to all the 
        /// other threads
        /// </summary>
        static int _nrThreads;

        /// <summary>
        /// changes status of all worker threads to "DoingStuff"
        /// </summary>
        static void MakeWorkerThreadsDoStuff()
        {
            for (var i = 1; i < _nrThreads + 1; i++)
            {
                _threadStatuses[i] = ThreadStatus.DoingStuff;
            }
        }

        /// <summary>
        /// changes status of all worker threads to "Waiting"
        /// </summary>
        static void MakeWorkerThreadsWait()
        {
            for (var i = 1; i < _nrThreads + 1; i++)
            {
                _threadStatuses[i] = ThreadStatus.Waiting;
            }
        }

        /// <summary>
        /// changes status of all worker threads to "Dead"
        /// </summary>
        static void KillWorkerThreads()
        {
            for (var i = 1; i < _nrThreads + 1; i++)
            {
                _threadStatuses[i] = ThreadStatus.Dead;
            }
        }

        /// <summary>
        /// checks wether at least one of the worker threads is still doning something
        /// </summary>
        /// <returns></returns>
        static bool WorkerThreadsDoingStuff()
        {
            for (var i = 1; i < _nrThreads + 1; i++)
            {
                if (_threadStatuses[i] == ThreadStatus.DoingStuff)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// checks wether at least one of the worker threads is waiting(on stand by)
        /// </summary>
        /// <returns></returns>
        static bool WorkerThreadsWaiting()
        {
            for (var i = 1; i < _nrThreads + 1; i++)
            {
                if (_threadStatuses[i] == ThreadStatus.Waiting)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// checks wether the main thread is in wairing mode
        /// </summary>
        /// <returns></returns>
        static bool MainThreadWaiting()
        {
            return (_threadStatuses[0] == ThreadStatus.Waiting);
        }

        /// <summary>
        /// checks if all threads are done working and waiting
        /// </summary>
        /// <returns></returns>
        static bool AllThreadsDead()
        {
            foreach (var threadStatus in _threadStatuses)
            {
                if (threadStatus != ThreadStatus.Dead)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// each thread runs this function it marks non prime numbers within it's given range
        /// between start and stop
        /// </summary>
        /// <param name="threadNr"/>
        static void MarkNonPrimes(int threadNr)
        {

            while (_threadStatuses[threadNr] == ThreadStatus.Waiting)
            {
                Thread.Sleep(1);
            }

            if (_threadStatuses[threadNr] == ThreadStatus.Dead)
                return;
            //stores how many numbers we have to decide are primes or non primes
            var nrOfNumbers = _n - _k * _k;
            //divide that by the number of threads to get the interval within which each thread should 
            //look for primes
            var interval = nrOfNumbers / _nrThreads;

            //for this thread and this value of k start at this value
            var startNumber = _k * _k + interval * (threadNr - 1);
            //and end at this value
            var stopNumber = (threadNr == _nrThreads) ? _n : startNumber + interval;

            lock (_nums)
            {
                for (var j = startNumber; j < stopNumber; j++)
                {
                    //Mark all multiples of k between k² and n. 
                    if (j % _k == 0)
                        _nums[j] = false;
                }
            }

            //nothig to do now but wait
            _threadStatuses[threadNr] = ThreadStatus.Waiting;

            MarkNonPrimes(threadNr);
        }

        /// <summary>
        /// this is the main thread's function it manages the worker therads and finds the smallest prime
        /// when the worker threads finish marking non primes
        /// </summary>
        static void FindSmallestPrimeAndBroadcast()
        {
            while (WorkerThreadsDoingStuff())
            {
                Thread.Sleep(1);
            }

            //c. Process 0 broadcasts k to rest of processes. 
            for (var i = _k + 1; i < _n; i++)
            {
                if (_nums[i])
                {

                    _k = i;

                    break;
                }
            }
            //Until k² > n. 
            if (_k * _k > _n)
            {
                //kill worker threads
                KillWorkerThreads();
                //commit suicide
                _threadStatuses[0] = ThreadStatus.Dead;
                return;
            }
            //all worker threads can now read the correct new value of k so they can all get to work
            MakeWorkerThreadsDoStuff();

            FindSmallestPrimeAndBroadcast();
        }

        public static void Run()
        {
            var sNum = 1000000;
            _n = (int)(sNum * Math.Log(sNum) + sNum * Math.Log(Math.Log(sNum)));

            _nrThreads = Environment.ProcessorCount;

            //add one because one main thread is needed to manage all the others
            _threadStatuses = new ThreadStatus[_nrThreads + 1];

            //1. Create a list of natural numbers 2, 3, 4, 5, ….., n.  None of which is marked.
            _nums = new bool[_n];
            //suppose all these are primes, we will then start marking them as non primes
            for (var i = 2; i < _n; i++)
                _nums[i] = true;

            //initialize k with 1, start looking for primes bigger than 1, 1 is not a prime
            _k = 1;

            //make sure all threads are waiting so they don't run out and find primes on their 
            //own without the main thread regulating them
            MakeWorkerThreadsWait();

            //this is the main thread
            var mainThread = new Thread(FindSmallestPrimeAndBroadcast) {IsBackground = true};
            mainThread.Start();

            //worker threads
            for (var i = 1; i < _nrThreads + 1; i++)
            {
                //need to declare this as a locol variable so that the value passed to the thread
                //won't be changed causing chaos
                var localI = i;
                var workerThread = new Thread(() => MarkNonPrimes(localI)) {IsBackground = true};

                workerThread.Start();
            }

            //wait for all threads to end
            while (!AllThreadsDead())
            //commenting the line above and decommenting the one below really help with debugging
            //because you don't constantly get sidetracked to AllThreadsDead() when pressing F5
            //while (true)
            {
                Thread.Sleep(1);
            }

            ////print the primes you found
            //for (var i = 0; i < _n; i++)
            //    if (_nums[i])
            //        Console.WriteLine(i);
        }
    }
}