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
        static int[] unsortedElements = new int[10000];
        static int numThreads = 1;
        static ManualResetEvent resetEvent;

        static void Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();
            resetEvent = new ManualResetEvent(false);
            Random rng = new Random();

            for(int i = 0; i < unsortedElements.Length; i++)
            {
                unsortedElements[i] = rng.Next(1, 1000000);
            }
            sw.Start();
            ThreadStart threadRef = new ThreadStart(ThreadTest);
            Thread testThread = new Thread(threadRef);
            testThread.Start();

            resetEvent.WaitOne();
            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            Console.ReadKey();
        }

        public static void ThreadTest()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            int[] sorted = insertionSort(unsortedElements);
            /*for(int i = 0; i< sorted.Length; i++)
            {
                Console.Write("{0} ",sorted[i]);
            }*/
            if(Interlocked.Decrement(ref numThreads) == 0)
            {
                resetEvent.Set();
            }
            
        }

        public static int[] insertionSort(int[] toBeSorted)
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
