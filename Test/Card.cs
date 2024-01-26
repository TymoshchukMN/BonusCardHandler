//////////////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 26/01/202
// Last Modified On :
// Description: class for describing a card object
// Project: CardsHandler
//////////////////////////////////////////////////

using System;

namespace CardsHandler
{
    internal class Card
    {
        private const int DefaultSumm = 1000;
        /// <summary>
        /// Номер карты.
        /// </summary>
        private int _number;

        /// <summary>
        /// Номер телефона.
        /// </summary>
        private long _phoneNumber;

        /// <summary>
        /// Срок действия карты.
        /// </summary>
        private DateTime _expirationDate;

        /// <summary>
        /// Баланс на карте.
        /// </summary>
        private int _ballance;

        /// <summary>
        /// Имя владельца карты.
        /// </summary>
        private string _ownerFirstName;

        /// <summary>
        /// Отчество владельца карты.
        /// </summary>
        private string _ownerMiddleName;

        /// <summary>
        /// Фамилия владельца карты.
        /// </summary>
        private string _ownerLastName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// Конструктор, для получения объекта из БД.
        /// </summary>
        /// <param name="number">номер карты.</param>
        /// <param name="phone">номер телефона.</param>
        /// <param name="date">Дата истечения срока карты.</param>
        /// <param name="ballance">баланс.</param>
        /// <param name="isActive">флаг актуальности.</param>
        public Card(
             int number,
             long phone,
             string firstName,
             string middleName,
             string lasName,
             DateTime date,
             int balance)
        {
            _number = number;
            _phoneNumber = phone;
            _expirationDate = date;
            _ballance = DefaultSumm;
            _ownerFirstName = firstName;
            _ownerMiddleName = middleName;
            _ownerLastName = lasName;
            _ballance = balance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// Конструктор, для создания карыт и помещения в БД.
        /// </summary>
        /// <param name="number">номер карты.</param>
        /// <param name="phone">номер телефона.</param>
        /// <param name="firstName">имя клиента.</param>
        /// <param name="middleName">отчетство клиента.</param>
        /// <param name="lasName">фамилия клиента.</param>
        public Card(
            int number,
            long phone,
            string firstName,
            string middleName,
            string lasName)
        {
            _number = number;
            _phoneNumber = phone;
            _expirationDate = DateTime.Today;
            _ballance = DefaultSumm;
            _ownerFirstName = firstName;
            _ownerMiddleName = middleName;
            _ownerLastName = lasName;
        }

        public int Number
        {
            get { return _number; }
            private set { _number = value; }
        }

        public long PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }

        public int Ballance
        {
            get { return _ballance; }
            set { _ballance = value; }
        }

        public string OwnerFirstName
        {
            get { return _ownerFirstName; }
            private set { _ownerFirstName = value; }
        }

        public string OwnerMiddleName
        {
            get { return _ownerMiddleName; }
            private set { _ownerMiddleName = value; }
        }

        public string OwnerLastName
        {
            get { return _ownerLastName; }
            private set { _ownerLastName = value; }
        }
    }
}
