using System;
using System.Linq.Expressions;

// Lambda식을 이용해서 Expression Tree(식 트리)를 만드는 예제
namespace ExpressionTreeViaLambda
{
	class MainApp
	{
		static void Main(string[] args)
		{
			// lambda expression
			Expression<Func<int, int, int>> expression = (a, b) => 1 * 2 + (a - b);

			// lambda compile
			Func<int, int, int> func = expression.Compile();

			// x = 7, y = 8
			Console.WriteLine($"1*2+({7} - {8}) = {func(7, 8)}");
		}
	}
}