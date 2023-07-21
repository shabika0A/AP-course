using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace q2
{
    static class Program
    {
        static List<Seller> sellers = new List<Seller>();
        static List<Student> students = new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();
        static List<Customer> customers = new List<Customer>();
        static List<Media> medias = new List<Media>();
        static Library lib = new Library();
        enum access
        {
            Admin,User
        }
        public static bool checkSSN(this string ssn)
            {
            if (ssn.Length != 10)
            {
                return false;
            }
            if (long.Parse(ssn) % 1111111111 == 0)
            {
                return false;
            }
            try
            {
                int a = int.Parse(ssn[9].ToString());
                int b = 0;
                for(int i = 0; i < 10; i++)
                {
                    b += int.Parse(ssn[i].ToString()) * (10 - i);
                }
                int c = b % 11;
                if (c == 0 && a == c)
                {
                    return true;
                }
                if (c == 1&& a ==1)
                {
                    return true;
                }
                if (c > 1 && a == Math.Abs(c - 11)) ;
                {
                    return true;
                }
                
            }
            catch
            {
                Console.WriteLine("wrong input");
                return false;
            }
            } 
        public static bool checkUserNameFormat(string s)
        {

            Regex email = new Regex("^(.+)+@+(.+)+.+(.+)+$");
            if (email.IsMatch(s))
            {
                return true;
            }
            else
                return false;

        }
        class Seller
        {
            string username;
            string password = "MyShop1234$";
            
            public Seller(string u,string p)
            {
                if (checkUserNameFormat(u))
                {
                    username = u;
                    
                }
                if (p != password)
                {
                   
                    throw new Exception("you have to enter deafult pass");
                }

            }
        }
        class Student
        {
            string username;
            string studentID;
            //pass??
            public Student(string u, string i)
            {
                if (!checkStudentID(i))
                {
                    throw new Exception("wrong ID");
                }
                username = u;
                studentID = i;
            }
            bool checkStudentID(string id)
            {
                if (id.Length != 8)
                {
                    Console.WriteLine("wrong length");
                    return false;
                }
                if (id[0]!= '9')
                {
                    Console.WriteLine("wrong format. it has to start with 9");
                    return false;
                }
                return true;

            }
            public void SaveToFile()
            {
                StreamWriter w = new StreamWriter(@"D:\uni\ap\codes\hw6\q2\CustomersInfo.txt", true);
                w.WriteLine(username);
                w.WriteLine(studentID);
                w.WriteLine("Student");
                w.Close();
            }
        }
        class Teacher
        {
            string username;
            string institute;
            public Teacher(string u,string i)
            {
                username = u;
                institute = i;
            }
            public void SaveToFile()
            {
                StreamWriter w = new StreamWriter(@"D:\uni\ap\codes\hw6\q2\CustomersInfo.txt", true);
                w.WriteLine(username);
                w.WriteLine(institute);
                w.WriteLine("Teacher");
                w.Close();
            }
        }
        class Customer
        {
            string username;
            string ssn;
            public Customer(string u,string s)
            {
                if (s.checkSSN())
                {
                    ssn = s;
                    username = u;
                }
                else
                {
                    throw new Exception("wrong ssn");
                }
            }
            public void SaveToFile()
            {
                StreamWriter w = new StreamWriter(@"D:\uni\ap\codes\hw6\q2\CustomersInfo.txt", true);
                w.WriteLine(username);
                w.WriteLine(ssn);
                w.WriteLine("Customer");
                w.Close();
            }
        }
        static void AdminMenu()
        {
            bool valid = false;
            do
            {
            Console.WriteLine("username:");
            string u = Console.ReadLine();
            Console.WriteLine("pass:");
            string p = Console.ReadLine();
            try
            {
                sellers.Add(new Seller(u, p));
                valid = true;
                    ProductMenu();
            }
            catch (Exception e)
            {
                    Console.WriteLine(e.Message);
                Console.WriteLine("enter them again.");
            }
            } while (!valid);

        }
        static void CostumerMenu()
        {
            bool valid = false;
            Console.WriteLine("Student or Teacher or Customer?");
            do
            {
                string ans = Console.ReadLine();
                if (ans == "Student")
                {
                    bool stuValid = false;
                    do
                    {
                        Console.WriteLine("username:");
                        string u = Console.ReadLine();
                        Console.WriteLine("studentID:");
                        string p = Console.ReadLine();
                        try
                        {
                            Student s = new Student(u, p);
                            s.SaveToFile();
                            students.Add(s);
                            stuValid= true;
                            valid = true;
                            CostumerShopping(ans);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("enter them again.");
                        }
                    } while (!stuValid);
                }
                else if (ans == "Teacher")
                {
                    bool TValid = false;
                    do
                    {
                        Console.WriteLine("username:");
                        string u = Console.ReadLine();
                        Console.WriteLine("institute:");
                        string p = Console.ReadLine();
                        try
                        {
                            Teacher t = new Teacher(u, p);
                            t.SaveToFile();
                            teachers.Add(t);
                            TValid = true;
                            valid = true;
                            CostumerShopping(ans);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("enter them again.");
                        }
                    } while (!TValid);
                }
                else if (ans == "Customer")
                {
                    bool cValid = false;
                    do
                    {
                        Console.WriteLine("username:");
                        string u = Console.ReadLine();
                        Console.WriteLine("ssn:");
                        string p = Console.ReadLine();
                        try
                        {
                            Customer t = new Customer(u, p);
                            t.SaveToFile();
                            customers.Add(t);
                            cValid = true;
                            valid = true;
                            CostumerShopping(ans);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("enter them again.");
                        }
                    } while (!cValid);
                }
                else
                {
                    Console.WriteLine("enter a valid input");
                }
            } while (!valid);
        }
        interface calculatePrice
        {

        }
        class Media
        {
            public string name;
            public float priceWithoutTax;
            public string ID;//must be unique
            public Media(string n,int p,string i)
            {
                name = n;
                priceWithoutTax = p;
                ID = i;
            }
        }
        class Books : Media
        {
            public string writer;
            public string publisher;
             public double calculateTax()
            {
                return priceWithoutTax * 1.1;
            }
            public Books(string w,string pub,string n,int p,string i) : base(n, p, i)
            {
                writer = w;
                publisher = pub;
            }
        }
        class Videos : Media
        {
            public int lengthByMinutes;
            public int CDcount;
            public double calculateTax()///be careful!returns final price
            {
                int pSum = 0;
                pSum += 3 * CDcount;
                pSum += 5 * (lengthByMinutes / 60);
                return priceWithoutTax * pSum/100 + priceWithoutTax;
            }
            public Videos(int l, int c, string n, int p, string i) : base(n, p, i)
            {
                lengthByMinutes = l;
                CDcount = c;
            }
        }
        class Magazines : Media
        {
            
            public string publisher;
            public int pageCount;
            public double calculateTax()
            {
                if (pageCount > 1 && pageCount < 21)
                {
                    return 1.02 * priceWithoutTax;
                }else if (pageCount > 20 && pageCount < 50)
                {
                    return 1.03 * priceWithoutTax;
                }
                else
                {
                    return 1.05 * priceWithoutTax;
                }

            }
            public Magazines(string pub,int pc, string n, int p, string i) : base(n, p, i)
            {
                publisher = pub;
                pageCount = pc;
            }
        }
        class Library
        {
            public void AddMedia(Books b)
            {
                StreamWriter s3 = new StreamWriter(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt", true);
                s3.WriteLine(b.ID);
                s3.WriteLine(b.name);
                s3.WriteLine(b.calculateTax());
                s3.WriteLine("Books");
                s3.WriteLine(b.writer);
                s3.WriteLine(b.publisher);
                s3.Close();
            }
            public void AddMedia(Videos b)
            {
                StreamWriter s3 = new StreamWriter(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt", true);
                s3.WriteLine(b.ID);
                s3.WriteLine(b.name);
                s3.WriteLine(b.calculateTax());
                s3.WriteLine("Videos");
                s3.WriteLine(b.lengthByMinutes);
                s3.WriteLine(b.CDcount);
                s3.Close();
            }
            public void AddMedia(Magazines b)
            {
                StreamWriter s3 = new StreamWriter(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt", true);
                s3.WriteLine(b.ID);
                s3.WriteLine(b.name);
                s3.WriteLine(b.calculateTax());
                s3.WriteLine("Magazines");
                s3.WriteLine(b.publisher);
                s3.WriteLine(b.pageCount);
                s3.Close();
            }
            public void DelMedia(string i)
            {
                string[] mediaFile = File.ReadAllLines(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt");
                int index = -1;
                for(int j = 0; j< mediaFile.Length; j *= 6)
                {
                    if (mediaFile[j] == i)
                    {
                        index = j;
                        break;
                    }
                }
                if (index == -1)
                {
                    throw new Exception("wrong id");
                }
                string[] newFileLines = new string[mediaFile.Length - 6];
                for(int j = 0; j < index; j++)
                {
                    newFileLines[j] = mediaFile[j];
                }
                for (int j = index; j < mediaFile.Length - 6; j++)
                {
                    newFileLines[j] = mediaFile[j+6];
                }
                File.WriteAllLines(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt", newFileLines);
                Console.WriteLine("product was deleted successfully ");
            }
            public void SearchMedia(string i)
            {
                string[] mediaFile = File.ReadAllLines(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt");
                int index = -1;
                for (int j = 0; j < mediaFile.Length; j *= 6)
                {
                    if (mediaFile[j] == i)
                    {
                        index = j;
                        break;
                    }
                }
                if (index == -1)
                {
                    throw new Exception("wrong id");
                }
                Console.WriteLine("ID "+mediaFile[index]);
                Console.WriteLine("Name "+mediaFile[index]+1);
                Console.WriteLine("price "+mediaFile[index]+2);
                Console.WriteLine("type " + mediaFile[index] + 3);
                Console.WriteLine(mediaFile[index]+4);
                Console.WriteLine(mediaFile[index]+5);
            }
        }
        static void firstMenu()
        {
            bool valid = false;
            Console.WriteLine("Admin or User?");
            do
            {
                string ans = Console.ReadLine();
                if (ans == "Admin")
                {
                    AdminMenu();
                    valid = true;
                }
                else if (ans == "User")
                {
                    valid = true;
                    CostumerMenu();
                    
                }
                else
                {
                    Console.WriteLine("enter a valid input");
                }
            } while (!valid);
            
        }
        static void ProductMenu()
        {
            bool end = false;
            do
            {
                Console.WriteLine("1.ADD\n2.DELETE\n3.SEARCH\n4.SHOW CUSTOMERS\n5.CHANGE PASS\n6.EXIT");
                try
                {
                    int ans=int.Parse(Console.ReadLine());
                    if (ans < 1 || ans > 6)
                    {
                        throw new Exception("wrong input");
                    }
                    switch (ans)
                    {
                        case 1:
                            try
                            {
                            Console.WriteLine("name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("price");
                            int price = int.Parse(Console.ReadLine());
                            Console.WriteLine("ID:");
                            string id = Console.ReadLine();
                            Console.WriteLine("enter type: Book or Video or Magazine");
                            string t = Console.ReadLine();
                            switch (t)
                            {
                                case "Book":
                                    Console.WriteLine("writer:");
                                    string writer = Console.ReadLine();
                                    Console.WriteLine("publisher");
                                    string publish = Console.ReadLine();
                                        lib.AddMedia(new Books(writer, publish, name, price, id));
                                    break;
                                case "Video":
                                    Console.WriteLine("length by minute:");
                                    int len = int.Parse(Console.ReadLine());
                                    Console.WriteLine("number of CDs:");
                                    int cd = int.Parse(Console.ReadLine());
                                        lib.AddMedia(new Videos(len, cd, name, price, id));
                                    break;
                                case "Magazine":
                                    Console.WriteLine("publisher:");
                                    string pub = Console.ReadLine();
                                    Console.WriteLine("number of pages:");
                                    int pc = int.Parse(Console.ReadLine());
                                        lib.AddMedia(new Magazines(pub, pc, name, price, id));
                                    break;
                            }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("wrong input");
                            }
                            break;
                        case 2:
                            Console.WriteLine("enter ID:");
                            string i = Console.ReadLine();
                            lib.DelMedia(i);
                            break;
                        case 3:
                            Console.WriteLine("enter ID:");
                            i = Console.ReadLine();
                            lib.SearchMedia(i);
                            break;
                        case 4:
                            Console.WriteLine("costumers:");
                            string[] lines = File.ReadAllLines(@"D:\uni\ap\codes\hw6\q2\CustomersInfo.txt");
                            for(int j = 0; j < lines.Length; j++)
                            {
                                Console.WriteLine(lines[j]);
                            }
                            break;
                        case 5:
                            ////////remained: change pass
                            break;
                        case 6:
                            end = true;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (!end);
        }
        static void showProducts()
        {
           
                Console.WriteLine(File.ReadAllText(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt"));
                
        }
        static (string,int) findProduct(string n)
        {
            string[] lines = File.ReadAllLines(@"D:\uni\ap\codes\hw6\q2\CustomersInfo.txt");
            for(int i = 1; i < lines.Length; i += 6)
            {
                if (lines[i] == n)
                {
                    return (n, int.Parse(lines[i + 1]));
                }
            }
            return ("",-1);
        }
        static void CostumerShopping(string s)
        {
            List<(string, float)> bag = new List<(string, float)>();
            bool end = false;
            int disPersentage = 0;
            float totalPrice = 0;
            switch (s)
            {
                case "Student":
                    disPersentage = 20;
                    break;
                case "Teacher":
                    disPersentage = 15;
                    break;
                case "Customer":
                    disPersentage = 5;
                    break;
            }
            while (!end)
            {
                Console.WriteLine("1.SELECT \n2.EDIT \n3.BUY \n4.CHANCE \n5.EXIT");
                try
                {
                    int ans = int.Parse(Console.ReadLine());
                    

                    switch (ans)
                    {
                        case 5:
                            end = true;
                            break;
                        case 1:
                            showProducts();
                            Console.WriteLine("enter product name:");
                            string name = Console.ReadLine();
                            if (!File.ReadAllText(@"D:\uni\ap\codes\hw6\q2\MediaInfo.txt").Contains(name)) {
                                Console.WriteLine("this product does not exist");
                            }
                            else
                            {
                                bag.Add(findProduct(name));
                            }
                            break;
                        case 2:
                            for (int i = 0; i < bag.Count; i++)
                            {
                                Console.WriteLine(bag[i].Item1 + "     " + bag[i].Item2);
                            }
                            Console.WriteLine("enter product name:");
                            name = Console.ReadLine();
                            bool deleted = false;
                            for (int i = 0; i < bag.Count; i++)
                            {
                                if (bag[i].Item1 == name)
                                {
                                    bag.RemoveAt(i);
                                    deleted = true;
                                    break;
                                }
                            }
                            if (!deleted)
                            {
                                Console.WriteLine("wrong input");
                            }
                            break;
                        case 3:
                            float sum = 0;
                            for (int i = 0; i < bag.Count; i++)
                            {
                                sum += bag[i].Item2;
                            }
                           // totalPrice = sum;
                            sum *= (100 - disPersentage) / 100;
                            Console.WriteLine("total price:{0}", sum);

                                /////////////tayid va kharid
                                break;
                        case 4:
                            int[] chances = { 0, 2, 3, 5, 7, 10, 15, 25, 30 };
                            Random r = new Random(9);
                            int t = chances[r.Next()];
                            disPersentage += t;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("there is a problem. enter them again.");
                }
            }
        }
        static void Main(string[] args)
        {
            bool valid = false;
            do
            {
                firstMenu();
            } while (!valid);

        }

    }
}
