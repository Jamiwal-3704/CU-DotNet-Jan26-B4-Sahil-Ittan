namespace Const_ReadOnly
{

    public class MyClass
    {
        public const int MyConstValue = 10;
        public readonly int MyReadOnlyValue;

        public MyClass()
        {
            MyReadOnlyValue = 30;
        }

        public int changeMyReadOnlyValue()
        {
            return MyReadOnlyValue + 20;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            MyClass newClass = new MyClass();
            newClass.changeMyReadOnlyValue();
        }
    }
}
