using System;
using System.Collections;

namespace Enumerable
{
    class MyList : IEnumerable, IEnumerator
    {
        private int[] array;
        int position = -1;

        public MyList()
        {
            array = new int[3];
        }

        // indexer(인덱서) 구현
        public int this[int index]
        {
            get { return array[index]; }
            set
            {
                if (index >= array.Length)
                {
                    Array.Resize<int>(ref array, index + 1);
                    Console.WriteLine($"Array Resized : {array.Length}");
                }
                array[index] = value;
            }
        }

        // IEnumerator 멤버
        public object Current
        {
            get { return array[position]; }
        }

        // IEnumerable 멤버
        public IEnumerator GetEnumerator()
        {
            return this; // 클래스의 객체를 반환
        }

        // IEnumerator 멤버
        public bool MoveNext()
        {
            if (position == array.Length - 1)
            {
                Reset();
                return false;
            }

            position++;
            return (position < array.Length);
        }

        // IEnumerator 멤버
        public void Reset()
        {
            position = -1;
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            MyList list = new MyList();
            for (int i = 0; i < 5; i++)
            {
                list[i] = i;
            }

            foreach (int e in list)
            {
                Console.WriteLine(e);
            }

        }
    }
}