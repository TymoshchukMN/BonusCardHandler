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
using Npgsql;

namespace CardsHandler.Database
{
    internal class PostgresDB
    {
        #region FIELDS

        private string _connectionString;
        private string _server;
        private string _dbName;

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
            set { _dbName = value; }
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
        public bool CheckIfPhone(int phoneNumber)
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
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        /*public Card FindCard(int cardNumber, string searchType)
        {
            const string SearchByPhone = "Телефону";
            const string SearchByCard = "Номеру карты";
            return new Card;
        }*/
        #endregion METHODS
    }
}
