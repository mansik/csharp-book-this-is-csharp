using System;

namespace FUP
{
    /// <summary>
    /// 메시지 헤더
    /// </summary>
    public class Header:ISerializable
    {
        #region FUP의 헤더가 갖고 있는 각 속성 필드의 프로프티(헤더 구조):16 byte
        /// <summary>
        /// 메시지 식별 번호(4 byte)
        /// </summary>
        public uint MSGID { get; set; }

        /// <summary>
        /// 메시지 타입(4 byte)
        /// </summary>
        public uint MSGTYPE { get; set; }

        /// <summary>
        /// 메시지 길이(4 byte)
        /// </summary>
        public uint BODYLEN { get; set; }

        /// <summary>
        /// 메시지 분할 여부(1 byte)
        /// </summary>
        public byte  FRAGMENTED { get; set; }

        /// <summary>
        /// 분할된 메시지가 마지막인지 여부(1 byte)
        /// </summary>
        public byte LASTMSG { get; set; }
        
        /// <summary>
        /// 메시지 파편 변호(2 byte)
        /// </summary>
        public ushort SEQ { get; set; }
        #endregion

        public Header() { }

        // 생성자에서 헤더의 데이터(bytes)를 헤더의 프로프티에 나눠 담음
        public Header(byte[] bytes)
        {
            // Header의 구조(16byte) = MSGID(4byte) + MSGTYPE(4byte) + BODLEN(4byte) + FRAGMENTED(1byte) + LASTMSG(1byte) + SEQ(2byte)
            // 메시지 별로 끊어서 보면 bytes는 다음과 같은 인덱스를 가진다.
            // 0123 4567 8901 2 3 45
            MSGID = BitConverter.ToUInt32(bytes, 0); // bytes(byte 배열)에서 0번 인덱스부터 시작하는 부호없는 4바이트(32비트)정수를 반환
            MSGTYPE = BitConverter.ToUInt32(bytes, 4); // bytes(byte 배열)에서 4번 인덱스(MSGID가 4바이트이므로 0+4)부터 시작하는 부호없는 4바이트(32비트)정수를 반환
            BODYLEN = BitConverter.ToUInt32(bytes, 8); // 8번 인덱스(MSGID 4 바이트 + MSGTYPE 4 바이트)에서 시작하는 부호없는 4바이트(32비트)정수를 반환
            FRAGMENTED = bytes[12]; // bytes의 12번 인덱스에 있는 1바이트
            LASTMSG = bytes[13];
            SEQ = BitConverter.ToUInt16(bytes, 14); // 14번 인덱스에서 시작하는 부호없는 2바이트(16비트)정수를 반환
        }

        #region ISerializable 인터페이스 구현
        public byte[] GetByte()
        {
            // 헤더는 총 16byte로 구성되어 있다.
            byte[] bytes = new byte[16];  // 헤더의 메시지를 담을 배열


            byte[] temp = BitConverter.GetBytes(MSGID);
            Array.Copy(temp, 0 , bytes, 0, temp.Length); // temp배열의 0번 인덱스부터 temp.Length길이만큼의 데이터를 bytes배열에 0번 인덱스 위치에 복사

            temp = BitConverter.GetBytes(MSGTYPE);
            Array.Copy(temp, 0, bytes, 4, temp.Length); //  temp배열의 0번 인덱스부터 temp.Length길이만큼의 데이터를 bytes배열에 4번 인덱스 위치에 복사

            temp = BitConverter.GetBytes(BODYLEN);
            Array.Copy(temp, 0, bytes, 8, temp.Length);

            bytes[12] = FRAGMENTED;
            bytes[13] = LASTMSG;

            temp = BitConverter.GetBytes(SEQ);
            Array.Copy(temp, 0, bytes, 12, temp.Length);

            return bytes;
        }

        public int GetSize()
        {
            return 16;
        }
        #endregion
    }
}
