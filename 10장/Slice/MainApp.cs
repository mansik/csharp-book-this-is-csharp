using System;

namespace Slice
{
    class MainApp
    {
        static void PrintArray(System.Array array)
        {
            foreach (var e in array)
            {
                Console.Write(e);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            char[] array = new char['Z' - 'A' + 1];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (char)('A' + i);
            }


            Console.WriteLine("A"); // 문자열 A
            Console.WriteLine('A'); // 문자 A
            Console.WriteLine("A"+1); // A1
            Console.WriteLine('A'+ 1); // ASCII Code 'A' = 65 를 의미함. -> 65+1

            // 끝에서 2번째 
            Console.WriteLine(array[^1]);


            // 0~마지막까지
            PrintArray(array[..]);
            // 5~마지막까지
            PrintArray(array[5..]);           

            // 5~9까지
            Range range_5_10 = 5..10;
            PrintArray(array[range_5_10]);

            // 5~마지막(^0)까지 : 마지막(^1)을 포함시키기 위해서 ^0사용함.
            // 마지막 포함이면 .. 뒤에 아무것도 입력하지 않고 사용하는 것을 추천
            Index last = ^0;
            Range range_5_last = 5..last;
            PrintArray(array[range_5_last]);

            // 마지막(^1)은 포함 안됨
            PrintArray(array[^4..^1]);            
        }
    }
}