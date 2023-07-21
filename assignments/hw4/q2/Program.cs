using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace q2
{
    public class Program
    {
        public class Product
        {
            static int totalNum = 0;
            int id;
            public int ID
            {
                get { return id; }
                set { id = value; }
            }
            int price;
            public int Price
            {
                get { return price; }
                set { price = value; }
            }
            string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            int score=1;
            public int Score
            {
                get { return score; }
                set { score = value; }
            }
            string factory;
            public string Factory
            {
                get { return factory; }
            }
            public Product(int p, string n)
            {
                Name = n;
                Price = p;
                totalNum++;
                int i = totalNum;
                ID = i;
                if (i >= 1 && i <= 5)
                {
                    factory = "a";
                }
                else if (i > 5 && i <= 10)
                {
                    factory = "b";
                }
                if (i > 10)
                {
                    factory = "c";
                }
            }
        }
        public enum ProcuctType { Phone = 1, car = 2, watch = 3, T_shirt = 4, Laptop = 5, Tablet = 6, Charger = 7, Glass = 8, Robot = 9 }
        public class Category
        {
            public int ID;
            public string name;
            public List<Product> products = new List<Product>();
            List<int> prices = new List<int>();
            public Category(ProcuctType p)
            {
                name = p.ToString();
                ID = (int)p;

            }
            public void AddPrductCategory(List<Product> p)
            {
                products.AddRange(p);
                foreach (Product pr in p)
                {
                    prices.Add(pr.Price);
                }
            }
            public void FilterByPrice(int l, int m)
            {
                Console.WriteLine("price    ID      name   score   factory");
                foreach (Product p in products)
                {
                    if (p.Price >= l && p.Price <= m)
                    {
                        Console.WriteLine("{0}        {1}        {2}        {3}        {4}", p.Price, p.ID, p.Name, p.Score, p.Factory);
                    }
                }
            }
            public void ShowSupply()
            {
                prices.Sort();
                Product[] sortedpr = new Product[products.Count];
                int index = 0;
                while (index < prices.Count - 1)
                {
                    foreach (Product p in products)
                    {
                        if (p.Price == prices[index])
                        {
                            sortedpr[index] = p;
                            index++;
                        }
                    }
                }
                Console.WriteLine("price        ID      name        score       factory");
                foreach (Product p in sortedpr)
                {
                    Console.WriteLine("{0}        {1}        {2}        {3}        {4}",p.Price,p.ID,p.Name,p.Score,p.Factory);
                }
            }
        }
        public enum Sex { male = 1, female = 2, others = 3 }
        public struct People
        {
            public string firstName;
            public string lastName;
            public int age;
            public Sex sex;
            public string phoneNum;
           public bool checkPhoneNum(string p)
            {
                if (p.Length != 11)
                {
                    return false;
                }
                if (p[0] != '0' || p[1] != '9')
                {
                    return false;
                }
                if (!(Regex.IsMatch(p,"^[0-9]+$")))
                {
                    return false;
                }
                return true;
            }
            public void EditProfile(string f,string l)
            {
                firstName = f;
                lastName = l;
            }
            public void EditProfile(int a)
            {
                age = a;
            }
            public void EditProfile(string p)
            {
                if (checkPhoneNum(p))
                {
                    phoneNum = p;
                }
            }
        }
        class Cart
        {
            public People owner;
            List<Product> products=new List<Product>();
            public Cart(People p)
            {
                owner = p;
            }
            public void AddProductToCart(List<Product> p)
            {
                products.AddRange(p);
            }
            public void DeleteProduct(List<Product> p)
            {
                foreach(Product pr in p)
                {
                    for(int i = 0; i < products.Count; i++)
                    {
                        if (pr == products[i])
                        {
                            products.RemoveAt(i);
                            break;
                        }
                    }
                }
                Console.WriteLine("products were deleted\nremained items:");
                foreach(Product pr in products)
                {
                    Console.WriteLine("{0}    {1}", pr.Name, pr.Price);
                }
            }
            public void CalculatePrice()
            {
                int sum = 0;
                foreach (Product pr in products)
                {
                    Console.WriteLine("{0}    {1}", pr.Name, pr.Price);
                    sum += pr.Price;
                }
                Console.WriteLine("total price is: {0}", sum);
            }
        }
        public static int showMenu()
        {
            bool valid = false;
            int ans = 0;
            do
            {
                Console.WriteLine("1.Category\n2.Cart\n3.exit");

                try
                {
                    ans = int.Parse(Console.ReadLine());
                    if (ans >= 1 && ans <= 3)
                    {
                        valid = true;
                    }
                    else
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("wrong input!");
                }
            } while (!valid);

            return ans;
        }
        public static int showCategoryMenu()
        {
            bool valid = false;
            int ans = 0;
            do
            {
                Console.WriteLine("1.AddProductCategory\n2.FilterByPrice\n3.ShowSupply\n4.back");

                try
                {
                    ans = int.Parse(Console.ReadLine());
                    if (ans >= 1 && ans <= 4)
                    {
                        valid = true;
                    }
                    else
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("wrong input!");
                }
            } while (!valid);

            return ans;
        }
        public static int showCartMenu()
        {
            bool valid = false;
            int ans = 0;
            do
            {
                Console.WriteLine("1.AddProductToCart\n2.CalculatePrice\n3.DeleteProduct\n4.EditProfile\n5.Back");

                try
                {
                    ans = int.Parse(Console.ReadLine());
                    if (ans >= 1 && ans <= 5)
                    {
                        valid = true;
                    }
                    else
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("wrong input!");
                }
            } while (!valid);

            return ans;
        }
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>();
            List<Product> allProducts = new List<Product>();
            List<Cart> allCarts = new List<Cart>();
            bool end = false;
            do
            {
                int ans = showMenu();
                switch (ans)
                {
                    case 3:
                        end = true;
                        break;
                    case 1:
                        try { 
                        Console.WriteLine("enter category name: Phone,car,watch,T_shirt,Laptop,Tablet,Charger,Glass,Robot");
                        ProcuctType catName;
                        catName=(ProcuctType)Enum.Parse(typeof(ProcuctType),Console.ReadLine());
                        Category cat = new Category(catName);
                        categories.Add(cat);
                        int catIndex = categories.IndexOf(cat);
                        bool endCat = false;
                        do
                        {
                            int CatAns = showCategoryMenu();
                            switch (CatAns)
                            {
                                case 1:
                                    Console.WriteLine("enter numbew of products :");
                                    int pNum = int.Parse(Console.ReadLine());
                                    string name;
                                    int price;
                                    for (int i = 0; i < pNum; i++)
                                    {
                                        Console.WriteLine("product name:");
                                        name = Console.ReadLine();
                                        Console.WriteLine("product price:");
                                        price = int.Parse(Console.ReadLine());
                                        Product p = new Product(price, name);
                                        categories[catIndex].products.Add(p);
                                    }
                                    break;
                                case 2:
                                    int min, max;
                                    Console.WriteLine("enter minimum price :");
                                    min = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter maximum price :");
                                    max = int.Parse(Console.ReadLine());
                                    Console.WriteLine("category name:{0}    ID:{1}", categories[catIndex].name, categories[catIndex].ID);
                                    categories[catIndex].FilterByPrice(min, max);
                                    Console.WriteLine("enter yes if you want to go back");
                                    if (Console.ReadLine() == "yes")
                                    {
                                        endCat = true;
                                    }
                                    break;
                                case 3:
                                    categories[catIndex].ShowSupply();
                                    Console.WriteLine("enter yes if you want to go back");
                                    if (Console.ReadLine() == "yes")
                                    {
                                        endCat = true;
                                    }
                                    break;
                                case 4:
                                    endCat = true;
                                    break;
                            }
                        } while (!endCat);
                        }
                        catch
                        {
                            Console.WriteLine("wrong input!");
                        }
                        break;
                    case 2:
                        try
                        {

                            string fName = "";
                            string lName = "";
                            string phoneNum = "";
                            People person = new People();
                            Console.WriteLine("costumer first name:");
                            person.firstName = fName;
                            fName = Console.ReadLine();
                            Console.WriteLine("costumer last name:");
                            lName = Console.ReadLine();
                            person.lastName = lName;
                            Console.WriteLine("costumer age:");
                            person.age = int.Parse(Console.ReadLine());
                            bool valid = false;
                            do
                            {
                                Console.WriteLine("costumer phone number:");
                                phoneNum = Console.ReadLine();
                                if (person.checkPhoneNum(phoneNum))
                                {
                                    person.phoneNum = phoneNum;
                                    valid = true;
                                }
                                else
                                {
                                    Console.WriteLine("wrong number");
                                }
                            } while (!valid);
                            allCarts.Add(new Cart(person));
                            int Cindex = allCarts.Count - 1;
                            bool CartEnd = false;
                            do
                            {
                                int CartAns = showCartMenu();
                                switch (CartAns)
                                {
                                    case 5:
                                        CartEnd = true;
                                        break;
                                    case 1:
                                        int pIndex = 0;
                                        Console.WriteLine("how many products:");
                                        int pnum = int.Parse(Console.ReadLine());
                                        List<Product> pToAdd = new List<Product>();
                                        for (int j = 0; j < pnum; j++)
                                        {
                                            Console.WriteLine("product name:");
                                            string pname = Console.ReadLine();
                                            foreach (Product p in allProducts)
                                            {
                                                if (p.Name == pname)
                                                {
                                                    pToAdd.Add(p);
                                                }
                                            }
                                        }
                                        allCarts[Cindex].AddProductToCart(pToAdd);
                                        break;
                                    case 2:
                                        allCarts[Cindex].CalculatePrice();
                                        break;
                                    case 3:
                                        pIndex = 0;
                                        Console.WriteLine("how many products:");
                                        pnum = int.Parse(Console.ReadLine());
                                        List<Product> pToDelete = new List<Product>();
                                        for (int j = 0; j < pnum; j++)
                                        {
                                            Console.WriteLine("product name:");
                                            string prname = Console.ReadLine();

                                            foreach (Product p in allProducts)
                                            {
                                                if (p.Name == prname)
                                                {
                                                    pToDelete.Add(p);
                                                    break;
                                                }
                                            }

                                        }
                                        allCarts[Cindex].DeleteProduct(pToDelete);
                                        break;
                                    case 4:
                                        Console.WriteLine("1.Age\n2.name and family name\n3.phone number");
                                        int editAns = int.Parse(Console.ReadLine());
                                        switch (editAns)
                                        {
                                            case 1:
                                                Console.WriteLine("enter age");
                                                allCarts[Cindex].owner.EditProfile(int.Parse(Console.ReadLine()));

                                                break;
                                            case 2:
                                                Console.WriteLine("enter name");
                                                string name = Console.ReadLine();
                                                Console.WriteLine("enter family name");
                                                allCarts[Cindex].owner.EditProfile(name, Console.ReadLine());
                                                break;
                                            case 3:
                                                Console.WriteLine("enter phone number");
                                                string pn = Console.ReadLine();
                                                if (
                                                allCarts[Cindex].owner.checkPhoneNum(pn))
                                                {
                                                    allCarts[Cindex].owner.EditProfile(pn);
                                                }
                                                break;
                                        }
                                        break;
                                }
                            } while (!CartEnd);
                        }
                        catch
                        {
                            Console.WriteLine("wrong input!");
                        }
                        break;
                }
            } while (!end);
        }
    }
}
