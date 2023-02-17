using System;

namespace FuncTest
{
	class MainApp
	{
		static void Main(string[] args)
		{
            // Func delegate(대리자): 반환값이 있는 delegate, .NET에 미리 선언되어 있다.

            // 매개변수가 없는 Func 대리자(delegate)
            // public delegate TResult Func<out TResult>();  TResult 는 반환 형식이며, 형식 매개변수인 <out TResult>는 반환값 형식을, ()는 매개변수가 없음을 의미한다.
            Func<int> func1 = () => 10;
			Console.WriteLine($"func1() : {func1()}");

            // 매개변수가 1개 인 Func delegate
            // public delegate TResult Func<in T, out TResult>(T arg); TResult 는 반환 형식이며, out TResult는 반환값 형식을 의미한다.
            Func<int, int> func2 = (x) => x * 2;
            Console.WriteLine($"func2() : {func2(4)}");


            // 매개변수가 2개 인 Func delegate
            // public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2); 
            Func<double, double, double> func3 = (x, y) => x / y;
            Console.WriteLine($"func3() : {func3(22, 7)}");
        }
    }
}