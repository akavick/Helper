using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class AsyncAwaitLesson002 : ILesson
    {
        private readonly TaskQueue _semaQueue = new TaskQueue();
        private readonly TaskQueue _semaQueue2 = new TaskQueue();
        private readonly SerialQueue _seriQueue = new SerialQueue();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly SemaphoreSlim _semaphore2 = new SemaphoreSlim(1);


        public async Task Run()
        {
            //var r = await DoCycle(1000);
            _ = DoCycle(1000);
            //Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Count: {r}");
        }



        private async Task<int> DoCycle(int count)
        {
            //var tasks =
            //    Enumerable.Range(1, count)
            //              .Select(i => _semaQueue.Enqueue(() => DoSingle(i)))
            //              //.Select(i => _semaQueue.Enqueue(async () => await DoSingle(i)))
            //              .ToArray();

            //var tasks2 =
            //    Enumerable.Range(count + 1, count)
            //              .Select(i => _semaQueue2.Enqueue(() => DoSingle(i)))
            //              //.Select(i => _semaQueue.Enqueue(async () => await DoSingle(i)))
            //              .ToArray();

            //var tasks =
            //    Enumerable.Range(1, count)
            //              .Select(i =>  _seriQueue.Enqueue(() => DoSingle(i)))
            //              .ToArray();

            var tasks =
                Enumerable.Range(1, count)
                          .Select(i => Enqueue(() => DoSingle(i)))
                          .ToArray();

            var tasks2 =
                Enumerable.Range(count + 1, count)
                          .Select(i => Enqueue(() => DoSingle(i)))
                          .ToArray();

            //var c = (await Task.WhenAll(tasks)).Length;

            return count;
        }



        private async Task<int> DoSingle(int i)
        {
            await Task.Delay(10);

            //await Task.Run(() =>
            //{
            //    for (var j = 0; j < 100000000; j++) {};
            //});

            Console.Write($"{i,-5}");

            return i;
        }



        public async Task Enqueue(Func<Task> taskGenerator)
        {
            await _semaphore.WaitAsync();

            try
            {
                await taskGenerator();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }



    public class TaskQueue
    {
        private readonly SemaphoreSlim _semaphore;



        public TaskQueue()
        {
            _semaphore = new SemaphoreSlim(1);
        }



        public async Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
        {
            await _semaphore.WaitAsync();

            try
            {
                return await taskGenerator();
            }
            finally
            {
                _semaphore.Release();
            }
        }



        public async Task Enqueue(Func<Task> taskGenerator)
        {
            await _semaphore.WaitAsync();

            try
            {
                await taskGenerator();
            }
            finally
            {
                _semaphore.Release();
            }
        }

    }



    public class SerialQueue
    {
        private readonly object _locker = new object();

        private WeakReference<Task> _lastTask;



        public Task Enqueue(Action action)
        {
            return Enqueue<object>(() =>
            {
                action();

                return null;
            });
        }



        public Task<T> Enqueue<T>(Func<T> function)
        {
            lock (_locker)
            {
                Task<T> resultTask = null;

                if (_lastTask != null && _lastTask.TryGetTarget(out var lastTask))
                {
                    resultTask = lastTask.ContinueWith(_ => function());
                }
                else
                {
                    resultTask = Task.Run(function);
                }

                _lastTask = new WeakReference<Task>(resultTask);

                return resultTask;
            }
        }

    }

}