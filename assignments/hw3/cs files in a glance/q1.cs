using System;
using System.IO;
using System.Collections.Generic;
namespace q1
{
    class Program
    {
        static void Main(string[] args)
        {
            try { 
            StreamReader reader=new StreamReader(@"D:\uni\ap\codes\hw3\q1\t1.txt");
            StreamWriter writer = new StreamWriter(@"D:\uni\ap\codes\hw3\q1\t2.txt");
            string line;
            int lineNum = 0;
            int digitNum = 0;
            int vowelNum = 0;
            int starNum = 0;
            int startAendE = 0;
            int studentNum = 0;
            List<int> wovelCodes = new List<int>(){ 65, 69, 73, 79, 85, 89, 97, 101, 105, 111, 117, 121 };
            while (reader.EndOfStream == false)
            {
                line = reader.ReadLine();
                Console.WriteLine(line);/////
                lineNum++;
                string writtenLine = "";
                for (int i=0;i<line.Length;i++){
                    if (line[i] >= 48 && line[i] <= 57)
                    {
                        digitNum++;
                        writtenLine += line[i];
                    }
                    else
                    if (wovelCodes.Contains(line[i]))
                    {
                        vowelNum++;
                        writtenLine += line[i];
                    }
                    else
                    if (line[i] == ' ')
                    {
                        writtenLine += '*';
                        starNum++;
                    }
                    else
                    if (line[i] == '*')
                    {
                        starNum++;
                    }
                    else
                        writtenLine += line[i];
                }
                string[] words = line.Split(' ');
                foreach( string w in words)
                {
                    if ( w.Length>0&&w[0] == 'a' &&w[w.Length-1] == 'e')
                    {
                        startAendE++;
                    }
                    if (w.ToLower() == "student")
                    {
                        studentNum++;
                    }
                }
                writer.WriteLine(writtenLine);
            }
            Console.WriteLine("Number of lines : {0}",lineNum);
            Console.WriteLine("Number of stars : {0}",starNum);
            Console.WriteLine("Number of digits : {0}",digitNum);
            Console.WriteLine("Number of vowel sounds : {0}",vowelNum);
            Console.WriteLine("Number of words start with 'a' and end with 'e' : {0}",startAendE);
            Console.WriteLine("Number of \"student\" : {0}",studentNum);
            reader.Close();
            writer.Close();
            }
            catch
            {
                Console.WriteLine("one or more files could not be oppened !");
            }

        }
    }
}
