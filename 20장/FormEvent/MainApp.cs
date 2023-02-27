using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FormEvent
{
	class MainApp:Form
	{
		public void MyMouseHandler(object sender, MouseEventArgs e)
		{
            Console.WriteLine($"Sender : {((Form)sender).Text}"); // sender은 이벤트가 발생한 객체를 가리킨다.
			Console.WriteLine($"X:{e.X}, Y:{e.Y}"); // 마우스 이벤트가 발생한 폼 또는 컨트롤 상의 좌표
            Console.WriteLine($"Button:{e.Button}, Clicks:{e.Clicks}"); // 마우스 버튼, 클릭 횟수
            Console.WriteLine();
        }

        // 이벤트 핸들러 등록 방법 2가지
        // 1. 생성자에서 this.MouseDown += ...;
        // 2. 객체 생성후 객체 인스턴스에 등록. MainApp form = new MainApp(); form.MouseDown += ...;
        public MainApp(string title)
        {
            this.Text = title;
            this.MouseDown += new MouseEventHandler(MyMouseHandler); // 생성자에서 이벤트 핸들러를 이벤트에 등록
        }
        static void Main(string[] args)
		{
            Application.Run(new MainApp("Mouse Event Test"));
		}

	}
}