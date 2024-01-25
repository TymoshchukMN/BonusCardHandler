using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsHandler
{
    /// <summary>
    /// Ошибки.
    /// </summary>
    internal enum WrongData : ushort
    {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        None,

        /// <summary>
        /// Не верный телеон.
        /// </summary>
        WrongPhone,

        /// <summary>
        /// не верный номер карты.
        /// </summary>
        WrongCard,

        /// <summary>
        /// Не верное имя.
        /// </summary>
        WrongName,

        /// <summary>
        /// Данные не заполнены.
        /// </summary>
        EmptyField,
    }
}
