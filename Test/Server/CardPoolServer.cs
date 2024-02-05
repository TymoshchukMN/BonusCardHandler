using System;
using System.Net.Sockets;
using System.Text;

namespace CardsHandler.Server
{
    internal class CardPoolServer
    {
        public static int RequestCardNum()
        {
            // Получаем конфиг подключения к серверу.
            SrvConfig srvConfig = BL.GetServerConfig();

            string serverAddress = srvConfig.Server;
            int serverPort = srvConfig.Port;

            const string RequestCard = "CardRequest";

            TcpClient client = new TcpClient(serverAddress, serverPort);
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.ASCII.GetBytes(RequestCard);
            stream.Write(data, 0, data.Length);

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
