using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace MyBenchmarks
{
    public class Sort
    {
        public long[] BuiltInSort(long[] arr)
        {
            List<long> arrList = new List<long>(arr);
            arrList.Sort();
            return arrList.ToArray();
        }
        public long[] SelectionSort(long[] array)
        {
            var arrayLength = array.Length;
            for (int i = 0; i < arrayLength - 1; i++)
            {
                var smallestVal = i;
                for (int j = i + 1; j < arrayLength; j++)
                {
                    if (array[j] < array[smallestVal])
                    {
                        smallestVal = j;
                    }
                }
                var tempVar = array[smallestVal];
                array[smallestVal] = array[i];
                array[i] = tempVar;
            }
            return array;
        }

        public long[] BubbleSort(long[] array)
        {
            long length = array.Length;

            long temp = array[0];

            for (long i = 0; i < length; i++)
            {
                for (long j = i + 1; j < length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];

                        array[i] = array[j];

                        array[j] = temp;
                    }
                }
            }

            return array;
        }

    }
    public class SortsCompare
    {
        private const int _N = 1000;
        private readonly long[] _data;

        private Sort _sort = new Sort();
        public SortsCompare()
        {
            _data = new long[_N];
            for (int i = 0; i < _N; i++)
            {
                _data[i] = new Random(42).NextInt64(10000);
            }
        }


        [Benchmark]
        public long[] SelectionSort() => _sort.SelectionSort(_data);

        [Benchmark]
        public long[] BubbleSort() => _sort.BubbleSort(_data);
        [Benchmark]
        public long[] BuiltInSort() => _sort.BuiltInSort(_data);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}