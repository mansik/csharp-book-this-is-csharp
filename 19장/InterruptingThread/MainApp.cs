using System;
using System.Security.Permissions;
using System.Threading;


// Thread를 임의로 종료 : Interrupt()
namespace IntertuptingThread
{
    class SideTask
    {
        int count;

        public SideTask(int count)
        {
            this.count = count;
        }

        public void KeepAlive()
        {
            try
            {
                Console.WriteLine("Running thread isn't gonna be interrupted");
                Thread.SpinWait(100000000); // Thread 가 Running 상태를 유지하게 함

                while (count > 0)
                {
                    Console.WriteLine($"{count--} left");

                    Console.WriteLine("Entering into WaitJoinSleep State...");
                    Thread.Sleep(10); // 여기서 WaitSleepJoin 상태로 들어가고, Interrupt가 발생한다.
                }
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Clearing resource...");
            }
        }

        class MainApp
        {
            static void Main(string[] args)
            {
                SideTask task = new SideTask(100);
                Thread t1 = new Thread(new ThreadStart(task.KeepAlive));
                t1.IsBackground = false;

                Console.WriteLine("Starting Thread...");
                t1.Start();

                Thread.Sleep(100);

                //스레드가 WaitJoinSleep 상태에 들어갔을 때 ThreadInterruptedException 예외를 발생시켜 스레드를 중지시킨다.
                Console.WriteLine("Interrupting Thread...");
                t1.Interrupt(); // 스레드 임의 종료..

                Console.WriteLine("Waiting until Thread Stops...");
                t1.Join();

                Console.WriteLine("Finished");
            }
        }
    }
}