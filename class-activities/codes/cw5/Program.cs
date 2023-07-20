using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
    class Program
    {
        enum Diploma
        { undergraduate = 0, graduated = 1 }
        interface CanTalk
        {
            void Talk();
        }
        class person : CanTalk
        {
            public string name;
            public Diploma certificate;
            public void Talk()
            {
                Console.WriteLine("hi my name is{0} and I am {1}.", name, certificate.ToString());
            }
            public person(string n, Diploma d)
            {
                name = n;
                certificate = d;
            }
            public virtual void Brief()
            {
                Console.WriteLine("name: {0} education: {1}", name, certificate.ToString());
            }
        }
        class teacher : person
        {
            public string subject;
            public List<student> students = new List<student>();
            public teacher(string n, Diploma d, string s) : base(n, d)
            {
                name = n;
                certificate = d;
                subject = s;
            }
            public override void Brief()
            {
                base.Brief();
                foreach (student s in students)
                {
                    Console.WriteLine(s.name);
                }
                if (students.Count == 0)
                {
                    Console.WriteLine("there is no students!");
                }

            }
        }
        class student : person
        {
            int classNum;
            public string subject;
            public List<teacher> Teachers = new List<teacher>();
            public student(string n, Diploma d, string s, int c) : base(n, d)
            {
                name = n;
                certificate = d;
                subject = s;
                classNum = c;
            }
            public override void Brief()
            {
                base.Brief();
                if (Teachers.Count == 0)
                {
                    
                        Console.WriteLine("there is no teachers!");
                   
                }else
                Console.WriteLine("name of teacher:{0}  class number:{1}", Teachers[0].name, classNum);

            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("enter information");
            try { 
            string[] ostad1 = Console.ReadLine().Split("-");
            string[] ostad2 = Console.ReadLine().Split("-");
            string[] student1 = Console.ReadLine().Split("-");
            string[] student2 = Console.ReadLine().Split("-");
            for (int i = 0; i < ostad1.Length; i++)
            {
                ostad1[i] = ostad1[i].Replace("<<", "");
                ostad1[i] = ostad1[i].Replace(">>", "");
            }
            for (int i = 0; i < ostad2.Length; i++)
            {
                ostad2[i] = ostad2[i].Replace("<<", "");
                ostad2[i] = ostad2[i].Replace(">>", "");
            }
            for (int i = 0; i < student1.Length; i++)
            {
                student1[i] = student1[i].Replace("<<", "");
                student1[i] = student1[i].Replace(">>", "");
            }
            for (int i = 0; i < student2.Length; i++)
            {
                student2[i] = student2[i].Replace("<<", "");
                student2[i] = student2[i].Replace(">>", "");
            }
            List<student> students = new List<student>();
            List<teacher> teachers = new List<teacher>();
            for (int i = 0; i < student1.Length; i += 4)
            {
                students.Add(new student(student1[i], (Diploma)Enum.Parse(typeof(Diploma), student1[i + 1]), student1[i + 2], int.Parse(student1[i + 3])));
            }
            for (int i = 0; i < student2.Length; i += 4)
            {
                students.Add(new student(student2[i], (Diploma)Enum.Parse(typeof(Diploma), student2[i + 1]), student2[i + 2], int.Parse(student2[i + 3])));
            }
            for (int i = 0; i < ostad1.Length; i += 3)
            {
                teachers.Add(new teacher(ostad1[i], (Diploma)Enum.Parse(typeof(Diploma), ostad1[i + 1]), ostad1[i + 2]));
            }
            for (int i = 0; i < ostad2.Length; i += 3)
            {
                teachers.Add(new teacher(ostad2[i], (Diploma)Enum.Parse(typeof(Diploma), ostad2[i + 1]), ostad2[i + 2]));
            }
            for (int i = 0; i < students.Count; i++)
            {
                for (int j = 0; j < teachers.Count; j++)
                {
                    if (students[i].subject == teachers[j].subject)
                    {
                        students[i].Teachers.Add(teachers[j]);
                        teachers[j].students.Add(students[i]);
                        break;
                    }
                }
            }
            for (int i = 0; i < students.Count; i++)
            {
                students[i].Brief();
            }
            for (int i = 0; i < teachers.Count; i++)
            {
                teachers[i].Brief();

            }
            }
            catch
            {
                Console.WriteLine("wrong input!");
            }
        }
    }
}
