using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FUP
{
    /// <summary>
    /// 4가지 MSGTYPE(메시지 타입)에 따른 Body(본문 형식)을 각각의 클래스로 작성
    /// MSGTYPE이 파일 전송 요청(0x01)일 경우 
    /// 바디 구조 : 파일크기(8byte), 파일명(바디크기 - 파일크기의 byte)
    /// 클라이언트에서 사용
    /// </summary>
    public class BodyRequest : ISerializable
    {

        public long FILESIZE; // 파일 크기(8 byte)
        public byte[] FILENAME; // 파일명(바디크기 - 파일크기의 바이트)

        public BodyRequest() { }

        public BodyRequest(byte[] bytes)
        {
            FILESIZE = BitConverter.ToInt64(bytes, 0);
            FILENAME = new byte[bytes.Length - sizeof(long)]; // sizeof(long) : FILESIZE의 형식의 크기
            Array.Copy(bytes, sizeof(long), FILENAME, 0, FILENAME.Length);
        }

        // 바디의 데이터를 바이트 배열로 변환
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(FILESIZE);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            Array.Copy(FILENAME, 0, bytes, temp.Length, FILENAME.Length);

            return bytes;
        }

        // 바디의 크기        
        public int GetSize()
        {
            return sizeof(long) + FILENAME.Length;
        }
    }

    /// <summary>
    /// MSGTYPE이 파일 전송 요청(0x01)에 대한 응답(0x02)일 경우
    /// 바디 구조 : 파일 전송 요청(0x01)의 메지시 식별 번호(4byte), 파일 전송 승인 여부(1byte)
    /// 서버에서 사용
    /// </summary>
    public class BodyResponse : ISerializable
    {
        public uint MSGID; // 파일 전송 요청(0x01)의 MSGID(4byte)
        public byte RESPONSE; // 파일 전송 승인 여부(1byte)

        public BodyResponse() { }

        public BodyResponse(byte[] bytes)
        {
            MSGID = BitConverter.ToUInt32(bytes, 0);
            RESPONSE = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MSGID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESPONSE;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }

    /// <summary>
    /// MSGTYPE이 파일 전송 데이터(0x02)일 경우
    /// 바디 구조 : 파일 내용(크기는 헤더의 BODYLEN)
    /// 서버에서 사용
    /// </summary>
    public class BodyData : ISerializable
    {
        public byte[] DATA;

        public BodyData(byte[] bytes)
        {
            DATA = new byte[bytes.Length];
            bytes.CopyTo(DATA, 0); // DATA = bytes 를 하면 얕은 복사가 일어나며(같은 값을 참조), bytes 가 지워지면 DATA도 지워지므로 깊은 복사를 통한 값을 보존한다.
        }

        public byte[] GetBytes()
        {
            return DATA;
        }

        public int GetSize()
        {
            return DATA.Length;
        }
    }

    /// <summary>
    /// MSGTYPE이 파일 수신 결과(0x03)일 경우
    /// 바디 구조 : 파일 전송 데이터(0x02)의 메시지 식별 번호(4byte), 파일 전송 결과(1byte)
    /// 서버에서 사용
    /// </summary>
    public class BodyResult : ISerializable
    {
        public uint MSGID; // 파일 전송 데이터(0x02)의 메시지 식별 번호(4byte)
        public byte RESULT; // 파일 전송 결과(1byte)

        public BodyResult() { }
        public BodyResult(byte[] bytes)
        {
            MSGID = BitConverter.ToUInt32(bytes, 0);
            RESULT = bytes[4];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            byte[] temp = BitConverter.GetBytes(MSGID);
            Array.Copy(temp, 0, bytes, 0, temp.Length);
            bytes[temp.Length] = RESULT;

            return bytes;
        }

        public int GetSize()
        {
            return sizeof(uint) + sizeof(byte);
        }
    }
}
