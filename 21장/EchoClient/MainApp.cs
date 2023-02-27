
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient
{
    class MainApp
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine(
                    "Usage : {0} <Bind IP> <Bind Port> <Server IP> <Message>",
                    Process.GetCurrentProcess().ProcessName);
                return;
            }

            string bindIp = args[0];
            int bindPort = Convert.ToInt32(args[1]);
            string serverIp = args[2];
            const int serverPort = 5425;
            string message = args[3];

            try
            {
                IPEndPoint clientAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                Console.WriteLine("Client : {0}, Server : {1}",
                    clientAddress.ToString(), serverAddress.ToString());

                // 로컬 TcpClient 객체 생성
                TcpClient client = new TcpClient(clientAddress);

                // 서버 접속
                client.Connect(serverAddress);

                byte[] data = Encoding.Default.GetBytes(message);

                // NetworkStream 객체 생성
                NetworkStream stream = client.GetStream();

                // NetworkStream을 통해 서버에 메시지 보내기
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent : {0}", message);

                // 메시지 받기
                data = new byte[256];
                string responseData = "";

                // while ((bytes = stream.Read(data, 0, data.Length)) != 0 ) 이 더 안전하다.
                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.Default.GetString(data);
                Console.WriteLine("Received : {0}", responseData);

                // Close
                stream.Close();
                client.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Closing Client.");
        }
    }
}