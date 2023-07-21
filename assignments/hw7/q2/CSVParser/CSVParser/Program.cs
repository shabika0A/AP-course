using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Text.RegularExpressions;

namespace Assignment11
{
    class Program
    {
        public static void print(string[] a)
        {
            foreach (string s in a)
            {
                Console.WriteLine(s);
            }
        }
        public static void print(List<string> a)
        {
            foreach (string s in a)
            {
                Console.WriteLine(s);
            }
        }
        public static void print(IMDBData[] a)
        {
            foreach (IMDBData s in a)
            {
                print(s);

            }
        }
        public static void print((string, string)[]  a)
        {
            foreach ((string, string) s in a)
            {
                Console.WriteLine("title: "+s.Item1+" revenue: "+s.Item2);
            }
        }
        public static void print(IMDBData s)
        {

            Console.WriteLine($"Title: {s.Title}");
            Console.WriteLine($"Genre: {s.Genre}");
            Console.WriteLine($"Director: {s.Director}");
            Console.WriteLine($"Actors: {s.Actor1}, {s.Actor2}, {s.Actor3}, {s.Actor4} ");
            Console.WriteLine($"year: {s.Year}");
            Console.WriteLine($"Runtime: {s.Runtime}");
            Console.WriteLine($"Rating: {s.Rating}");
            Console.WriteLine($"Votes: {s.Votes}");
            Console.WriteLine($"Revenue: {s.Revenue}");
            Console.WriteLine($"Metascore: {s.Metascore}");

        }
        static void Main(string[] args)
        {
            var data = File.ReadAllLines(@"..\..\IMDB-Movie-Data.csv")
                .Skip(1)
                .Select(line => new IMDBData(line));
            Console.WriteLine($"The film with highest metascore : {data.GetHighestMetascore().Title}");

            // If necessary, you can use more than one extension method to calculate these answers.
            Console.WriteLine($"Question 1: ---------------------------------------------------");
                print(data.GenreOfLessThan100());
            Console.WriteLine($"Question 2: ---------------------------------------------------");
                print(data.DirectorNamesOfVinDiselMovies());
            Console.WriteLine($"Question 3: ---------------------------------------------------");
                print(data.MostRated2016());
            Console.WriteLine($"Question 4: ---------------------------------------------------");
            print(data.NameAndRevenueOfBryanSingerMovies());
            Console.WriteLine($"Question 5: {data.SumOfSold2011()}");
            
            Console.WriteLine($"Question 6: {data.AverageOfSold2014()}");
            Console.WriteLine($"Question 7: ---------------------------------------------------");
            print(data.TenActionMostSellerlongerthan2h());
            Console.WriteLine($"Question 8: ---------------------------------------------------");
            print(data.TitleContainsNumber());
            Console.WriteLine($"Question 9: ---------------------------------------------------");
            print(data.JenniferLwrenceAndAnneHathawayMovies());
            Console.WriteLine($"Question 10: ---------------------------------------------------");
            print(data.DramaAndComedyCompare());
            //Console.WriteLine($"Question 11: {data.WorstActor()}");
            Console.WriteLine($"Question 12: ---------------------------------------------------");
            print(data.TitleLengthMoreThanPrometheus());
            Console.WriteLine($"Question 13: ---------------------------------------------------");
            print(data.ActionOr2014());
            Console.WriteLine($"Question 14: ---------------------------------------------------");
            print(data.TitlesOf4to10BestActionMovies());

        }
    }

    public static class Extensions
    {
        public static Nullable<int> ParseIntOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? int.Parse(str) as Nullable<int> : null;
        public static string ParseStringOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? str : null;

        //For example
        public static IMDBData GetHighestMetascore(this IEnumerable<IMDBData> data)
            => data.OrderByDescending(x => x.Metascore).First();

        /// <summary>
        /// you must modify the name of this method and its 
        /// implementation to fit your need and create more methods like this
        public static IMDBData ExtensionMethodPlaceHolder(this IEnumerable<IMDBData> data)
            => data.First();
        //3
        public static IMDBData MostRated2016(this IEnumerable<IMDBData> data)
            => data.OrderByDescending(x => x.Votes).Where(x => x.Year == 2016).First();
        //7
        public static string[] TenActionMostSellerlongerthan2h(this IEnumerable<IMDBData> data) =>
             data.Where(x => x.Runtime > 120 && x.Genre == "Action").OrderByDescending(x => x.Revenue).Select(x => x.Title).Take(10).ToArray();
        //1
        public static List<string> GenreOfLessThan100(this IEnumerable<IMDBData> data) => data.Where(x => x.Runtime < 100).Select(x => x.Genre).Distinct().ToList();
        //2
        public static List<string> DirectorNamesOfVinDiselMovies(this IEnumerable<IMDBData> data) => data.Where(x => x.Actor1.Contains("Vin Diesel") || x.Actor2.Contains("Vin Diesel") || x.Actor3.Contains("Vin Diesel") || x.Actor4.Contains("Vin Diesel")).Select(x => x.Director).ToList();
        //4
        public static (string, string)[] NameAndRevenueOfBryanSingerMovies(this IEnumerable<IMDBData> data) => data.Where(x => x.Director == "Bryan Singer").OrderByDescending(s => s.Revenue).Select(x => (x.Title, x.Revenue)).ToArray();
        //5
        public static double SumOfSold2011(this IEnumerable<IMDBData> data) => data.Where(x => x.Year == 2011).Select(x => x.Revenue).ToList().ConvertAll(double.Parse).Sum();
        //6
        public static float AverageOfSold2014(this IEnumerable<IMDBData> data) => data.Where(x => x.Revenue != null && x.Year == 2014).Select(x => (float.Parse(x.Revenue))).Average();
        //8
        public static string[] TitleContainsNumber(this IEnumerable<IMDBData> data) =>
             data.Where(x => x.Title.Any(char.IsDigit)).Select(x => x.Title).ToArray();
        //9
        public static List<string> JenniferLwrenceAndAnneHathawayMovies(this IEnumerable<IMDBData> data) {
            List<string> ans = new List<string>();
            ans.Add("Jennifer Lawrence order by date:");
            ans.AddRange( data.Where(s => s.Actor1.Contains("Jennifer Lawrence") || s.Actor2.Contains("Jennifer Lawrence") || s.Actor3.Contains("Jennifer Lawrence") || s.Actor4.Contains("Jennifer Lawrence")).OrderBy(d => d.Year).Select(o=>o.Title));
           
            ans.Add("Jennifer Lawrence order by rate:");
            ans.AddRange(data.Where(s => s.Actor1.Contains("Jennifer Lawrence") || s.Actor2.Contains("Jennifer Lawrence") || s.Actor3.Contains("Jennifer Lawrence") || s.Actor4.Contains("Jennifer Lawrence")).OrderByDescending(d => d.Rating).Select(o => o.Title).ToArray());
           
            ans.Add("Anne Hathaway order by date:");
            ans.AddRange(data.Where(s =>  s.Actor2.Contains("Anne Hathaway") || s.Actor4.Contains("Anne Hathaway") || s.Actor3.Contains("Anne Hathaway") || s.Actor1.Contains("Anne Hathaway")).OrderBy(d => d.Year).Select(o => o.Title).ToArray());

            ans.Add("Anne Hathaway order by rate:");
            ans.AddRange(data.Where(s =>s.Actor2.Contains("Anne Hathaway") || s.Actor4.Contains("Anne Hathaway") || s.Actor3.Contains("Anne Hathaway") || s.Actor1.Contains("Anne Hathaway")).OrderByDescending(d => d.Rating).Select(o => o.Title).ToArray());
            return ans;

        }
        //10
        public static List<string> DramaAndComedyCompare(this IEnumerable<IMDBData> data) {
            List<string> ans = new List<string>();
            ans.Add("Drama Movies:");
            ans.AddRange(data.Where(x => x.Rating != null && double.Parse(x.Rating) > 8&& x.Genre=="Drama").Select(x => x.Title).ToList());
            int dn = ans.Count - 1;
            ans.Add("Comedy Movies:");
            ans.AddRange(data.Where(x => x.Rating!=null&&double.Parse(x.Rating) > 8 && x.Genre == "Comedy").Select(x => x.Title).ToList());
            if (ans.Count() - 2 - dn > dn)
            {
                ans.Add("Comedy is more than drama");
            }
            else
            {
                ans.Add("drama is more than comedy");

            }
            return ans;
        }
        //11  remained
        // I want to return the name that was repeated the most.
        public static string WorstActor(this IEnumerable<IMDBData> data) {
            List <string> actors=
            (data.Where(c => c.Rating != null && double.Parse(c.Rating) < 7).Select(x => x.Actor1).Concat(data.Where(c => int.Parse(c.Rating) < 7).Select(x => x.Actor2)).Concat(data.Where(c => int.Parse(c.Rating) < 7).Select(x => x.Actor3)).Concat(data.Where(c => int.Parse(c.Rating) < 7).Select(x => x.Actor4))).ToList();
            List<string> da = actors.Distinct().ToList();
            List<int > n = new List<int>();
            for(int i = 0; i < da.Count; i++)
            {
                n.Add(0);
            }
            foreach( string s in actors)
            {
                n[da.IndexOf(s)]++;
            }
            return da[n.IndexOf(n.Max())];
                }
        //12 }
        public static List<string> TitleLengthMoreThanPrometheus(this IEnumerable<IMDBData> data) => data.Where(a => a.Title.Length > "Prometheus".Length).Select(f => f.Title).ToList();
        //13
        public static string[] ActionOr2014(this IEnumerable<IMDBData> data) => data.Where(c => c.Genre == "Action").Select(x => x.Title).Concat(data.Where(c => c.Year == 2014).Select(x => x.Title)).ToArray();
        //14
        public static List<string> TitlesOf4to10BestActionMovies(this IEnumerable<IMDBData> data) => data.Where(a => a.Genre == "Comedy").OrderByDescending(f => f.Rating).Select(x => x.Title).Take(10).Reverse().Take(7).Reverse().ToList();



    }



    public class IMDBData
    {
        public IMDBData(string line)
        {
            var toks = line.Split(',');
            Rank = int.Parse(toks[0]);
            Title = toks[1];
            Genre = toks[2];
            Director = toks[3];
            Actor1 = toks[4];
            Actor2 = toks[5];
            Actor3 = toks[6];
            Actor4 = toks[7];
            Year = int.Parse(toks[8]);
            Runtime = int.Parse(toks[9]);
            Rating = (toks[10]);
            Votes = int.Parse(toks[11]);
            Revenue = toks[12].ParseStringOrNull();
            Metascore = toks[13].ParseIntOrNull();
        }
        public int Rank;
        public string Title;
        public string Genre;
        public string Director;
        public string Actor1;
        public string Actor2;
        public string Actor3;
        public string Actor4;
        public int Year;
        public int Runtime;
        public string Rating;
        public int Votes;
        public string Revenue;
        public Nullable<int> Metascore;
    }
}
