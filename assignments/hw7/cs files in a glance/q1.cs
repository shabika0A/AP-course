using System;

namespace q1
{
    class Program
    {
        interface IPersonality
        {
            public string Name
            {
                get;
                set;
            }
            public int Score
            {
                get;
                set;
            }
            public string Personality();
            
        }
        class Bear : IPersonality
        {
            public string Name
            {
                get;
                set;
            }
            public int Score
            {
                get;
                set;
            }
            
            public string Personality()
            {
                return Name+" is yellow bear.\nHe loves honey.\nHis name’s rate is " + Score;
            }
            public Bear(string n,int s)
            {
                Name = n;
                Score = s;
            }
            
        }
        class Pig : IPersonality
        {
            public string Name
            {
                get;
                set;
            }
            public int Score
            {
                get;
                set;
            }
            public string Personality()
            {
                return Name+" is a pink cute pig.\nShe is a bit timid.\nHer name’s rate is " + Score;
            }
            public Pig(string n, int s)
            {
                Name = n;
                Score = s;
            }
        }
        class Tiger : IPersonality
        {
            public string Name
            {
                get;
                set;
            }
            public int Score
            {
                get;
                set;
            }
            public string Personality()
            {
 
                return Name +" is playful.\nHe is always smiling.\nHis name’s rate is " + Score;
            }
            public Tiger(string n, int s)
            {
                Name = n;
                Score = s;
            }
        }
        class kangaroo : IPersonality
        {
            public string Name
            {
                get;
                set;
            }
            public int Score
            {
                get;
                set;
            }
            public string Personality()
            {
  
                return Name + " is playful.\nHe is Tigger's friend.\nHis name’s rate is " + Score;
            }
            public kangaroo(string n, int s)
            {
                Name = n;
                Score = s;
            }
        }
        class donkey : IPersonality
        {
            public string Name
            {
                get;
                set;
            }
            public int Score
            {
                get;
                set;
            }
            public string Personality()
            {
              
                return Name + " is tired.\nHe is always complaining.\nHis name’s rate is " + Score;
            }
            public donkey(string n, int s)
            {
                Name = n;
                Score = s;
            }
        }
        class Friend<T>where T:IPersonality
        {
            T f;
            public Friend(T input)
            {
                f = input;
            }
            public static implicit operator Friend<T>(T input)
            {
                return new Friend<T>(input);
            }
            public string Personality()
            {
                return f.Personality();
            }

        }
        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Friend<Bear> bear = new Bear("Pooh", 5);
            Friend<Tiger> tiger = new Tiger("Tiger", 3);
            Friend<Pig> piglet = new Pig("Piglet",4 );
            Friend<kangaroo> roo = new kangaroo("Roo", 2);
            Friend<donkey> p = new donkey("Eeyore", 1);
            Console.WriteLine(bear.Personality());
            Console.WriteLine(tiger.Personality());
            Console.WriteLine(piglet.Personality());
            Console.WriteLine(roo.Personality());
            Console.WriteLine(p.Personality());

        }
    }
}
