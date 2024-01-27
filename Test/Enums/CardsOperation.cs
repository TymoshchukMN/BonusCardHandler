using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsHandler.Enums
{
    public enum CardsOperation
    {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        None,

        /// <summary>
        /// Операция создания карты.
        /// </summary>
        Create,

        /// <summary>
        /// Поиск карты.
        /// </summary>
        Find,

        /// <summary>
        /// Списание с карты.
        /// </summary>
        Change,

        /// <summary>
        /// Просмотр баланса на карте.
        /// </summary>
        SeeBalance,
    }
}
