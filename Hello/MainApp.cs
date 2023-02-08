using System; // System 네임스페이스안에 있는 클래스를 사용하겠다고 컴파일러에게 알리는 역할
using static System.Console; // 어떤 데이터 형식(ex. 클래스)의 정적 멤버를 데이터 형식의 이름을 명시하지 않고 참조하겠다고 선언하는 기능

namespace Hello // 성격이나 하는 일이 비슷한 클래스, 구조체, 인터페이스, 대리자, 열거 형식등을 하나의 이름 아래 묶는 일을 한다.
{
    class MainApp
    {
        // 프로그램 실행이 시작되는 곳, entry point
        static void Main(string[] args) // Main Method, static: 한정자, void: 반환 형식, Main: 메소드 명, args: 매개변수, static 키워드: 프로그램 구동시 메모리에 할당된다. 
        { 
            if (args.Length == 0) 
            {
                Console.WriteLine("usage: Hello.exe <name>"); 
                return; // 원래 메소드의 호출자에게 메소드의 실행 결과를 돌려주는 역할
            }
            WriteLine("Hello, {0}!", args[0]); // using static System.Console;로 인해 Console 생략함. WriteLine는 Console 클래스의  정적 멤버이다.
        }
    }
}
