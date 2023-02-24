using System;
using System.IO;

// 이진 데이터 처리를 위한 BinaryWriter/BinaryReader 도우미 클래스
//
// FileStream 클래스를 사용시 byte 형식 또는 byte의 배열 형식으로 변환해야하는 문제를 없앰
// Stream에 이진 데이터(Binary Data)를 쓰기/읽기를 위한 클래스이다.
// 사용하기 위해서는 Stream으로부터 파생된 클래스의 인스턴스가 있어야 한다.
namespace BinaryFile
{
	class MainApp
	{
		static void Main(string[] args)
		{
			using (BinaryWriter bw = new BinaryWriter(new FileStream("a.dat", FileMode.Create)))
			{

                bw.Write(123);
                bw.Write(456f);
                bw.Write(int.MaxValue);
				bw.Write("Good morning");
				bw.Write(uint.MaxValue);
				bw.Write("안녕하세요");
				bw.Write(double.MaxValue);
				bw.Write(new string('S', 255) + "A");

                Func<string> func1 = () =>
                {
                    string str = "";
                    for (int i = 0; i < 26; i++)
                    {
                        str += "1234567890";
                    }
                    return str + "E";
                };
                string overByte = func1();
                bw.Write(overByte);
                Console.WriteLine($"bw.writer 255자 이상 길이: {overByte.Length}");

                // 문자열 길이가 255를 초과하는 경우 바이너리 파일에서 문자열 길이(16진수) 기록하는 방법
                // 길이가 255를 초과하는 경우 길이를 표시하는 16진수는 UTF-8이라는 유니코드 인코딩 방식 사용하며, 1바이트 표현식 = 7비트 인코딩 사용
                // * 7비트 인코딩 : 255자를 초과하면 2진수로 변환 후
                //     1) 7비트로 자르고
                //     2) 7비트 앞에 1추가 후
                //     3) 2)에 의해 8비트로 만들어진 것을 먼저 쓰고
                //     4) 앞 2비트를 뒤쪽에 쓴 후
                //     5) 16진수로 변경
                //
                // 길이가 256 이라고 가정하면.
                // 1) 10진수 256을 16진수로 변환: 16으로 나누어서 더 이상 나누어지지 않을 때 몫과 나머지를 역순으로 하면됨.
                //    16)256 
                //    16) 16 - 0				          
                //         1 - 0 
                // 2) 16진수는 100
                // 3) 16진수를 2진수로 변환(16진수 1자리는 4비트씩 표현 0xF = 1111) => 1 0000 0000
                // 4)  1 0000 0000 (2진수)
                // 5) 10/ 000 0000 (7비트로 자름)
                // 6) 10/1000 0000 (7비트의 앞쪽에 1을 추가, x000 0000 -> x자리에 1대입)
                // 7) 1000 0000    (뒤쪽 8비트를 먼저 쓰고)
                // 8) 1000 0000  0000 0010 (앞 2비트를 뒤쪽으로 이동하면서 8비트로 변경)
                // 9)   8   0     0    2  (16진수로 변경: 16진수 2자리는 8비트로 표현 0xFF = 1111 1111)


                // > 바이너리 문자에서 255 초과하는 문자의 길이를 계산
                // 문자열 길이 0x80 0x02 저장되어 있는 경우(16진수)
                // 1)   8    0     0   2   (16진수)
                // 2) 1000 0000  0000 0010 (2진수)
                // 3) 0000 0010  1000 0000 (0x02 0x80 순서로 변경)
                // 4) 0000 0010  x000 0000 (뒤 8비트에서 1000 0000의  맨앞1을 x으로 변경)
                // 5)        10   000 0000 (앞 8비트에서 0 삭제)
                // 6) 1 0000 0000 을 16진수로 변경
                // 7) 1 0    0
                // 8) 1*16^2 + 0*16^0 = 256 + 0 = 256
            }

            using BinaryReader br = new BinaryReader(new FileStream("a.dat", FileMode.Open));

            // File Size 는 br.BaseStream.Length를 통해서 얻음
            Console.WriteLine($"File Size : {br.BaseStream.Length} bytes");

            Console.WriteLine($"{br.ReadInt32()}");
            Console.WriteLine($"{br.ReadSingle()}");
            Console.WriteLine($"{br.ReadInt32()}");
			Console.WriteLine($"{br.ReadString()}");
			Console.WriteLine($"{br.ReadUInt32()}");
			Console.WriteLine($"{br.ReadString()}");
			Console.WriteLine($"{br.ReadDouble()}");
            Console.WriteLine($"{br.ReadString()}");

            Console.WriteLine($"{br.ReadString()}");           
        }
	}
}