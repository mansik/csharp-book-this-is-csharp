// 해당 예제: https://learn.microsoft.com/ko-kr/visualstudio/get-started/csharp/visual-studio-ide?view=vs-2022

// See http://aka.ms/new-console-template for more information

Console.WriteLine("\nWhat is your name?");
var username = Console.ReadLine();
Console.WriteLine($"\nHello {username}!");
// 디버그 실습
// 1. Console.WriteLine($"\nHello {username}!"); 에서 F9 또는 맨 왼쪽 여백을 클릭하여 중단점 설정
// 2. F5로 디버깅 시작
// 3. 콘솔 창이 나타나면 이름 입력
// 4. 중단점 설정된 줄의 username 변수 위에 마우스를 가져가면 해당값 볼 수 있다.
//    또는 username을 마우스 오른쪽 단추로 클릭해서 '조사식 추가'를 선택하여 조사식 창에서 변수의 값을 확인할 수 있다.
// 5. F5를 눌러 앱 실행을 완료 한다.

//DateTime now = DateTime.Now;
//int dayOfYear = now.DayOfYear;

//리펙토링 실습: 위의 코드에서 now를 선택 후 좌측 숫자 옆의 스크루드라이버 아이콘에서 '인라인 임시 변수'를 선택하여 코드를 리펙터링 하였다.
int dayOfYear = DateTime.Now.DayOfYear;

Console.Write("Day of year: ");
Console.WriteLine(dayOfYear);
