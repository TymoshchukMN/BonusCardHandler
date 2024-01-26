//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 10/04/2023
// Last Modified On : 26/01/2024
// Description: Workking with Postgres
// Project: CardsHandler
//////////////////////////////////////////

using System;
using System.Collections.Generic;
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

        public bool CheckIfCardExist(string card)
        {
            return true;
        }

        public void CreateCard(Card card)
        {
            using (NpgsqlConnection connection
                    = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {

                    return;
                }

                NpgsqlDataReader data;

            }
        }

        #endregion METHODS
    }
}
