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
        const int numElements = 1000;
        static int[] unsortedElements = new int[numElements];
        static int[] firstArray;
        static int[] secondArray;
        static int[] thirdArray;
        static int[] fourthArray;
        static int[] fifthArray;
        static int[] sixthArray;
        static int[] seventhArray;
        static int[] eighthArray;

        static int numThreads = 8;
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
            List<int> fifthList = new List<int>();
            List<int> sixthList = new List<int>();
            List<int> seventhList = new List<int>();
            List<int> eighthList = new List<int>();


            for (int i = 0; i < unsortedElements.Length; i++)
            {
                next = rng.Next(1, numElements);
                if(next < numElements / numThreads)
                {
                    firstList.Add(next);
                }
                else if(next > numElements / numThreads && next < numElements / (numThreads / 2))
                {
                    secondList.Add(next);
                }
                else if(next > numElements / (numThreads / 2) && next < (numElements / numThreads) * 3)
                {
                    thirdList.Add(next);
                }
                else if(next > (numElements / numThreads) * 3 && next < numElements / 2)
                {
                    fourthList.Add(next);
                }
                else if(next > numElements / 2 && next < (numElements / numThreads) * 5)
                {
                    fifthList.Add(next);
                }
                else if(next > (numElements / numThreads) * 5 && next < (numElements / numThreads) * 6)
                {
                    sixthList.Add(next);
                }
                else if(next > (numElements / numThreads) * 6 && next < (numElements / numThreads) * 7)
                {
                    seventhList.Add(next);
                }
                else
                {
                    eighthList.Add(next);
                }

            }
            firstArray = firstList.ToArray();
            secondArray = secondList.ToArray();
            thirdArray = thirdList.ToArray();
            fourthArray = fourthList.ToArray();
            fifthArray = fifthList.ToArray();
            sixthArray = sixthList.ToArray();
            seventhArray = seventhList.ToArray();
            eighthArray = eighthList.ToArray();

            sw.Start();
            
            Thread threadOne = new Thread(new ThreadStart(ThreadSortOne));
            Thread threadTwo = new Thread(new ThreadStart(ThreadSortTwo));
            Thread threadThree = new Thread(new ThreadStart(ThreadSortThree));
            Thread threadFour = new Thread(new ThreadStart(ThreadSortFour));
            Thread threadFive = new Thread(new ThreadStart(ThreadSortFive));
            Thread threadSix = new Thread(new ThreadStart(ThreadSortSix));
            Thread threadSeven = new Thread(new ThreadStart(ThreadSortSeven));
            Thread threadEight = new Thread(new ThreadStart(ThreadSortEight));

            threadOne.Start();
            threadTwo.Start();
            threadThree.Start();
            threadFour.Start();
            threadFive.Start();
            threadSix.Start();
            threadSeven.Start();
            threadEight.Start();

            resetEvent.WaitOne();
            int[] sortedArray = new int[numElements];
            Array.Copy(firstArray, sortedArray, firstArray.Length);
            Array.Copy(secondArray, 0, sortedArray, firstArray.Length, secondArray.Length);
            Array.Copy(thirdArray, 0, sortedArray, firstArray.Length + secondArray.Length, thirdArray.Length);
            Array.Copy(fourthArray, 0, sortedArray, firstArray.Length + secondArray.Length + thirdArray.Length, fourthArray.Length);
            Array.Copy(fifthArray, 0, sortedArray, firstArray.Length + secondArray.Length + thirdArray.Length + fourthArray.Length, fifthArray.Length);
            Array.Copy(sixthArray, 0, sortedArray, firstArray.Length + secondArray.Length + thirdArray.Length + fourthArray.Length + fifthArray.Length, sixthArray.Length);
            Array.Copy(seventhArray, 0, sortedArray, firstArray.Length + secondArray.Length + thirdArray.Length + fourthArray.Length + fifthArray.Length + sixthArray.Length, seventhArray.Length);
            Array.Copy(eighthArray, 0, sortedArray, firstArray.Length + secondArray.Length + thirdArray.Length + fourthArray.Length + fifthArray.Length + sixthArray.Length + seventhArray.Length, eighthArray.Length);
            sw.Stop();
            //for (int i = 0; i < sortedArray.Length; i++)
            //{
            //    Console.WriteLine(sortedArray[i]);
            //}
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

        public static void ThreadSortFive()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(fifthArray);
            //for(int i = 0; i< sorted.Length; i++)
            //{
            //    Console.Write("{0} ",sorted[i]);
            //}
            if (Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }

        }

        public static void ThreadSortSix()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(sixthArray);
            //for(int i = 0; i< sorted.Length; i++)
            //{
            //    Console.Write("{0} ",sorted[i]);
            //}
            if (Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }

        }

        public static void ThreadSortSeven()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(seventhArray);
            //for(int i = 0; i< sorted.Length; i++)
            //{
            //    Console.Write("{0} ",sorted[i]);
            //}
            if (Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }

        }

        public static void ThreadSortEight()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = InsertionSort(eighthArray);
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
