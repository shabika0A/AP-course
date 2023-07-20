using System;
using System.IO;
using System.Collections.Generic;
namespace q3
{
    class Program
    {
        public class Bank
        {
            public string name;
            public Bank(string n)
            {
                name = n;
            }
        }
        enum costomerType { good,bad}
        enum accountType { shortTerm,longTerm,vip}
        class costomer
        {
            string name;
            public float lockBox;
            public int minus;
            public costomerType type = costomerType.good;
            public List<account> accounts = new List<account>();
            public List<loan> loans = new List<loan>();
            public costomer(string N,int M)
            {
                name = N;
                lockBox = M;
                minus = 0;
            }
            public void addaccount(int money, int time, string b, accountType t,int date)
            {
                lockBox -= money;
                accounts.Add(new account(accounts.Count+1, money, time, b, t,date));
            }
            public void Withdraw(int acNum,int date)
            {
                if(date- accounts[acNum - 1].creationDate>=accounts[acNum - 1].term)
                {
                   // Console.WriteLine("aditional money:{0}",accounts[acNum - 1].deposit + ((float)accounts[acNum - 1].interestPercentage / 100));

                    lockBox += accounts[acNum - 1].deposit * (1 +((float) accounts[acNum - 1].interestPercentage / 100));
                }
                else
                {
                    lockBox += accounts[acNum - 1].deposit;
                }
                
                accounts.RemoveAt(acNum - 1);
            }
            public void getloan(int amount,int profit,int parts,string bName,int d)
            {
                if (minus >= 5)
                {
                    Console.WriteLine("you can not take loan anymore!");
                }
                else
                {
                    loans.Add(new loan(amount, bName,d, profit, parts));
                    lockBox += amount;
                }
            }
        }
        class account
        {


            public int ID;
            public float deposit;
            public int interestPercentage;
            public int term;
            accountType type;

            public Bank bank;
            public int creationDate;
            
            public account(int id,int money,int time,string b,accountType t,int d)
            {
                ID = id;
                deposit = money;
                term = time;
                bank=new Bank(b);
                type = t;
                creationDate = d;
                switch (type)
                {
                    case (accountType.shortTerm):
                        interestPercentage = 10;
                        break;
                    case (accountType.longTerm):
                        interestPercentage = 30;
                        break;
                    case (accountType.vip):
                        interestPercentage = 50;
                        break;
                }
            }
        }
        class loan
        {
            int amount;
            int interest;
            int partCount;
            public float part;
            public int startDate;
            string bank;
            public int remainPartNum;
            public loan(int m,string b,int date,int i=5,int pc=12)
            {
                amount = m;interest = i;partCount = pc;bank = b;startDate = date;
                part = m + ((float)m * interest / 100)/partCount;
                remainPartNum = partCount;
            }
        }
        public static int showMenu()
        {
            int ans=0;
            
            Console.WriteLine("1.add customer");
            Console.WriteLine("2.add bank");
            Console.WriteLine("3.add account");
            Console.WriteLine("4.get money");
            Console.WriteLine("5.pay loan");
            Console.WriteLine("6.update\n7.show info\n8.exit\nplease choose a number ");
            bool valid = false;
            do
            {
                try
                {
                    ans=int.Parse(Console.ReadLine());
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("wrong input please a number");
                }
            } while (!valid);
            return ans;
        }
        static void Main()
        {
            //Console.WriteLine("Hello World!");
            List<costomer> costomers = new List<costomer>();
            List<string> costomerNames = new List<string>();
            List<Bank> banks = new List<Bank>();
            List<string> bankNames = new List<string>();
            bool exit = false;
            int ans = 0;
            int date = 0;
            int money = 0;
            string name=" ";
            string AcBank;
            bool valid;
            while(!exit){
            ans = showMenu();
            switch (ans)
            {
                case 8:
                    exit = true;
                    break;
                case 1:
                    valid = false;
                    do
                    {
                        Console.WriteLine("enter name of costomer :");
                        name = Console.ReadLine();
                        if (costomerNames.Contains(name))
                        {
                            Console.WriteLine("the name is already exist!");
                        }
                        else
                        {
                            valid = true;
                            costomerNames.Add(name);
                        }
                    } while (!valid);
                    Console.WriteLine("enter amount of money :");
                    money = int.Parse(Console.ReadLine());
                    costomers.Add(new costomer(name, money));
                    break;
                case 2:
                    valid = false;
                    do
                    {
                        Console.WriteLine("enter name of bank :");
                        name = Console.ReadLine();
                        if (costomerNames.Contains(name))
                        {
                            Console.WriteLine("the name is already exist!");
                        }
                        else
                        {
                            valid = true;
                            bankNames.Add(name);
                        }
                    } while (!valid);

                    banks.Add(new Bank(name));
                    break;
                case 3:
                    //int money, int time, string b, accountType t,int date
                    int Acterm=0;
                    string t;
                    valid = false;
                    do//costomer name
                    {
                        Console.WriteLine("enter name of costomer :");
                        name = Console.ReadLine();
                        if (!costomerNames.Contains(name))
                        {
                            Console.WriteLine("the name does not exist!");
                        }
                        else
                        {
                            valid = true;
                        }
                    } while (!valid);
                    valid = false;
                    int costomerIndex = costomerNames.IndexOf(name);
                    do
                    {
                        try
                        {
                            Console.WriteLine("enter amount of money :");
                            money = int.Parse(Console.ReadLine());
                            if (costomers[costomerIndex].lockBox >= money)
                            {
                                valid = true;
                            }
                            else
                            {
                                Console.WriteLine("it is more than costomer's money !");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("wrong input. please enter an integer");
                        }
                            
                    } while (!valid);
                    valid = false;
                    do
                    {
                        try { 
                        Console.WriteLine("enter account term :");
                        Acterm = int.Parse(Console.ReadLine());
                            valid = true;
                        }
                        catch
                        {
                            Console.WriteLine("wrong input. please enter an integer");
                        }
                    } while (!valid);
                    valid = false;
                    do
                    {
                        Console.WriteLine("enter name of bank :");
                        name = Console.ReadLine();
                            valid = true;
                        if (!bankNames.Contains(name))
                        {
                            Console.WriteLine("the name does not exist!");
                                valid = false;
                        }
                    } while (!valid);
                    valid = false;
                    string Act;
                    accountType Actype=accountType.shortTerm;
                    do
                    {    
                        Console.WriteLine("what is type (shortTerm or longTerm or vip) ?");
                        Act = Console.ReadLine();
                            valid = true;
                        if (Act=="shortTerm")
                        {
                            Actype = accountType.shortTerm;
                        }
                        else if (Act == "longTerm")
                        {
                            Actype = accountType.longTerm;
                        }else if (Act == "vip")
                        {
                            Actype = accountType.vip;
                        }else
                        {
                            Console.WriteLine("wrong input!");
                            valid = false;
                        }
                    } while (!valid);
                    costomers[costomerIndex].addaccount(money, Acterm, name, Actype, date);
                        break;
                case 4:
                        valid = false;
                        do//costomer name
                        {
                            Console.WriteLine("enter name of costomer :");
                            name = Console.ReadLine();
                            if (!costomerNames.Contains(name))
                            {
                                Console.WriteLine("the name does not exist!");
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (!valid);
                        do
                        {
                            try
                            {
                                Console.WriteLine("enter account ID :");
                                int id = int.Parse(Console.ReadLine());
                                if (costomers[costomerNames.IndexOf(name)].accounts.Count<= id)
                                {
                                    valid = true;
                                    costomers[costomerNames.IndexOf(name)].Withdraw(id, date);
                                }
                                else
                                {
                                    Console.WriteLine("ID is not valid !");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("wrong input. please enter an integer");
                            }

                        } while (!valid);
                        break;
                case 5:
                        valid = false;
                        do//costomer name
                        {
                            Console.WriteLine("enter name of costomer :");
                            name = Console.ReadLine();
                            if (!costomerNames.Contains(name))
                            {
                                Console.WriteLine("the name does not exist!");
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (!valid);
                        valid = false;
                        costomerIndex = costomerNames.IndexOf(name);
                        do
                        {
                            try
                            {
                                Console.WriteLine("enter amount of loan :");
                                money = int.Parse(Console.ReadLine());
                                valid = true;
                            }
                            catch
                            {
                                Console.WriteLine("wrong input. please enter an integer");
                            }

                        } while (!valid);
                        valid = false;
                        int payNum = 0;
                        do
                        {
                            try
                            {
                                Console.WriteLine("enter number of payments (6 or12) :");
                                payNum = int.Parse(Console.ReadLine());
                                if (payNum != 6 && payNum != 12)
                                {
                                    throw new Exception();
                                }
                                valid = true;
                            }
                            catch
                            {
                                Console.WriteLine("wrong input. ");
                            }
                        } while (!valid);
                        int benefit = 0;
                        do
                        {
                            try
                            {
                                Console.WriteLine("enter benefit percentage :");
                                benefit = int.Parse(Console.ReadLine());
                                valid = true;
                            }
                            catch
                            {
                                Console.WriteLine("wrong input. please enter an integer");
                            }
                        } while (!valid);
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter name of bank :");
                            name = Console.ReadLine();
                            valid = true;
                            if (!bankNames.Contains(name))
                            {
                                Console.WriteLine("the name does not exist!");
                                valid = false;
                            }
                            
                        } while (!valid);
                        
                        costomers[costomerIndex].getloan(money, benefit, payNum, name, date);
break;
                    case 6:
                        valid = false;
                        int days = 0;
                        do
                        {
                            try
                            {
                                Console.WriteLine("enter number of days:");
                                days = int.Parse(Console.ReadLine());
                                valid = true;
                            }
                            catch
                            {
                                Console.WriteLine("wrong input. please enter an integer");
                            }

                        } while (!valid);
                        for(int i = 1; i <= days; i++)
                        {
                            date++;
                            for(int j = 0; j < costomers.Count; j++)
                            {
                                for (int k = 0; k < costomers[j].accounts.Count; k++)
                                {
                                    if (costomers[j].accounts[k].term <= date - costomers[j].accounts[k].creationDate)
                                    {
                                        costomers[j].Withdraw(k + 1, date);
                                    }
                                }
                                for (int k = 0; k < costomers[j].loans.Count; k++)
                                {
                                    
                                    if(costomers[j].lockBox>= costomers[j].loans[k].part)
                                    {
                                        costomers[j].lockBox -= costomers[j].loans[k].part;
                                        costomers[j].loans[k].remainPartNum--;
                                        if (costomers[j].loans[k].remainPartNum==0)
                                        {
                                            costomers[j].loans.RemoveAt(k);
                                        }
                                    }
                                    else
                                    {
                                        costomers[j].minus++;
                                        if(costomers[j].type==costomerType.good&& costomers[j].minus >= 5)
                                        {
                                            costomers[j].type = costomerType.bad;
                                        }
                                    }
                                }
                                
                                }
                        }
                        break;
                    case 7:
                        valid = false;
                        do//costomer name
                        {
                            Console.WriteLine("enter name of costomer :");
                            name = Console.ReadLine();
                            if (!costomerNames.Contains(name))
                            {
                                Console.WriteLine("the name does not exist!");
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (!valid);
                        costomerIndex = costomerNames.IndexOf(name);
                        Console.WriteLine("money in safebox: {0}\tcostomer type: {1}\tnumber of minus points: {2}\naccounts:", costomers[costomerIndex].lockBox, costomers[costomerIndex].type, costomers[costomerIndex].minus);
                        foreach(account a in costomers[costomerIndex].accounts)
                        {
                            Console.WriteLine("ID:{0} Bank:{1}", a.ID, a.bank.name);
                        }
                        break;
                }
            }
        }
    }
}
