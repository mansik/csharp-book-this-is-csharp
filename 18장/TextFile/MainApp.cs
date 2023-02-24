using System;
using System.IO;

// 텍스트 파일 처리를 위한 StreamWriter/StreamReader 도우미 클래스 사용
//
// 텍스트 파일에 쓰고 읽기 위한 도우미 클래스이다.
// Stream(FileStream, NetworkStream 등)이 주연이고 StreamWriter/StreamReader는 도우미 클래스이다.
namespace TextFile
{
    class MainApp
    {
        static void Main(string[] args)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream("a.txt", FileMode.Create)))
            {
                sw.WriteLine(int.MaxValue);
                sw.WriteLine("Good morning");
                sw.WriteLine(uint.MaxValue);
                sw.WriteLine("안녕하세요!");
                sw.WriteLine(double.MaxValue);
            }

            using (StreamReader sr = new StreamReader(new FileStream("a.txt", FileMode.Open)))
            {
                Console.WriteLine($"File Size : {sr.BaseStream.Length}");

                while (sr.EndOfStream == false)
                    Console.WriteLine(sr.ReadLine());
            }
        }
    }
}