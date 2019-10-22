using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorts
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            START TEST: SORT ARRAY WITH 1000 ELEMENTS
            ==============================================
            BubbleSort took:    3 milliseconds
            SelectionSort took: 1 milliseconds
            InsertionSort took: 1 milliseconds
            ShellSort took:     0 milliseconds
            MergeSort took:     0 milliseconds
            LinQ took:          3 milliseconds
            
            START TEST: SORT ARRAY WITH 5000 ELEMENTS
            ==============================================
            BubbleSort took:    87 milliseconds
            SelectionSort took: 41 milliseconds
            InsertionSort took: 67 milliseconds
            ShellSort took:      9 milliseconds
            MergeSort took:      1 milliseconds
            LinQ took:           3 milliseconds
            
            START TEST: SORT ARRAY WITH 25000 ELEMENTS
            ==============================================
            BubbleSort took:    2316 milliseconds
            SelectionSort took:  866 milliseconds
            InsertionSort took: 1129 milliseconds
            ShellSort took:      217 milliseconds
            MergeSort took:        8 milliseconds
            LinQ took:             9 milliseconds
            
            START TEST: SORT ARRAY WITH 50000 ELEMENTS
            ==============================================
            BubbleSort took:    9038 milliseconds
            SelectionSort took: 3435 milliseconds
            InsertionSort took: 4483 milliseconds
            ShellSort took:      857 milliseconds
            MergeSort took:       16 milliseconds
            LinQ took:            16 milliseconds
            
            START TEST: SORT ARRAY WITH 250000 ELEMENTS
            ==============================================
            BubbleSort took:    223269 milliseconds
            SelectionSort took:  86133 milliseconds
            InsertionSort took: 112090 milliseconds
            ShellSort took:      21647 milliseconds
            MergeSort took:         83 milliseconds
            LinQ took:              77 milliseconds
             */
            int length = args.Length > 0 ? Convert.ToInt32(args[0]) : 250000;
            int[] test = new int[length];
            bool error = false;
            Random randNum = new Random();
            for (int i = 0; i < length; i++)
            {
                test[i] = randNum.Next(0, (int)Math.Ceiling(length * 1.5));
            }
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine($"START TEST: SORT ARRAY WITH {length} ELEMENTS");
            Console.WriteLine("==============================================");

            int[] array = (int[])test.Clone(); error = false;
            stopwatch.Start();
            BubbleSort(array);
            stopwatch.Stop();
            for (int i = 1; i < array.Length; i++) { if (array[i] >= array[i - 1]) continue; else error = true; }
            Console.WriteLine($"BubbleSort took: {stopwatch.ElapsedMilliseconds} milliseconds");
            if (error) Console.WriteLine("Sort did not work correctly");
            stopwatch.Reset();

            array = (int[])test.Clone(); error = false;
            stopwatch.Start();
            SelectionSort(array);
            stopwatch.Stop();
            for (int i = 1; i < array.Length; i++) { if (array[i] >= array[i - 1]) continue; else error = true; }
            Console.WriteLine($"SelectionSort took: {stopwatch.ElapsedMilliseconds} milliseconds");
            if (error) Console.WriteLine("Sort did not work correctly");
            stopwatch.Reset();

            array = (int[])test.Clone(); error = false;
            stopwatch.Start();
            InsertionSort(array);
            stopwatch.Stop();
            for (int i = 1; i < array.Length; i++) { if (array[i] >= array[i - 1]) continue; else error = true; }
            Console.WriteLine($"InsertionSort took: {stopwatch.ElapsedMilliseconds} milliseconds");
            if (error) Console.WriteLine("Sort did not work correctly");
            stopwatch.Reset();

            array = (int[])test.Clone(); error = false;
            stopwatch.Start();
            ShellSort(array);
            stopwatch.Stop();
            for (int i = 1; i < array.Length; i++) { if (array[i] >= array[i - 1]) continue; else error = true; }
            Console.WriteLine($"ShellSort took: {stopwatch.ElapsedMilliseconds} milliseconds");
            if (error) Console.WriteLine("Sort did not work correctly");
            stopwatch.Reset();

            array = (int[])test.Clone(); error = false;
            stopwatch.Start();
            MergeSort(array, 0, array.Length - 1);
            stopwatch.Stop();
            for (int i = 1; i < array.Length; i++) { if (array[i] >= array[i - 1]) continue; else error = true; }
            Console.WriteLine($"MergeSort took: {stopwatch.ElapsedMilliseconds} milliseconds");
            if (error) Console.WriteLine("Sort did not work correctly");
            stopwatch.Reset();
            
            array = (int[])test.Clone(); error = false;
            stopwatch.Start();
            array = array.OrderBy(i => i).ToArray();
            stopwatch.Stop();
            for (int i = 1; i < array.Length; i++) { if (array[i] >= array[i - 1]) continue; else error = true; }
            Console.WriteLine($"LinQ took: {stopwatch.ElapsedMilliseconds} milliseconds");
            if (error) Console.WriteLine("Sort did not work correctly");
            stopwatch.Reset();

            Console.WriteLine("==============================================");
            Console.WriteLine($"                  END TEST");

            Console.ReadLine();
        }

        private static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            bool swapped;
            for (int i = 0; i < n; i++)
            {
                swapped = false;
                for (int j = 1; j < (n - i); j++)
                    if (arr[j - 1] > arr[j])
                    {
                        int temp = arr[j - 1];
                        arr[j - 1] = arr[j];
                        arr[j] = temp;
                        swapped = true;
                    }
                if (!swapped)
                    break;
            }
        }

        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int index = i;
                for (int j = i + 1; j < arr.Length; j++)
                    if (arr[j] < arr[index])
                        index = j;

                int temp = arr[index];
                arr[index] = arr[i];
                arr[i] = temp;
            }
        }

        public static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
                for (int j = i; j > 0 && arr[j] < arr[j - 1]; j--)
                    if (arr[j] < arr[j - 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = temp;
                    }

        }

        public static void ShellSort(int[] arr)
        {
            int i, j, pos, temp;
            pos = 3;
            while (pos > 0)
            {
                for (i = 0; i < arr.Length; i++)
                {
                    j = i;
                    temp = arr[i];
                    while ((j >= pos) && (arr[j - pos] > temp))
                    {
                        arr[j] = arr[j - pos];
                        j = j - pos;
                    }
                    arr[j] = temp;
                }
                if (pos / 2 != 0)
                    pos = pos / 2;
                else if (pos == 1)
                    pos = 0;
                else
                    pos = 1;
            }
        }
        
        private void QuickSort(int[] arr, int start, int end)
        {
            int d;
            if (start < end)
            {
                int temp;
                int p = arr[end];
                int i = start - 1;

                for (int j = start; j <= end - 1; j++)
                {
                    if (arr[j] <= p)
                    {
                        i++;
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }

                temp = arr[i + 1];
                arr[i + 1] = arr[end];
                arr[end] = temp;

                d = i + 1;

                QuickSort(arr, start, d - 1);
                QuickSort(arr, d + 1, end);
            }
        }

        public static void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(arr, left, middle);
                MergeSort(arr, middle + 1, right);

                int[] leftArray = new int[middle - left + 1];
                int[] rightArray = new int[right - middle];

                Array.Copy(arr, left, leftArray, 0, middle - left + 1);
                Array.Copy(arr, middle + 1, rightArray, 0, right - middle);

                int i = 0;
                int j = 0;
                for (int k = left; k < right + 1; k++)
                {
                    if (i == leftArray.Length)
                    {
                        arr[k] = rightArray[j];
                        j++;
                    }
                    else if (j == rightArray.Length)
                    {
                        arr[k] = leftArray[i];
                        i++;
                    }
                    else if (leftArray[i] <= rightArray[j])
                    {
                        arr[k] = leftArray[i];
                        i++;
                    }
                    else
                    {
                        arr[k] = rightArray[j];
                        j++;
                    }
                }
            }
        }
    }
}
