//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 10/04/2023
// Last Modified On : 05/02/2024
// Description: Workking with Postgres
// Project: CardsHandler
//////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using CardsHandler.Interfaces;
using Dapper;
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

            var queryArguments = new
            {
                card = cardNumber,
            };

            string sqlCommand = "SELECT COUNT(*) FROM cards WHERE cardnumber = @card";

            int count = _connection.QueryFirstOrDefault<int>(sqlCommand, queryArguments);

            if (count >= 1)
            {
                isExist = true;
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

            var queryArguments = new
            {
                phone = phoneNumber,
            };

            string sqlCommand = "SELECT COUNT(*) FROM cards WHERE \"phoneNumber\" = @phone";

            int count = _connection.QueryFirstOrDefault<int>(sqlCommand, queryArguments);

            if (count >= 1)
            {
                isExist = true;
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
            DateTime expirationDate = DateTime.Today.AddMonths(12).Date;

            string sqlCommand = $"INSERT INTO public.cards(" +
                $"cardnumber, \"expirationDate\", ballance, " +
                $"\"firstName\", \"middleName\", \"lastName\", " +
                $"\"phoneNumber\")" +
                " VALUES(@cardNum, @expDate, @balance, @firstName, " +
                "@middleName, @lastName, @phone);";

            var queryArguments = new
            {
                expirationDateCol = "expirationDate",
                firstNameCol = "firstName",
                middleNameCol = "middleName",
                lastNameCol = "lastName",
                phoneNumberCol = "phoneNumber",
                cardNum = card.Cardnumber,
                expDate = expirationDate,
                balance = card.Ballance,
                firstName = card.FirstName,
                middleName = card.MiddleName,
                lastName = card.LastName,
                phone = card.PhoneNumber,
            };

            try
            {
                _connection.Execute(sqlCommand, queryArguments);
            }
            catch (Exception ex)
            {
                UI.PrintErrorAdintCard(ex.Message);
            }
        }

        /// <summary>
        /// Поиск карты.
        /// </summary>
        /// <param name="card">Карта.</param>
        /// <param name="number">номер телефона/карты.</param>
        /// <returns>Объект карты.</returns>
        public ResultOperations FindCardByPhone(out Card card, string number)
        {
            ResultOperations resultOperations = ResultOperations.None;

            card = new Card();

            if (CheckIfPhone(number))
            {
                string sqlCommand = $"SELECT * FROM cards WHERE \"phoneNumber\" = @phone";

                IEnumerable<Card> results = _connection.Query<Card>(sqlCommand, new { phone = number });
                FillCard(card, results);
            }
            else
            {
                resultOperations = ResultOperations.PhoneDoesnEsixt;
            }

            return resultOperations;
        }

        /// <summary>
        /// Поиск карты.
        /// </summary>
        /// <param name="card">Карта.</param>
        /// <param name="number">карты.</param>
        /// <returns>Объект карты.</returns>
        public ResultOperations FindCardByCard(out Card card, int number)
        {
            ResultOperations resultOperations = ResultOperations.None;

            card = new Card();

            if (CheckIfCardExist(number))
            {
                string sqlCommand =
                    $"SELECT * FROM cards WHERE cardnumber = @cardNum";

                IEnumerable<Card> results =
                    _connection.Query<Card>(
                        sqlCommand,
                        new { cardNum = number });

                FillCard(card, results);
            }
            else
            {
                resultOperations = ResultOperations.CardDoesnExist;
            }

            return resultOperations;
        }

        /// <summary>
        /// Списание.
        /// </summary>
        /// <param name="card">объект карты.</param>
        /// <param name="cardNum">номер карты.</param>
        /// <param name="summ">Сумма к списанию.</param>
        /// <returns>ResultOperations.</returns>
        public ResultOperations Charge(out Card card, int cardNum, int summ)
        {
            ResultOperations resultOperations = ResultOperations.None;

            card = new Card();

            if (CheckIfCardExist(cardNum))
            {
                string sqlCommand =
                    $"SELECT * FROM cards WHERE cardnumber = @cardnumber";

                IEnumerable<Card> results =
                    _connection.Query<Card>(
                        sqlCommand,
                        new { cardNum = cardNum });

                FillCard(card, results);

                if (card.ExpirationDate < DateTime.Today)
                {
                    resultOperations = ResultOperations.CardExpired;
                }
                else
                {
                    if (card.Ballance - summ < 0)
                    {
                        resultOperations = ResultOperations.ChargeError;
                    }
                    else
                    {
                        int newVol = card.Ballance - summ;
                        UpdateBallance(card, cardNum, out results, newVol);

                        FillCard(card, results);
                    }
                }
            }
            else
            {
                resultOperations = ResultOperations.CardDoesnExist;
            }

            return resultOperations;
        }

        /// <summary>
        /// Начисление бонусов.
        /// </summary>
        /// <param name="card">объект карты.</param>
        /// <param name="cardNum">Номер карты.</param>
        /// <param name="summ">Сумма к списанию.</param>
        /// <returns>Результат.</returns>
        public ResultOperations AddBonus(out Card card, int cardNum, int summ)
        {
            ResultOperations resultOperations = ResultOperations.None;

            card = new Card();

            if (CheckIfCardExist(cardNum))
            {
                string sqlCommand =
                    $"SELECT ballance FROM cards WHERE cardnumber = @cardnumber";

                IEnumerable<Card> results =
                    _connection.Query<Card>(
                        sqlCommand,
                        new { cardNum = cardNum });

                FillCard(card, results);

                if (card.ExpirationDate < DateTime.Today)
                {
                    resultOperations = ResultOperations.CardExpired;
                }
                else
                {
                    int newVol = card.Ballance + summ;
                    UpdateBallance(card, cardNum, out results, newVol);

                    FillCard(card, results);
                }
            }
            else
            {
                resultOperations = ResultOperations.CardDoesnExist;
            }

            return resultOperations;
        }

        /// <summary>
        /// Получить все карты.
        /// </summary>
        /// <param name="dataTable">Таблица с данными.</param>
        /// <returns>Таблица с картами.</returns>
        public ResultOperations GetAllCards(out DataTable dataTable)
        {
            ResultOperations resultOperations = ResultOperations.None;

            dataTable = new DataTable();

            string sqlCommand =
                    $"SELECT * FROM cards;";

            IEnumerable<Card> results =
                _connection.Query<Card>(
                    sqlCommand);

            // Определяем структуру таблицы на основе класса Person
            dataTable.Columns.Add("Cardnumber", typeof(int));
            dataTable.Columns.Add("ExpirationDate", typeof(DateTime));
            dataTable.Columns.Add("Ballance", typeof(int));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("MiddleName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("PhoneNumber", typeof(string));

            // Заполняем DataTable из IEnumerable<T>
            foreach (var item in results)
            {
                dataTable.Rows.Add(
                    item.Cardnumber,
                    item.ExpirationDate,
                    item.Ballance,
                    item.FirstName,
                    item.MiddleName,
                    item.LastName,
                    item.PhoneNumber);
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

        private static void FillCard(Card card, IEnumerable<Card> results)
        {
            foreach (var item in results)
            {
                card.Cardnumber = item.Cardnumber;
                card.ExpirationDate = item.ExpirationDate;
                card.Ballance = item.Ballance;
                card.FirstName = item.FirstName;
                card.MiddleName = item.MiddleName;
                card.LastName = item.LastName;
                card.PhoneNumber = item.PhoneNumber;
            }
        }

        private void UpdateBallance(Card card, int cardNum, out IEnumerable<Card> results, int newVol)
        {
            string sqlCommand = "UPDATE cards SET ballance = @newBallance WHERE  cardnumber = @cardnumber;";

            _connection.Execute(sqlCommand, new
            {
                newBallance = newVol,
                cardnumber = card.Cardnumber,
            });

            sqlCommand =
            $"SELECT ballance FROM cards WHERE cardnumber = @cardnumber";

            results =
                _connection.Query<Card>(
                    sqlCommand,
                    new { cardNum = cardNum });
        }
        #endregion METHODS
    }
}
