using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoServer
{
    class MainApp
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage : {0} <Bind IP>", Process.GetCurrentProcess().ProcessName);
                return;
            }

            string bindIp = args[0];
            const int bindPort = 5425;
            TcpListener? server = null;
            try
            {
                IPEndPoint localAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);

                server = new TcpListener(localAddress);

                server.Start();

                Console.WriteLine("Start Echo Server...");

                while (true)
                {
                    // AcceptTcpClient()를 호출하면 코드는 블록되어 그 자리에서 이 메소드가 반환할 때까지 진행하지 않는다.
                    // 클라이언트의 연결 요청이 있기 전까지는 반환되지 않는다.
                    // 클라이언트의 요청이 오면 TcpClient 객체를 반환한다.
                    TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("Connect Client : {0}",
                        ((IPEndPoint)client.Client.RemoteEndPoint).ToString());

                    // TcpClient 형식의 객체로부터 NetworkStream 형식의 객체를 가져와서 데이터를 읽고 쓸 수 있다.
                    NetworkStream stream = client.GetStream();

                    int length;
                    string? data = null;
                    byte[] bytes = new byte[256];

                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.Default.GetString(bytes, 0, length);
                        Console.WriteLine($"Received : {data}");

                        // 클라이언트로 받은 데이터 전송
                        byte[] msg = Encoding.Default.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine($"Sent : {data}");
                    }

                    stream.Close();
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if(server != null) 
                    server.Stop();
            }

            Console.WriteLine("Closing Server.");
        }
    }
}