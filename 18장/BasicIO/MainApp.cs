using System;
using System.IO;

// FileStream 클래스를 이용한 파일에 데이터를 쓰고 읽기(p.618)

// CLR이 지원하는 바이트 오더(byte order)가 데이터의 낮은 주소부터 기록(1의 자리 기록후 10의 자리 기록)하는 리틀 엔디안(Little Endian)방식이기 때문에 
// 순서가 뒤집힌 결과가 나온다.			
// offset(h)  00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F
// 00000000   F0 DE BC 9A 78 56 34 12
//
// ARM과 x86계열의 CPU들은 리틀 엔디안 방식
// 자바의 가상머신 및 Power CPU나 Sparc 계열의 CPU들은 빅 엔디안(Big Endian) 방식

// 16진수(0123456789ABCDEF)는 한자리의 최대값은 F이며
// 16진수 0xF(16진수 1자리 표시) = 10진수 15 = 2진수 1111이며, 4 bit를 차지한다.
// 1 byte의 최대값은 10진수 255 = 2진수 1111 1111 = 16진수 0xFF(16진수 2자리 표시), 8 bit를 차지한다.
// 그러므로, 1 byte로 16진수 2자리를 표시한다.
namespace BasicIO
{
    class MainApp
    {
        static void Main(string[] args)
        {
            long someValue = 0x123456789ABCDEF0; // 16진수 16자리
            Console.WriteLine("{0,-1} : 0x{1:X16}", "Original Data", someValue);

            // 쓰기
            Stream outStream = new FileStream("a.dat", FileMode.Create);
            byte[] wBytes = BitConverter.GetBytes(someValue); // 16진수 2자리는 1 byte, 16자리는 8 byte, 

            Console.Write("{0,-13} : ", "Byte array");

            foreach (byte b in wBytes)
                Console.Write("{0:X2} ", b); // 결과 : F0 DE BC 9A 78 56 34 12 (리틀 엔디안(Little Endian)방식)
            Console.WriteLine();

            outStream.Write(wBytes, 0, wBytes.Length);
            outStream.Close();

            // 읽기
            Stream inStream = new FileStream("a.dat", FileMode.Open);
            byte[] rBytes = new byte[8]; // 16진수 2자리는 1 byte, 16자리는 8 byte, 

            int i = 0;
            while (inStream.Position < inStream.Length)
                rBytes[i++] = (byte)inStream.ReadByte();

            long readValue = BitConverter.ToInt64(rBytes, 0);

            Console.WriteLine("{0, -13} : 0x{1:X16}", "Read Data", readValue);
            inStream.Close();
        }
    }
}