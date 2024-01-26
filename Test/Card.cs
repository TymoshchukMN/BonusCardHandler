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
        /// <summary>
        /// Номер карты.
        /// </summary>
        private int _number;

        /// <summary>
        /// Номер телефона.
        /// </summary>
        private int _phoneNumber;

        /// <summary>
        /// Срок действия карты.
        /// </summary>
        private DateTime _expirationDate;

        /// <summary>
        /// Баланс на карте.
        /// </summary>
        private int _ballance;

        /// <summary>
        /// Активность карты.
        /// </summary>
        private bool _isActive;

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
            int phone,
            DateTime date,
            int ballance,
            bool isActive)
        {
            _number = number;
            _phoneNumber = phone;
            _expirationDate = date;
            _ballance = ballance;
            _isActive = isActive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// Конструктор, для создания карыт и помещения в БД.
        /// </summary>
        /// <param name="number">номер карты.</param>
        /// <param name="phone">номер телефона.</param>
        public Card(
            int number,
            int phone)
        {
            _number = number;
            _phoneNumber = phone;
            _expirationDate = DateTime.Today;
            _ballance = 0;
            _isActive = true;
        }

        public int Number
        {
            get { return _number; }
            private set { _number = value; }
        }

        public int PhoneNumber
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

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
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
