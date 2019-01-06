using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class NaiveImmutableList<T>
    {
        private readonly int _length;
        private readonly T[] _data;



        public NaiveImmutableList(params T[] data)
        {
            (_data, _length) = (data, data.Length);
        }



        public ref readonly T this[int idx] => 
            ref idx >= _length 
                    ? ref Throw() 
                    : ref _data[idx];



        private static ref T Throw()
        {
            throw new IndexOutOfRangeException();
        }

        //https://blogs.msdn.microsoft.com/seteplia/2018/04/11/performance-traps-of-ref-locals-and-ref-returns-in-c/

        //{
        //    get
        //    {
        //        // Extracting 'throw' statement into a different
        //        // method helps the jitter to inline a property access.
        //        if ((uint)idx >= (uint)_length)
        //            ThrowIndexOutOfRangeException();
        //        return ref _data[idx];
        //    }
        //}
        //private static void ThrowIndexOutOfRangeException()
        //{
        //    throw new IndexOutOfRangeException();
        //}

    }



    struct LargeStruct48
    {
        public int N { get; }

        private readonly long _l1, _l2, _l3, _l4, _l5;



        public LargeStruct48(int n) : this()
        {
            N = n;
        }
    }



    public struct Mutable
    {
        private int _x;

        public Mutable(int x) => _x = x;

        public int X => _x;



        public void IncrementX()
        {
            _x++;
        }
    }



    class EncapsulationWentWrong
    {
        private readonly Guid _guid;
        private int _x;

        public EncapsulationWentWrong(int x) => _x = x;

        // Return an alias to the private field. No encapsulation any more.
        public ref int X => ref _x;

        // Return a readonly alias to the private field.
        public ref readonly Guid Guid => ref _guid;
    }



    public class RefStruct : ILesson
    {

        public async Task Run()
        {
            //int x = default;

            CheckMutability();


        }



        public void CheckMutability()
        {
            var ma = new[] {new Mutable(1)};
            ma[0].IncrementX();

            // X has been changed!
            Console.WriteLine(ma[0].X);

            var ml = new List<Mutable> {new Mutable(1)};
            ml[0].IncrementX();

            // X hasn't been changed!
            Console.WriteLine(ml[0].X);


            int[] array = {1, 2};

            // Capture an alias to the first element into a local
            ref var first = ref array[0];
            first = 42;
            Console.WriteLine(first);

            // Local function that returns the first element by ref
            ref int getByRef(int[] a) => ref a[0];

            // Weird syntax: the result of a function call is assignable
            getByRef(array) = -1;
            Console.WriteLine(array[0]);


            var instance = new EncapsulationWentWrong(42);
            instance.X++;
            Console.WriteLine(instance.X);

            // Cannot assign to property 'EncapsulationWentWrong.Guid' because it is a readonly variable
            // instance.Guid = Guid.Empty;
        }
    }

}