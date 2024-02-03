//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 10/04/2023
// Last Modified On : 26/01/2024
// Description: Workking with Postgres
// Project: CardsHandler
//////////////////////////////////////////

using System;
using System.Data;
using Npgsql;

namespace CardsHandler.Database
{
    internal class PostgresDB
    {
        #region FIELDS

        private string _connectionString;
        private string _server;
        private string _dbName;
        private int _port;

        #endregion FIELDS

        #region CTORs

        public PostgresDB(
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
        }

        #endregion CTORs

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
            using (NpgsqlConnection connection
                   = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                }
            }

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
            using (NpgsqlConnection connection
                   = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                }
            }

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
            using (NpgsqlConnection connection
                    = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string expirationDate
                        = DateTime.Today.AddMonths(12).Date.ToString("dd.MM.yyyy");

                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();
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
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                    return;
                }

                // NpgsqlDataReader data;
            }
        }

        /// <summary>
        /// Поиск карты.
        /// </summary>
        /// <param name="number">номер телефона/карты.</param>
        /// <returns>Объект карты.</returns>
        public Card FindCardByPhone(string number)
        {
            using (NpgsqlConnection connection
                  = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                catch (Exception ex)
                {
                    UI.PrintErrorConnectionToDB(this);
                }

                return null;
            }
        }

        /// <summary>
        /// Поиск карты.
        /// </summary>
        /// <param name="number">карты.</param>
        /// <returns>Объект карты.</returns>
        public Card FindCardByCard(int number)
        {
            using (NpgsqlConnection connection
                  = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                }

                return null;
            }
        }

        /// <summary>
        /// >
        /// </summary>
        /// <param name="card">объект карты.</param>
        /// <param name="summ">Сумма к списанию.</param>
        /// <returns>ResultOperations.</returns>
        public ResultOperations Charge(Card card, int summ)
        {
            ResultOperations result = ResultOperations.None;

            using (NpgsqlConnection connection
                     = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
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
            using (NpgsqlConnection connection
                     = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                    result = ResultOperations.CannontConnectToDB;
                }

                return result;
            }
        }

        /// <summary>
        /// Получить все карты.
        /// </summary>
        /// <returns>Таблица с картами.</returns>
        public ResultOperations GetAllCards(out DataTable dataTable)
        {
            ResultOperations resultOperations = ResultOperations.None;

            dataTable = new DataTable();
            using (NpgsqlConnection connection
                 = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                }
                catch (Exception)
                {
                    resultOperations = ResultOperations.CannontConnectToDB;
                    UI.PrintErrorConnectionToDB(this);
                }
            }

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
            using (NpgsqlConnection connection
                 = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    NpgsqlCommand npgsqlCommand = connection.CreateCommand();

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
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                    resultOperations = ResultOperations.CannontConnectToDB;
                }
            }

            return resultOperations;
        }

        #endregion METHODS
    }
}
