using System;
using System.IO;

namespace FUP
{
    /// <summary>
    /// 스트림으로부터 메시지를 보내고 받기 위핸 메소드를 가진 클래스
    /// </summary>
    public class MessageUtil
    {
        /// <summary>
        /// 스트림을 통해 메시지를 보낸다.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="msg"></param>
        public static void Send(Stream writer, Message msg)
        {
            writer.Write(msg.GetByte(), 0, msg.GetSize());
        }

        public static Message Receive(Stream reader)
        {
            // 헤더
            int totalRecv = 0;
            int sizeToRead = 16; // 헤더의 크기가 16바이트이므로
            byte[] hBuffer = new byte[sizeToRead];

            // NetworkStream 사용시 읽는 방법
            while (sizeToRead > 0)
            {
                byte[] buffer = new byte[sizeToRead];
                int recv = reader.Read(buffer, 0, sizeToRead);
                if (recv == 0) // NetworkStream에서 반환값이 0은 읽은 값이 없다는 것이다.
                    return null;

                buffer.CopyTo(hBuffer, totalRecv);
                totalRecv += recv;
                sizeToRead -= recv;
            }

            Header header = new Header(hBuffer);


            // 바디
            totalRecv = 0;
            byte[] bBuffer = new byte[header.BODYLEN];
            sizeToRead = (int)header.BODYLEN;

            while (sizeToRead > 0)
            {
                byte[] buffer = new byte[sizeToRead];
                int recv = reader.Read(buffer, 0, sizeToRead);
                if (recv == 0)
                    return null;

                buffer.CopyTo(bBuffer, totalRecv);
                totalRecv += recv;
                sizeToRead -= recv;
            }

            ISerializable body = null;
            switch (header.MSGTYPE)
            {
                case CONSTANTS.REQ_FILE_SEND:
                    body = new BodyRequest(bBuffer);
                    break;
                case CONSTANTS.REP_FILE_SEND:
                    body = new BodyResponse(bBuffer);
                    break;
                case CONSTANTS.FILE_SEND_DATA:
                    body = new BodyData(bBuffer);
                    break;
                case CONSTANTS.FILE_SEND_RES:
                    body = new BodyResult(bBuffer);
                    break;
                default:
                    throw new Exception(String.Format("Unknown MSGTYPE : {0}", header.MSGTYPE));
            }

            return new Message() { Header = header, Body = body };
        }
    }
}
