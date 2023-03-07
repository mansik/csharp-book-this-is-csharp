using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FUP;

namespace FileReceiver
{
    class MainApp
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("사용법 : {0} <Directory>", Process.GetCurrentProcess().ProcessName);
                return;
            }
            // MSGID(4bytes)
            uint msgId = 0;

            string dir = args[0];
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);

            const int bindPort = 5425;
            TcpListener server = null;
            try
            {
                // ip 주소를 0으로 입력하면 127.0.0.1 뿐 아니라 OS에 할당되어 있는 어떤 주소로도 서버에 접속이 가능하다.
                IPEndPoint localAddress = new IPEndPoint(0, bindPort);

                server = new TcpListener(localAddress);
                server.Start();

                Console.WriteLine("파일 업로드 서버 시작...");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("클라이언트 접속 : {0}", ((IPEndPoint)client.Client.RemoteEndPoint).ToString());

                    NetworkStream stream = client.GetStream();

                    #region 파일 전송 요청 (0x01)
                    // 클라이언트가 보내온 파일 전송 요청 메시지를 수신한다.
                    Message reqMsg = MessageUtil.Receive(stream);

                    if (reqMsg.Header.MSGTYPE != CONSTANTS.REQ_FILE_SEND)
                    {
                        stream.Close();
                        client.Close();
                        continue;
                    }

                    BodyRequest reqBody = (BodyRequest)reqMsg.Body;

                    Console.WriteLine("파일 업로드 요청이 왔습니다. 수락하시겠습니까? yes/no");
                    string answer = Console.ReadLine();
                    #endregion


                    #region 파일 전송에 대한 응답 (0x02)
                    Message rspMsg = new Message();
                    rspMsg.Body = new BodyResponse()
                    {
                        MSGID = reqMsg.Header.MSGID,
                        RESPONSE = CONSTANTS.ACCEPTED
                    };
                    rspMsg.Header = new Header()
                    {
                        MSGID = msgId++,
                        MSGTYPE = CONSTANTS.REP_FILE_SEND,
                        BODYLEN = (uint)rspMsg.Body.GetSize(),
                        FRAGMENTED = CONSTANTS.NOT_FRAGMENTED,
                        LASTMSG = CONSTANTS.LASTMSG,
                        SEQ = 0
                    };

                    if (answer != "yes")
                    {
                        // 사용자가 "yes"가 아닌 답을 입력하면 클라이언트에게 "거부" 응답을 보낸다.
                        rspMsg.Body = new BodyResponse()
                        {
                            MSGID = reqMsg.Header.MSGID,
                            RESPONSE = CONSTANTS.DENIED
                        };
                        MessageUtil.Send(stream, rspMsg);
                        stream.Close();
                        client.Close();

                        continue;
                    }
                    else
                        // "yes"를 입력하면 클라이언트에 "승낙" 응답을 보낸다.
                        MessageUtil.Send(stream, rspMsg);
                    #endregion

                    Console.WriteLine("파일 전송을 시작합니다...");


                    #region 파일 전송 (0x03)
                    long fileSize = reqBody.FILESIZE;
                    string fileName = Encoding.Default.GetString(reqBody.FILENAME);

                    // 업로드 파일 스트림을 생성한다.
                    FileStream file = new FileStream(dir + "\\" + fileName, FileMode.Create);

                    uint? dataMsgId = null;
                    ushort prevSeq = 0;
                    while ((reqMsg = MessageUtil.Receive(stream)) != null)
                    {
                        Console.Write("#");
                        if (reqMsg.Header.MSGTYPE != CONSTANTS.FILE_SEND_DATA)
                            break;

                        // MSGID 체크
                        if (dataMsgId == null)
                            dataMsgId = reqMsg.Header.MSGID;
                        else
                        {
                            if (dataMsgId != reqMsg.Header.MSGID)
                                break;
                        }

                        // 메시지 순서 체크, 메시지 순서가 어긋나면 전송을 중단한다.
                        if (prevSeq++ != reqMsg.Header.SEQ)
                        {
                            Console.WriteLine("{0}, {1}", prevSeq, reqMsg.Header.SEQ);
                            break;
                        }

                        // 전송
                        file.Write(reqMsg.Body.GetBytes(), 0, reqMsg.Body.GetSize());

                        // 마지막 메시지면 반복문을 빠져나온다.
                        if (reqMsg.Header.LASTMSG == CONSTANTS.LASTMSG)
                            break;
                    }

                    long recvFileSize = file.Length;
                    file.Close();

                    Console.WriteLine();
                    Console.WriteLine("수신 파일 크기 : {0} bytes", recvFileSize);
                    #endregion

                    #region 파일 수신 결과 (0x04)
                    Message rstMsg = new Message();
                    rstMsg.Body = new BodyResult()
                    {
                        MSGID = reqMsg.Header.MSGID,
                        RESULT = CONSTANTS.SUCCESS
                    };
                    rstMsg.Header = new Header()
                    {
                        MSGID = msgId++,
                        MSGTYPE = CONSTANTS.FILE_SEND_RES,
                        BODYLEN = (uint)rstMsg.Body.GetSize(),
                        FRAGMENTED = CONSTANTS.NOT_FRAGMENTED,
                        LASTMSG = CONSTANTS.LASTMSG,
                        SEQ = 0
                    };

                    if (fileSize == recvFileSize)
                        // 파일 전송 요청에  담겨온 파일 크기와 실제로 받은 파일의 크기를 비교하여 같으면 성공 메시지를 보낸다.
                        MessageUtil.Send(stream, rstMsg);
                    else
                    {
                        rstMsg.Body = new BodyResult()
                        {
                            MSGID = reqMsg.Header.MSGID,
                            RESULT = CONSTANTS.FAIL
                        };

                        MessageUtil.Send(stream, rstMsg);
                    }
                    Console.WriteLine("파일 전송을 마쳤습니다.");

                    stream.Close();
                    client.Close();
                    #endregion
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("서버를 종료합니다.");
        }
    }
}