using System;
using System.Threading;

namespace AbortingThread
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
                while(count>0)
                {
                    Console.WriteLine($"{count--} left");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Count : 0");
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine(e); 
                Thread.ResetAbort(); //.net 5.0 이상에서 지원하지 않음
            }
            finally
            {
                Console.WriteLine("Clearing resource...");
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            SideTask task = new SideTask(100);
            Thread t1 = new Thread(new ThreadStart(task.KeepAlive));
            t1.IsBackground = false;

            Console.WriteLine("Starting thread...");
            t1.Start();

            Thread.Sleep(100);            

            Console.WriteLine("Aborting Thread...");
            t1.Abort(); // .Net 5.0이상에서  PlatformNotSupportedException 예외를 발생시킴

            Console.WriteLine("Waiting until thread stops...");
            t1.Join();

            Console.WriteLine("Finished");


        }
    }
}
