using System;
using System.Collections.Generic;
namespace q4
{
    class Program
    {
        static void Main(string[] args)
        {

            int n,s,t;
            n = int.Parse(Console.ReadLine());
            string street = Console.ReadLine();
            string[] numbers = Console.ReadLine().Split(' ');
            s = int.Parse(numbers[0]);
            t = int.Parse(numbers[1]);

            int counter = 0;
            if (s > t)
            {
                t = s + t;
                s = t - s;
                t = t - s;
            }//so s is always less than t
            string temp;
            int sum = 0;
            for(int i = s ; i < t-1; i++)
            {
                if (street[i] == 'P')
                {

                    temp = Convert.ToString(counter, 2);
                    sum+= temp.Length - temp.Replace("1", "").Length;
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }

            temp = Convert.ToString(counter, 2);
            sum += temp.Length - temp.Replace("1", "").Length;

            Console.WriteLine(sum);
            
        }
    }
}
