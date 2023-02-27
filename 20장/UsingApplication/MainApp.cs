using System;
using System.Windows.Forms;

// Application 클래스 역할
// 1. 윈도우 응용 프로그램을 시작하고 종료시키는 메소드를 제공 : Application.Run(), Application.Exit()
// 2. 윈도우 메시지 처리
namespace UsingApplication
{
    class MainApp : Form
    {
        static void Main(string[] args)
        {
            MainApp form = new MainApp();

            form.Click += new EventHandler(
                (sender, eventArgs) =>
                {
                    Console.WriteLine("Closing Window...");
                    Application.Exit();
                    // Application.Exit();는 응용프로그램이 바로 종료되지 않고,
                    // 응용 프로그램이 같고 있는 모든 윈도우를 닫은 뒤 Run() 메소드가 반환되도록 하는 것이다.
                    // 그러므로, Application.Run() 메소드 뒤에 자원을 정리하는 코드를 넣어두면 된다.
                });

            Console.WriteLine("Starting Window Application...");
            Application.Run(form);

            //  Application.Exit() 호출이 되면 Run() 뒤의 코드를 처리한다. 여기에 자원 정리코드를 사용하면 된다.
            Console.WriteLine("Exiting Window Application...");
        }
    }
}
