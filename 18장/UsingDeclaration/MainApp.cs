using System;
using System.IO;
using FS = System.IO.FileStream; // using 별칭 지시문


// using 선언 : Close() 메소드가 필요 없다.
namespace UsingDeclaration
{
    class MainApp
    {
        static void Main(string[] args)
        {
            long someValue = 0x123456789ABCDEF0; // 16 진수 16자리
            Console.WriteLine("{0, -1} : 0x{1:X16}", "Original Data", someValue);

            // using 사용법 1
            // using ( Dispose() 메소드를 구현하는 객체의 인스턴스 = new 생성자)
            // {
            //    인스턴스 사용
            //    // 인스턴스.Close(); 없이 블록의 마지막 } 에서 IDispose() 메소드가 호출
            // }

            using (Stream outStream = new FS("a.dat", FileMode.Create))
            {
                byte[] wBytes = BitConverter.GetBytes(someValue);

                Console.Write("{0:-13} : ", "Byte array"); // "Original Data 의 길이가 13이고 왼쪽정렬

                foreach (byte b in wBytes)
                    Console.Write("{0:X2} ", b);
                Console.WriteLine();

                outStream.Write(wBytes, 0, wBytes.Length);
            }

            // using 사용법 2
            // {
            //   using Stream inStream = new FileStream("a.dat", FileMode.Open);
            //   ...inStream 을 사용
            //   // inStream.Close(); 없이 블록의 마지막 } 에서 IDispose() 메소드가 호출
            // }
            using Stream inStream = new FileStream("a.dat", FileMode.Open);
            byte[] rBytes = new byte[8]; // 16진수 2자리(0xFF)는 1 Byte, 16자리는 8 Byte

            int i = 0;
            while (inStream.Position < inStream.Length)
                rBytes[i++] = (byte)inStream.ReadByte();

            long readValue = BitConverter.ToInt64(rBytes, 0);

            Console.WriteLine("{0, -13} : 0x{1:X16}", "Read Data", readValue);
        }
    }

}

