///////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 26/01/202
// Last Modified On :
// Description: logic class.
// Project: CardsHandler
//////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardsHandler
{
    internal class BL
    {
        /// <summary>
        /// Метод для проверки валидности введенных данных на создание карты.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <param name="firstName"> Имя.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <returns>номер ошибки.</returns>
        public static WrongData CheckCreationCompliance(
            string phone,
            string firstName,
            string middleName,
            string lastName)
        {
            WrongData statCompliante = WrongData.None;

            if (string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(firstName))
            {
                return WrongData.EmptyField;
            }

            if (IsPhoneCorrect(phone))
            {
                if (IsNameCorrect(firstName))
                {
                    if (IsNameCorrect(middleName))
                    {
                        if (!IsNameCorrect(lastName))
                        {
                            return WrongData.WrongName;
                        }
                    }
                    else
                    {
                        return WrongData.WrongName;
                    }
                }
                else
                {
                    return WrongData.WrongName;
                }
            }
            else
            {
                return WrongData.WrongPhone;
            }

            return statCompliante;
        }

        /// <summary>
        /// Метод для проверки валидности введенных данных на ПОИСК карты.
        /// </summary>
        /// <param name="choice">
        /// Поле с Типом поиска.
        /// </param>
        /// <param name="phone">
        /// Поле с Телефоном.
        /// </param>
        /// <param name="card">
        /// Поле с номером карты.</param>
        /// <returns>номер ошибки.</returns>
        public static WrongData CheckSearchCompliance(
           string choice,
           string phone,
           string card)
        {
            WrongData statCompliante = WrongData.None;

            if (string.IsNullOrEmpty(phone) &&
                string.IsNullOrEmpty(card))
            {
                statCompliante = WrongData.EmptyField;
            }
            else
            {
                if (choice == "Телефону")
                {
                    if (!IsPhoneCorrect(phone))
                    {
                        statCompliante = WrongData.WrongPhone;
                    }
                }
                else
                {
                    if (choice == "Номеру карты")
                    {
                        if (!IsCardCorrect(card))
                        {
                            statCompliante = WrongData.WrongCard;
                        }
                    }
                }
            }

            return statCompliante;
        }

        public static WrongData ChechChargeCompliance(
            string summ,
            string card)
        {
            WrongData statCompliante = WrongData.None;

            // проверка на коректность введенной суммы
            if (!int.TryParse(summ, out int _))
            {
                statCompliante = WrongData.WrongSumm;
            }
            else
            {
                if (!IsCardCorrect(card))
                {
                    statCompliante = WrongData.WrongCard;
                }
            }

            return statCompliante;
        }

        private static bool IsCardExist(string card)
        {
            return true;
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

        /// <summary>
        /// Проверка валидности карты.
        /// </summary>
        /// <param name="card">
        /// номер карты.
        /// </param>
        /// <returns>bool.</returns>
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
    }
}
