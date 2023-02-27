using System;
using System.Windows.Forms;

// Application 클래스의 기능인 메시지 필터링
// Application.AddMessageFilter(new MessageFilter()) : 걸러내고 싶은 메시지(MessageFilter에서 true를 반환한 메시지)를 등록
namespace MessageFilter
{
    class MessageFilter : IMessageFilter
    {        
        // PreFilterMessage() 구현시
        // 입력받은 메시지를 처리했으니 응용 프로그램은 관심을 가질 필요가 없다는 true를 반환하거나
        // 메시지를 건더리지 않았으니 응용프로그램이 처리 해야 한다고 false를 반환하면 된다.
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x0F || m.Msg == 0xA0 || m.Msg == 0x200 || m.Msg == 0x113) // WM_PAINT(0x0F), WM_MOUSEMOVE(0x200), WM_TIMER(0x113)
                return false; // AddMessageFilter()에 등록이 안되며 응용 프로그램에서 처리해야됨

            Console.WriteLine($"{m.ToString()} : {m.Msg}");

            if (m.Msg == 0x201) // WM_LBUTTONDOWN
                Application.Exit();

            return true; // AddMessageFilter()에 등록되며 응용 프로그램에서 걸러짐
        }
    }

    class MainApp : Form
    {
        static void Main(string[] args)
        {
            // 메시지 필터 등록: 등록된 메시지는 응용 프로그램에서 처리 안함
            Application.AddMessageFilter(new MessageFilter());

            Application.Run(new MainApp());

        }
    }
}