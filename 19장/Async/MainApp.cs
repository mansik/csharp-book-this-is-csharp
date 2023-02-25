using System;
using System.Threading.Tasks;

// async 한정자와 await 연산자로 만드는 비동기 코드
//
// 1. async 한정자로 메소드나 테스크를 수식하기만 하면 비동기 코드가 만들어진다.
//  다만, async 한정자로 한정하는 메소드는 반환 형식이 Task, Task<TResult>, void여야 한다.
// 2. async로 한정한 Task 또는 Task<TResult>를 반환하는 메소드/테스크/람다식은
//  await 연산자를 만나는 곳에서 호출자에게 제어를 돌려주며 이후 코드는 비동기로 실행되며,
//  await 연산자가 없는 경우 동기로 실행된다.
namespace Async
{
	class MainApp
	{
		async static private void MyMethodAsync(int count)
		{
			Console.WriteLine("C");
			Console.WriteLine("D");

			// 여기까지 동기로 실행
			// await 연산자에서 호출자에게 제어권을 돌려주며 이후의 코드는 비동기로 실행된다.
			await Task.Run(async () =>
			{
				for (int i = 0; i < count; i++)
				{
                    Console.WriteLine($"{i}/{count} ...");
					await Task.Delay(100); 

					// Task.Delay() : 입력받은 시간이 지나면 Task 객체를 반환한다(시간지연). 스레드를 블록시키지 않는다. UI가 사용자에게 잘 응답한다.
                    // Thread.Sleep()와 하는 일은 동일하나, Thread.Sleep()는 스레드 전체를 블록시킨다. UI가 Sleep()이 반환되기까지 사용자에게 응답하지 못한다.
                }                
            });

			Console.WriteLine("G");
			Console.WriteLine("H");
        }

		static void Caller()
		{
            Console.WriteLine("A");
            Console.WriteLine("B");

			MyMethodAsync(3);

            Console.WriteLine("E");
            Console.WriteLine("F");
        }

        static void Main(string[] args)
		{
			Caller();

			Console.ReadLine(); // 프로그램 종료 방지
		}
	}
}