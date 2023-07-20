using System;

namespace cw4
{
    class Program
    {
        class OldMachine
        {
            int ProcessorCount;
            string[] ShouldHaveAtLeastOne = { "reshte", "str" };
            public OldMachine(string num, string s1 = "reshte", string s2 = "str")
            {
                int n = int.Parse(num);
                ProcessorCount = n;
                ShouldHaveAtLeastOne[0] = s1;
                ShouldHaveAtLeastOne[1] = s2;
            }
            public void Print(string[] str, int n)
            {
                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        if (!str[i].Contains(ShouldHaveAtLeastOne[0]) && !str[i].Contains(ShouldHaveAtLeastOne[1]))
                        {
                            throw new Exception("string does not have substrings");
                        }
                        string[] parts = str[i].Split(ShouldHaveAtLeastOne[0]);
                        for (int j = 0; j < parts.Length; j++)
                        {
                            if (parts[j].Contains(ShouldHaveAtLeastOne[1]))
                            {
                                string[] Spart = parts[j].Split(ShouldHaveAtLeastOne[1]);
                                parts[j] = "";
                                foreach (string p in Spart)
                                {
                                    parts[j] += p;
                                }
                            }
                        }
                        str[i] = "";
                        foreach (string p in parts)
                        {
                            str[i] += p;
                        }
                        Console.WriteLine(str[i]);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            public void Sum()
            {
                int sum = 0;
                string ans = "";
                ans = Console.ReadLine();
                while (ans != "SUM")
                {
                    try
                    {
                        sum += int.Parse(ans);
                    }
                    catch
                    {
                        Console.WriteLine("ERROR: please enter an integer or SUM to end");
                    }
                    ans = Console.ReadLine();
                }
                Console.WriteLine("sum is {0}", sum);
            }
        }
        static void Main(string[] args)
        {
            try
            {
                string[] numbers = Console.ReadLine().Split(' ');
                int strCount = int.Parse(numbers[1]);
                OldMachine m;
                if (strCount == 1)
                {
                    string s1 = Console.ReadLine();
                    m = new OldMachine(numbers[0], s1);
                }
                else
                {
                    string[] s = Console.ReadLine().Split(' ');
                    m = new OldMachine(numbers[0], s[0], s[1]);
                }
                string ans = "";
                while (ans != "End")
                {
                    try { 
                    Console.WriteLine("p = print\ns = sum\nEnd = finish");
                    ans = Console.ReadLine();
                    if (ans == "s")
                    {
                        m.Sum();
                    }
                    else if (ans == "p")
                    {
                        int wordNum = 0;
                        string[] words = new string[100];
                        string pAns = "";
                        pAns = Console.ReadLine();
                        while (pAns != "End")
                        {
                            words[wordNum] = pAns;
                            wordNum++;
                            pAns = Console.ReadLine();
                        }
                        m.Print(words, wordNum);
                    }
                        else if (ans != "End")
                        {
                            throw new Exception();
                        }
}
                    catch
                    {
                        Console.WriteLine("please enter a valid answer ");
                    }
                }

            }
            catch
            {
                Console.WriteLine("wronge input so this is the end");
            }
        }
    }
}
