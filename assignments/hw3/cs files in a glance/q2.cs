using System;
using System.Collections.Generic;
using System.IO;
namespace q2
{
    class Program
    {
        enum type {Monkey=10,Lion,Elephant,Bear,Tiger,Giraffe }
        class Zoo
        {
            int ID;
            type t;
            public string name;
            public string location;
            public string[] food;
            private static int number = 0;
            static List<string> animalnames = new List<string>();
            
            public static int[] typeCount = new int[6];
           public Zoo(type TYPE,string Name,string loc,string[] Food)
            {
                if (IsValidName(Name))
                {
                t = TYPE;
                name = Name;
                location = loc;
                food = Food;
                    number++;
                    ID = number;
                animalnames.Add(Name);
                    typeCount[(int)t - 10]++;
                }
                else
                {
                    throw new Exception("invalid name!");
                }
                
            }
            public void SaveToFile()
            {
                try
                {
                int TYPE = (int)t;
                StreamWriter writer;
                switch (TYPE)
                {
                    case 10:
                    writer=new StreamWriter(@"D:\uni\ap\codes\hw3\q2\10.txt",true);
                    writer.Write("ID: ");
                    writer.WriteLine(ID);
                    writer.Write("Name: ");
                    writer.WriteLine(name);
                    writer.Write("Location: ");
                    writer.WriteLine(location);
                        writer.Write("Food: ");
                    for(int i=0;i<food.Length;i++)
                    {
                    writer.Write(food[i]);
                    if(i!=food.Length-1)
                    writer.Write("-");
                    }
                        writer.WriteLine();
                           writer.Close();
                    break;
                    case 11:
                        writer = new StreamWriter(@"D:\uni\ap\codes\hw3\q2\11.txt", true);
                        writer.Write("ID: ");
                        writer.WriteLine(ID);
                        writer.Write("Name: ");
                        writer.WriteLine(name);
                        writer.Write("Location: ");
                        writer.WriteLine(location);
                        writer.Write("Food: ");
                        for (int i = 0; i < food.Length; i++)
                        {
                            writer.Write(food[i]);
                            if (i != food.Length - 1)
                                writer.Write("-");
                        }
                        writer.WriteLine();
                        writer.Close();
                        break;
                    case 12:
                        writer = new StreamWriter(@"D:\uni\ap\codes\hw3\q2\12.txt", true);
                        writer.Write("ID: ");
                        writer.WriteLine(ID);
                        writer.Write("Name: ");
                        writer.WriteLine(name);
                        writer.Write("Location: ");
                        writer.WriteLine(location);
                        writer.Write("Food: ");
                        for (int i = 0; i < food.Length; i++)
                        {
                            writer.Write(food[i]);
                            if (i != food.Length - 1)
                                writer.Write("-");
                        }
                        writer.WriteLine();
                        writer.Close();
                        break;
                    case 13:
                        writer = new StreamWriter(@"D:\uni\ap\codes\hw3\q2\13.txt", true);
                        writer.Write("ID: ");
                        writer.WriteLine(ID);
                        writer.Write("Name: ");
                        writer.WriteLine(name);
                        writer.Write("Location: ");
                        writer.WriteLine(location);
                        writer.Write("Food: ");
                        for (int i = 0; i < food.Length; i++)
                        {
                            writer.Write(food[i]);
                            if (i != food.Length - 1)
                                writer.Write("-");
                        }
                        writer.WriteLine();
                        writer.Close();
                        break;
                    case 14:
                        writer = new StreamWriter(@"D:\uni\ap\codes\hw3\q2\14.txt", true);
                        writer.Write("ID: ");
                        writer.WriteLine(ID);
                        writer.Write("Name: ");
                        writer.WriteLine(name);
                        writer.Write("Location: ");
                        writer.WriteLine(location);
                        writer.Write("Food: ");
                        for (int i = 0; i < food.Length; i++)
                        {
                            writer.Write(food[i]);
                            if (i != food.Length - 1)
                                writer.Write("-");
                        }
                        writer.WriteLine();
                        writer.Close();
                        break;
                    case 15:
                        writer = new StreamWriter(@"D:\uni\ap\codes\hw3\q2\15.txt", true);
                        writer.Write("ID: ");
                        writer.WriteLine(ID);
                        writer.Write("Name: ");
                        writer.WriteLine(name);
                        writer.Write("Location: ");
                        writer.WriteLine(location);
                        writer.Write("Food: ");
                        for (int i = 0; i < food.Length; i++)
                        {
                            writer.Write(food[i]);
                            if (i != food.Length - 1)
                                writer.Write("-");
                        }
                        writer.WriteLine();
                        writer.Close();
                        break;
                }
                }
                catch
                {
                    Console.WriteLine("could not open the related file!");
                }
            }
            public bool IsValidName(string n)
            {
                if (Zoo.animalnames.Contains(n))
                {
                    return false;
                }
                foreach (char c in n)
                {
                    if (!((c < 91 && c > 64) || (c > 96 && c < 123)))
                    {
                        return false;
                    }
                }
                return true;
            }
            public static void Change(string N,string loc,List<Zoo> animals)
            {
                try { 
                if (animalnames.Contains(N)) { 
                int index = animalnames.IndexOf(N);
                
                string findName = "Name: " + N;
                int foundIndex = 0;
                string address = @"D:\uni\ap\codes\hw3\q2\";
                address +=(int) animals[index].t;
                address += ".txt";
                string[] lines = File.ReadAllLines(address);
                        
                for(int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == findName)
                    {
                        foundIndex = i ;
                        break;
                    }
                }
                lines[foundIndex + 1]="Location: " + loc;
                File.WriteAllLines(address,lines);
                animals[index].location = loc;
                }
                else
                {
                    Console.WriteLine("animal name is not valid!");
                }}
                catch
                {
                    Console.WriteLine("could not open related file!");
                }
            }
            public static void Change(string N, string[] f, List<Zoo> animals)
            {
                try
                {
                    if (animalnames.Contains(N))
                    {
                        int index = 0;
                        for (int i = 0; i < animalnames.Count; i++)
                        {
                            if (animalnames[i] == N)
                            {
                                index = i;
                                break;
                            }
                        }
                        
                        string findName = "Name: " + N;
                        int foundIndex = 0;
                        string address = @"D:\uni\ap\codes\hw3\q2\";
                        address += (int)animals[index].t;
                        address += ".txt";
                        string[] lines = File.ReadAllLines(address);

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i] == findName)
                            {
                                foundIndex = i;
                                break;
                            }
                        }
                        lines[foundIndex + 2] = "Food: ";
                        for (int i = 0; i < f.Length; i++)
                        {
                            lines[foundIndex + 2] += f[i];
                            if (i != f.Length - 1)
                                lines[foundIndex + 2] += "-";
                        }
                        lines[foundIndex + 2].Remove(lines[foundIndex + 2].Length - 1);
                        File.WriteAllLines(address, lines);
                        animals[index].food = f;
                    }
                    else
                    {
                        Console.WriteLine("animal name is not valid!");
                    }
                }
                catch
                {
                    Console.WriteLine("could not open related file!");
                }
            }
            public static void Change(string N, string loc, string[] f, List<Zoo> animals)
            {
                try
                {
                if (animalnames.Contains(N))
                {
                        
                int index = 0;
                for (int i = 0; i < animalnames.Count; i++)
                {
                    if (animalnames[i] == N)
                    {
                        index = i;
                        break;
                    }
                }
                animals[index].food = f;
                animals[index].location = loc;
                string findName = "Name: " + N;
                int foundIndex = 0;
                string address = @"D:\uni\ap\codes\hw3\q2\";
                address += (int)animals[index].t;
                address += ".txt";
                string[] lines = File.ReadAllLines(address);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == findName)
                    {
                        foundIndex = i;
                        break;
                    }
                }
                lines[foundIndex + 2] = "Food: ";
                for (int i = 0; i < f.Length; i++)
                {
                    lines[foundIndex + 2] += f[i];
                    if (i != f.Length - 1)
                        lines[foundIndex + 2] += "-";
                }
                lines[foundIndex + 1] = "Location: " + loc;
                File.WriteAllLines(address, lines);
            }
                 else
                {
                    Console.WriteLine("animal name is not valid!");
                }
            }
                catch
                {
                    Console.WriteLine("could not open related file!");
                }
    }
            public static void AllInfo()
            {
                try
                {
                    StreamWriter s = new StreamWriter(@"D:\uni\ap\codes\hw3\q2\AllInfo.txt");
                    s.WriteLine("number of monkeys: {0}", typeCount[0]);
                    s.WriteLine("number of lions: {0}", typeCount[1]);
                    s.WriteLine("number of elephants: {0}", typeCount[2]);
                    s.WriteLine("number of bears: {0}", typeCount[3]);
                    s.WriteLine("number of tigers: {0}", typeCount[4]);
                    s.WriteLine("number of giraffes: {0}", typeCount[5]);
                    s.WriteLine("total number of animals: {0}", animalnames.Count);
                    s.Close();
                }
                catch
                {
                    Console.WriteLine("could not open the related file!");
                }
            }
        }
        static void Main(string[] args)
        {
            List<Zoo> animals = new List<Zoo>();
            int animalNum = 0;
            bool valid = false;
            do
            {
                try
                {
                    
                    Console.WriteLine("Enter the number of animals to save:");
                    animalNum = int.Parse(Console.ReadLine());
                    valid = true;
                    for (int i = 0; i < animalNum; i++)
                    {
                        try
                        {
                            type t = type.Lion;
                            Console.WriteLine("Animal type (Monkey,Lion,Elephant,Bear,Tiger,Giraffe):");
                            switch (Console.ReadLine())
                            {
                                case "Monkey":
                                    t = type.Monkey;
                                    break;
                                case "Lion":
                                    t = type.Lion;
                                    break;
                                case "Elephant":
                                    t = type.Elephant;
                                    break;
                                case "Bear":
                                    t = type.Bear;
                                    break;
                                case "Tiger":
                                    t = type.Tiger;
                                    break;
                                case "Giraffe":
                                    t = type.Giraffe;
                                    break;
                                default:
                                    throw new Exception("type was not valid");
                            }
                            Console.WriteLine("enter name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("enter location: ");
                            string loc = Console.ReadLine();
                            Console.WriteLine("enter food (separated by ','):");
                            string[] foods = Console.ReadLine().Split(',');
                            animals.Add(new Zoo(t, name, loc, foods));
                            animals[animals.Count - 1].SaveToFile();
                        }
                        catch
                        {
                            Console.WriteLine("input has wrong format. please enter animal again.");
                            i--;
                        }
                    }
                    
                }
                catch
                {
                    Console.WriteLine("input has wrong format. please enter an integer.");
                }
            } while (!valid);
            Console.WriteLine("It’s time to change information.");

            Console.WriteLine("Enter the number of animals to change:");
            
            valid = false;
            while (!valid)
            {
                try
                {
                    animalNum = int.Parse(Console.ReadLine());
                    if (animalNum > animals.Count)
                    {
                        Console.WriteLine("Wrong! it is more than total number of animals . Enter a smaller number: ");
                    }
                    else
                        valid = true;
                }
                catch
                {
                    Console.WriteLine("input has wrong format. please enter an integer.");
                }
            }
            for (
                
                int i = 0; i < animalNum; i++)
            {
                try
                {
                    valid = true;
                    do
                    {
                        Console.WriteLine("Enter the name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter the new food: (if you do not want to change it press enter)\nEnter the new location: (if you do not want to change it press enter)");
                        string[] food = Console.ReadLine().Split(",");
                        string loc = Console.ReadLine();
                        if (food[0] == "" && loc == "")
                        {
                            Console.WriteLine("None of the values has been entered.Enter the values again:");
                            valid = false;
                        }
                        else if (food[0] == "")
                        {
                            Zoo.Change(name, loc, animals);
                            valid = true;
                        }
                        else if (loc == "")
                        {
                            Zoo.Change(name, food, animals);
                            valid = true;
                        }
                        else
                        {
                            Zoo.Change(name, loc, food, animals);
                            valid = true;
                        }
                    } while (!valid);
                }
                catch
                {
                    Console.WriteLine("input has wrong format. please enter animal again.");
                    i--;
                }
            }
            Zoo.AllInfo();
        }
    }
}
