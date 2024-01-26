//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 10/04/2023
// Last Modified On : 26/01/2024
// Description: Workking with Postgres
// Project: CardsHandler
//////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsHandler.Enums;
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

        public void Get(NpgsqlConnection connection)
        {
            NpgsqlCommand npgsqlCommand = connection.CreateCommand();
            npgsqlCommand.CommandText =
                @"
                    BEGIN;
                    TRUNCATE oldTitles;


                    INSERT INTO oldTitles
                    SELECT titles.samaccountname ,
                           titles.title
                    FROM titles;

                    COMMIT;
                ";
            npgsqlCommand.ExecuteNonQuery();
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
            using (NpgsqlConnection connection
                   = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                }

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
        public bool CheckIfPhone(long phoneNumber)
        {
            bool isExist = false;
            using (NpgsqlConnection connection
                   = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                }

                NpgsqlCommand npgsqlCommand = connection.CreateCommand();

                npgsqlCommand.CommandText = $"SELECT EXISTS(" +
                    $"SELECT \"phoneNumber\" " +
                    $"FROM clients " +
                    $"WHERE \"phoneNumber\" = {phoneNumber})";

                NpgsqlDataReader data;
                data = npgsqlCommand.ExecuteReader();

                DataTable isAccessExist = new DataTable();
                isAccessExist.Load(data);

                isExist = (bool)isAccessExist.Rows[0].ItemArray[0];

                data.Close();
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
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                    return;
                }

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

                // NpgsqlDataReader data;
            }
        }

        /// <summary>
        /// Поиск карты.
        /// </summary>
        /// <param name="number">номер телефона/карты.</param>
        /// <returns>Объект карты.</returns>
        public Card FindCardByPhone(long number)
        {
            using (NpgsqlConnection connection
                  = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                }

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
                    $" INNER JOIN CARDS as cd  ON cl.\"phoneNumber\" = cd.\"phoneNumber\" " +
                    $" WHERE cl.\"phoneNumber\" = {number};";

                NpgsqlDataReader data;
                data = npgsqlCommand.ExecuteReader();

                long phoneNumber = 0;
                string firstName = string.Empty;
                string middleName = string.Empty;
                string lastName = string.Empty;
                int cardnumber = 0;
                int ballance = 0;
                DateTime expirationDate = DateTime.Now;

                while (data.Read())
                {
                    phoneNumber = (long)data["phoneNumber"];
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
                }
                catch (Exception)
                {
                    UI.PrintErrorConnectionToDB(this);
                }

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

                long phoneNumber = 0;
                string firstName = string.Empty;
                string middleName = string.Empty;
                string lastName = string.Empty;
                int cardnumber = 0;
                int ballance = 0;
                DateTime expirationDate = DateTime.Now;

                while (data.Read())
                {
                    phoneNumber = (long)data["phoneNumber"];
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
        }

        #endregion METHODS
    }
}
