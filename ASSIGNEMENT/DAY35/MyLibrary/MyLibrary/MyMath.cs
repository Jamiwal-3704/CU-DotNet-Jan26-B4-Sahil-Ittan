namespace MyLibrary
{
    public class MyMath
    {
        public int GetSum(params int[] values)
        {
            int sum = 0;
            foreach(int n in values)
            {
                sum += n;
            }
            return sum;
        }
    }
}
