using System;
using System.Collections.Generic;
using System.Collections;
namespace q3
{
    class Program
    {
        public class MyStack<T> : IEnumerable, IData<T>
    {
        public void Add(T a)
        {
                    Push(a);
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return Elements[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get;
            set;
        }
        public int MaxSize
        {
            get;
            set;
        }
        public T[] Elements
        {
            get;
            set;
        }
        public MyStack(int m)
        {
            MaxSize = m;
            Elements = new T[MaxSize];
            Count = 0;
        }
        public T Top
        {
            get
            {
                if (Count != 0)
                    return Elements[Count - 1];
                else
                {
                    throw new Exception("stack is empty");
                }
            }
        }
        public void Push(T a)
        {

            if (Count < MaxSize)
            {
                Elements[Count] = a;
                Count ++;
            }
            else
            {
                throw new Exception("stack is full !");
            }

        }
        public T Pop()
        {
            if (Count > 0)
            {

                Count --;
                return Elements[Count];
            }
            else
            {
                throw new Exception("stack is empty !");
            }
        }
        public void Clear()
        {
            Count = 0;
        }

        public void Print(PrintType t)
        {
            List<T> items = new List<T>();
            for (int i = Count - 1; i >= 0; i--)
            {
                if (t(int.Parse(Elements[i].ToString())))
                {
                    items.Add(Elements[i]);
                }
            }
            foreach (T item in items)
            {
                Console.WriteLine(item);
            }
        }


    }

    public interface IData<T>
    {
        public int Count
        {
            get;
            set;
        }
        public int MaxSize
        {
            get;
            set;
        }
        public T[] Elements//size should be the maxsize
        {
            get;
            set;
        }
    }
    
        public delegate bool PrintType(int a);
        public static bool isEven(int a)
        {
            if (a % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isOdd(int a)
        {
            if (a % 2 == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool nothing (int a)
        {
                return true;
        }
       
        static void Main(string[] args)
        {
            Console.WriteLine("enter max size:");
            
            string input = Console.ReadLine();
            int size=0;
            bool valid = false;
            while (!valid)
            {
                if (!int.TryParse(input, out size))
                {
                    Console.WriteLine("wrong input!");
                    input = Console.ReadLine();
                }
                else
                    valid = true;
            }

            //MyStack<int> s = new MyStack<int>(3) { 1,2,3 };
            MyStack<int> s = new MyStack<int>(size) {};
            bool end = false;
            do
            {
                Console.WriteLine("1.Push\n2.Pop\n3.Top\n4.Print\n5.Exit");
                try
                {
                    int ans = int.Parse(Console.ReadLine());
                    switch (ans)
                    {
                        case 5:
                            end = true;
                            break;
                        case 1:
                            input = Console.ReadLine();
                            int parameter;
                                if (!int.TryParse(input, out parameter))
                                {
                                    Console.WriteLine("wrong input!");
                                }
                                else
                                    s.Push(parameter);
                            break;
                        case 2:
                            Console.WriteLine(s.Pop());
                            break;
                        case 3:
                            Console.WriteLine(s.Top);
                            break;
                        case 4:
                            Console.WriteLine("1.Odd items\n2.Even items\n3.all items");
                            input = Console.ReadLine();
                            if (!int.TryParse(input, out parameter))
                            {
                                Console.WriteLine("wrong input!");
                            }
                            else
                            {
                                switch (parameter)
                                {
                                    case 1:
                                        PrintType pType = new PrintType(isOdd);
                                        s.Print(pType);
                                        break;
                                    case 2:
                                        pType = new PrintType(isEven);
                                        s.Print(pType);
                                        break;
                                    case 3:
                                        pType = new PrintType(nothing);
                                        s.Print(pType);
                                        break;
                                }
                            }
                            break;

                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            } while (!end);
        }
    }

}
