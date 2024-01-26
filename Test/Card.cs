//////////////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 26/01/202
// Last Modified On :
// Description: class for describing a card object
// Project: CardsHandler
//////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
