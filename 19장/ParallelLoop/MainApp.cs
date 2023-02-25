using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoop
{
    class MainApp
    {
        static bool IsPrime(long number)
        {
            if (number < 2)
                return false;

            if (number % 2 == 0 && number != 2)
                return false;

            for (long i = 2; i < number; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            long from = Convert.ToInt64(args[0]);
            long to = Convert.ToInt64(args[1]);

            Console.WriteLine("Please press enter to start...");
            Console.ReadLine();
            Console.WriteLine("Started...");

            DateTime startTime = DateTime.Now;
            List<long> total = new List<long>();

            Parallel.For(from, to, (long i) =>
            {
                if (IsPrime(i))
                    lock (total) // 스레드 동기화
                        total.Add(i);
            });

            DateTime endTime = DateTime.Now;

            TimeSpan elasped = endTime - startTime; // TimeSpan은 시간차이를 인스턴스화 한다.

            Console.WriteLine($"Prime number count between {from} and {to} : {total.Count}");
            Console.WriteLine($"Elasped time : {elasped}");

            foreach (long list in total)            
                Console.Write($"{list} ");

            Console.WriteLine();
        }
    }
}