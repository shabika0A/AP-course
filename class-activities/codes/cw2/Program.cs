using System;

namespace cw2
{
    class PhotoAlbum
    {
        int NumberOfPages;
        int CurrentPage;
        public PhotoAlbum(int n,int c)
        {
            NumberOfPages = n;
            CurrentPage = c;
        }
        public void RipApartSomePages(int n)
        {
            if(NumberOfPages-CurrentPage >= n)
            {
                Console.Write("pages to rip apart: {0} ",n);
                Console.WriteLine(" possible ");
                NumberOfPages -= n;
                Console.WriteLine("new number of pages:{0}", NumberOfPages);
            }
            else
            {
                Console.WriteLine("impossible! ");
            }
        }
        public void TurnNextPage()
        {
            if (CurrentPage < NumberOfPages) {
                Console.Write("current page was :{0} " , CurrentPage);
            CurrentPage++;
                Console.WriteLine("now current page is :" + CurrentPage);
            }else if (CurrentPage == NumberOfPages)
            {
                Console.WriteLine("this is the last page!");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] numbers = Console.ReadLine().Split('-');
            int n = int.Parse(numbers[0]);
            int c = int.Parse(numbers[1]);
            PhotoAlbum album1=new PhotoAlbum(n,c);
            album1.TurnNextPage();
            Random rnd = new Random();
            album1.RipApartSomePages(rnd.Next(1,25));
            album1.TurnNextPage();
            Console.WriteLine("enter numbers for second album: ");
            numbers = Console.ReadLine().Split('-');
            n = int.Parse(numbers[0]);
            c = int.Parse(numbers[1]);
            PhotoAlbum album2 = new PhotoAlbum(n, c);
            album2.TurnNextPage();
            album2.RipApartSomePages(rnd.Next(1, 25));
            album2.TurnNextPage();
        }
    }
}
