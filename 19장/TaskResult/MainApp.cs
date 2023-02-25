using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


namespace TaskResult
{
    class MainApp
    {
        // 소수판정
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
            long taskCount = Convert.ToInt32(args[2]);

            // 소수를 List<long>로 반환
            Func<object, List<long>> FindPrimeFunc = (objRange) =>
            {
                long[] range = (long[])objRange;
                List<long> found = new List<long>();

                for (long i = range[0]; i <= range[1]; i++)
                {
                    if (IsPrime(i))
                        found.Add(i);

                    //Debug.WriteLine(i);
                }

                return found;
            };

            Task<List<long>>[] tasks = new Task<List<long>>[taskCount];
            long currentFrom = from;
            long currentTo = to / tasks.Length;
            for (int i = 0; i < taskCount; i++)
            {
                Console.WriteLine($"Task[{i}] : {currentFrom} - {currentTo}");

                tasks[i] = new Task<List<long>>(FindPrimeFunc, new long[] { currentFrom, currentTo });

                currentFrom = currentTo + 1;

                if (i == tasks.Length - 2) // 마지막 계산의 범위
                    currentTo = to;
                else
                    currentTo = currentTo + (to / tasks.Length);
            }

            Console.WriteLine("Please press enter to start...");
            Console.ReadLine();
            Console.WriteLine("Started....");

            DateTime startTime = DateTime.Now;

            foreach (Task<List<long>> task in tasks)
                task.Start();

            List<long> total = new List<long>();
            foreach (Task<List<long>> task in tasks)
            {
                task.Wait();
                total.AddRange(task.Result.ToArray()); // ToArray() 없어도 됨
            }
            DateTime endTime = DateTime.Now;

            TimeSpan elapsed = endTime - startTime;

            Console.WriteLine($"Prime number count between {from} and {to} : {total.Count}");
            Console.WriteLine($"Elsped time : {elapsed}");

            foreach (long i in total)
                Console.Write($"{i} ");
            Console.WriteLine();
        }
    }
}

