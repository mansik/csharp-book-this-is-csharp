using System;

namespace UsingDictionary
{ 
    class MainApp
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary["하나"] = "one";
            dictionary["둘"] = "two";
            dictionary["셋"] = "three";
            dictionary["넷"] = "four";
            dictionary["다섯"] = "five";

            Console.WriteLine(dictionary["하나"]);
            Console.WriteLine(dictionary["둘"]);
            Console.WriteLine(dictionary["셋"]);
            Console.WriteLine(dictionary["넷"]);
            Console.WriteLine(dictionary["다섯"]);
        }
    }
}
