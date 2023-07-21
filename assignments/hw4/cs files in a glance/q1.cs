using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
namespace q1
{
    class Program
    {
        enum Currency
        {
            Dollar = 30, pound = 35, dirham = 7, franc = 29
        }
        enum Airline
        {
            Mahan = 1, Turkish = 2, Kish = 3, Saha = 4, Pars = 5
        }
        struct Location
        {
            public string country;
            public string province;
            public string city;
            public string street;
            public int num;
            public static bool operator ==(Location a, Location t)
            {
                if (a.country != t.country)
                {
                    return false;
                }
                if (a.province != t.province)
                {
                    return false;
                }
                if (a.city != t.city)
                {
                    return false;
                }
                if (a.street != t.street)
                {
                    return false;
                }
                if (a.num != t.num)
                {
                    return false;
                }
                return true;
            }
            public static bool operator !=(Location a, Location t)
            {
                
                return !(a==t);
            }
        }
        struct Time
        {
            public int year;
            public int mounth;
            public int day;
            public int hour;
            public int minute;
            public static bool operator ==(Time a,Time t)
            {
                if (a.year != t.year)
                {
                    return false;
                }
                if (a.mounth != t.mounth)
                {
                    return false;
                }
                if (a.day != t.day)
                {
                    return false;
                }
                if (a.hour != t.hour)
                {
                    return false;
                }
                if (a.minute != t.minute)
                {
                    return false;
                }
                return true;
            }
            public static bool operator !=(Time a, Time t)
            {
                return !(a==t);
            }
        }
        struct PriceInForeignCountry
        {
            public int amount;
            public Currency unit;
            
            public static PriceInToman operator *(PriceInForeignCountry a,Currency t)
            {
                PriceInToman p = new PriceInToman();
                p.price= a.amount * (int)a.unit;
                return p;

            }
            
        }
        struct PriceInToman
        {
            
            public int price;
            public static bool operator ==(PriceInToman a, PriceInToman b)
            {
                if (a.price != b.price)
                {
                    return false;
                }
                return true;
            }
            public static bool operator !=(PriceInToman a, PriceInToman b)
            {
                return !(a == b);
            }
            }
        class Airplane
        {
            public int seatNum;
            public int rowNum;
            public Airline airline;
            public static bool operator ==(Airplane a, Airplane b)
            {
                if (a.seatNum != b.seatNum)
                {
                    return false;
                }
                if (a.rowNum != b.rowNum)
                {
                    return false;
                }
                if (a.airline != b.airline)
                {
                    return false;
                }
                return true;
            }
            public static bool operator !=(Airplane a, Airplane b)
            {
                return !(a == b);
            }
        }
        class flight
        {
            string boardingno;
            public string boardingNo
            {
                get { return boardingno; }
                set { boardingno = value; }
            }
            Location startingP;
            public Location startingPoint {
                get { return startingP; }
                set { startingP = value; }
            }
            Location endingP;
            public Location destination
            {
                get { return endingP; }
                set { endingP = value; }
            }
            Time Start;
            public Time start
            {
                get { return Start; }
                set { Start = value; }
            }
            public PriceInToman Price;
            public PriceInToman price
            {
                get { return Price; }
                set { Price = value; }
            }
            Time endt;
            public Time end
            {
                get { return endt; }
                set { endt = value; }
            }
            private int iD;
            public int ID
            {
                get { return iD; }
                set { iD = value; }
            }
            Airplane a;
            public Airplane airplane
            {
                get { return a; }
                set { a = value; }
            }
            public static bool operator == (flight a, flight t)
            {
                
                if (a.boardingNo != t.boardingNo)
                {
                    return false;
                }
                if (a.airplane != t.airplane)
                {
                    return false;
                }
                if (a.start != t.start)
                {
                    return false;
                }
                if (a.startingPoint != t.startingPoint)
                {
                    return false;
                }
                if (a.destination != t.destination)
                {
                    return false;
                }
                if (a.end != t.end)
                {
                    return false;
                }
                if (a.ID != t.ID)
                {
                    return false;
                }
                return true;
            }
            public static bool operator !=(flight a, flight t)
            {
                return !(a == t);
            }



        }
        public static bool checkBoardingNO(string b)
            {
                Regex r = new Regex(@"^[a-zA-Z]{2}[0-9]{4}[@#$]{1}$");
                MatchCollection numMatches = r.Matches(b);
                if (numMatches.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        public static int showMenu()
        {
            bool valid = false;
            int ans=0;
            do
            {
                Console.WriteLine("1.Add_flight\n2.Sort_flight\n3.Change_data_flight\n4.exit");
                
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
        static void Main(string[] args)
        {
            List<flight> flights = new List<flight>();
            List<int> prices = new List<int>();
            string[] flightFile = File.ReadAllLines(@"D:\uni\ap\codes\hw4\q1\flight.txt");
            string[] airFile = File.ReadAllLines(@"D:\uni\ap\codes\hw4\q1\airplane.txt");
            string[] timeFile = File.ReadAllLines(@"D:\uni\ap\codes\hw4\q1\time.txt");
            string[] placeFile = File.ReadAllLines(@"D:\uni\ap\codes\hw4\q1\place.txt");
            foreach (string line in flightFile)
            {
                string[] parameters = line.Split(" ");
                flight f = new flight();
                f.ID = int.Parse(parameters[0]);
                f.boardingNo = parameters[1];
                f.Price.price = int.Parse(parameters[2]);
                prices.Add(f.Price.price);
                foreach(string air in airFile)
                {
                    string[] airparameters = air.Split(" ");
                    if (int.Parse(airparameters[0]) == f.ID)
                    {
                        f.airplane= new Airplane();
                        f.airplane.airline = (Airline)Enum.Parse(typeof(Airline), airparameters[1]);
                        f.airplane.rowNum = int.Parse(airparameters[2]);
                        f.airplane.seatNum= int.Parse(airparameters[3]);
                        break;
                    }
                }
                bool foundStart = false;
                bool foundEnd = false;
                foreach (string time in timeFile)
                {
                    string[] timeparameters = time.Split(" ");
                    
                    if (int.Parse(timeparameters[0]) == f.ID&&timeparameters[1]== "startTime")
                    {
                        Time s = new Time();
                       // f.start = s;
                        s.year = int.Parse(timeparameters[2]);
                        s.mounth = int.Parse(timeparameters[3]);
                        s.day = int.Parse(timeparameters[4]);
                        s.hour = int.Parse(timeparameters[5]);
                        s.minute = int.Parse(timeparameters[6]);
                        f.start = s;
                        foundStart = true;
                        if (foundEnd)
                        {
                            break;
                        }
                    }
                    if (int.Parse(timeparameters[0]) == f.ID && timeparameters[1] == "endTime")
                    {
                        Time s = new Time();
                        // f.start = s;
                        s.year = int.Parse(timeparameters[2]);
                        s.mounth = int.Parse(timeparameters[3]);
                        s.day = int.Parse(timeparameters[4]);
                        s.hour = int.Parse(timeparameters[5]);
                        s.minute = int.Parse(timeparameters[6]);
                        f.start = s;
                        foundEnd = true;
                        if (foundStart)
                        {
                            break;
                        }
                    }

                }
                foundStart = false;
                foundEnd = false;
                foreach (string p in placeFile)
                {
                    string[] Pparameters = p.Split(" ");
                    if (int.Parse(Pparameters[0]) == f.ID && Pparameters[1] == "startingPoint")
                    {
                        Location s = new Location();
                        s.country = Pparameters[2];
                        s.province = Pparameters[3];
                        s.city = Pparameters[4];
                        s.street = Pparameters[5];
                        s.num = int.Parse(Pparameters[6]);
                        f.startingPoint = s;
                        foundStart = true;
                        if (foundEnd)
                        {

                            break;
                        }
                    }
                    if (int.Parse(Pparameters[0]) == f.ID && Pparameters[1] == "endingPoint")
                    {
                        Location s = new Location();
                        s.country = Pparameters[2];
                        s.province = Pparameters[3];
                        s.city = Pparameters[4];
                        s.street = Pparameters[5];
                        s.num = int.Parse(Pparameters[6]);
                        f.destination = s;
                        foundEnd = true;
                        if (foundEnd)
                        {
                            break;
                        }
                    }
                }
                flights.Add(f);
            }
           
            bool end = false;
            do
            {
                int ans = showMenu();
                int id;
                
                switch (ans)
                {
                    case 4:
                        end = true;
                        break;
                    case 1:
                        bool valid = false;
                        string bN;
                        do
                        {
                            Console.WriteLine("enter flight boarding number");
                            bN = Console.ReadLine();
                            if (checkBoardingNO(bN))
                            {
                                valid = true;
                            }
                            else
                                Console.WriteLine("it is not valid!");
                        } while (!valid);
                        valid = false;
                    do
                    {
                    try
                        {
                            Location l = new Location();
                            Console.WriteLine("location: enter flight starting point country");
                            l.country = Console.ReadLine();
                            Console.WriteLine("location: enter flight starting point province");
                            l.province = Console.ReadLine();
                            Console.WriteLine("location: enter flight starting point city");
                            l.city = Console.ReadLine();
                            Console.WriteLine("location: enter flight starting point street");
                            l.street = Console.ReadLine();
                            Console.WriteLine("location: enter flight starting point number");
                            l.num = int.Parse(Console.ReadLine());

                            Location le = new Location();
                            Console.WriteLine("location: enter flight destination country");
                            le.country = Console.ReadLine();
                            Console.WriteLine("location: enter flight destination province");
                            le.province = Console.ReadLine();
                            Console.WriteLine("location: enter flight destination city");
                            le.city = Console.ReadLine();
                            Console.WriteLine("location: enter flight destination street");
                            le.street = Console.ReadLine();
                            
                            Console.WriteLine("location: enter flight destination number");
                            le.num = int.Parse(Console.ReadLine());

                            Time t = new Time();
                            Console.WriteLine("time: enter flight start time year");
                            t.year = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight start time mounth");
                            t.mounth = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight start time day");
                            t.day = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight start time hour");
                            t.hour = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight start time minute");
                            t.minute = int.Parse(Console.ReadLine());

                            Time te = new Time();
                            Console.WriteLine("time: enter flight end time year");
                            te.year = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight end time mounth");
                            te.mounth = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight end time day");
                            te.day = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight end time hour");
                            te.hour = int.Parse(Console.ReadLine());
                            Console.WriteLine("time: enter flight end time minute");
                            te.minute = int.Parse(Console.ReadLine());

                            PriceInForeignCountry p = new PriceInForeignCountry();
                            Console.WriteLine("enter price currency: Dollar = 30, pound = 35, dirham = 7, franc = 29");
                            p.unit = (Currency)Enum.Parse(typeof(Currency),Console.ReadLine());
                            Console.WriteLine("enter price amount of money");
                            p.amount = int.Parse(Console.ReadLine());
                            Airplane a = new Airplane();

                            Console.WriteLine("enter airline name or number Mahan = 1, Turkish = 2, Kish = 3, Saha = 4, Pars = 5");
                            a.airline = (Airline)Enum.Parse(typeof(Airline), Console.ReadLine());
                            Console.WriteLine("enter number of seat rows");
                            a.rowNum = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter number of seats in each row");
                            a.seatNum = int.Parse(Console.ReadLine());
                            id=0;
                            Console.WriteLine("enter flight ID");
                            id = int.Parse(Console.ReadLine());
                                flight f = new flight();
                                f.ID = id;
                                f.airplane=a ;
                                f.startingPoint=l ;
                                f.destination=le ;
                                f.end=te ;
                                f.start=t ; f.price=p*p.unit ;
                                f.boardingNo=bN ;
                                prices.Add(f.price.price);
                                valid = true;
                                foreach(flight check in flights)
                                {
                                    if (f == check)
                                    {
                                        Console.WriteLine("this flight is already exist");
                                        
                                        valid = false;
                                        break;
                                    }
                                }
                                if (valid)
                                {
                                    try { 
                                    
                                    StreamWriter s = new StreamWriter(@"D:\uni\ap\codes\hw4\q1\place.txt",true);
                                    s.WriteLine("{0} startingPoint {1} {2} {3} {4} {5}", id.ToString(), l.country, l.province, l.city, l.street,l.num);
                                    s.WriteLine("{0} endingPoint {1} {2} {3} {4} {5}", id.ToString(), le.country, le.province, le.city, le.street, le.num);
                                    s.Close();
                                        StreamWriter s1 = new StreamWriter(@"D:\uni\ap\codes\hw4\q1\time.txt", true);
                                    s1.WriteLine("{0} startTime {1} {2} {3} {4} {5}", id.ToString(),t.year,t.mounth,t.day,t.hour,t.minute);
                                    
                                        s1.WriteLine("{0} endTime {1} {2} {3} {4} {5}", id.ToString(), te.year, te.mounth, te.day, te.hour, te.minute);
                                    s1.Close();
                                        StreamWriter s2 = new StreamWriter(@"D:\uni\ap\codes\hw4\q1\airplane.txt", true);
                                    s2.WriteLine("{0} {1} {2} {3}", id.ToString(), f.airplane.airline.ToString(),a.rowNum, a.seatNum);
                                    s2.Close();
                                        StreamWriter s3= new StreamWriter(@"D:\uni\ap\codes\hw4\q1\flight.txt", true);
                                    s3.WriteLine("{0} {1} {2}", id.ToString(), f.boardingNo, f.price.price);
                                    s3.Close();
                                    flights.Add(f);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("I could not open files");
                                    }
                                }
                                valid = true;
                        }
                        catch
                        {
                            Console.WriteLine("wrong input! enter them all again");
                        }
                        } while (!valid);
                        break;
                    case 2:
                        Console.WriteLine("sort by 1: price 2: starting point and destination");
                        try
                        {
                        ans = int.Parse(Console.ReadLine());
                        switch (ans)
                        {
                            case 1:
                                    prices.Sort();
                                    Console.WriteLine("enter price");
                                    int p=0;
                                    valid = false;
                                    do
                                    {
                                        try
                                        {
                                            p = int.Parse(Console.ReadLine());
                                            valid = true;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("wrong input . please enter an integer");
                                        }
                                    } while (!valid);
                                    prices.Sort();
                                    int Index = 0;
                                    flight[] sortedFlights = new flight[prices.Count];
                                    for (int i = 0; i < prices.Count; i++)
                                    {
                                        if (prices[i] < p)
                                        {
                                            Index = i;
                                        }
                                        foreach (flight f in flights)
                                        {
                                            if (prices[i] == f.price.price)
                                            {
                                                sortedFlights[i] = f;
                                                break;
                                            }
                                        }
                                    }
                                    for (int i = Index ; i < prices.Count; i++)
                                    {
                                        
                                        Console.Write("{0} ",sortedFlights[i].ID);
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write("boardingNo. {0} ", sortedFlights[i].boardingNo);
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write(" from {0} ", sortedFlights[i].startingPoint.city);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write(" to {0} ", sortedFlights[i].destination.city.ToString());
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("  price: {0} \n", sortedFlights[i].price.price);
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    
                                    break;

                            case 2:
                                    Console.WriteLine("enter starting point country province city street and number separated by space");
                                    string[] startPointS = Console.ReadLine().Split(" ");
                                    Location startPoint = new Location();
                                    valid = false;
                                    do
                                    {
                                        try
                                        {
                                            startPoint.country = startPointS[0]; startPoint.province = startPointS[1];
                                            startPoint.city = startPointS[2]; startPoint.street = startPointS[3];
                                            startPoint.num = int.Parse(startPointS[4]);
                                            valid = true;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("wrong input please enter in the correct format");
                                        }
                                    } while (!valid);
                                    Console.WriteLine("enter destination country province city street and number separated by space");
                                    Location endPoint = new Location();
                                    string[] endPointS = Console.ReadLine().Split();
                                    valid = false;
                                    do
                                    {
                                        try
                                        {
                                            endPoint.country = endPointS[0]; endPoint.province = endPointS[1];
                                            endPoint.city = endPointS[2]; endPoint.street = endPointS[3];
                                            endPoint.num = int.Parse(endPointS[4]);
                                            valid = true;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("wrong input please enter in the correct format");
                                        }
                                    } while (!valid);
                                    foreach (flight f in flights)
                                    {
                                        if (f.startingPoint == startPoint && f.destination == endPoint)
                                        {
                                            Console.Write("{0} ", f.ID);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.Write("boardingNo. {0} ",f.boardingNo);
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.Write(" from {0} ", f.startingPoint.city);
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write(" to {0} ", f.destination.city);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.Write("  price: {0} \n", f.price.price);
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                    }
                                    break;
                                default:
                                Console.WriteLine("wrong input");
                                break;
                        }
                            
                        }
                        catch
                        {
                            Console.WriteLine("wrong input");
                        }
                        break;
                    case 3:
                        Console.WriteLine("enter flight ID");
                        valid = false;
                        id=0;
                        do
                        {
                            try
                            {
                                id = int.Parse(Console.ReadLine());
                                valid = true;
                            }
                            catch
                            {
                                Console.WriteLine("wrong input format enter a number!");
                            }

                        } while (!valid);
                        int fIndex = 0;
                        
                        
                        valid = false;
                        do
                        {
                            Console.WriteLine("what do you want to change?\n1.boarding number 2.starting point 3.destination" +
                            "\n4.start time 5.end time 6.airplane 7.price\nenter a number");

                            try
                            {
                                ans = int.Parse(Console.ReadLine());
                                if (ans >= 1 && ans <= 7)
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
                        for(int i = 0; i < flights.Count; i++)
                        {
                            if (flights[i].ID == id)
                            {
                                fIndex = i;
                                break;
                            }
                        }
                        switch (ans)
                        {
                            case 1:
                                valid = false;
                          
                                do
                                {
                                    Console.WriteLine("enter flight boarding number");
                                    bN = Console.ReadLine();
                                    if (checkBoardingNO(bN))
                                    {
                                        valid = true;
                                    }
                                    else
                                        Console.WriteLine("it is not valid!");
                                } while (!valid);
                                flights[fIndex].boardingNo = bN;
                                break;
                            case 2:
                                valid = false;
                                do
                                {
                                    try
                                    {
                                        Location l = new Location();
                                        Console.WriteLine("location: enter flight starting point country");
                                        l.country = Console.ReadLine();
                                        Console.WriteLine("location: enter flight starting point province");
                                        l.province = Console.ReadLine();
                                        Console.WriteLine("location: enter flight starting point city");
                                        l.city = Console.ReadLine();
                                        Console.WriteLine("location: enter flight starting point street");
                                        l.street = Console.ReadLine();
                                        Console.WriteLine("location: enter flight starting point number");
                                        l.num = int.Parse(Console.ReadLine());
                                        flights[fIndex].startingPoint = l;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! enter them again");
                                    }
                                } while (!valid);
                                break;
                            case 3:
                                valid = false;
                                do
                                {
                                    try
                                    {
                                        Location le = new Location();
                                        Console.WriteLine("location: enter flight destination country");
                                        le.country = Console.ReadLine();
                                        Console.WriteLine("location: enter flight destination province");
                                        le.province = Console.ReadLine();
                                        Console.WriteLine("location: enter flight destination city");
                                        le.city = Console.ReadLine();
                                        Console.WriteLine("location: enter flight destination street");
                                        le.street = Console.ReadLine();
                                        Console.WriteLine("location: enter flight destination number");
                                        le.num = int.Parse(Console.ReadLine());
                                        flights[fIndex].destination = le;
                                        valid = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! enter them again");
                                    }
                                } while (!valid);
                                break;
                            case 4:
                                do
                                {
                                    try
                                    {
                                        Time t = new Time();
                                        Console.WriteLine("time: enter flight start time year");
                                        t.year = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight start time mounth");
                                        t.mounth = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight start time day");
                                        t.day = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight start time hour");
                                        t.hour = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight start time minute");
                                        t.minute = int.Parse(Console.ReadLine());
                                        flights[fIndex].start = t;
                                        valid = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! enter them again");
                                    }
                                } while (!valid);
                                break;
                            case 5:
                                do
                                {
                                    try
                                    {
                                        Time te = new Time();
                                        Console.WriteLine("time: enter flight end time year");
                                        te.year = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight end time mounth");
                                        te.mounth = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight end time day");
                                        te.day = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight end time hour");
                                        te.hour = int.Parse(Console.ReadLine());
                                        Console.WriteLine("time: enter flight end time minute");
                                        te.minute = int.Parse(Console.ReadLine());
                                        flights[fIndex].end = te;
                                        valid = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! enter them again");
                                    }
                                } while (!valid);
                                break;
                            case 6:
                                do
                                {
                                    try
                                    {
                                        Airplane a = new Airplane();
                                        Console.WriteLine("enter airline name or number Mahan = 1, Turkish = 2, Kish = 3, Saha = 4, Pars = 5");
                                        a.airline = (Airline)Enum.Parse(typeof(Airline), Console.ReadLine());
                                        Console.WriteLine("enter number of seat rows");
                                        a.rowNum = int.Parse(Console.ReadLine());
                                        Console.WriteLine("enter number of seats in each row");
                                        a.seatNum = int.Parse(Console.ReadLine());
                                        flights[fIndex].airplane = a;
                                        valid = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! enter them again");
                                    }
                                } while (!valid);
                                break;
                            case 7:
                                do
                                {
                                    try
                                    {
                                        PriceInForeignCountry p = new PriceInForeignCountry();
                                        Console.WriteLine("enter price currency: Dollar = 30, pound = 35, dirham = 7, franc = 29");
                                        p.unit = (Currency)Enum.Parse(typeof(Currency), Console.ReadLine());
                                        Console.WriteLine("enter price amount of money");
                                        p.amount = int.Parse(Console.ReadLine());
                                        flights[fIndex].price = p*p.unit;
                                        valid = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! enter them again");
                                    }
                                } while (!valid);
                                break;
                        }
                        break;
                }
            } while (!end);

        }
    }
}
