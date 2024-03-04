using CardsHandler.Enums;
using CardsHandler.Server;

namespace CardsHandler
{
    internal class Compliance
    {
        public static ResultOperations CheckCompliance(
            CardsOperation cardsOperation,
            FieldValues fieldsValues,
            ServerInstance server)
        {
            ResultOperations resultOperations = ResultOperations.None;
            switch (cardsOperation)
            {
                case CardsOperation.Create:

                    resultOperations =
                        CheckCreationCompliance(
                           fieldsValues.PhoneNumber,
                           fieldsValues.FirstName,
                           fieldsValues.MiddleName,
                           fieldsValues.LastName);

                    break;
                case CardsOperation.Find:

                    resultOperations =
                        CheckSearchCompliance(
                           fieldsValues.SearchType,
                           fieldsValues.PhoneNumber,
                           fieldsValues.CardNumber);

                    break;
                case CardsOperation.Change:

                    if (string.IsNullOrEmpty(fieldsValues.CardNumber))
                    {
                        resultOperations = ResultOperations.EmptyField;
                    }
                    else
                    {
                        resultOperations =
                        CheckChargeCompliance(fieldsValues.Summ);
                    }

                    break;
                case CardsOperation.SeeBalance:

                    resultOperations =
                        CheckCardCompliance(fieldsValues.CardNumber);

                    break;
            }

            return resultOperations;
        }

        /// <summary>
        /// Метод для проверки валидности введенных данных на создание карты.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <param name="firstName"> Имя.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <returns>номер ошибки.</returns>
        private static ResultOperations CheckCreationCompliance(
            string phone,
            string firstName,
            string middleName,
            string lastName)
        {
            ResultOperations statCompliante = ResultOperations.None;

            if (string.IsNullOrEmpty(phone))
            {
                return ResultOperations.EmptyField;
            }
            else
            {
                if (!(IsNameCorrect(firstName) &&
                    IsNameCorrect(middleName) &&
                    IsNameCorrect(lastName)))
                {
                    return ResultOperations.WrongName;
                }
            }

            return statCompliante;
        }

        /// <summary>
        /// Проверка правильности имени/отчества/фамилии.
        /// Должны быть только буквы в имени.
        /// </summary>
        /// <param name="name">
        /// строка к проверке.
        /// </param>
        /// <returns>истина/ложь.</returns>
        private static bool IsNameCorrect(string name)
        {
            bool isCorrect = true;

            if (string.IsNullOrEmpty(name))
            {
                isCorrect = false;
                return isCorrect;
            }

            for (ushort i = 0; i < name.Length; ++i)
            {
                if (!char.IsLetter(name[i]))
                {
                    isCorrect = false;
                    return isCorrect;
                }
            }

            return isCorrect;
        }

        private static ResultOperations CheckSearchCompliance(
           SearchType searchType,
           string phone,
           string card)
        {
            ResultOperations statCompliante = ResultOperations.None;

            if (string.IsNullOrEmpty(phone) &&
                string.IsNullOrEmpty(card))
            {
                statCompliante = ResultOperations.EmptyField;
            }
            else
            {
                if (searchType == SearchType.ByPhone)
                {
                    if (!IsPhoneCorrect(phone))
                    {
                        statCompliante = ResultOperations.WrongPhone;
                    }
                }
                else
                {
                    if (searchType == SearchType.ByCard)
                    {
                        if (!IsCardCorrect(card))
                        {
                            statCompliante = ResultOperations.WrongCard;
                        }
                    }
                }
            }

            return statCompliante;
        }

        /// <summary>
        /// Проверка валидности номера.
        /// </summary>
        /// <param name="phoneNumber">
        /// номер телефона.
        /// </param>
        /// <returns>true/false.</returns>
        private static bool IsPhoneCorrect(string phoneNumber)
        {
            bool isCorrect = true;

            // стандартная длина номера телефона.
            // например, 380930453119
            const ushort StandartLenth = 12;
            const string FirstSymbols = "380";

            if (phoneNumber.Length == StandartLenth)
            {
                if (phoneNumber.Substring(0, 3) != FirstSymbols)
                {
                    isCorrect = false;
                }
                else
                {
                    for (ushort i = 3; i < phoneNumber.Length; ++i)
                    {
                        if (!char.IsDigit(phoneNumber[i]))
                        {
                            isCorrect = false;
                        }
                    }
                }
            }
            else
            {
                isCorrect = false;
            }

            return isCorrect;
        }

        private static bool IsCardCorrect(string card)
        {
            bool isCorrect = true;

            const ushort LenthCardNubber = 6;

            if (card.Length != LenthCardNubber)
            {
                isCorrect = false;
            }
            else
            {
                for (ushort i = 0; i < card.Length; ++i)
                {
                    if (!char.IsDigit(card[i]))
                    {
                        isCorrect = false;
                        break;
                    }
                }
            }

            return isCorrect;
        }

        private static ResultOperations CheckChargeCompliance(string sum)
        {
            ResultOperations result = ResultOperations.None;

            if (string.IsNullOrEmpty(sum))
            {
                return ResultOperations.EmptyField;
            }

            if (!int.TryParse(sum, out int res))
            {
                return ResultOperations.WrongSumm;
            }

            if (res <= 0)
            {
                return ResultOperations.NegativeDigit;
            }

            return result;
        }

        private static ResultOperations CheckCardCompliance(string cardNumber)
        {
            ResultOperations statCompliante = ResultOperations.None;

            if (string.IsNullOrEmpty(cardNumber))
            {
                statCompliante = ResultOperations.EmptyField;
            }
            else
            {
                if (!IsCardCorrect(cardNumber))
                {
                    statCompliante = ResultOperations.WrongCard;
                }
            }

            return statCompliante;
        }
    }
}
