using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
namespace q4
{
    public class Program
    {
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "abc123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public enum newsType{economy=12 ,social=23,accident=34,tech=45,sport=56,weather=67}
        public class news
        {
            static int totalNumber = 0;
            public int ID;
            public string sender;
            public string receiver;
            public newsType type;
            public string text;
            public DateTime newsTime ;
            public news(string s,string r,newsType t,string text)
            {
                totalNumber++;
                ID = totalNumber;
                sender = s;
                receiver = r;
                type = t;
                this.text = text;
                newsTime=DateTime.Now;
            }
        }
        public class user
        {
            public string username;
            public string pass;
            public List<string> contacts=new List<string>();
            public List<news> sentNews=new List<news>();
            public List<news> takenNews = new List<news>();
            public user(string usern,string pas)
            {
                username = usern;
                pass = pas;
                
            }
            public void newUser(string usern,string pass)
            {
                try
                {
                    StreamWriter w = new StreamWriter(@"D:\uni\ap\codes\hw3\q4\q4\User.txt", true);
                    w.WriteLine(usern);
                    w.WriteLine(Encrypt(pass));
                    w.Close();
                    StreamWriter wc = new StreamWriter(@"D:\uni\ap\codes\hw3\q4\q4\Contact.txt", true);
                    wc.WriteLine("{0} : ", usern);
                    wc.Close();
                    StreamWriter wn = new StreamWriter(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt", true);
                    wn.WriteLine("{0} : ", usern);
                    wn.Close();
                }
                catch
                {
                    Console.WriteLine("file opening was failed!!");
                }
            }
        }
        public static bool validPass(string p)
        {
            Regex capital = new Regex("[A-Z]");
            MatchCollection capitalMatches = capital.Matches(p);
            Regex small = new Regex("[a-z]");
            MatchCollection smallMatches = small.Matches(p);
            Regex num = new Regex("\\d");
            MatchCollection numMatches = num.Matches(p);
            if (numMatches.Count > 0&&smallMatches.Count>0&&capitalMatches.Count>0&&p.Length>=10)
            {
                return true;
            }
            return false;
        }
        public static int showMenu()
        {
            Console.WriteLine("1.log in\n2.sign up\n3.Add contact\n4.remove contact\n5.send NEWS\n6.edit NEWS");
            Console.WriteLine("7.show NEWS\n8.sort NEWS\n9.search NEWS\n10.delete NEWS\n11.change Password\n12.exit");

            bool valid = false;
            int ans = 0;
            do
            {
                try
                {
                    Console.WriteLine("choose a number");
                     ans =int.Parse( Console.ReadLine());
                    if (ans > 0 && ans < 13)
                    {
                        valid = true;
                    }
                }catch
                {
                    Console.WriteLine("input was not a valid integer");
                }
            } while (!valid);
            return ans;
        }
        public static void signInError()
        {
            Console.WriteLine("sign in required!");
        }
        public static int findUserIndexByName(List<user> u,string name)
        {
            for(int i = 0; i < u.Count; i++)
            {
                if (name == u[i].username)
                {
                    return i;
                }
            }
            return -1;
        }
        static void mergeTime(news[] arr, long l, long m, long r)
        {
            long n1 = m - l + 1;
            long n2 = r - m;
            news[] L = new news[n1];
            news[] R = new news[n2];
            long i, j;
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];
            i = 0;
            j = 0;
            long k = l;
            while (i < n1 && j < n2)
            {
                if (L[i].newsTime <= R[j].newsTime)
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
        static void sortTime(news[] arr, long l, long r)
        {
            if (l < r)
            {

                long m = l + (r - l) / 2;
                sortTime(arr, l, m);
                sortTime(arr, m + 1, r);
                mergeTime(arr, l, m, r);
            }
        }
        static void mergeID(news[] arr, long l, long m, long r)
        {
            long n1 = m - l + 1;
            long n2 = r - m;
            news[] L = new news[n1];
            news[] R = new news[n2];
            long i, j;
            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];
            i = 0;
            j = 0;
            long k = l;
            while (i < n1 && j < n2)
            {
                if (L[i].ID <= R[j].ID)
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
        static void sortID(news[] arr, long l, long r)
        {
            if (l < r)
            {

                long m = l + (r - l) / 2;
                sortID(arr, l, m);
                sortID(arr, m + 1, r);
                mergeID(arr, l, m, r);
            }
        }
        static void Main(string[] args)
        {
            string[] lines;
            List<user> users = new List<user>();
            List<news> allNews = new List<news>();
            List<news> firstNews = new List<news>();
            bool end = false;
            int ans;
            string username;
            string pass;
            bool valid;
            int userindex = 0;
            bool userSignedIn = false;
            try {
                lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\User.txt");

                for (int i = 0; i < lines.Length; i += 2)
                {
                    string u = lines[i];
                    string p = Decrypt(lines[i + 1]);
                    users.Add(new user(u, p));
                }
                lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\Contact.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] cs = lines[i].Substring(users[i].username.Length + 3).Split(",");
                    
                    foreach (string s in cs)
                    {
                        users[i].contacts.Add(s);
                    }
                    users[i].contacts.Remove("");
                }
                lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] ns = lines[i].Split("(");
                    for (int j = 0; j < ns.Length; j++)
                    {
                        string[] data = ns[j].Split(", ");
                        if (data.Length > 1) { 
                        string dataTime = data[4].Substring(0, data[4].Length - 2);
                            int me = 0;
                            DateTime t = DateTime.ParseExact(dataTime, "d/M/yyyy h:mm:s tt", null);
                            
                            newsType type = 0;
                        switch (data[2])
                        {
                            case "social":
                                type = newsType.social;
                                break;
                            case "accident":
                                type = newsType.accident;
                                break;
                            case "economy":
                                type = newsType.economy;
                                break;
                            case "sport":
                                type = newsType.sport;
                                break;
                            case "tech":
                                type = newsType.tech;
                                break;
                            case "weather":
                                type = newsType.weather;
                                break;
                        }

                        news n = new news(users[i].username, data[0], type, data[3]);
                        n.ID = int.Parse(data[1]);
                        n.newsTime = t;
                        firstNews.Add(n);
                        users[i].sentNews.Add(n);
                        users[findUserIndexByName(users, data[0])].takenNews.Add(n);
                        }
                    }
                }
                news[] nsort = firstNews.ToArray();
                sortID(nsort, 0, allNews.Count - 1);
                foreach (news n in nsort)
                {
                    allNews.Add(n);
                }
            }
            catch
            {
                Console.WriteLine("one or more files were not found! please exit and try again");
            }
            
                do
            {
                ans = showMenu();
                switch (ans) { 
                    case 12:
                        if (!userSignedIn)
                        {
                        end = true;
                        }
                        else
                        {
                            userSignedIn = false;
                        }
                    
                break;
                    case 2:
                        if (userSignedIn)
                        {
                            Console.WriteLine("a user has already signed in .you can not sign up");
                            break;
                        }
                        valid = false;
                        do
                        {
                        Console.WriteLine("enter username:");
                        username = Console.ReadLine();
                            valid = true;
                            foreach (user u in users)
                            {
                                if (username == u.username)
                                {
                                    Console.WriteLine("this username already exist!");
                                    
                                    valid = false;
                                    break;
                                }
                            }
                        } while (!valid);
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter password:");
                            pass = Console.ReadLine();
                            if (validPass(pass))
                            {
                                valid = true;
                            }
                            else
                            {
                                Console.WriteLine("password is not valid please enter again.");
                            }
                        } while (!valid);
                        users.Add(new user(username, pass));
                        users[users.Count - 1].newUser(username, pass);
                        break;
                    case 1:
                        if (userSignedIn)
                        {
                            Console.WriteLine("a user has already signed in .you can not sign in");
                            break;
                        }
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter username:");
                            username = Console.ReadLine();
                            foreach (user u in users)
                            {
                                if (username == u.username)
                                {
                                    userindex = users.IndexOf(u);
                                    valid = true;
                                    break;
                                }
                            }
                            if (!valid)
                            {
                                Console.WriteLine("wrong username!");
                            }
                        } while (!valid);
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter password:");
                            pass = Console.ReadLine();
                            if (pass != users[userindex].pass)
                            {
                                Console.WriteLine("wrong password!");
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (!valid);
                        userSignedIn = true;
                        break;
                    case 3://add contact
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter contact username:");
                            username = Console.ReadLine();
                            foreach (user u in users)
                            {
                                if (username == u.username)
                                {
                                    valid = true;
                                    break;
                                }
                            }
                            if (!valid)
                            {
                                Console.WriteLine("wrong username!");
                            }
                        } while (!valid);
                        if (!users[userindex].contacts.Contains(username))
                        {
                            users[userindex].contacts.Add(username);
                            try
                            {
                                lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\Contact.txt");
                                lines[userindex] += username;
                                lines[userindex] += ",";
                                File.WriteAllLines(@"D:\uni\ap\codes\hw3\q4\q4\Contact.txt", lines);
                            }
                            catch
                            {
                                Console.WriteLine("the related file could not be opened");
                            }

                        }
                        else
                        {
                            Console.WriteLine("this is already a contact");
                        }
                        break;
                    case 4://delete contact news in file
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter contact username:");
                            username = Console.ReadLine();
                            
                                if (users[userindex].contacts.Contains(username))
                                {
                                try { 
                                valid = true;
                                users[userindex].contacts.Remove(username);
                                lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\Contact.txt");
                                lines[userindex].Remove(lines[userindex].IndexOf(username),username.Length+1);
                                File.WriteAllLines(@"D:\uni\ap\codes\hw3\q4\q4\Contact.txt", lines);
                                    int recIndex = findUserIndexByName(users, username);
                                    //taken news
                                    for(int j = 0; j < users[recIndex].takenNews.Count; j++)
                                    {
                                        if (users[recIndex].takenNews[j].receiver== users[userindex].username)
                                        {
                                            users[recIndex].takenNews.RemoveAt(j);
                                            j--;
                                        }
                                    }
                                    //sent news
                                    lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt");
                                    
                                    
                                    for (int j = 0; j < users[userindex].sentNews.Count; j++)
                                    {
                                        if (users[userindex].sentNews[j].receiver ==username)
                                        {
                                            string l = lines[userindex].Substring(0, lines[userindex].IndexOf(users[userindex].sentNews[j].ID.ToString() + ", ") - 3 - username.Length);
                                            string r = lines[userindex].Substring(lines[userindex].IndexOf(users[userindex].sentNews[j + 1].ID.ToString() + ", ") - 3 - users[userindex].sentNews[j + 1].receiver.Length);
                                            lines[userindex] = l + r;
                                            users[userindex].sentNews.RemoveAt(j);
                                            j--;
                                        }
                                    }
File.WriteAllLines(@"D:\uni\ap\codes\hw3\q4\q4\Contact.txt", lines);
                                    for(int j = 0; j < allNews.Count; j++)
                                    {
                                        if(allNews[j].receiver==username&&allNews[j].sender== users[userindex].username)
                                        {
                                            allNews.RemoveAt(j);
                                            j--;
                                        }
                                    }

                                }
                                catch
                                {
                                    Console.WriteLine("Could not open the related file! try again.");
                                }
                            }
                            
                            if (!valid)
                            {
                                Console.WriteLine("wrong username!");
                            }
                        } while (!valid);

                        break;
                    case 5://send news
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter receiver's username:");
                            username = Console.ReadLine();
                            if (users[userindex].contacts.Contains(username))
                            {
                                valid = true;
                            }

                            if (!valid)
                            {
                                Console.WriteLine("wrong username!");
                            }
                        } while (!valid);
                        valid = false;
                        newsType ty=newsType.accident;
                        do
                        {
                            Console.WriteLine("what is news type? economy=12,social=23,accident=34,tech=45,sport=56,weather=67");
                            string t = Console.ReadLine();
                            
                            try
                            {
                                ty=(newsType)int.Parse(t);
                                valid = true;
                            }
                            catch
                            {
                                Console.WriteLine("wrong input! choose one of the types and enter an integer");
                            }
                            
                        } while (!valid);
                        Console.WriteLine("enter the news text in one line");
                        string text=Console.ReadLine();
                        news N = new news(users[userindex].username, username, ty, text);
                        allNews.Add(N);
                        users[userindex].sentNews.Add(N);
                        try { 
                        string[] lines2 = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt");
                        lines2[userindex] +=(N.receiver,N.ID,N.type,N.text,N.newsTime);
                        lines2[userindex] += ",";
                        File.WriteAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt", lines2);
                        int recieverIndex = findUserIndexByName(users,username);
                        users[recieverIndex].takenNews.Add(N);
                        }
                        catch
                        {
                            Console.WriteLine("Contact.txt could not be opened");
                        }
                        break;
                    case 6:
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        int newsIndex = 0;
                        int id = 0;
                        int i;
                        do
                        {
                            Console.WriteLine("enter news ID:");
                            try
                            {
                            id = int.Parse(Console.ReadLine());

                            }
                            catch
                            {
                            Console.WriteLine("enter an integer!");
                            }
                            
                           for(i = 0; i < users[userindex].sentNews.Count; i++)
                            {
                                if (users[userindex].sentNews[i].ID == id)
                                {
                                    newsIndex = i;
                                    
                                    valid = true;
                                break;
                                }
                            }

                            if (!valid)
                            {
                                Console.WriteLine("wrong ID!");
                            }
                        } while (!valid);
                        Console.WriteLine("enter new text:");
                        string txt = Console.ReadLine();
                        
                        for(i = 0; i < users[userindex].sentNews.Count; i++)
                        {
                            if (users[userindex].sentNews[i].ID == id)
                            {
                                users[userindex].sentNews[i].text = txt;
                                break;
                            }
                        }
                       
                        int rIndex = findUserIndexByName(users, users[userindex].sentNews[i].receiver);
                        for(int j=0;j<users[rIndex].takenNews.Count; j++)
                        {
                           if( users[rIndex].takenNews[j].ID == id)
                            {
                                users[rIndex].takenNews[j].text = txt;
                                break;
                            }
                        }
                        try {
                            string oldtext = allNews[id - 1].text;
                        allNews[id - 1].text = txt;
                        string[] lines3 = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt");
                        int idIndex=1+lines3[userindex].IndexOf(id.ToString())+ id.ToString().Length+2+allNews[id-1].type.ToString().Length;
                        int timeIndex = idIndex+2+oldtext.Length;
                        string left = lines3[userindex].Substring(0, idIndex);
                        string right = lines3[userindex].Substring(timeIndex, lines3[userindex].Length-timeIndex-1);
                        lines3[userindex] = left + " "+txt + right;
                        File.WriteAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt", lines3);
                        }
                        catch
                        {
                            Console.WriteLine("the related file could not be opened");
                        }
                        break;
                    case 7:
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        Console.WriteLine("sent news:");
                        foreach(news n in users[userindex].sentNews)
                        {
                            Console.WriteLine("to {0}\t text:{1}",n.receiver,n.text);
                        }
                        Console.WriteLine("received news:");
                        foreach (news n in users[userindex].takenNews)
                        {
                            Console.WriteLine("from {0}\t text:{1}", n.sender, n.text);
                        }

                        break;
                    case 8:
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        int A=0;
                        do
                        {
                            Console.WriteLine("sort by 1.time or 2.ID?");
                            try
                            {
                                A = int.Parse(Console.ReadLine());
                                if (A == 1 || A == 2)
                                {
                                    valid = true;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("enter an integer 1 or 2!");
                            }
                        } while (!valid);
                        switch (A){
                            case 1:
                            news[] a = allNews.ToArray();
                            sortTime(a, 0, a.Length-1);
                            foreach( news n in a)
                            {
                                Console.WriteLine("({0}  from:{1}  to:{2}  ID: {3}  type:{4}  text:{5})",n.newsTime,n.sender, n.receiver, n.ID, n.type, n.text);
                            }
                                break;
                            case 2:
                            news[] aID = allNews.ToArray();
                            sortID(aID, 0, aID.Length-1);
                            foreach (news n in aID)
                            {
                                Console.WriteLine("(ID:{3}  from:{1}  to:{2}  time: {0}  type:{4}  text:{5})", n.newsTime, n.sender, n.receiver, n.ID, n.type, n.text);
                            }
                                break;
                        }
                        

                        break;
                    case 9:
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        int input = 0;
                        bool found = false;
                        do
                        {
                            Console.WriteLine("sort by 1.time or 2.type?");
                            try
                            {
                                input = int.Parse(Console.ReadLine());
                                if (input == 1 || input == 2)
                                {
                                    valid = true;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("enter an integer 1 or 2!");
                            }
                        } while (!valid);
                        switch (input)
                        {
                            case 1:
                                valid = false;
                                string sender = "";
                                do
                                {
                                    Console.WriteLine("enter sender's name:");
                                    sender = Console.ReadLine();
                                    foreach (user u in users)
                                    {
                                        if (sender == u.username)
                                        {
                                            valid = true;
                                            break;
                                        }
                                    }
                                    if (!valid)
                                    {
                                        Console.WriteLine("wrong username!");
                                    }
                                } while (!valid);
                                foreach(news n in allNews)
                                {
                                    if (n.sender == sender)
                                    {
                                        Console.WriteLine("type: {0}  text: {1}", n.type, n.text);
                                        found = true;
                                    }
                                }
                                if (!found)
                                {
                                    Console.WriteLine("no news was found!");
                                }
                                break;
                            case 2:
                                valid = false;
                                newsType tys = newsType.accident;
                              
                                do
                                {
                                    Console.WriteLine("what is news type? economy=12,social=23,accident=34,tech=45,sport=56,weather=67");
                                    string t = Console.ReadLine();

                                    try
                                    {
                                        tys = (newsType)int.Parse(t);
                                        valid = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! choose one of the types and enter an integer");
                                    }

                                } while (!valid);
                                found = false;
                                foreach (news n in allNews)
                                {
                                    if (n.type == tys)
                                    {
                                        Console.WriteLine("sender: {0}  receiver:{2}  text: {1}", n.sender, n.text,n.receiver);
                                        found = true;
                                    }
                                }
                                if (!found)
                                {
                                    Console.WriteLine("no news was found!");
                                }
                                break;
                        }
                        break;
                    case 10:
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        input = 0;///error?
                        found = false;
                        do
                        {
                            Console.WriteLine("delete by 1.sender or 2.type?");
                            try
                            {
                                input = int.Parse(Console.ReadLine());
                                if (input == 1 || input == 2)
                                {
                                    valid = true;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("enter an integer 1 or 2!");
                            }
                        } while (!valid);
                        switch (input)
                        {
                            case 1:
                                string sender;
                                do
                                {
                                    Console.WriteLine("enter sender's username:");
                                    sender = Console.ReadLine();
                                    foreach (user u in users)
                                    {
                                        if (sender == u.username)
                                        {
                                            valid = true;
                                            break;
                                        }
                                    }
                                    if (!valid)
                                    {
                                        Console.WriteLine("wrong username!");
                                    }
                                } while (!valid);
                                userindex = findUserIndexByName(users,sender);
                                for(i=0;i<users[userindex].sentNews.Count;i++)
                                
                                {
                                    int recIndex = findUserIndexByName(users, users[userindex].sentNews[i].receiver);
                                    for(int j=0;j<users[recIndex].takenNews.Count;j++)
                                    
                                    {
                                        if (users[recIndex].takenNews[j].sender == sender)
                                        {
                                            users[recIndex].takenNews.RemoveAt(j);
                                            j--;
                                        }
                                    }
                                }
                                users[userindex].sentNews.Clear();
                                for(i=0;i<allNews.Count;i++)

                                {
                                    if (allNews[i].sender == sender)
                                    {
                                        allNews.RemoveAt(i);
                                    }
                                }
                                try { 
                                lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt");
                                lines[userindex] = sender;
                                lines[userindex] += " : ";
                                File.WriteAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt", lines);
                                }
                                catch
                                {
                                    Console.WriteLine("the related file could not be opened");
                                }
                                break;
                            case 2:
                                newsType tp = newsType.accident;
                                do
                                {
                                    Console.WriteLine("what is news type? economy=12,social=23,accident=34,tech=45,sport=56,weather=67");
                                    string t = Console.ReadLine();

                                    try
                                    {
                                        tp = (newsType)int.Parse(t);
                                        valid = true;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("wrong input! choose one of the types and enter an integer");
                                    }

                                } while (!valid);
                                try { 
                                lines = File.ReadAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt");
                                for (i = 0; i < users.Count; i++)
                                {
                                    for(int j = 0; j < users[i].sentNews.Count; j++)
                                    {
                                        if (users[i].sentNews[j].type == tp)
                                        {
                                            
                                            string l = lines[i].Substring(0, lines[i].IndexOf(users[i].sentNews[j].ID.ToString() + ", ") - 3 - users[i].sentNews[j].receiver.Length);
                                            string r = lines[i].Substring(lines[i].IndexOf(users[i].sentNews[j+1].ID.ToString() + ", ") - 3 - users[i].sentNews[j+1].receiver.Length);
                                            lines[i] = l + r;
                                            users[i].sentNews.RemoveAt(j);
                                            j--;
                                        }
                                    }
                                }
                                for (i = 0; i < users.Count; i++)
                                {
                                    for (int j = 0; j < users[i].takenNews.Count; j++)
                                    {
                                        if (users[i].takenNews[j].type == tp)
                                        {
                                            users[i].takenNews.RemoveAt(j);
                                            j--;
                                        }
                                    }
                                }
                                for (i = 0; i < allNews.Count; i++)
                                {
                                    if (allNews[i].type == tp)
                                    {
                                        allNews.RemoveAt(i);
                                        i--;
                                    }
                                }
                                File.WriteAllLines(@"D:\uni\ap\codes\hw3\q4\q4\NEWS.txt", lines);
}
                                catch
                                {
                                    Console.WriteLine("the related file could not be opened");
                                }
                                break;

                        }
                        break;
                    case 11:
                        if (!userSignedIn)
                        {
                            signInError();
                            break;
                        }
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter password:");
                            pass = Console.ReadLine();
                            if (pass != users[userindex].pass)
                            {
                                Console.WriteLine("wrong password!");
                            }
                            else
                            {
                                valid = true;
                            }
                        } while (!valid);
                        valid = false;
                        valid = false;
                        do
                        {
                            Console.WriteLine("enter new password:");
                            pass = Console.ReadLine();
                            if (validPass(pass))
                            {
                                valid = true;
                            }
                            else
                            {
                                Console.WriteLine("password is not valid please enter again.");
                            }
                        } while (!valid);
                        users[userindex].pass = pass;
                        Console.WriteLine("password is changed.");
                        break;
                }
            } while (!end);

        }
    }
}
