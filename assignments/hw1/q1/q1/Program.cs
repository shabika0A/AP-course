using System;

namespace q1
{
    class Program
    {
        static int findsubstring(int len,string str)
        {
            int sublen = 2;
            int aCount, bCount, cCount;
            while (sublen <= 7)
            {
                if (sublen == 2)
                {
                    for (int i = 0; i <= len - sublen; i++)
                    {
                        if (str[i] == 'a' && str[i + 1] == 'a')
                        {
                            return 2;
                        }
                    }
                    sublen++;
                }
                    for (int i = 0; i <= len - sublen; i++)
                {
                    aCount = 0;
                    bCount = 0;
                    cCount = 0;
                    for(int j = i; j < i + sublen; j++)
                    {
                        if (str[j] == 'a')
                        {
                            aCount++;
                        }
                        else if (str[j] == 'b')
                        {
                            bCount++;
                        }
                        else if (str[j] == 'c')
                        {
                            cCount++;
                        }
                    }
                    if (aCount > bCount && aCount > cCount)
                    {
                        return sublen;
                    }
                }
                sublen++;
            }
            return -1;
        }
        static void Main(string[] args)
        {
            int n,len;
            n = int.Parse(Console.ReadLine());
            string[] text = new string[n];
            int[] length = new int[n];
            int[] ans = new int[n];
            for (int i = 0; i < n ; i++)
            {
                length[i]= int.Parse(Console.ReadLine());
                text[i] = Console.ReadLine();
            }
            for (int i = 0; i < n ; i++)
            {
                Console.WriteLine(findsubstring(length[i], text[i]));
            }
        }
    }
}
