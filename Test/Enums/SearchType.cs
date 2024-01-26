using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsHandler.Enums
{
    internal enum SearchType
    {
        /// <summary>
        /// Значение по умолчанию.
        /// </summary>
        None,

        /// <summary>
        /// Тип поиска по телефону.
        /// </summary>
        ByPhone,

        /// <summary>
        /// Тип поска по номеру карты.
        /// </summary>
        ByCard,
    }
}
