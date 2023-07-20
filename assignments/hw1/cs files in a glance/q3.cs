using System;
namespace q3
{
    class Program
    {
        static bool isPrime(int n)
        {
            if (n == 1)
            {
                return false;
            }
            bool prime = true;
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    prime = false;
                    break;
                }
            }
            return prime;
        }
        static int digitSum(int n)
        {
            int sum = 0;
            while (n / 10 > 0)
            {
                sum += n % 10;
                n = n / 10;
            }
            sum += n;
            return sum;
        }
        static bool Pprime(int n)
        {
            if (!isPrime(n))//so n is not prime
            {
                return false;
            }
            else
            {
                if (n / 10 != 0)
                    return Pprime(digitSum(n));
                else
                    return true;
            }
        }
        static void kulatz(int n)
        {
            int levels = 0;
            while (n != 1)
            {
                Console.Write(n);
                if (Pprime(n))
                {
                    Console.WriteLine(" Y");
                }
                else
                {
                    Console.WriteLine(" N");
                }
                if (n % 2 == 0)
                {
                    n = n / 2;
                }
                else
                {
                    n += 1;
                }
                levels++;
            }
            Console.Write(n);//so n is 1
            if (Pprime(n))
            {
                Console.WriteLine(" Y");
            }
            else
            {
                Console.WriteLine(" N");
            }
            Console.WriteLine(levels);
        }
        static void Main(string[] args)
        {
            
            Random rnd = new Random();
            int num = rnd.Next(100000000);
            //just for checking : int num = int.Parse(Console.ReadLine());
            kulatz(num);
        }
    }
}
