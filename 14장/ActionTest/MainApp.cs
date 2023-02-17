using System;

namespace ActionTest
{
    class MainApp
    {
        static void Main(string[] args)
        {
            // Action delegate(대리자)
            //
            // 반환값이 없다.
            // 일련의 작업을 수행하는 것이 목적이다.
            // .NET에 미리 선언되어 있다.


            // 매개변수가 없는 Action delegate
            // public delegate void Action(); 
            // 아래 Action에서 F12 키를 누르면 선언으로 이동한다.
            Action act1 = () => Console.WriteLine("Action()");
            act1();


            // 매개변수 1개인 Action delegate
            // public delegate void Action<in T>(T arg); 
            int result = 0;
            Action<int> act2 = (x) => result = x * x;

            act2(3);
            Console.WriteLine($"result : {result}");


            // 매개변수 2개인 Action delegate
            Action<double, double> act3 = (x, y) =>
            {
                double pi = x / y;
                Console.WriteLine($"Action<T1, T2>({x}, {y}) : {pi}");
            };

            act3(22.0, 7.0);
        }
    }
}