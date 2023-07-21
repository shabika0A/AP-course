using System;
using System.Collections.Generic;
namespace q1
{
    class Program
    {
        static bool checkSSN(string s, List<IDoctor> dlist)
        {
            for(int i = 0; i < dlist.Count; i++)
            {
                if (dlist[i].SSN == s)
                {
                    return false;
                }
                for (int j = 0; j < dlist[i].Patients.Count; j++)
                {
                    if (dlist[i].SSN == s)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static int ShowMenu()
        {
            bool valid = false;
            do
            {
                Console.WriteLine("1.Add doctor\n2.Add patient\n3.change patient condition\n4.show recovered patients\n5.show doctors\n6.compare two doctors\n7.sort doctors\n8.doctor state of work and education\n9.exit");
                try
                {
                    int ans = int.Parse(Console.ReadLine());
                    if (ans < 10 && ans > 0)
                    {
                        return ans;
                    }
                    else
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("wrong input!");
                }
            } while (!valid);
            return 0;
        }
        static int findDoctorIndexByLastName(string l,List<IDoctor> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].Lastname == l)
                {
                    return i;
                }
            }
            return -1;

        }
        static int findPatientIndexByLastName(string l, List<Patient> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Lastname == l)
                {
                    return i;
                }
            }
            return -1;

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            Doctors allDoctors = new Doctors();

            bool end = false;
            do
            {
                int ans = ShowMenu();
                switch (ans)
                {
                    case 9:
                        end = true;
                        break;
                    case 1:
                        try
                        {
                            IDoctor d;
                            Console.WriteLine("enter doctor type (General Practitioner or Dentist or Surgeon)");
                            string a = Console.ReadLine();
                            if (a == "Dentist")
                            {
                                d = new Dentist();
                            }
                            else if (a == "Surgeon")
                            {
                                d = new Surgeon();
                            }
                            else if (a == "General Practitioner")
                            {
                                d = new GeneralPractitioner();
                            }
                            else
                            {
                                throw new Exception();
                            }
                            Console.WriteLine("enter first name:");
                            d.Firstname = Console.ReadLine();
                            Console.WriteLine("enter last name:");
                            d.Lastname = Console.ReadLine();
                            string s;
                            do
                            {
                                Console.WriteLine("enter SSN:");
                                s = Console.ReadLine();
                            } while (!checkSSN(s, allDoctors.DoctorList));
                           
                            d.SSN = s;
                            Console.WriteLine("enter field:");
                            d.Field = Console.ReadLine();
                            Console.WriteLine("enter salary:");
                            d.Salary = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter university name and level separated by space :");
                            string[] input1 = Console.ReadLine().Split();
                            d.University =input1[0];
                            d.UniLevel = int.Parse(input1[1]);
                            d.Patients = new List<Patient>();
                            allDoctors.DoctorList.Add(d);
                            Console.WriteLine("doctor was added successfully .");
                        }
                        catch
                        {
                            Console.WriteLine("wrong input!");
                        }
                        break;
                    case 2:
                        try
                        {
                            Patient p = new Patient();
                            Console.WriteLine("enter first name:");
                            p.Firstname = Console.ReadLine();
                            Console.WriteLine("enter last name:");
                            p.Lastname = Console.ReadLine();
                            string s;
                            do
                            {
                                Console.WriteLine("enter SSN:");
                                s = Console.ReadLine();
                            } while (!checkSSN(s, allDoctors.DoctorList));
                           
                            p.SSN = s;
                            Console.WriteLine("enter disease ():");////////////remained
                            p.Disease = Console.ReadLine();
                            Console.WriteLine("enter recoovery (healthy underCare or sick:");
                            p.Recovered = (PatientState)Enum.Parse(typeof(PatientState), Console.ReadLine());
                            Console.WriteLine("enter doctor's last name :");
                            int Dindex = findDoctorIndexByLastName(Console.ReadLine(), allDoctors.DoctorList);
                            if (Dindex == -1)
                            {
                                throw new Exception();
                            }
                            ///////////////remained
                            allDoctors.DoctorList[Dindex].Patients.Add(p);
                            Console.WriteLine("patient was added successfully .");
                        }
                        catch
                        {
                            Console.WriteLine("wrong input!");
                        }
                        break;
                    case 3:
                        try {
                            Console.WriteLine("enter doctor's last name:");
                            string dl = Console.ReadLine();
                            int doctorIndex = findDoctorIndexByLastName(dl, allDoctors.DoctorList);
                            Console.WriteLine("enter patient's last name:");
                            int pIndex = findPatientIndexByLastName(Console.ReadLine(), allDoctors.DoctorList[doctorIndex].Patients);
                            Console.WriteLine("enter new condition");
                            allDoctors.DoctorList[doctorIndex].Patients[pIndex].Recovered = (PatientState)Enum.Parse(typeof(PatientState), Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("wrong input!");
                        }
                        break;
                    case 4:
                        for (int i = 0; i < allDoctors.DoctorList.Count; i++)
                        {
                            for (int j = 0; j < allDoctors.DoctorList[i].Patients.Count; j++)
                            {
                                if (allDoctors.DoctorList[i].Patients[j].Recovered == PatientState.healthy)
                                {
                                    Console.WriteLine(allDoctors.DoctorList[i].Patients[j].Firstname + " " + allDoctors.DoctorList[i].Patients[j].Lastname + "\tdoctor:" + allDoctors.DoctorList[i].Lastname);

                                }
                            }
                        }
                        break;
                    case 5:
                        for (int i = 0; i < allDoctors.DoctorList.Count; i++)
                        {
                            Console.WriteLine(allDoctors.DoctorList[i].Firstname + "  " + allDoctors.DoctorList[i].Lastname + "\t" + allDoctors.DoctorList[i].Field);

                        }
                        break;
                    case 6:
                        try
                        {
                            Console.WriteLine("enter first doctor's last name:");
                            int fIndex = findDoctorIndexByLastName(Console.ReadLine(), allDoctors.DoctorList);
                            Console.WriteLine("enter second doctor's last name:");
                            int sIndex = findDoctorIndexByLastName(Console.ReadLine(), allDoctors.DoctorList);
                            //if (allDoctors.DoctorList[fIndex].GetType()== allDoctors.DoctorList[sIndex].GetType())
                            //{
                            //    if ()
                            //    {
                            //        Console.WriteLine("first is better");
                            //    }
                            //    else
                            //if (a == 0)
                            //    {
                            //        Console.WriteLine("both are equal");
                            //    }
                            //    else
                            //if (a == -1)
                            //    {
                            //        Console.WriteLine("second is better");
                            //    }
                            //}
                            int a = allDoctors.DoctorList[fIndex].CompareTo(allDoctors.DoctorList[fIndex], allDoctors.DoctorList[sIndex]);
                            if (a == 1)
                            {
                                Console.WriteLine("first is better");
                            } else
                            if (a == 0)
                            {
                                Console.WriteLine("both are equal");
                            } else
                            if (a == -1)
                            {
                                Console.WriteLine("second is better");
                            } }
                        catch
                        {
                            Console.WriteLine("wrong input");
                        }
                        break;
                    case 7:
                        allDoctors.SortDoctors();
                        Console.WriteLine("sorted successfully");
                        break;
                    case 8:
                        Console.WriteLine("enter doctor's last name");
                        try { 
                        int i = findDoctorIndexByLastName(Console.ReadLine(), allDoctors.DoctorList);
                            Console.WriteLine(allDoctors.DoctorList[i].work());
                            Console.WriteLine(allDoctors.DoctorList[i].GraduatedFrom());
                        }
                        catch
                        {
                            Console.WriteLine("wrong input");
                        }
                        break;

                }
            } while (!end);
            

        }
        interface IPerson
        {
            
            public string Firstname
            {
                get;
                set;
            }
            public string Lastname
            {
                get;
                set;
            }
            public string SSN
            {
                get;
                set;
            }

        }
        interface IDoctor:IPerson
        {
            public string Field
            {
                get;
                set;
            }
            public long Salary
            {
                get;
                set;
            }
            public string University
            {
                get;
                set;
            }
            public int UniLevel
            {
                get;
                set;
            }
            public List<Patient> Patients
            {
                get;
                set;
            }
            public string work();
            public string GraduatedFrom();
            public int CompareTo(IDoctor a, IDoctor b)
            {
                if (a.Patients.Count==0 && b.Patients.Count!=0)
                {
                    return -1;
                }
                else if (a.Patients.Count != 0 && b.Patients.Count == 0)
                {
                    return 1;
                }
                else if (a.Patients.Count == 0 && b.Patients.Count == 0)
                {
                    return 0;
                }
                else
                {

                
                if (HealthyNumber(a) / a.Patients.Count > HealthyNumber(b) / b.Patients.Count)
                {
                    return 1;
                }
                else if (HealthyNumber(a) / a.Patients.Count == HealthyNumber(b) / b.Patients.Count)
                {
                    return 0;
                }
                else
                    return -1;
            }}
            //public static virtual Patient operator +(IDoctor s, Patient p) { return new Patient(); }
        }
        enum PatientState { healthy,underCare,sick}
        class Patient:IPerson
        {
            public string Firstname
            {
                get;
                set;
            }
            public string Lastname
            {
                get;
                set;
            }
            public string SSN
            {
                get;
                set;
            }
            public string Disease;
            public PatientState Recovered;
        }
        class GeneralPractitioner:IPerson,IDoctor
        {
            public string Firstname
            {
                get;
                set;
            }
            public string Lastname
            {
                get;
                set;
            }
            public string SSN
            {
                get;
                set;
            }
            public string Field
            {
                get;
                set;
            }
            public long Salary
            {
                get;
                set;
            }
            public string University//must be separated by space
            {
                get;
                set;
            }
            public int UniLevel
            {
                get;
                set;
            }
            public List<Patient> Patients
            {
                get;
                set;
            }
            public string work() {
                string s = "This General Practitioner works on ";
                s += Field;
                return s;
            }
            public static GeneralPractitioner operator +(GeneralPractitioner a, Patient p)
            {
                if(p.Disease== "Cough"|| p.Disease == "Sneezing"|| p.Disease == "Sore throat")
                {
                a.Patients.Add(p);
                return a;
                }
                else
                {
                    Console.WriteLine("this patient is not proper");
                    return a;
                }
                
            }
            public static bool operator >(GeneralPractitioner a, GeneralPractitioner b)
            {
                if (a.UniLevel < b.UniLevel)
                {
                    return true;
                }
                else
                return false;
            }
            public static bool operator <(GeneralPractitioner a, GeneralPractitioner b)
            {
                if (a.UniLevel > b.UniLevel)
                {
                    return true;
                }
                else
                    return false;
            }
            public string GraduatedFrom()
            {
                return Firstname + " " + Lastname + " is graduated from " + University;
            }
        }
        class Dentist : IPerson, IDoctor
        {
            public string Firstname
            {
                get;
                set;
            }
            public string Lastname
            {
                get;
                set;
            }
            public string SSN
            {
                get;
                set;
            }
            public string Field
            {
                get;
                set;
            }
            public long Salary
            {
                get;
                set;
            }
            public string University//must be separated by space
            {
                get;
                set;
            }
            public int UniLevel
            {
                get;
                set;
            }
            public List<Patient> Patients
            {
                get;
                set;
            }
            public string work()
            {
                string s = "This Dentist works on ";
                s += Field;
                return s;
            }
            public static Patient operator +(Dentist a, Patient p)
            {
                if (p.Disease == "Dental" || p.Disease == "Teeth" || p.Disease == "Toothache") 
                {
                    a.Patients.Add(p);
                    return p;
                }
                else
                {
                    Console.WriteLine("this patient is not proper");
                    return p;
                }
            }
            public static bool operator >(Dentist a, Dentist b)
            {
                if (a.Salary > b.Salary)
                {
                    return true;

                }
                else
                    return false;
                
            }
            public static bool operator <(Dentist a, Dentist b)
            {
                if (a.Salary < b.Salary)
                {
                    return true;

                }
                else
                    return false;
            }
            public string GraduatedFrom()
            {
                return Firstname + " " + Lastname + " is graduated from " + University;
            }
           
        }
        class Surgeon : IPerson, IDoctor
        {
            public string Firstname
            {
                get;
                set;
            }
            public string Lastname
            {
                get;
                set;
            }
            public string SSN
            {
                get;
                set;
            }
            public string Field
            {
                get;
                set;
            }
            public long Salary
            {
                get;
                set;
            }
            public string University//must be separated by space
            {
                get;
                set;
            }
            public int UniLevel
            {
                get;
                set;
            }
            public List<Patient> Patients
            {
                get;
                set;
            }
            public string work()
            {
                string s = "This Surgeon works on ";
                s += Field;
                return s;
            }
            public static Surgeon operator +(Surgeon a, Patient p)
            {
                if (p.Disease == "Kidney" || p.Disease == "Appendix" || p.Disease == "Cancer")
                {
                    a.Patients.Add(p);
                    return a;
                }
                else
                {
                    Console.WriteLine("this patient is not proper");
                    return a;
                }
            }
            public static bool operator >(Surgeon a, Surgeon b)
            {
                if (a.Patients.Count > b.Patients.Count)
                {
                    return true;

                }
                else
                    return false;
            }
            public static bool operator <(Surgeon a, Surgeon b)
            {
                if (a.Patients.Count < b.Patients.Count)
                {
                    return true;

                }
                else
                    return false;
            }
            public string GraduatedFrom()
            {
                return Firstname + " " + Lastname + " is graduated from " + University;
            }
        }
        static int HealthyNumber(IDoctor d)
        {
            int a = 0;
            for(int i = 0; i < d.Patients.Count; i++)
            {
                if (d.Patients[i].Recovered == PatientState.healthy)
                    a++;
            }
            return a;
        }
        class Doctors
        {
            /// <summary>
            /// check if doctor list works well or not
            /// </summary>
            public List<IDoctor> DoctorList = new List<IDoctor>();
            public List<string> ListOfRecoveredPatient()
            {
                List<string> ans = new List<string>();
                for(int i = 0; i < DoctorList.Count; i++)
                {
                    for(int j = 0; j < DoctorList[i].Patients.Count; j++)
                    {
                        if (DoctorList[i].Patients[j].Recovered == PatientState.healthy)
                        {
                            ans.Add(DoctorList[i].Patients[j].Firstname + " " + DoctorList[i].Patients[j].Lastname);
                        }
                    }
                }
                return ans;
            }
            public void SortDoctors()
            {
                for(int i = 0; i < DoctorList.Count; i++)
                {
                    for (int j = i+1; j < DoctorList.Count; j++)
                    {
                        if (0 == DoctorList[i].CompareTo(DoctorList[i], DoctorList[j]))
                        {
                            if (0 > string.Compare(DoctorList[j].Firstname, DoctorList[i].Firstname))
                            {
                                IDoctor temp = DoctorList[j];
                                DoctorList[j] = DoctorList[i];
                                DoctorList[i] = temp;
                            }
                        }
                        else if (-1 == DoctorList[i].CompareTo(DoctorList[i], DoctorList[j]))
                        {
                            IDoctor temp = DoctorList[j];
                            DoctorList[j] = DoctorList[i];
                            DoctorList[i] = temp;
                        }
                    }
                }
            }
        }
        
    }
}
