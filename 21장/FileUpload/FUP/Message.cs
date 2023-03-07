namespace FUP
{
    /// <summary>
    /// FUP의 헤더 및 바디가 갖고 있는 각 속성 필드에 대한 상수 정의 
    /// </summary>
    public class CONSTANTS
    {
        #region FUG의 헤더에 필요한 상수 정의
        // MSGTYPE(메시지 타입) 상수 정의(4 byte)
        public const uint REQ_FILE_SEND = 0x01; // 파일 전송 요청 (클라이언트)
        public const uint REP_FILE_SEND = 0x02; // 파일 전송 요청에 대한 응답(서버)
        public const uint FILE_SEND_DATA = 0x03; // 파일 전송 데이터
        public const uint FILE_SEND_RES = 0x04; // 파일 수신 결과

        // FRAGMENTED(메시지의 분할 여부) 상수 정의(1 byte)
        public const byte NOT_FRAGMENTED = 0x00; // 미분할
        public const byte FRAGMENTED = 0x01; // 분할

        // LASTMSG(분할된 메시지가 마지막인지 여부) 상수 정의(1 byte)
        public const byte NOT_LASTMSG = 0x00; // 마지막 메시지가 아님
        public const byte LASTMSG = 0x01; // 마지막 메시지
        #endregion

        #region FUG의 바디에 필요한 상수 정의
        // 파일 전송 승인 여부 상수 정의(1 byte), 파일 전송 요청에 대한 응답(0x02) 메시지에 필요
        public const byte ACCEPTED = 0x01; // 파일 전송 승인
        public const byte DENIED = 0x00; // 파일 전송 거부

        // 파일 전송 성공 여부 상수 정의(1 byte)
        public const byte FAIL = 0x00; // 파일 전송 성공
        public const byte SUCCESS = 0x01; // 파일 전송 실패
        #endregion
    }

    /// <summary>
    /// NETWORKSTREAM.Read(), Write()에 필요한 인터페이스 정의
    /// 자신의 데이터를 바이트 배열로 변환하고, 그 바이트 배열의 크기를 반환하는 인터페이스
    /// 메시지 헤더, 바디는 모두 이 인터페이스를 상속한다.
    /// </summary>
    public interface ISerializable
    {
        /// <summary>
        /// 자신의 데이터를 바이트 배열로 변환
        /// </summary>
        /// <returns></returns>
        byte[] GetBytes();

        /// <summary>
        /// 그 바이트 배열의 크기
        /// </summary>
        /// <returns></returns>
        int GetSize();
    }

    /// <summary>
    /// FUP의 메시지를 나타내는 클래스
    /// Header와 Body로 구성된다.
    /// </summary>
    public class Message :  ISerializable
    {
        public Header Header { get; set; }
        public ISerializable Body { get; set; }


        /// <summary>
        /// Header 와 Body의 데이터를 바이트 배열로 변환
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[GetSize()];
            Header.GetBytes().CopyTo(bytes, 0); //ArrayA.CopyTo(ArrayB, int32) : 현재 배열(ArrayA)의 모든 요소를 지정된 배열 인덱스부터 시작하여 지정된 배열(ArrayB)에 복사
            Body.GetBytes().CopyTo(bytes, Header.GetSize());

            return bytes;
        }

        /// <summary>
        /// Header 와 Body의 데이터를 바이트 배열로 변환한 값의 크기
        /// </summary>
        /// <returns></returns>
        public int GetSize()
        {
            return Header.GetSize() + Body.GetSize();
        }
    }
}