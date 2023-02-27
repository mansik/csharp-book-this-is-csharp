using System;

// WinForm 클래스를 이용한 윈도우 생성 절차
// 1. System.Windows.Forms.Form 클래스에서 상속 받도록 선언
// 2. System.Windows.Forms.Application.Run() 메소드에 1번의 인스턴스를 인수로 넘겨 호출한다.
// 프로젝트 파일(SimpleWindow)에 추가 <UseWindowsForms>true</UseWindowsForms>
namespace SimpleWindow
{    
    class MainApp : System.Windows.Forms.Form
	{
		static void Main(string[] args)
		{
			System.Windows.Forms.Application.Run(new MainApp());
		}
	}
}
