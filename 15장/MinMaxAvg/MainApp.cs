using System;
using System.Linq;
using System.Net.Http.Headers;

namespace MinMaxAvg
{
    class Profile
    {
        public string Name { get; set; }
        public int Height { get; set; }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Profile[] arrProfile =
            {
                new Profile { Name = "정우성", Height = 186 },
                new Profile { Name = "김태희", Height = 158 },
                new Profile { Name = "고현경", Height = 172 },
                new Profile { Name = "이문세", Height = 178 },
                new Profile { Name = "하하", Height = 171 }
            };

            // Linq와 표준 연산자(.Max(), .Min(), .Average()) 같이 사용함.
            var heightStat = from profile in arrProfile
                             group profile by profile.Height < 175 into g
                             select new
                             {
                                 Group = g.Key == true ? "175미만" : "175이상",
                                 Count = g.Count(), // 표준 연산자
                                 Max = g.Max(profile => profile.Height), // 표준 연산자 .Max()
                                 Min = g.Min(profile => profile.Height), // 표준 연산자
                                 Average = g.Average(profile => profile.Height) // 표준 연산자
                             };

            foreach (var stat in heightStat)
            {
                Console.Write($"{stat.Group} - Count:{stat.Count}, Max:{stat.Max}, ");
                Console.WriteLine($"Min:{stat.Min}, Average:{stat.Average}");
            }
        }
    }
}