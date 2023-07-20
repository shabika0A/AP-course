using System;
//code of Q2
namespace s1
{
    class Program
    {
        static void  chap(int n)
        {
            if (n == 0)
            {
                return;
            }
           int ghaede= n*4 - 1;
            Console.WriteLine(new string('*', ghaede));
            int i;
            //Console.WriteLine();
            for(i = 1; i < 2 * n-1;i++)
            {
                Console.Write(new string(' ', i));
                Console.Write("*");
                Console.Write(new string(' ', ghaede-2*(i+1)));
                Console.Write("*\n");
            }
            Console.Write(new string(' ', 2 * n - 1));
            Console.Write("*");
            Console.WriteLine();
            chap(n - 1);
        }
        static void Main(string[] args)
        {
            int n;
            n= int.Parse(Console.ReadLine());
            chap(n);
        }
    }
}
