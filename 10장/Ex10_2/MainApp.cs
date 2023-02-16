using System;

namespace Ex10_2
{
    class MainApp
    {
        static void Main(string[] args)
        {

            int[,] a = new int[2, 2] { { 3, 2 }, { 1, 4 } };
            int[,] b = new int[2, 2] { { 9, 2 }, { 1, 7 } };
            int[,] ab = new int[2, 2];

            ab[0, 0] = (a[0, 0] * b[0, 0]) + (a[0, 1] * b[1, 0]);
            ab[0, 1] = (a[0, 0] * b[0, 1]) + (a[0, 1] * b[1, 1]);
            ab[1, 0] = (a[1, 0] * b[0, 0]) + (a[1, 1] * b[1, 0]);
            ab[1, 1] = (a[1, 0] * b[0, 1]) + (a[1, 1] * b[1, 1]);
            
            Console.WriteLine($"AxB[0, 0] = {ab[0, 0]}");
            Console.WriteLine($"AxB[0, 1] = {ab[0, 1]}");
            Console.WriteLine($"AxB[1, 0] = {ab[1, 0]}");
            Console.WriteLine($"AxB[1, 1] = {ab[1, 1]}");


        }
    }

}