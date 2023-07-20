using System;

namespace cw1
{
    class Program
    {
        static void PrintInReverseOrder(int[] a)
        {
            for(int i = a.Length-1;i >= 0;  i--)
            {
                Console.Write("{0} ",a[i]);//using place holder
            }
        }
        static void MinNum(int[] a)
        {
            int min=a[a.Length-1];
            for (int i = a.Length - 2; i >= 0; i--)
            {
                if (a[i] < min)
                {
                    min = a[i];
                }
            }
            Console.WriteLine("\n"+min);
        }
        static void Main()
        {
            int n;
            n=int.Parse(Console.ReadLine());
            int[] a = new int[n];
            string[] numbers = Console.ReadLine().Split(' ');
            a = Array.ConvertAll(numbers, int.Parse);
            PrintInReverseOrder(a);
            MinNum(a);
        }
    }
}
