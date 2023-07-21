using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace q1
{
    class Program
    {
        enum solarMonth { farvardin=1,ordibehesht=2,khordad=3,tir=4,mordad=5,shahrivar,mehr,aban,azar,dey,bahman ,esfand };
        enum lunarMonth { jan=1,feb=2,mar,apr,may,jun,jul,aug,sep,oct,nov,dec};
        class Time
        {
            protected int hour;
            protected int minute;
        }
        class Day : Time
        {
            protected int christianDay;
            protected int solarDay;
        }
        class Month : Day
        {
            protected lunarMonth christianMonthName;
            protected solarMonth solarMonthName;
            protected int christianNum;
            protected int solarNum;
        }
        class Calendar : Month
        {
            public int ID;
            public string Name;//because we need it in calendar menu;so I made it public 
            protected int christianYear;
            protected int solarYear;
            static int totalNum=0;
            public List<Event> events = new List<Event>();
            public bool show;

            public Calendar(string s)
            {
                totalNum++;
                ID = totalNum;
                Name = s;
                show = true;
                    
            }
            public void change(int sy, int sm, int sd, int cy ,int cm, int cd, int th, int tm)
            {
                solarYear = sy;
                christianYear = cy;
                christianMonthName = (lunarMonth)Enum.Parse(typeof(lunarMonth),cm.ToString());
                solarMonthName = (solarMonth)Enum.Parse(typeof(solarMonth),sm.ToString());
                solarNum = sm;
                christianNum = cm;
                solarDay = sd;
                christianDay = cd;
                hour = th;
                minute = tm;
            }
            public void CHANGE(int ID,int h, int m)
            {
                int index = 0;
                for(int i = 0; i < events.Count; i++)
                {
                    if (events[i].ID == ID)
                    {
                        index = i;
                        break;
                    }
                }
            }
        }
        enum meeting { conference, VIP, ceremony, tradeShow }
        struct Event
        {
            public Calendar date;
            string Name;
            public int ID;
            meeting type;
            public Event(string n,Calendar d,int i,meeting t)
            {
                date = d;
                ID = i;
                type = t;
                Name = n;
            }
           
        }
        class user
        {
            public string username;
            public string password;
            public bool hasCalendar;
            //public List<Event> events = new List<Event>();
            public List<Calendar> calendars = new List<Calendar>();
            public user(string u,string p)
            {
                username = u;
                password = p;
                hasCalendar = false;
            }
            public void CalMenu(string o)
            {
                bool valid = false;
                int calIndex = 0;
                int ID = int.Parse(o);
                    for(int i = 0; i < calendars.Count; i++)
                    {
                        if (ID == calendars[i].ID)
                        {
                            calIndex = i;
                            valid = true;
                            break;

                        }
                    }
                    if (!valid)
                    {
                        Console.WriteLine("the name does not exist enter again.");
                    return;
                    }
                valid = false;
                do
                {
                    Console.WriteLine("1.Add event\n2.Delete Event [title]\n3.back");
                    int ans = int.Parse(Console.ReadLine());
                    switch(ans){
                        case 1:
                            string n;
                            int id;
                            meeting t;
                            try
                            {
                            Console.WriteLine("enter event name");
                            n = Console.ReadLine();
                            Console.WriteLine("enter event ID");
                            id = int.Parse(Console.ReadLine());
                            Console.WriteLine("choose meeting type: conference, VIP, ceremony or tradeShow?");
                            t = (meeting)Enum.Parse(typeof(meeting), Console.ReadLine());
                            Console.WriteLine("solarDate:(separated by space)");
                            int[] s = Array.ConvertAll(Console.ReadLine().Split(" "), s => int.Parse(s));
                            Console.WriteLine("lunarDate:(separated by space)");
                            int[] l = Array.ConvertAll(Console.ReadLine().Split(" "), s => int.Parse(s));
                            Console.WriteLine("enter time hour:");
                            int h = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter time minute");
                            int m = int.Parse(Console.ReadLine());
                            calendars[calIndex].change(s[2], s[1], s[0], l[2], l[1], l[0], h, m);
                            Event e = new Event(n, calendars[calIndex], id, t);
                            calendars[calIndex].events.Add(e);
                            }
                            catch
                            {
                                Console.WriteLine("wrong input");
                            }
                            break;
                        case 2:
                            Console.WriteLine("enter event ID");
                            id = int.Parse(Console.ReadLine());
                            int index = 0;
                            for(int i = 0; i < calendars[calIndex].events.Count; i++)
                            {
                                if (calendars[calIndex].events[i].ID == id)
                                {
                                    index = i;
                                    calendars[calIndex].events.RemoveAt(i);
                                    break;
                                }
                            }
                            break;
                        case 3:
                            valid = true;
                            break;
                    }
                    
                } while (!valid);
                
            }
        }
        public static bool checkUserText(string t)
        {
            if (!(Regex.IsMatch(t, @"[\w]")))
            {
                return false;
            }
            return true;
        }
        public static bool checkUserPass(string t)
        {
            if (t.Length < 5)
            {
                return false;
            }
            Regex n = new Regex("[0-9]");
            Regex s = new Regex("[a-z]");
            Regex c = new Regex("[A-Z]");
            MatchCollection cMatches = c.Matches(t);
            MatchCollection sMatches = s.Matches(t);
            MatchCollection nMatches = n.Matches(t);
            if (cMatches.Count == 0 || sMatches.Count == 0 || nMatches.Count == 0)
            {
                return false;
            }
            return true;
        }
        public static string[] showLogInMenu()
        {
            bool valid = false;
            string ans = "";
            //string order = "";
            do
            {
                Console.WriteLine("Register [username] [password]\nLogin [username] [password]\nChange Password [username] [old password] [new password]");
                Console.WriteLine("Remove [username] [password]\nShow All Usernames\nExit");

                try
                {
                    ans = Console.ReadLine();
                    string[] ansParts = ans.Split(" ");
                    if (ansParts[0] == "Register")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 3 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Login")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 3 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Change")
                    {
                        if (ansParts.Length != 5)
                        {// password is also separated
                            throw new Exception("input must be in 4 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Remove")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 3 parts");
                        }
                        return ansParts;
                    }
                    if (ans == "Show All Usernames")
                    {
                        return ansParts;
                    }
                    if (ans == "Exit")
                    {
                        return ansParts;
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine("wrong input! please enter again");
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("wrong input! please enter again");
            } while (!valid);
            string[] p = new string[2];
            return p;
        }
        public static string[] showCalendarMenu()
        {
            bool valid = false;
            string ans = "";
            do {
                Console.WriteLine("Create New Calendar [title]\nOpen Calendar [calendarID]\nEnable Calendar [calendarID]");
                Console.WriteLine("Disable Calendar [calendarID]\nDelete Calendar [calendarID]\nEdit Calendar [calendarID] [new title]\nShow [Date]\nevents on [Date]");
                Console.WriteLine("Show Enabled Calendars\nLogout");
                try
                {
                    ans = Console.ReadLine();
                    string[] ansParts = ans.Split(" ");
                    if (ansParts[0] == "Create")
                    {
                        if (ansParts.Length != 4)
                        {
                            throw new Exception("input must be in 2 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Open")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 2 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Enable")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 2 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Disable")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 2 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Delete")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 2 parts");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "Edit")
                    {
                        if (ansParts.Length != 4)
                        {
                            throw new Exception("input must be in 3 parts");
                        }
                        return ansParts;
                    }
                    if (ans == "Show Enabled Calendars")
                    {
                        return ansParts;
                    }
                    if (ansParts[0] == "Show")
                    {
                        //////remained 
                        if (ansParts.Length != 4)
                        {
                            throw new Exception("input must be in 4 parts: Show and date separated by space");
                        }
                        return ansParts;
                    }
                    if (ansParts[0] == "events")
                    {
                        if (ansParts.Length != 3)
                        {
                            throw new Exception("input must be in 2 parts");
                        }
                        return ansParts;
                    }
                    
                    if (ans == "Logout")
                    {
                        return ansParts;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("wrong input! please enter again");
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("wrong input! please enter again");
            } while (!valid);
            string[] p = new string[2];
            return p;
        }

        static void Main(string[] args)
        {
            
            List<user> users = new List<user>();
            bool end = false;
            int userIndex = 0;
            int calIndex = 0;
            do
            {
                string[] ans = showLogInMenu();
                switch (ans[0])
                {
                    case "Exit":
                        end = true;
                        break;
                    case "Register":
                        bool valid = false;
                        do
                        {
                            if (checkUserText(ans[1]) && checkUserText(ans[2]) && checkUserPass(ans[2]))
                            {
                                valid = true;
                                users.Add(new user(ans[1], ans[2]));
                            }
                            else
                            {
                                Console.WriteLine("wrong input please enter again.\nuserName: ");
                                ans[1] = Console.ReadLine();
                                Console.WriteLine("password : ");
                                ans[2] = Console.ReadLine();
                            }
                        } while (!valid);
                        break;
                    case "Login":
                        bool found = false;
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (ans[1] == users[i].username)
                            {
                                userIndex = i;
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            while (ans[2] != users[userIndex].password)
                            {
                                Console.WriteLine("wrong password enter again.");
                                ans[2] = Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("the user was not found");
                        }
                        if (found)
                        {
                            if (!users[userIndex].hasCalendar)
                            {
                                users[userIndex].calendars.Add(new Calendar(users[userIndex].username));
                                users[userIndex].hasCalendar = true;
                                calIndex = 0;
                            }
                            else
                            {
                                for (int i = 0; i < users[userIndex].calendars.Count; i++)
                                {
                                    if (users[userIndex].username == users[userIndex].calendars[i].Name)
                                    {
                                        calIndex = i;
                                        break;
                                    }
                                }
                            }
                        }
                        ///////menu for calendar
                        bool endCal = false;
                        do
                        {
                            string[] calAns = showCalendarMenu();
                            switch (calAns[0])
                            {
                                case "Create":
                                    if (Regex.IsMatch(calAns[3], @"[\w]"))
                                    {
                                        users[userIndex].calendars.Add(new Calendar(calAns[3]));
                                    }
                                    else
                                    {
                                        Console.WriteLine("wrong input format!");
                                    }
                                    break;
                                case "Open":
                                    users[userIndex].CalMenu(calAns[2]);
                                    break;
                                case "Enable":
                                    int check = int.Parse(calAns[2]);
                                    for (int i = 0; i < users[userIndex].calendars.Count; i++)
                                    {
                                        //handle if was not int
                                        if (check == users[userIndex].calendars[i].ID)
                                        {
                                            users[userIndex].calendars[i].show = true ;
                                            break;
                                        }
                                    }
                                    break;
                                case "Disable":
                                    check = int.Parse(calAns[2]);
                                    for (int i = 0; i < users[userIndex].calendars.Count; i++)
                                    {
                                        //handle if was not int
                                        if (check == users[userIndex].calendars[i].ID)
                                        {
                                            users[userIndex].calendars[i].show = false;
                                            break;
                                        }
                                    }
                                    break;
                                case "Delete":
                                    try
                                    {
                                        for (int i = 0; i < users[userIndex].calendars.Count; i++)
                                        {
                                            if (int.Parse(calAns[2]) == users[userIndex].calendars[i].ID)
                                            {
                                                users[userIndex].calendars.RemoveAt(i);
                                                break;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("input must be an integer");
                                    }
                                    
                                    break;
                                case "Edit":
                                    try
                                    {
                                        for (int i = 0; i < users[userIndex].calendars.Count; i++)
                                        {
                                            if (int.Parse(calAns[2]) == users[userIndex].calendars[i].ID)
                                            {
                                                users[userIndex].calendars[i].Name = calAns[3];
                                                break;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("input must be an integer");
                                    }

                                    break;
                                //case "Show":
                                //    /////remained
                                //    break;
                                case "Show":
                                    for (int i = 0; i < users[userIndex].calendars.Count; i++)
                                    {
                                        if (users[userIndex].calendars[i].show)
                                        {
                                            //users[userIndex].calendars[i].
                                            Console.WriteLine("ID: {0} Name: {1}", users[userIndex].calendars[i].ID, users[userIndex].calendars[i].Name);
                                        }
                                    }
                                    break;
                                case "Logout":
                                    endCal = true;
                                    break;
                            }
                        } while (!endCal);


                        break;
                    case "Change":
                        found = false;
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (ans[2] == users[i].username)
                            {
                                userIndex = i;
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            while (ans[3] != users[userIndex].password)
                            {
                                Console.WriteLine("wrong password enter again.");
                                ans[3] = Console.ReadLine();
                            }
                            users[userIndex].password = ans[4];
                        }
                        else
                        {
                            Console.WriteLine("the user was not found");
                        }
                        break;
                    case "Remove":
                        found = false;
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (ans[1] == users[i].username)
                            {
                                userIndex = i;
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            while (ans[2] != users[userIndex].password)
                            {
                                Console.WriteLine("wrong password enter again.");
                                ans[2] = Console.ReadLine();
                            }
                            users.RemoveAt(userIndex);
                        }
                        else
                        {
                            Console.WriteLine("the user was not found");
                        }
                        break;
                    case "Show":
                        List<string> userNames = new List<string>();
                        foreach (user u in users)
                        {
                            userNames.Add(u.username);
                        }
                        userNames.Sort();
                        foreach (string u in userNames)
                        {
                            Console.WriteLine(u);
                        }
                        if (userNames.Count == 0)
                        {
                            Console.WriteLine("Nothing.");
                        }
                        break;
                }
            } while (!end);
        }
    }
}
