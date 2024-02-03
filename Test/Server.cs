using System;
using System.Net.Sockets;
using System.Text;

namespace Test
{
    internal class Server
    {
        public static int RequestCardNum()
        {
            const string ServerAddress = "127.0.0.1";
            const string RequestCard = "CardRequest";
            const int ServerPort = 49001;

            TcpClient client = new TcpClient(ServerAddress, ServerPort);
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.ASCII.GetBytes(RequestCard);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Сообщение отправлено.");

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string responseMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Ответ от сервера: {responseMessage}");
            int.TryParse(responseMessage, out int cardNubber);
            client.Close();

            return cardNubber;
        }
    }

}
