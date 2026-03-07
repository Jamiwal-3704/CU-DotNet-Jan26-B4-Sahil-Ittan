namespace ConsoleApp1
{
    internal class Program
    {

        //static void Func(int n)
        //{
        //    if(n < 1)
        //    {
        //        return;
        //    }
        //    Func(n - 1);
        //    Console.WriteLine(n);
        //}

        static int factorial(int n,int level)
        {
            string indent = new string (' ', level * 4);
            Console.WriteLine (indent + n);
            if(n == 1)
            {
                return 1;
            }
            int result = n * factorial(n - 1, level + 1);
            Console.WriteLine(new string(' ', level * 4) + result);

            return result;
        }

        static int RFact2(int n)
        {
            int d = 5;
            Console.WriteLine (new string(' ' ,d - n) + n);

            if (n == 0 || n == 1) return 1;
            int result = n * RFact2 (n-1);

            Console.WriteLine(new string(' ',d - n) + result);
            return result;
        }



        static void Main(string[] args)
        {
            //Func(5);
            //factorial(5,0);
            RFact2(5);
        }
    }
}
