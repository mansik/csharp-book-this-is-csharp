using System;
using System.IO;

// WriteByte()를 이용해서 순차적으로 파일내의 위치를 옮겨가며 데이터를 기록하는 순차적 접근 방식과
// Seek() 메소드로 임의의 주소로 단번에 점프하는 임의 접근 방식 예제.
namespace SeqNRand
{
	class MainApp
	{
		static void Main(string[] args)
		{
			Stream outStream = new FileStream("a.dat", FileMode.Create);
			Console.WriteLine($"Position : {outStream.Position}");

			// 16진수 0x01 = 0000 0001 =>  4bit + 4 bit = 8 bit = 1 byte
			outStream.WriteByte(0x01); // 16진수는 2자리는 1 byte를 차지한다.
			Console.WriteLine($"Position : {outStream.Position}");
						
			outStream.WriteByte(0x02);
            Console.WriteLine($"Position : {outStream.Position}");

            outStream.WriteByte(0x03);
            Console.WriteLine($"Position : {outStream.Position}");

			// Position을 현재위치(SeekOrigin.Current)에서 5byte 뒤로 이동
			outStream.Seek(5, SeekOrigin.Current);
            Console.WriteLine($"Position : {outStream.Position}");

            outStream.WriteByte(0x04);
            Console.WriteLine($"Position : {outStream.Position}");

			outStream.Close();
        }
	}
}
