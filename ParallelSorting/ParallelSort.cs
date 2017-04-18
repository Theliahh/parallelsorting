using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelSorting
{
    class ParallelSort
    {
        static int[] unsortedElements = new int[10000];
        static void Main(string[] args)
        {
            
            Random rng = new Random();
            for(int i = 0; i < unsortedElements.Length; i++)
            {
                unsortedElements[i] = rng.Next(1, 1000000);
            }

            ThreadStart threadRef = new ThreadStart(ThreadTest);
            Thread testThread = new Thread(threadRef);
            testThread.Start();
            Console.ReadKey();
        }

        public static void ThreadTest()
        {
            int[] sorted = insertionSort(unsortedElements);
            for(int i = 0; i< sorted.Length; i++)
            {
                Console.Write("{0} ",sorted[i]);
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
