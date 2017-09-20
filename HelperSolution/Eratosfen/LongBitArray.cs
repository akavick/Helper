using System;
using System.Collections;
using System.Threading;

namespace Eratosfen
{
    //TODO:
    public sealed class BitArray : ICollection, IEnumerable, ICloneable
    {
        private int[] _mArray;
        private int _mLength;
        private int _version;
        private object _syncRoot;

        private BitArray()
        {
        }


        public BitArray(int length)
          : this(length, false)
        {
        }


        public BitArray(int length, bool defaultValue)
        {
            _mArray = new int[GetArrayLength(length, 32)];
            _mLength = length;
            int num = defaultValue ? -1 : 0;
            for (int index = 0; index < _mArray.Length; ++index)
                _mArray[index] = num;
            _version = 0;
        }


        public BitArray(bool[] values)
        {
            _mArray = new int[GetArrayLength(values.Length, 32)];
            _mLength = values.Length;
            for (int index = 0; index < values.Length; ++index)
            {
                if (values[index])
                    _mArray[index / 32] |= 1 << index % 32;
            }
            _version = 0;
        }


        public BitArray(int[] values)
        {
            _mArray = new int[values.Length];
            _mLength = values.Length * 32;
            Array.Copy(values, _mArray, values.Length);
            _version = 0;
        }


        public BitArray(BitArray bits)
        {
            int arrayLength = GetArrayLength(bits._mLength, 32);
            _mArray = new int[arrayLength];
            _mLength = bits._mLength;
            Array.Copy(bits._mArray, _mArray, arrayLength);
            _version = bits._version;
        }


        public bool this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }


        public bool Get(int index)
        {
            return (uint)(_mArray[index / 32] & 1 << index % 32) > 0U;
        }


        public void Set(int index, bool value)
        {
            if (value)
                _mArray[index / 32] |= 1 << index % 32;
            else
                _mArray[index / 32] &= ~(1 << index % 32);
            _version = _version + 1;
        }


        public void SetAll(bool value)
        {
            int num = value ? -1 : 0;
            int arrayLength = GetArrayLength(_mLength, 32);
            for (int index = 0; index < arrayLength; ++index)
                _mArray[index] = num;
            _version = _version + 1;
        }


        public BitArray And(BitArray value)
        {
            int arrayLength = GetArrayLength(_mLength, 32);
            for (int index = 0; index < arrayLength; ++index)
                _mArray[index] &= value._mArray[index];
            _version = _version + 1;
            return this;
        }


        public BitArray Or(BitArray value)
        {
            int arrayLength = GetArrayLength(_mLength, 32);
            for (int index = 0; index < arrayLength; ++index)
                _mArray[index] |= value._mArray[index];
            _version = _version + 1;
            return this;
        }


        public BitArray Xor(BitArray value)
        {
            int arrayLength = GetArrayLength(_mLength, 32);
            for (int index = 0; index < arrayLength; ++index)
                _mArray[index] ^= value._mArray[index];
            _version = _version + 1;
            return this;
        }

        public BitArray Not()
        {
            int arrayLength = GetArrayLength(_mLength, 32);
            for (int index = 0; index < arrayLength; ++index)
                _mArray[index] = ~_mArray[index];
            _version = _version + 1;
            return this;
        }


        public int Length
        {

            get => _mLength;

            set
            {
                int arrayLength = GetArrayLength(value, 32);
                if (arrayLength > _mArray.Length || arrayLength + 256 < _mArray.Length)
                {
                    int[] numArray = new int[arrayLength];
                    Array.Copy(_mArray, numArray, arrayLength > _mArray.Length ? _mArray.Length : arrayLength);
                    _mArray = numArray;
                }
                if (value > _mLength)
                {
                    int index = GetArrayLength(_mLength, 32) - 1;
                    int num = _mLength % 32;
                    if (num > 0)
                        _mArray[index] &= (1 << num) - 1;
                    Array.Clear(_mArray, index + 1, arrayLength - index - 1);
                }
                _mLength = value;
                _version = _version + 1;
            }
        }


        public void CopyTo(Array array, int index)
        {
            if (array is int[])
                Array.Copy(_mArray, 0, array, index, GetArrayLength(_mLength, 32));
            else if (array is byte[])
            {
                int arrayLength = GetArrayLength(_mLength, 8);
                byte[] numArray = (byte[])array;
                for (int index1 = 0; index1 < arrayLength; ++index1)
                    numArray[index + index1] = (byte)(_mArray[index1 / 4] >> index1 % 4 * 8 & byte.MaxValue);
            }
            else
            {
                bool[] flagArray = (bool[])array;
                for (int index1 = 0; index1 < _mLength; ++index1)
                    flagArray[index + index1] = (uint)(_mArray[index1 / 32] >> index1 % 32 & 1) > 0U;
            }
        }


        public int Count => _mLength;


        public object Clone()
        {
            return new BitArray(_mArray)
            {
                _version = _version,
                _mLength = _mLength
            };
        }


        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                    Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }


        public bool IsReadOnly => false;


        public bool IsSynchronized => false;

        public IEnumerator GetEnumerator()
        {
            return new BitArrayEnumeratorSimple(this);
        }

        private static int GetArrayLength(int n, int div)
        {
            if (n <= 0)
                return 0;
            return (n - 1) / div + 1;
        }


        private sealed class BitArrayEnumeratorSimple : IEnumerator, ICloneable
        {
            private readonly BitArray _bitarray;
            private int _index;
            private bool _currentElement;

            internal BitArrayEnumeratorSimple(BitArray bitarray)
            {
                _bitarray = bitarray;
                _index = -1;
            }

            public object Clone()
            {
                return MemberwiseClone();
            }

            public bool MoveNext()
            {
                if (_index < _bitarray.Count - 1)
                {
                    _index = _index + 1;
                    _currentElement = _bitarray.Get(_index);
                    return true;
                }
                _index = _bitarray.Count;
                return false;
            }

            public object Current => _currentElement;

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
