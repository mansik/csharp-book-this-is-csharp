using System;
using System.Data.SqlTypes;
using System.Threading;

// Moniter.Wait(), Moniter.Pulse()로 하는 저수준 동기화
// 멀티 스레드의 성능 향상을 위해서 사용한다.
// Wait() : 스레드를 WaitSleepJoin 상태로 만든다. 갖고 있는 lock를 내려놓은 뒤 Waiting Queue에 입력되고 다른 스레드가 lock을 얻어 작업을 수행한다.
// Pulse() : 수행하던 스레드가 일을 마친 뒤 Pulse()를 호출하면 CLR은 Waiting Queue에서 첫 번째 위치에 있는 스레드를 꺼낸 뒤에 Ready Queue에 입력시킨다.
// Ready Queue에 입력된 스레드는 입력된 차례에 따라 lock을 얻어 Running 상태(다시 작업을 수행한다)에 들어간다.
namespace WaitPulse
{
    class Counter
    {
        const int LOOP_COUNT = 1000;

        readonly object lockThis;
        bool lockedCount = false; // count 변수를 다른 스레드가 사용하고 있는지 판별하기 위해

        private int count; // 각 스레드가 너무 오랬동안 count 변수를 사용하는 것을 막기 위해
        public int Count
        {
            get { return count; }
        }

        public Counter()
        {
            lockThis = new object();
            count = 0;
        }

        public void Increase()
        {
            int loopCount = LOOP_COUNT;

            while(loopCount-- > 0)
            {
                lock (lockThis)
                {
                    // count > 0 이거나 lockedCount가 다른 스레드에 의해 true로 바뀌어 있으면 현재 스레드를 블록시킨다.
                    // 다른 스레드가 Pulse()를 호출해 줄 때까지는 WaitSleepJoin 상태로 남는다.
                    while (count > 0 || lockedCount == true)                        
                        Monitor.Wait(lockThis);

                    //Console.WriteLine("increase");

                    lockedCount = true;
                    count++;
                    lockedCount = false; // lockedCount를 false로 만든 뒤에 

                    Monitor.Pulse(lockThis); // 다른 스레드를 깨운다.
                }
            }
        }

        public void Decrease()
        {
            int loopCount = LOOP_COUNT;

            while(loopCount-- > 0)
            {
                lock(lockThis)
                {
                    while(count < 0 || lockedCount == true)
                        Monitor.Wait(lockThis);                        
                    
                    //Console.WriteLine("decrease");

                    lockedCount = true;
                    count--;
                    lockedCount = false;

                    Monitor.Pulse(lockThis);
                }
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();

            Thread incThread = new Thread(new ThreadStart(counter.Increase));
            Thread decThread = new Thread(() => counter.Decrease()); // ThreadStart ts = new ThradStart(counter.Increase)); 를 람다로 ThreadStart ts = ()=>counter.Increase();

            incThread.Start();
            decThread.Start();

            incThread.Join();
            decThread.Join();

            Console.WriteLine(counter.Count);
        }
    }

}
