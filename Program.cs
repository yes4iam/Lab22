using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задайте размерность массива:");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);
            

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(GetSum);
            Task<int> task2 = task1.ContinueWith<int>(func2);
           

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(ShowMax);
            Task<int> task3 = task1.ContinueWith<int>(func3);
                     

            task1.Start();

            //task2.Result();

            Console.ReadKey();

        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{array[i]} ");
            }

            return array;
        }

        static int GetSum(Task<int[]> task)
        {
            int sum = 0;
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];
            }
            return sum;
            //Console.WriteLine(sum);
           
        }

        static int ShowMax(Task<int[]> task)
        {
            int max = 0;            
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                if (array[i]>max)
                {
                    max = array[i];
                }

            }
            
            return max;
            //Console.WriteLine(max);
        }

        

    }
}
