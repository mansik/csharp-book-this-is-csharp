using System;
using System.Windows.Forms;

// 폼에 컨트롤 추가
namespace FromAndControl
{
    class MainApp : Form
    {
        static void Main(string[] args)
        {
            // 1. 컨트롤의 인스턴스 생성
            Button button = new Button();

            // 2. 컨트롤의 프로퍼티에 값 지정
            button.Text = "Click Me!";
            button.Left = 100;
            button.Top = 50;

            // 3. 컨트롤의 이벤트에 이벤트 처리기 등록
            button.Click += new EventHandler((object sender, EventArgs e) =>
            {
                MessageBox.Show("딸깍!");

            });

            MainApp form  = new MainApp();
            form.Text = "Form & Control";
            form.Height = 200;

            // 4. 폼에 컨트롤 추가
            form.Controls.Add(button);

            Application.Run(form);
        }
    }
}