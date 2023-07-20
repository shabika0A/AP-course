using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace q5
{
    class Program
    {
        class restaurant
        {
            public double wallet;
            public List<(int, int)> discount;
        }
        class customer
        {
            public string name;
            public int ID;
            public double money;
            public int discountUsage;
            public (int, int) disCode;
            public static List<customer> customers;
            public customer(string n,int id)
            {
                name = n;
                ID = id;
                money = 0;
                discountUsage = 0;
            }
        }
        class food
        {
            public string name;
            public double price;
            public List<(string, int)> ingredients=new List<(string, int)>();
            public int amount;
            public static List<food> foods=new List<food> ();
            public food(string n, double p)
            {
                name = n;
                price = p;
                amount = 0;
            }
        }
        class warehouse
        {
            public string materialName;
            public int amount;
            public static List<warehouse> allMaterials=new List<warehouse>();
            public warehouse(string n,int a)
            {
                materialName = n;
                amount = a;
            }
        }
        class transaction
        {
            public int ID;
            public int costumerID;
            public double money;
            public double discount;
            public List<transaction> allTransactions;

        }
        public static int showMenu()
        {
            Console.WriteLine("1.add customer\n2.increase balance of customer\n3.Add warehouse material\n4.increase warehouse material\n5.add food\n6.increase food");
            Console.WriteLine("7.Add discount code\n8.Add discount code to customer\n9.sell food\n10.accept transaction\n11.print transaction list\n12.print income\n13.exit");
            bool valid = false;
            int ans = 0;
            do
            {
                try
                {
                    Console.WriteLine("choose a number");
                    ans = int.Parse(Console.ReadLine());
                    if (ans > 0 && ans < 14)
                    {
                        valid = true;
                    }
                }
                catch
                {
                    Console.WriteLine("input was not a valid integer");
                }
            } while (!valid);
            return ans;
        }
        static void Main(string[] args)
        {
            bool end = false;
            bool valid;
            int ans;
            int id;
            string name;
            string[] lines;
            int custIndex = 0;
            //add customers of file
            do
            {
                ans = showMenu();
                switch (ans)
                {
                case 13:
                        end = true;
                        break;
                case 1:
                    valid = false;
                        
                    Console.WriteLine("enter name:");
                    name = Console.ReadLine();
                    bool valid2 = false;
                    id = 0;
                    do
                    {
                        Console.WriteLine("enter customer ID:");
                        try
                        {
                            id = int.Parse(Console.ReadLine());
                            valid2 = true;
                            foreach (customer c in customer.customers)
                            {
                                if (c.ID == id)
                                {
                                    Console.WriteLine("this ID is already exist!");
                                    valid2 = false;
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("enter an integer!");
                        }
                    } while (!valid2);
                    customer.customers.Add(new customer(name, id));
                        StreamWriter w = new StreamWriter(@"D:\uni\ap\codes\hw3\q5\User.txt", true);
                        w.WriteLine(name + "," + id.ToString() + ",0");//dis number is 0 at first
                        w.Close();
                        
                    break;
                    case 2:
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter customer ID:");
                            try
                            {
                                id = int.Parse(Console.ReadLine());

                                for(int i=0;i<customer.customers.Count;i++)
                                {
                                    if (customer.customers[i].ID == id)
                                    {
                                        custIndex = i;
                                        valid = true;
                                        break;
                                    }
                                }
                                if (!valid)
                                    Console.WriteLine("this ID was not found");
                            }
                            catch
                            {
                                Console.WriteLine("enter an integer!");
                            }
                        } while (!valid);
                        valid = false;
                        double money;
                        do
                        {
                            Console.WriteLine("enter amount of money:");
                            try
                            {
                                money = double.Parse(Console.ReadLine());
                                valid = true;
                                customer.customers[custIndex].money += money;
                            }
                            catch
                            {
                                Console.WriteLine("enter a number!");
                            }
                        } while (!valid);
                        break;
                    case 3:
                        /////sell food
                        break;
                    case 4:
                        valid = false;
                        int amount = 0;
                        Console.WriteLine("material:");
                        string material = Console.ReadLine();
                        do
                        {
                            Console.WriteLine("enter amount of material:");
                            try
                            {
                                amount = int.Parse(Console.ReadLine());
                                valid = true;
                               
                            }
                            catch
                            {
                                Console.WriteLine("enter a number!");
                            }
                        } while (!valid);
                        for(int i = 0; i < warehouse.allMaterials.Count; i++)
                        {
                            if (warehouse.allMaterials[i].materialName == material)
                            {
                                Console.WriteLine("This material is already exist!");
                                valid = false;
                                break;
                            }
                        }
                        if (valid)
                        {
                            warehouse.allMaterials.Add(new warehouse(material, amount));
                        }
                        break;
                    case 5:
                        valid = false;
                        amount = 0;
                        Console.WriteLine("material:");
                        material = Console.ReadLine();
                        do
                        {
                            Console.WriteLine("enter amount of material:");
                            try
                            {
                                amount = int.Parse(Console.ReadLine());
                                valid = true;

                            }
                            catch
                            {
                                Console.WriteLine("enter a number!");
                            }
                        } while (!valid);
                        int mIndex=0;
                        for (int i = 0; i < warehouse.allMaterials.Count; i++)
                        {
                            if (warehouse.allMaterials[i].materialName == material)
                            {
                                mIndex = i;
                                valid = false;
                                break;
                            }
                        }
                        if (valid)
                        {
                            Console.WriteLine("material was not found!");

                        }
                        else
                        {
                            warehouse.allMaterials[mIndex].amount += amount;

                        }
                        break;
                    case 6:
                        valid = false;
                        string foodname = "";
                        do
                        {
                            Console.WriteLine("enter food name:");
                            foodname = Console.ReadLine();
                            bool found = false;
                            foreach(food f in food.foods)
                            {
                                if (f.name == foodname)
                                {
                                    found = true;
                                    Console.WriteLine("the food already exist");
                                    break;
                                }
                            }
                          
                        } while (!valid);
                        int price = 0;
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter food price:");

                            try
                            {
                                price = int.Parse(Console.ReadLine());

                            }
                            catch
                            {
                                Console.WriteLine("enter an integer!");
                            }
                        } while (!valid);
                        ///error for store
                        ///food amount=0 in resturant
                        food.foods.Add(new food(foodname, price));
                        do
                        {
                            
                            
                            Console.WriteLine("enter food ingredient: (if there is no more press enter)");
                            string ingname = Console.ReadLine();
                            if (ingname == "")
                            {
                                valid = true;
                            }
                            else
                            {
                                valid2 = false;
                                do
                                {
                                     Console.WriteLine("enter food ingredient amount");

                                    try
                                    {
                                        int ingAmount = int.Parse(Console.ReadLine());
                                        food.foods[food.foods.Count - 1].ingredients.Add((ingname, ingAmount));
                                        valid2 = true;

                                    }
                                    catch
                                    {
                                        Console.WriteLine("enter an integer!");
                                    }
                                } while (!valid2);
                            }
                        } while (!valid);
                        break;
                    case 7:
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter food price:");

                            try
                            {
                                price = int.Parse(Console.ReadLine());

                            }
                            catch
                            {
                                Console.WriteLine("enter an integer!");
                            }
                        } while (!valid);

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:

                        break;
                    case 11:

                        break;
                }

            } while (!end);
        }
    }
}
