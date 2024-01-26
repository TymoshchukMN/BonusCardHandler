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

        #endregion PROPERTIES

        #region METHODS

        public void ProcessingOldTitles(NpgsqlConnection connection)
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

        #endregion METHODS
    }
}
