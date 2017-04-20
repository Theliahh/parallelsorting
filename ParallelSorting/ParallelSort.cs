using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ParallelSorting
{
    class ParallelSort
    {
        const int numElements = 1000000;
        static int[] unsortedElements = new int[numElements];
        static int[] firstArray;
        static int[] secondArray;
        static int[] thirdArray;
        static int[] fourthArray;

        static int numThreads = 4;
        static ManualResetEvent resetEvent;

        static void Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();
            resetEvent = new ManualResetEvent(false);
            Random rng = new Random();
            int next;
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();
            List<int> thirdList = new List<int>();
            List<int> fourthList = new List<int>();

            for (int i = 0; i < unsortedElements.Length; i++)
            {
                next = rng.Next(1, numElements);
                if(next < numElements / numThreads)
                {
                    firstList.Add(next);
                }
                else if(next > numElements / numThreads && next < numElements / 2)
                {
                    secondList.Add(next);
                }
                else if(next > numElements / (numThreads / 2) && next < (numElements / 4) * 3)
                {
                    thirdList.Add(next);
                }
                else
                {
                    fourthList.Add(next);
                }

            }
            firstArray = firstList.ToArray();
            secondArray = secondList.ToArray();
            thirdArray = thirdList.ToArray();
            fourthArray = fourthList.ToArray();

            sw.Start();
            
            Thread threadOne = new Thread(new ThreadStart(ThreadSortOne));
            Thread threadTwo = new Thread(new ThreadStart(ThreadSortTwo));
            Thread threadThree = new Thread(new ThreadStart(ThreadSortThree));
            Thread threadFour = new Thread(new ThreadStart(ThreadSortFour));
            threadOne.Start();
            threadTwo.Start();
            threadThree.Start();
            threadFour.Start();

            resetEvent.WaitOne();
            int[] sortedArray = new int[numElements];
            Array.Copy(firstArray, sortedArray, firstArray.Length);
            Array.Copy(secondArray, 0, sortedArray, firstArray.Length, secondArray.Length);
            Array.Copy(thirdArray, 0, sortedArray, firstArray.Length + secondArray.Length, thirdArray.Length);
            Array.Copy(fourthArray, 0, sortedArray, firstArray.Length + secondArray.Length + thirdArray.Length, fourthArray.Length);
            sw.Stop();
            for (int i = 0; i < sortedArray.Length; i++)
            {
                Console.WriteLine(sortedArray[i]);
            }
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            Console.ReadKey();
            Console.ReadKey();
        }

        public static void ThreadSortOne()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(firstArray);
            /*for(int i = 0; i< sorted.Length; i++)
            {
                Console.Write("{0} ",sorted[i]);
            }*/
            if(Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }
            
        }

        public static void ThreadSortTwo()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(secondArray);
            //for(int i = 0; i< sorted.Length; i++)
            //{
            //    Console.Write("{0} ",sorted[i]);
            //}
            if (Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }

        }

        public static void ThreadSortThree()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(thirdArray);
            //for(int i = 0; i< sorted.Length; i++)
            //{
            //    Console.Write("{0} ",sorted[i]);
            //}
            if (Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }

        }

        public static void ThreadSortFour()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(fourthArray);
            //for(int i = 0; i< sorted.Length; i++)
            //{
            //    Console.Write("{0} ",sorted[i]);
            //}
            if (Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }

        }

        public static int[] InsertionSort(int[] toBeSorted)
        {
            int currentIndex;
            for(int i = 1; i<toBeSorted.Length;i++)
            {
                currentIndex = i;
                while(currentIndex > 0 && toBeSorted[currentIndex - 1] > toBeSorted[currentIndex])
                {
                    int temp = toBeSorted[currentIndex];
                    toBeSorted[currentIndex] = toBeSorted[currentIndex - 1];
                    toBeSorted[currentIndex - 1] = temp;
                    currentIndex--; ;
                }
            }
            return toBeSorted;
        }
    }
}
