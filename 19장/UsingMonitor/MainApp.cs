using System;
using System.Threading;

// Moniter 클래스로 스레드 동기화하기 
// Moniter.Enter() : 크리티컬 섹션을 만든다. lock 키워드의 {에 해당
// Moiniter.Exit() : 크리티컬 섹션을 제거한다. lock 키워드의 }에 해당. 반드시 try 문의 finally문 안에서 사용해야만 안전하게 크리티컬 섹션이 제거된다.
//
// 크리티컬 섹션은 한 번에 한 스레드만 사용할 수 있는 코드 영역을 말한다.
// 사용법 : 
// Moniter.Enter();
// try
// {
// }
// finally
// { Moiniter.Exit(); } 

namespace UsingMonitor
{
    class Counter
    {
        const int LOOP_COUNT = 1000;

        readonly object thisLock;

        private int count;

        public int Count
        {
            get { return count; }
        }
        public Counter()
        {
            thisLock = new object();
            count = 0;
        }

        public void Increase()
        {
            int loopCount = LOOP_COUNT;
            while (loopCount-- > 0)
            {
                Monitor.Enter(thisLock); // 스레드 동기화, 크리티컬 섹선 생성
                try
                {
                    count++;
                }
                finally
                {
                    Monitor.Exit(thisLock); // 크리티컬 섹션 제거
                }
                Thread.Sleep(1);
            }
        }
        public void Decrease()
        {
            int loopCount = LOOP_COUNT;
            while (loopCount-- > 0)
            {
                Monitor.Enter(thisLock);
                try
                {
                    count--;
                }
                finally
                {
                    Monitor.Exit(thisLock);
                }
                Thread.Sleep(1);
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Counter counter = new   Counter();

            Thread incThread = new Thread(new ThreadStart(counter.Increase));
            Thread decThread = new Thread(new ThreadStart(counter.Decrease));

            incThread.Start();
            decThread.Start();

            incThread.Join();
            incThread.Join();

            Console.WriteLine(counter.Count);
        }
    }
}