using System;
using System.Threading;

// lock 키워드로 스레드 동기화 하기
//
// lock를 얻으면 크리티컬 섹션을 생성한다. 크리티컬 섹션은 한 번에 한 스레드만 사용할 수 있는 코드 영역을 말한다.
// lock 키워드의 매개변수로 사용하는 객체는 참조형이라야 한다.
// 매개변수로 절대 사용하지 말아야 하는 것 3가지: public 키워드 등을 통해 외부 코드에서도 접근할 수 있는 것은 사용 금지
// this : lock(this)
// Type형식 : lock(typeof(SomeClass)), lock(obj.GetType())
// string 형식 : lock("abc")
namespace Synchronize
{
    class Counter
    {
        const int LOOP_COUNT = 1000;

        readonly object thisLock;

        private int count;
        public int Count { get { return count; } }

        public Counter()
        {
            thisLock = new object();
            count = 0;
        }

        public void Increase()
        {
            int loopCount = LOOP_COUNT;

            while(loopCount-- > 0)
            {
                lock (thisLock) // 스레드 동기화, 크리티컬 섹션 생성
                {
                    count++;
                } // 크리티컬 섹션 제거
                Thread.Sleep(1);
            }
        }

        public void Decrease()
        {
            int loopCount = LOOP_COUNT;
            while(loopCount-- > 0)
            {
                lock(thisLock)
                {
                    count--;
                }                
            }
            Thread.Sleep(1);
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter();

            Thread incThread = new Thread(new ThreadStart(counter.Increase));
            Thread decThread = new Thread(new ThreadStart(counter.Decrease));

            incThread.Start(); 
            decThread.Start();

            incThread.Join();
            decThread.Join();

            Console.WriteLine(counter.Count);
        }
    }

}