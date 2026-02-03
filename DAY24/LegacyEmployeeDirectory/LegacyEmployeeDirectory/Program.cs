using System.Collections;

namespace LegacyEmployeeDirectory
{
    static class uniqueEmployee
    {
        public static void UniqueEmployeeDirectory(int id, string name,Hashtable employeeDictionary)
        {
            if (employeeDictionary != null)
            {
                if(!employeeDictionary.ContainsKey(id))
                {
                    Console.WriteLine("Employee id not found, adding to the directory.");
                    employeeDictionary.Add(id, name);
                }
                else
                {
                    Console.WriteLine("Employee id already exists in the directory.");
                }
            }
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            Hashtable employeeDictionary = new Hashtable()
            {
                {101,"Alice"},
                {102,"Bob"},
                {103,"Charlie"},
                {104,"Diana"},
            };

            // check if an employee id exist or not if not then add it to the dictionary
            uniqueEmployee.UniqueEmployeeDirectory(105,"Eve",employeeDictionary);
            Console.WriteLine("updated Dictionary");
            Console.WriteLine();

            foreach(var item in employeeDictionary.Keys)
            {
                Console.WriteLine($"Employee ID: {item}, Name: {employeeDictionary[item]}");
            }

            // delete the employee with id 103
            employeeDictionary.Remove(103);
            Console.WriteLine();
            Console.WriteLine("103 employee removed from the directory.");
            Console.WriteLine("Printing the updated employee directory:");
            Console.WriteLine();

            foreach (var item in employeeDictionary.Keys)
            {
                Console.WriteLine($"Employee ID: {item}, Name: {employeeDictionary[item]}");
            }


        }
    }
}
