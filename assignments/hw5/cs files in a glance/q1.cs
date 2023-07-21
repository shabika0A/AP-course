using System;
using System.Collections.Generic;
namespace q2
{
    class Program
    {
        enum sex
        {
            male = 0, female = 1, others
        }
        class Person
        {
            public string Name;
            public string SSN;
            public string Field;
            public sex Sex;
            public Person(string name, string ssn, string f, sex s)
            {
                Name = name;
                SSN = ssn;
                Field = f;
                Sex = s;
            }
        }
        class Professor : Person
        {
            public int RoomNo;
            public int MinTRA;
            public List<Student> ResearchAssistants = new List<Student>();
            static List<int> randoms = new List<int>();
            public List<Unit> units = new List<Unit>();
            public Professor(string name, string ssn, string f, sex s) : base(name, ssn, f, s) {
                Random rnd = new Random();
                int num = rnd.Next(1, 1000);
                while (randoms.Contains(num))
                {
                    num = rnd.Next(1, 1000);
                }
                RoomNo = num;
                randoms.Add(num);
            }
        }
        class Student : Person
        {
            public int EnteringYear;
            public List<Unit> units = new List<Unit>();

            public Student(string name, string ssn, string f, sex s, int y) : base(name, ssn, f, s)
            {
                EnteringYear = y;
            }
        }
        class TeacherAssistant : Student
        {
            public int UnitId;
            public TeacherAssistant(string name, string ssn, string f, sex s, int y) : base(name, ssn, f, s, y) { }

        }
        class ResearchAssistant : Student
        {
            public string ProjectName;
            public int FreeTime;
            public string ProfessorSSN;
            public ResearchAssistant(string name, string ssn, string f, sex s, int y) : base(name, ssn, f, s, y) { }
        }
        class Unit
        {
            public int UnitId = 1;
            public string Name;
            public string Field;
            public int MaxSize;
            public List<Student> students = new List<Student>();
            public string ProfessorSSN = null;
            public List<TeacherAssistant> teacherAssistants = new List<TeacherAssistant>();
            public List<(string, int)> mark = new List<(string, int)>();
            public Unit(int id, string n, string f, int ms)
            {
                UnitId = id;
                Name = n;
                Field = f;
                MaxSize = ms;
            }
        }
        static bool checklength3to20(string s)
        {
            if (s.Length < 3 || s.Length > 20)
            {
                return false;
            }
            return true;
        }
        static Student get_student_by_ssn(List<Student> studentsa, String ssn)
        {
            foreach (Student student in studentsa)
            {
                if (student.SSN == ssn)
                {
                    return student;
                }
            }
            return null;
        }
        static Unit get_unit_by_id(List<Unit> units, int id)
        {
            foreach (Unit Unit in units)
            {
                if (Unit.UnitId == id)
                {
                    return Unit;
                }
            }
            return null;
        }
        static void add_student_to_unit()
        {
            string[] ans = Console.ReadLine().Split(" ");
            string ssn;
            int unit_id;
            try
            {
                ssn = ans[1];
                unit_id = int.Parse(ans[2]);
            }
            catch
            {
                Console.WriteLine("wrong input for unit ID!");
                return;
            }
            
            Student student = get_student_by_ssn(students, ssn);
            if (student == null)
            {
                Console.WriteLine("No student exists with this ssn");
                return;
            }
            Unit unit = get_unit_by_id(units, unit_id);
            if (unit == null)
            {
                Console.WriteLine("No unit with exists with this id");
                return;
            }
            if (student.Field != unit.Field)
            {
                Console.WriteLine("Student field doesn't match the unit field.");
                return;
            }
            if (unit.students.Contains(student))
            {
                Console.WriteLine("Student already has the unit.");
                return;
            }
            if (unit.students.Count >= unit.MaxSize)
            {
                Console.WriteLine("Unit max size is reached.");
                return;
            }
            unit.students.Add(student);
            student.units.Add(unit);
            Console.WriteLine("Student is successfully added to the unit.");
        }
        static void register_student()
        {
            string[] ans = Console.ReadLine().Split(" ");

            try
            {
                if (!checklength3to20(ans[1]) || !checklength3to20(ans[4]))
                {
                    throw new Exception("input has wrong size");
                }
                if (int.Parse(ans[3]) < 1350 || int.Parse(ans[3]) > DateTime.Now.Year)
                {
                    throw new Exception("year is wrong");
                }
                foreach (Professor p in professors)
                {
                    if (ans[2] == p.SSN)
                    {
                        throw new Exception("this ssn already exist!");
                    }
                }
                foreach (Student p in students)
                {
                    if (ans[2] == p.SSN)
                    {
                        throw new Exception("this ssn already exist!");
                    }
                }
                students.Add(new Student(ans[1], ans[2], ans[4], (sex)Enum.Parse(typeof(sex), ans[5]), int.Parse(ans[3])));
                Console.WriteLine("Student is successfully registered.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void register_professor()
        {
            string[] ans = Console.ReadLine().Split(" ");

            try
            {
                if (!checklength3to20(ans[1]) || !checklength3to20(ans[3]))
                {
                    throw new Exception("input has wrong size");
                }
                if (ans[2].Length != 10)
                {
                    throw new Exception("ssn number is wrong");
                }
                foreach (Professor p in professors)
                {
                    if (ans[2] == p.SSN)
                    {
                        throw new Exception("this ssn already exist!");
                    }
                }
                foreach (Student p in students)
                {
                    if (ans[2] == p.SSN)
                    {
                        throw new Exception("this ssn already exist!");
                    }
                }
                professors.Add(new Professor(ans[1], ans[2], ans[3], (sex)Enum.Parse(typeof(sex), ans[4])));
                Console.WriteLine("Professor is successfully registered.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void make_unit()
        {
            string[] ans = Console.ReadLine().Split(" ");
            ////make_unit <UnitId> <Name> <Field> <MaxSize>
            try
            {
                if (!checklength3to20(ans[2]) || !checklength3to20(ans[3]))
                {
                    throw new Exception("input has wrong size");
                }
                if (int.Parse(ans[4]) < 10 || int.Parse(ans[4]) > 180)
                {
                    throw new Exception("size is wrong");
                }
                int id = int.Parse(ans[1]);
                foreach (Unit p in units)
                {
                    if (p.UnitId == id)
                    {
                        throw new Exception("this unit ID already exist!");
                    }
                }
                units.Add(new Unit(int.Parse(ans[1]), ans[2], ans[3], int.Parse(ans[4])));
                Console.WriteLine("unit is successfully made.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static Professor get_professor_by_ssn(string ssn)
        {
            foreach (Professor professor in professors)
            {
                if (professor.SSN == ssn)
                {
                    return professor;
                }
            }
            return null;
        }
        static void add_professor_to_unit( )
        {
            string[] ans = Console.ReadLine().Split(" ");
            string ssn = ans[1];
            int unitId;
            try
            {
                unitId = int.Parse(ans[2]);
            }
            catch
            {
                Console.WriteLine("wrong input for unit ID!");
                return;
            }
            Professor professor = get_professor_by_ssn(ssn);
            if (professor == null)
            {
                Console.WriteLine("No professor exists with this ssn.");
                return;
            }
            Unit unit = get_unit_by_id(units, unitId);
            if (unit == null)
            {
                Console.WriteLine("No unit exists with this id.");
                return;
            }
            if (unit.Field != professor.Field)
            {
                Console.WriteLine("Professor field doesn't match the unit field.");
                return;
            }
            if (unit.ProfessorSSN != null)
            {
                Console.WriteLine("The unit already has a professor.");
                return;
            }
            professor.units.Add(unit);
            unit.ProfessorSSN = ssn;
            Console.WriteLine("Professor is successfully added to the unit.");
        }
        static void set_student_teaching_assistant()
        {
            //set_student_teaching_assistant <SSN> <UnitId>
            //check min hour?
            string[] ans = Console.ReadLine().Split(" ");
            try
            {
                Student s = get_student_by_ssn(students, ans[1]);
                Unit u = get_unit_by_id(units, int.Parse(ans[2]));
                if (s == null || u == null)
                {
                    throw new Exception();
                }
                if (s.Field != u.Field || u.ProfessorSSN == null)
                {
                    throw new Exception();
                }
                if (u.ProfessorSSN == null)
                {
                    throw new Exception();
                }
                students.Remove(s);
                //TeacherAssistant t = s;
                TeacherAssistant t = new TeacherAssistant(s.Name, s.SSN, s.Field, s.Sex, s.EnteringYear);
                t.units = s.units;
                t.UnitId = u.UnitId;
                students.Add(t);
                u.teacherAssistants.Add(t);
                Console.WriteLine("Student was set as TA successfully ");
            }
            catch
            {
                Console.WriteLine("wrong input");
            }
        }
        static void set_student_research_assistant()
        {
            //set_student_research_assistant<Student_SSN><professor_SSN><ProjectName><time_in_week>
            string[] ans = Console.ReadLine().Split(" ");
            try
            {
                Student s = get_student_by_ssn(students, ans[1]);
                Professor p = get_professor_by_ssn(ans[2]);

                //ResearchAssistant(string name, string ssn, string f, sex s, int y)
                ResearchAssistant r = new ResearchAssistant(s.Name, s.SSN, s.Field, s.Sex, s.EnteringYear);
                r.units = s.units;
                r.ProfessorSSN = p.SSN;
                r.ProjectName = ans[3];
                r.FreeTime = int.Parse(ans[4]);
                if (s == null || p == null || s.Field != p.Field || r.FreeTime < p.MinTRA)
                {
                    throw new Exception();
                }
                if (s.GetType() == typeof(TeacherAssistant) || s.GetType() == typeof(ResearchAssistant))
                {
                    throw new Exception("this student is already an assistant!");
                }
                students.Remove(s);
                students.Add(r);
                p.ResearchAssistants.Add(r);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        static void get_unit_status()
        {
            Console.WriteLine("enter unit ID:");
            int unitId;
            try
            {
            unitId = int.Parse(Console.ReadLine());
            Unit unit = get_unit_by_id(units, unitId);
            if (unit == null)
            {
                Console.WriteLine("No unit exists with this id.");
                return;
            }
            Professor prof = get_professor_by_ssn(unit.ProfessorSSN);
            string profName = "";
            if (prof == null)
                profName = "None";
            else
                profName = prof.Name;
            Console.WriteLine("professor: " + profName + " " + "max size: " + unit.MaxSize + " " + "field: " + unit.Field);
            Console.Write("students: ");
            foreach (Student student in unit.students)
            {
                Console.Write(student.Name + " ");
            }
            Console.WriteLine(" ");
            Console.Write("TA: ");
            foreach (Student s in unit.teacherAssistants)
            {
                Console.Write(s.Name + " ");

            }
            Console.WriteLine(" ");
            }
            catch
            {
                Console.WriteLine("wrong input format");
            }
           
        }
        static void printStudents(List<Student> students)
        {
            foreach (Student student in students)
            {
                Console.Write(student.Name + " ");
            }
        }
        static void student_status()
        {
            string[] ans = Console.ReadLine().Split();

            Student s = get_student_by_ssn(students, ans[1]);
            if (s == null)
            {
                Console.WriteLine("there is no students with this ssn");
            }
            else
            {
                Console.WriteLine(s.Name + " " + s.EnteringYear + " " + s.Field);
                foreach (Unit u in s.units)
                {
                    Console.Write(u.Name + " ");
                }
                if (s.GetType() == typeof(TeacherAssistant))
                {
                    
                    TeacherAssistant t = (TeacherAssistant)s;
                    Console.WriteLine("is a TA in "+get_unit_by_id(units,t.UnitId).Name);
                }
                if (s.GetType() == typeof(ResearchAssistant))
                {
                    ResearchAssistant t = (ResearchAssistant)s;
                    Console.WriteLine("is a RA in " +t.ProjectName);

                }
            }
        }
        static void professor_status()
        {
            string[] ans = Console.ReadLine().Split();

            Professor s = get_professor_by_ssn(ans[1]);
            if (s == null)
            {
                Console.WriteLine("there is no professors with this ssn");
            }
            else
            {
                Console.WriteLine(s.Name + " " + s.Field + " " + s.RoomNo + " " + s.MinTRA);
                foreach (Unit u in s.units)
                {
                    Console.Write(u.Name + " ");
                }
                printStudents(s.ResearchAssistants);
                Console.WriteLine(" ");
            }
        }
        //static void unit_status()
        //{
        //    string[] ans = Console.ReadLine().Split();
        //    try
        //    {
        //        Unit u = get_unit_by_id(units, int.Parse(ans[1]));
        //        if (u == null)
        //        {
        //            Console.WriteLine("wrong input");
        //        }
        //        else
        //        {
        //            string n;
        //            try
        //            {
        //            n = get_professor_by_ssn(u.ProfessorSSN).Name;///check
        //            }
        //            catch
        //            {
        //                n = "None";
        //            }
                    
        //            Console.WriteLine(n + " " + u.Field + " " + u.MaxSize);
        //            printStudents(u.students);
        //            foreach (TeacherAssistant t in u.teacherAssistants)
        //            {
        //                Console.Write(t.Name + " ");
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        Console.WriteLine("wrong input");
        //    }



        //}
        static void set_final_mark ()
        {
            string[] ans = Console.ReadLine().Split();
            try
            {
            //set_final_mark <professor_SSN> <student_SSN> <UnitId> <mark>
            Student s = get_student_by_ssn(students,ans[2]);
            Professor p= get_professor_by_ssn(ans[1]);
            Unit u = get_unit_by_id(units,int.Parse(ans[3]));
                if(s==null || p == null || u == null)
                {
                    throw new Exception("unit or student or professor does not exist!");
                }
                if (!s.units.Contains(u))
                {
                    throw new Exception("student did not attend in this unit!");
                }
                if (!p.units.Contains(u))
                {
                    throw new Exception("proffesor did not have in this unit!");
                }
                bool found = false;
                for(int i=0;i<u.mark.Count;i++)
                {
                    if (u.mark[i].Item1 == s.SSN)
                    {
                        u.mark[i] = (ans[2], int.Parse(ans[4]));
                        found = true;
                        break;
                    }
                }if(!found)
                u.mark.Add((ans[2], int.Parse(ans[4])));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }
        static void mark_student()
        {
            string[] ans = Console.ReadLine().Split();
            try
            {
                //mark_student <SSN> <UnitId>
                Student s = get_student_by_ssn(students, ans[1]);
                
                Unit u = get_unit_by_id(units, int.Parse(ans[2]));
                if (s == null || u == null)
                {
                    throw new Exception("unit or student does not exist!");
                }
                if (!s.units.Contains(u))
                {
                    throw new Exception("student did not attend in this unit!");
                }
                bool found = false;
                foreach((string,int) m in u.mark)
                {
                    if (m.Item1 == s.SSN)
                    {
                        Console.WriteLine(m.Item2);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("None");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
        static void mark_list()
        {
            string[] ans = Console.ReadLine().Split();
            Unit u = get_unit_by_id(units, int.Parse(ans[1]));
            if (u == null)
            {
                Console.WriteLine("wrong input");

            }
            else
            {
                if (u.mark.Count == 0)
                {
                    Console.WriteLine("no student");
                }
                else
                {
                    foreach( (string,int )m in u.mark)
                    {
                        Console.WriteLine(m.Item1 + " : " + m.Item2);
                    }
                }
            }

        }
        static void average_mark_professor()
        {
            string[] ans = Console.ReadLine().Split();
            Professor p = get_professor_by_ssn(ans[1]);
            int sum = 0;
            int numberOfMarks = 0;
            if (p != null) { 
            foreach(Unit u in p.units)
            {
                foreach((string,int) m in u.mark)
                {
                    sum += m.Item2;
                    numberOfMarks++;
                }
            }
            if (numberOfMarks == 0)
            {
                Console.WriteLine("None");
            }
            else
            {
                    Console.WriteLine(Math.Round((double)sum / numberOfMarks, 2));
            }
            }
            else
            {
                Console.WriteLine("wrong input for ssn");
            }
        }
        static void top_student()
        {
            //top_student <Field> <EnteringYear>
            string[] ans = Console.ReadLine().Split();
            int sum, markNum;
            List<(string, float)> condidates = new List<(string, float)>();
            try
            {
                int year = int.Parse(ans[2]);

                foreach (Student s in students)
                {
                    if (s.Field == ans[1] && s.EnteringYear == year)
                    {
                        sum = 0; markNum = 0;
                        foreach(Unit u in s.units)
                        {
                            foreach((string,int) m in u.mark)
                            {
                                if (m.Item1 == s.SSN)
                                {
                                    markNum++;
                                    sum += m.Item2;
                                    break;
                                }
                            }
                        }
                        if (markNum != 0)
                        {
                        condidates.Add((s.Name, ((float)sum) / markNum));
                        }
                        
                    }
                }
                if (condidates.Count == 0)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    (string, float) top = condidates[0];
                    foreach((string, float) c in condidates)
                    {
                        if (c.Item2 > top.Item2)
                        {
                            top = c;
                        }
                        if (c.Item2 == top.Item2)
                        {
                            if (c.Item1.CompareTo(top.Item1) < 0)
                            {
                                top = c;
                            }
                        }
                    }
                    Console.WriteLine(top.Item1 + " " + top.Item2);
                }
            }
            catch
            {
                Console.WriteLine("wrong input!");
            }
           
        }

        static List<Student> students = new List<Student>();
        static List<Professor> professors = new List<Professor>();
        static List<Unit> units = new List<Unit>();
        
        static void Main(string[] args)
        {
            //students.Add(new Student("hamid", "1234567899", "comp", sex.male, 1399));
            //units.Add(new Unit(1, "algorithm", "comp", 11));
            //professors.Add(new Professor("kian", "2345678999", "comp", sex.male));
            bool end = false;
            while (!end)
            {
                Console.WriteLine("1.register_student\n2.register_professor\n3.make_unit");
                Console.WriteLine("4.add_student\n5.add_professor\n6.set_student_teaching_assistant\n7.set_student_research_assistant");
                Console.WriteLine("8.student_status\n9.professor_status\n10.unit_status\n11.set_final_mark\n12.mark_student\n13.mark_list\n14.average_mark_professor\n15.top_student\n16.exit =D");
                bool valid = false;
                int ans = 0;
                do
                {
                    try
                    {
                    ans = int.Parse(Console.ReadLine());
                        if (ans < 17 && ans > 0)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.WriteLine("wrong input please enter again");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("wrong input");
                    }
                    
                } while (!valid);
                switch(ans)
                {
                    case 1:
                        register_student();
                        break;
                    case 2:
                        register_professor();
                        break;
                    case 3:
                        make_unit();
                        break;
                    case 4:
                        add_student_to_unit();
                        break;
                    case 5:
                        add_professor_to_unit();
                        break;
                    case 6:
                        set_student_teaching_assistant();
                        break;
                    case 7:
                        set_student_research_assistant();
                        break;
                    case 8:
                        student_status();
                        break;
                    case 9:
                        professor_status();
                        break;
                    case 10:
                        get_unit_status();
                        break;
                    case 11:
                        set_final_mark();
                        break;
                    case 12 :
                        mark_student();
                        break;
                    case 13:
                        mark_list();
                        break;
                    case 14:
                        average_mark_professor();
                        break;
                    case 15:
                        top_student();
                        break;
                    case 16:
                        end = true;
                        break;

                }
            }
        }
    }
}

