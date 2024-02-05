//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 10/04/2023
// Last Modified On : 05/02/2024
// Description: Workking with Postgres
// Project: CardsHandler
//////////////////////////////////////////

using System;
using System.Data;
using CardsHandler.Interfaces;
using Npgsql;

namespace CardsHandler.Database
{
    internal class PostgresDB : IDisposable, IProcessCardsDB
    {
        #region FIELDS

        private static PostgresDB _instance;
        private readonly NpgsqlConnection _connection;

        private string _connectionString;
        private string _server;
        private string _dbName;
        private int _port;

        #endregion FIELDS

        #region CTORs

        private PostgresDB(
            string server,
            string userName,
            string dataBase,
            int port)
        {
            _connectionString = string.Format(
                    $"Server={server};" +
                    $"Username={userName};" +
                    $"Database={dataBase};" +
                    $"Port={port};" +
                    $"Password={string.Empty}");
            _server = server;
            _dbName = dataBase;
            _port = port;

            _connection = new NpgsqlConnection(_connectionString);
            try
            {
                _connection.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion CTORs

        ~PostgresDB()
        {
            _connection.Close();
        }

        #region PROPERTIES

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string Server
        {
            get { return _server; }
            private set { _server = value; }
        }

        public string DBname
        {
            get { return _dbName; }
            private set { _dbName = value; }
        }

        public int Port
        {
            get { return _port; }
            private set { _port = value; }
        }

        #endregion PROPERTIES

        #region METHODS

        public static PostgresDB GetInstance(
            string server,
            string userName,
            string dataBase,
            int port)
        {
            if (_instance == null)
            {
                _instance = new PostgresDB(
                    server,
                    userName,
                    dataBase,
                    port);
            }

            return _instance;
        }

        /// <summary>
        /// Проверяем естьли карты с таким номером.
        /// </summary>
        /// <param name="cardNumber">
        /// Номер карты.
        /// </param>
        /// <returns>
        /// bool.
        /// </returns>
        public bool CheckIfCardExist(int cardNumber)
        {
            bool isExist = false;

            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            npgsqlCommand.CommandText =
                $"SELECT EXISTS(" +
                $"  SELECT cardnumber " +
                $"  FROM cards " +
                $"  WHERE cardnumber = {cardNumber});";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();

            DataTable isAccessExist = new DataTable();
            isAccessExist.Load(data);

            isExist = (bool)isAccessExist.Rows[0].ItemArray[0];

            data.Close();

            return isExist;
        }

        /// <summary>
        /// Проверяем естьли карты с таким номером.
        /// </summary>
        /// <param name="phoneNumber">
        /// Номер карты.
        /// </param>
        /// <returns>
        /// bool.
        /// </returns>
        public bool CheckIfPhone(string phoneNumber)
        {
            bool isExist = false;

            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            npgsqlCommand.CommandText = $"SELECT EXISTS(" +
                $"SELECT \"phoneNumber\" " +
                $"FROM clients " +
                $"WHERE \"phoneNumber\" = {phoneNumber})";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();

            DataTable isPhoneExist = new DataTable();
            isPhoneExist.Load(data);

            isExist = (bool)isPhoneExist.Rows[0].ItemArray[0];

            data.Close();

            return isExist;
        }

        /// <summary>
        /// Создание карты.
        /// </summary>
        /// <param name="card">
        /// объект класса бонусной карты.
        /// </param>
        public void CreateCard(Card card)
        {
            string expirationDate
                = DateTime.Today.AddMonths(12).Date.ToString("dd.MM.yyyy");

            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();
            npgsqlCommand.CommandText =

                $"INSERT INTO public.cards(" +
                $" cardnumber, \"phoneNumber\", \"expirationDate\", ballance) " +
                $" VALUES({card.Number}, " +
                $" {card.PhoneNumber}, " +
                $" \'{expirationDate}\'," +
                $" {card.Ballance});" +
                $"INSERT INTO public.clients(" +
                $"  \"phoneNumber\", \"firstName\", \"middleName\", \"lastName\")" +
                $"  VALUES({card.PhoneNumber}," +
                $"  \'{card.OwnerFirstName}\'," +
                $"  \'{card.OwnerMiddleName}\'," +
                $"  \'{card.OwnerLastName}\'); ";

            npgsqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Поиск карты.
        /// </summary>
        /// <param name="number">номер телефона/карты.</param>
        /// <returns>Объект карты.</returns>
        public Card FindCardByPhone(string number)
        {
            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            npgsqlCommand.CommandText =
                $"SELECT * FROM CARDS WHERE phoneNumber = \"{number}\";";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();

            string phoneNumber = string.Empty;
            string firstName = string.Empty;
            string middleName = string.Empty;
            string lastName = string.Empty;
            int cardnumber = 0;
            int ballance = 0;
            DateTime expirationDate = DateTime.Now;

            while (data.Read())
            {
                phoneNumber = (string)data["phoneNumber"];
                firstName = (string)data["firstName"];
                middleName = (string)data["middleName"];
                lastName = (string)data["lastName"];
                cardnumber = (int)data["cardnumber"];
                ballance = (int)data["ballance"];
                expirationDate = (DateTime)data["expirationDate"];
            }

            return new Card(
                cardnumber,
                phoneNumber,
                firstName,
                middleName,
                lastName,
                expirationDate,
                ballance);

        }

        /// <summary>
        /// Поиск карты.
        /// </summary>
        /// <param name="number">карты.</param>
        /// <returns>Объект карты.</returns>
        public Card FindCardByCard(int number)
        {
            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            npgsqlCommand.CommandText =
                $"SELECT cl.\"phoneNumber\"," +
                $"      cl.\"firstName\"," +
                $"      cl.\"middleName\"," +
                $"      cl.\"lastName\"," +
                $"      cd.cardnumber," +
                $"      cd.ballance," +
                $"      cd.\"expirationDate\"" +
                $" FROM clients AS cl " +
                $" INNER JOIN CARDS as cd  ON cl.\"phoneNumber\" = cd.\"phoneNumber\"  " +
                $" WHERE cd.cardnumber = {number};";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();

            string phoneNumber = string.Empty;
            string firstName = string.Empty;
            string middleName = string.Empty;
            string lastName = string.Empty;
            int cardnumber = 0;
            int ballance = 0;
            DateTime expirationDate = DateTime.Now;

            while (data.Read())
            {
                phoneNumber = (string)data["phoneNumber"];
                firstName = (string)data["firstName"];
                middleName = (string)data["middleName"];
                lastName = (string)data["lastName"];
                cardnumber = (int)data["cardnumber"];
                ballance = (int)data["ballance"];
                expirationDate = (DateTime)data["expirationDate"];
            }

            return new Card(
                cardnumber,
                phoneNumber,
                firstName,
                middleName,
                lastName,
                expirationDate,
                ballance);
        }

        /// <summary>
        /// Списание.
        /// </summary>
        /// <param name="card">объект карты.</param>
        /// <param name="summ">Сумма к списанию.</param>
        /// <returns>ResultOperations.</returns>
        public ResultOperations Charge(Card card, int summ)
        {
            ResultOperations result = ResultOperations.None;

            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            npgsqlCommand.CommandText =
                $"SELECT ballance " +
                $"FROM CARDS " +
                $"WHERE cardnumber = {card.Number}; ";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();

            DataTable dateVol = new DataTable();
            dateVol.Load(data);

            int currentBalance = (int)dateVol.Rows[0].ItemArray[0];

            data.Close();

            if ((currentBalance - summ) < 0)
            {
                result = ResultOperations.ChargeError;
            }
            else
            {
                npgsqlCommand.CommandText =
                    $"SELECT \"expirationDate\"  " +
                    $"FROM CARDS " +
                    $"WHERE cardnumber = {card.Number}; ";

                dateVol = new DataTable();
                dateVol.Load(data);

                data = npgsqlCommand.ExecuteReader();
                dateVol.Load(data);

                DateTime expirationDate = (DateTime)dateVol.Rows[0].ItemArray[0];

                if (expirationDate < DateTime.Today)
                {
                    result = ResultOperations.CardExpired;
                }
                else
                {
                    currentBalance -= summ;
                    npgsqlCommand.CommandText = $"" +
                        $"UPDATE CARDS " +
                        $"SET ballance = {currentBalance} " +
                        $"WHERE cardnumber = {card.Number} ;";

                    npgsqlCommand.ExecuteReader();
                }
            }

            return result;
        }

        /// <summary>
        /// Начисление бонусов.
        /// </summary>
        /// <param name="card">объект карты.</param>
        /// <param name="summ">Сумма к списанию.</param>
        /// <returns>Результат.</returns>
        public ResultOperations AddBonus(Card card, int summ)
        {
            ResultOperations result = ResultOperations.None;

            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            npgsqlCommand.CommandText =
                $"SELECT ballance " +
                $"FROM CARDS " +
                $"WHERE cardnumber = {card.Number}; ";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();

            DataTable dateVol = new DataTable();
            dateVol.Load(data);

            int currentBalance = (int)dateVol.Rows[0].ItemArray[0];
            npgsqlCommand.CommandText =
                    $"SELECT \"expirationDate\"  " +
                    $"FROM CARDS " +
                    $"WHERE cardnumber = {card.Number}; ";

            dateVol = new DataTable();
            dateVol.Load(data);

            data = npgsqlCommand.ExecuteReader();
            dateVol.Load(data);

            DateTime expirationDate = (DateTime)dateVol.Rows[0].ItemArray[0];

            if (expirationDate < DateTime.Today)
            {
                result = ResultOperations.CardExpired;
            }
            else
            {
                currentBalance += summ;
                npgsqlCommand.CommandText = $"" +
                    $"UPDATE CARDS " +
                    $"SET ballance = {currentBalance} " +
                    $"WHERE cardnumber = {card.Number} ;";

                npgsqlCommand.ExecuteReader();
                data.Close();
            }

            return result;
        }

        /// <summary>
        /// Получить все карты.
        /// </summary>
        /// <returns>Таблица с картами.</returns>
        public ResultOperations GetAllCards(out DataTable dataTable)
        {
            ResultOperations resultOperations = ResultOperations.None;

            dataTable = new DataTable();

            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            npgsqlCommand.CommandText = $"" +
                $"SELECT cd.cardnumber," +
                $"  cl.\"phoneNumber\"," +
                $"  cl.\"firstName\"," +
                $"  cl.\"middleName\"," +
                $"  cl.\"lastName\"," +
                $"  cd.ballance," +
                $"  cd.\"expirationDate\"" +
                $" FROM clients AS cl " +
                $" INNER JOIN CARDS as cd ON cl.\"phoneNumber\" = cd.\"phoneNumber\" ;";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();
            dataTable.Load(data);

            return resultOperations;
        }

        /// <summary>
        /// Получить все карты.
        /// </summary>
        /// <returns>Таблица с картами.</returns>
        public ResultOperations GetExpiredCards(out DataTable dataTable)
        {
            ResultOperations resultOperations = ResultOperations.None;
            dataTable = new DataTable();

            NpgsqlCommand npgsqlCommand = _connection.CreateCommand();

            string expirationDate
                = DateTime.Today.Date.ToString("dd.MM.yyyy");

            npgsqlCommand.CommandText = $"" +
                $"SELECT cd.cardnumber," +
                $"  cl.\"phoneNumber\"," +
                $"  cl.\"firstName\"," +
                $"  cl.\"middleName\"," +
                $"  cl.\"lastName\"," +
                $"  cd.ballance," +
                $"  cd.\"expirationDate\"" +
                $" FROM clients AS cl " +
                $" INNER JOIN CARDS as cd ON cl.\"phoneNumber\" = cd.\"phoneNumber\" " +
                $" WHERE cd.\"expirationDate\"  < \'{expirationDate}\';;";

            NpgsqlDataReader data;
            data = npgsqlCommand.ExecuteReader();
            dataTable.Load(data);

            return resultOperations;
        }

        public void Dispose()
        {
            _connection.Close();
        }
        #endregion METHODS
    }
}
