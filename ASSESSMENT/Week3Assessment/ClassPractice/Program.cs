namespace ClassPractice
{
    internal class Program
    {
        static bool Palindrome(string s)
        {
            int j = s.Length - 1;
            for (int i = 0; i <= j; i++)
            {
                if (s[i] != s[j--]) return false;

            }
            return true;

        }

        static bool Palindrome(string s, int i, int j)
        {
            if (i >= j) return true;
            else if (s[i] != s[j]) return false;
            else return Palindrome(s, i + 1, j - 1);
        }

        static void Pattern(int num)
        {
            int x = 1; int limit = num;
            for (int i = x; i <= limit; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write(j);
                }
                Console.WriteLine();
            }
            for (int i = num-1; i >0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write(j);
                }
                Console.WriteLine();
            }

        }

        static int SecondLargest(int[] arr)
        {
            int max_arr = int.MinValue;int max2_arr = int.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max_arr)
                {
                    {
                        max2_arr = max_arr;
                        max_arr = arr[i];        
                    }
                }
            }
            return max2_arr;
        }

        static void Pattern3()
        {
            for(int j=0;j<5;j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if(i == j || i+j==4) Console.Write(1);
                    else Console.Write(0);
                }
                Console.WriteLine();
            }
        }
        //static void Pattern1(int num, int i)
        //{
        //    if (i >= num) return;
        //    for (int j = 1; j < i; j++)
        //    { Console.Write(j); }
        //    Console.WriteLine();
        //    Pattern1(num, i + 1);
        //}

        static string Upper(string s)
        {
            char[] arr = s.ToCharArray();
            arr[0] = (char)((int)arr[0] - 32);
            string ans = new string(arr);
            return ans;
        }

        
        static string[] Convert(string[] names)
        {
            for(int i=0;i<names.Length;i++)
            {
                string temp  = names[i].Trim().ToLower();
                names[i] = Upper(temp);
            }
            Array.Sort(names);
            return names;
        }

        static string StringCompression(string s)
        {
            string ans = "";
            for(int i=1;i<s.Length;i++)
            {
                int count = 0;
                if (s[i - 1] == s[i])
                {
                    count++;  
                }
                else
                {
                    ans += s[i];
                    ans += count;
                    count = 0;
                }
            }
            return ans;
        }
        static void Main(string[] args)
        {

            string s = "abbaaabbbccccccdhewufweoifjweojoih";
            Console.WriteLine(StringCompression(s));
            //string s = "12121";
            //char[] arr = s.ToCharArray();

            //Array.Reverse(arr);

            //string reverse = arr.ToString();

            ////string reverse = "";

            ////for (int i = 0; i < arr.Length; i++) reverse += arr[i];
            //Console.WriteLine($"{reverse}");

            //Console.WriteLine($"{s==reverse}");
            ////Console.WriteLine(Palindrome(s, 0, s.Length - 1));

            //Pattern(6);
            //Pattern(5);
            //Console.WriteLine();
            //Pattern3();

            //string[] names = { " aaroh", "MAYANK"};
            //names = Convert(names);
            //for(int i=0;i<names.Length;i++)
            //{
            //    Console.WriteLine(names[i]);
            //}

            //int[] arr = { 11, 22, 33, 66, 72, 90 };
            //Array.Sort(arr);
            //Array.Reverse(arr);
            //Console.WriteLine(SecondLargest(arr));




        }
    }
}
