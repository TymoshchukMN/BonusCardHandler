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
            TextBox phone,
            TextBox firstName,
            TextBox middleName,
            TextBox lastName)
        {
            WrongData statCompliante = WrongData.None;

            if (string.IsNullOrEmpty(phone.Text) ||
                string.IsNullOrEmpty(firstName.Text) ||
                string.IsNullOrEmpty(firstName.Text) ||
                string.IsNullOrEmpty(firstName.Text))
            {
                return WrongData.EmptyField;
            }

            if (IsPhoneCorrect(phone.Text))
            {
                if (IsNameCorrect(firstName.Text))
                {
                    if (IsNameCorrect(middleName.Text))
                    {
                        if (!IsNameCorrect(lastName.Text))
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
           ComboBox choice,
           TextBox phone,
           TextBox card)
        {
            WrongData statCompliante = WrongData.None;

            if (choice.Text == "Телефону")
            {

                if (!IsPhoneCorrect(phone.Text))
                {
                    statCompliante = WrongData.WrongPhone;
                }
            }
            else
            {
                if (choice.Text == "Номеру карты")
                {
                    if (!IsCardCorrect(card.Text))
                    {
                        statCompliante = WrongData.WrongCard;
                    }
                }
            }

            return statCompliante;
        }

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


        private static bool IsCardCorrect(string card)
        {
            bool isCorrect = true;
            return isCorrect;
        }
    }
}
