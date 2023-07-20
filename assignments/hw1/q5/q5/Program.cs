using System;
using System.Collections.Generic;
namespace q5
{
    class Program
    {
        static void Main()
        {
            int n ,k;
            string[] numbers = Console.ReadLine().Split(' ');
            n=int .Parse(numbers[0]);
            k= int.Parse(numbers[1]);
            List<int> nums = new List<int>();
            int[] factorial = new int[n+1];
            factorial[0] = 1;
            if (n == 1)
            {
                Console.Write(1);
                return;
            }
            for(int i = 1; i <= n; i++)
            {
                nums.Add(i);
                factorial[i] = factorial[i - 1] * i;
            }

            for (int i = n - 1; i >= 1; i--)
            {
                if (k % factorial[i] == 0)
                {

                    Console.Write(nums[(k / factorial[i]) - 1]);
                    nums.RemoveAt((k / factorial[i]) - 1);
                    while (nums.Count > 0)
                    {

                        Console.Write(nums[nums.Count - 1]);
                        nums.RemoveAt(nums.Count - 1);
                    }
                    break;
                }
                else
                {
                    Console.Write(nums[k / factorial[i]]);
                    nums.RemoveAt(k / factorial[i]);
                k = k % factorial[i];
                }
                
            }
        }
    }
}
