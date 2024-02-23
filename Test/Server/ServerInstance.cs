using System;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace CardsHandler.Server
{
    public class ServerInstance
    {
        private const string ConfFilePathSRV = @"\\172.16.112.40\share\TymoshchukMN\SRVconfigFile.json";

        private readonly string _serverAddress;
        private readonly int _serverPort;

        public ServerInstance(string serverAddress, int serverPort)
        {
            /*_serverAddress = serverAddress;
            _serverPort = serverPort;*/
            _serverAddress = "127.0.0.1";
            _serverPort = 49001;
        }

        public ServerInstance()
        {
            SrvConfig srvConfig = GetServerConfig();
            _serverAddress = srvConfig.Server;
            _serverPort = srvConfig.Port;
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

        public ResultOperations ProcessCard(string request, out Card card)
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
            if (responseMessage.ToString() == ResultOperations.CardExpired.ToString())
            {
                return ResultOperations.CardExpired;
            }

            if (responseMessage.ToString() == ResultOperations.CardDoesnExist.ToString())
            {
                return ResultOperations.CardDoesnExist;
            }

            if (responseMessage.ToString() == ResultOperations.ChargeError.ToString())
            {
                return ResultOperations.ChargeError;
            }

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

        public DataTable GetDatatable(string request)
        {
            TcpClient client = new TcpClient(_serverAddress, _serverPort);
            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(request);
            stream.Write(data, 0, data.Length);

            byte[] buffer = new byte[103072];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string jsonReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Десериализация JSON в DataTable
            DataTable receivedTable = JsonConvert.DeserializeObject<DataTable>(jsonReceived);

            return receivedTable;
        }

        private static SrvConfig GetServerConfig()
        {
            string srvConfigFile = File.ReadAllText(ConfFilePathSRV);
            SrvConfig srvConfigJSON = JsonConvert.DeserializeObject<SrvConfig>(srvConfigFile);

            return srvConfigJSON;
        }
    }
}
