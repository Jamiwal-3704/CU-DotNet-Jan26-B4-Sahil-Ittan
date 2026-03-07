namespace PasswordGenerator
{
    public class PassworGeneratorClass
    {
        public string CleanseAndInvert(string input)
        {
            // check if the input is null or less then 6 charcter
            if (string.IsNullOrEmpty(input) || input.Length < 6)
                return "";
            else
            {
                foreach (char c in input)
                {
                    if (!char.IsLetter(c))
                        return "";
                }

                input = input.ToLower();
                string result = "";
                //for(int i = 0; i < input.Count; i++)
                foreach (char ch in input)
                {
                    int tempASCII = (int)ch;
                    if (tempASCII % 2 != 0)
                    {
                        result += ch;
                    }
                }
                Console.WriteLine("just removed the even ASCII VALUES: " + result);

                // REVERSE THE RESULT
                char[] arr = result.ToCharArray();
                Array.Reverse(arr);

                // showing how it become so far after reversing the string
                Console.Write("After reversing string: ");
                for(int i =0; i < arr.Length; i++)
                {
                    Console.Write(arr[i]);
                }
                Console.WriteLine();


                for (int i = 0; i < arr.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        arr[i] = char.ToUpper(arr[i]);
                    }
                }
                return new string(arr);
            }
            return "";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            PassworGeneratorClass stringEncoded = new PassworGeneratorClass();
            string result = stringEncoded.CleanseAndInvert("ABCDE");
            Console.WriteLine("Password generated : " + result);
        }
    }
}


//using System;

//namespace PasswordGenerator
//{
//    public class PassworGeneratorClass
//    {
//        public string CleanseAndInvert(string input)
//        {
//            // Rule 1: null or length < 6
//            if (string.IsNullOrEmpty(input) || input.Length < 6)
//                return "";

//            // Rule 2: only alphabets allowed
//            foreach (char c in input)
//            {
//                if (!char.IsLetter(c))
//                    return "";
//            }

//            // Convert to lowercase
//            input = input.ToLower();

//            // Remove characters with even ASCII values
//            string result = "";
//            foreach (char ch in input)
//            {
//                int ascii = (int)ch;
//                if (ascii % 2 != 0)
//                {
//                    result += ch;
//                }
//            }

//            // Reverse the string
//            char[] arr = result.ToCharArray();
//            Array.Reverse(arr);

//            // Uppercase characters at even index positions
//            for (int i = 0; i < arr.Length; i++)
//            {
//                if (i % 2 == 0)
//                {
//                    arr[i] = char.ToUpper(arr[i]);
//                }
//            }

//            return new string(arr);
//        }
//    }

//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            PassworGeneratorClass stringEncoded = new PassworGeneratorClass();
//            string result = stringEncoded.CleanseAndInvert("ABCDEF");
//            Console.WriteLine("Password generated : " + result);
//        }
//    }
//}
