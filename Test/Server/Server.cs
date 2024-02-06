using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace CardsHandler.Server
{
    internal class ServerInstance
    {
        private string _serverAddress = "127.0.0.1";
        private int _serverPort = 49001;

        public ServerInstance()
        {

        }

        public ServerInstance(string serverAddress, int serverPort)
        {
            _serverAddress = serverAddress;
            _serverPort = serverPort;
        }

        public int RequestCardNum()
        {
            // Получаем конфиг подключения к серверу.
            const string RequestCard = "CardRequest";

            TcpClient client = new TcpClient(_serverAddress, _serverPort);
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

        public Card CreateCard(string request)
        {
            TcpClient client = new TcpClient(_serverAddress, _serverPort);
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(request);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Ответ от сервера: {responseMessage}");
            Card card = JsonConvert.DeserializeObject<Card>(responseMessage);
            client.Close();

            return card;
        }

        public ResultOperations GetCard(string request, out Card card)
        {
            card = null;
            TcpClient client = new TcpClient(_serverAddress, _serverPort);
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(request);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            ResultOperations resultOperations = ResultOperations.None;

            const string Mask = "{\"Cardnumber";
            if (responseMessage.ToString().Substring(0, 12) == Mask)
            {
                card = JsonConvert.DeserializeObject<Card>(responseMessage);
            }
            else
            {
                resultOperations = (ResultOperations)Enum.Parse(typeof(ResultOperations), responseMessage);
            }

            client.Close();

            return resultOperations;
        }
    }
}
