using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

// System.Threading.Tasks.Task 클래스를 사용한 비동기 처리
// 동기 코드 : 검사가 검으로 공격할 때처럼 동작한다. 검사가 검으로 상대를 찌른 뒤에 다시 뽑아야 칼을 쓸 수 있는 것처럼, 
//             메소드를 호출한 뒤에 이 메소드의 실행이 완전히 종료되어야만 다음 메소드를 호출할 수 있다. 일반적인 함수호출의 경우임.
// 비동기 코드 : 궁수가 화살을 쏘는 것 처럼 메소드를 호출한 뒤에 메소드의 종료를 기다리지 않고 바로 다음 코드를 실행한다.
// Task 클래스는 인스턴스 생성시 Action대리자(반환형이 없는 메소드, 익명메소드, 무명함수)를 인수로 받는다.
// Task 실행방법 4가지 (4번은 동기 실행):
// 1. t1.Start() 호출하여 생성자로 넘겨받은 Action대리자를 비동기로 실행
//   Action action = (object obj) => { ... };
//   Task t1 = new Task(action, obj인자값);
//   t1.Start();
//   t1.Wait(); // 호출이 완료될때까지 기다림
// 2.Task.Factory.StartNew(action, obj인자값) 로 Task 생성과 실행
//   Task t2 = Task.Factory.StartNew(S)
//   t2.Wait();
// 3. Task.Run(); 로 Task 생성과 실행
//   var t3 = Task.Run(() => { ... });
//   t3.Wait();
// 4. t4.RunSynchronously(); 로 Task 동기로 실행
//   Task t4 = new Task(action, obj인자값);
//   t4 = RunSynchronously();
//   t4.Wait();
namespace UsingTask
{
    class MainApp
    {
        static void Main(string[] args)
        {
            string srcFile = args[0];

            Action<object> FileCopyAction = (object state) =>
            {
                string[] paths = (string[])state;
                File.Copy(paths[0], paths[1]);

                Console.WriteLine($"TaskID:{Task.CurrentId}, ThreadID:{Thread.CurrentThread.ManagedThreadId}, {paths[0]} was copied to {paths[1]}");
            };

            //  Create a task but do not start it.
            Task t1 = new Task(FileCopyAction, new string[] { srcFile, srcFile + ".copy1" });

            // Construct a started task
            Task t2 = Task.Factory.StartNew(FileCopyAction, new string[] { srcFile, srcFile + ".copy2" });
            
            // Launch t1
            t1.Start();
            
            // Construct a started task using Task.Run.
            Task t3 = Task.Run(() =>
            {
                FileCopyAction(new string[] { srcFile, srcFile + "copy3" });
            });
            
            // Construct an unstarted task
            Task t4 = new Task(FileCopyAction, new string[] { srcFile, srcFile + "copy4" });

            // Run it synchronously, 동기 실행, 실행이 끝나야 반환된다.
            t4.RunSynchronously();

            // 실행 순서는 랜덤으로 정해짐
            // 호출이 완료될때까지 기다림
            t1.Wait();
            t2.Wait();            
            t3.Wait();                        
            t4.Wait();
        }
    }
}