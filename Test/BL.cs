///////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 26/01/202
// Last Modified On :
// Description: logic class.
// Project: CardsHandler
//////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardsHandler.Database;
using CardsHandler.JSON;
using Newtonsoft.Json;

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
        public static ResultOperations CheckCreationCompliance(
            string phone,
            string firstName,
            string middleName,
            string lastName)
        {
            ResultOperations statCompliante = ResultOperations.None;

            if (string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(firstName))
            {
                return ResultOperations.EmptyField;
            }

            if (IsPhoneCorrect(phone))
            {
                if (IsNameCorrect(firstName))
                {
                    if (IsNameCorrect(middleName))
                    {
                        if (!IsNameCorrect(lastName))
                        {
                            return ResultOperations.WrongName;
                        }
                    }
                    else
                    {
                        return ResultOperations.WrongName;
                    }
                }
                else
                {
                    return ResultOperations.WrongName;
                }
            }
            else
            {
                return ResultOperations.WrongPhone;
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
        public static ResultOperations CheckSearchCompliance(
           string choice,
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
                if (choice == "Телефону")
                {
                    if (!IsPhoneCorrect(phone))
                    {
                        statCompliante = ResultOperations.WrongPhone;
                    }
                }
                else
                {
                    if (choice == "Номеру карты")
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

        public static ResultOperations ChechChargeCompliance(
            string summ,
            string card)
        {
            ResultOperations statCompliante = ResultOperations.None;

            // проверка на коректность введенной суммы
            if (!int.TryParse(summ, out int _))
            {
                statCompliante = ResultOperations.WrongSumm;
            }
            else
            {
                if (!IsCardCorrect(card))
                {
                    statCompliante = ResultOperations.WrongCard;
                }
            }

            return statCompliante;
        }

        public static DBConfigJSON GetDBConfig()
        {
            // const string ConfFilePathDB = "N:\\Personal\\TymoshchukMN\\TitleProcessingConfigs\\DBconfigFile.json";
            const string ConfFilePathDB = "..\\..\\JSON\\DBconfigFile.json";
            string dbConfigFile = File.ReadAllText(ConfFilePathDB);
            DBConfigJSON dbConfigJSON = JsonConvert.DeserializeObject<DBConfigJSON>(dbConfigFile);

            return dbConfigJSON;
        }

        /// <summary>
        /// Генерация нромера карты.
        /// </summary>
        /// <param name="number">номер карты.</param>
        /// <returns>номер карты.</returns>
        public static int GenerateCardNumber(out int number)
        {
            number = 0;
            Random random = new Random();
            number = random.Next(99999, 1000000);

            return number;
        }

        /// <summary>
        /// Проверка правильности указанной суммы для списания.
        /// </summary>
        /// <param name="sum">Сумма для списания.</param>
        /// <returns>
        /// bool.
        /// </returns>
        public static ResultOperations IsSummCorrect(string sum)
        {
            ResultOperations result = ResultOperations.None;

            if (int.TryParse(sum, out int res))
            {
                if (res <= 0)
                {
                    result = ResultOperations.NegativeDigit;
                }
            }
            else
            {
                result = ResultOperations.WrongSumm;
            }

            return result;
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
